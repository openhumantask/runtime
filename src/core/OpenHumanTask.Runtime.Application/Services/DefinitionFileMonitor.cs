/*
 * Copyright © 2022-Present The Synapse Authors
 * <p>
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * <p>
 * http://www.apache.org/licenses/LICENSE-2.0
 * <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

using Microsoft.Extensions.Options;
using OpenHumanTask.Runtime.Application.Commands.HumanTaskTemplates;
using OpenHumanTask.Sdk.Services.IO;

namespace OpenHumanTask.Runtime.Application.Services
{

    /// <summary>
    /// Represents an <see cref="IHostedService"/> used to monitor definition files
    /// </summary>
    public class DefinitionFileMonitor
        : BackgroundService
    {

        /// <summary>
        /// Initializes a new <see cref="DefinitionFileMonitor"/>
        /// </summary>
        /// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
        /// <param name="logger">The service used to perform logging</param>
        /// <param name="options">The service used to access the current <see cref="ApplicationOptions"/></param>
        /// <param name="humanTaskDefinitionReader">The service used to read <see cref="HumanTaskDefinition"/>s</param>
        public DefinitionFileMonitor(IServiceProvider serviceProvider, ILogger<DefinitionFileMonitor> logger, IOptions<ApplicationOptions> options, IHumanTaskDefinitionReader humanTaskDefinitionReader)
        {
            this.ServiceProvider = serviceProvider;
            this.Logger = logger;
            this.Options = options.Value;
            this.HuamnTaskDefinitionReader = humanTaskDefinitionReader;
        }

        /// <summary>
        /// Gets the current <see cref="IServiceProvider"/>
        /// </summary>
        protected IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// Gets the service used to perform logging
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Gets the current <see cref="ApplicationOptions"/>
        /// </summary>
        protected ApplicationOptions Options { get; }

        /// <summary>
        /// Gets the service used to read <see cref="HumanTaskDefinition"/>s
        /// </summary>
        protected IHumanTaskDefinitionReader HuamnTaskDefinitionReader { get; }

        /// <summary>
        /// Gets the <see cref="DefinitionFileMonitor"/>'s <see cref="System.Threading.CancellationTokenSource"/>
        /// </summary>
        protected CancellationTokenSource CancellationTokenSource { get; private set; } = null!;

        /// <summary>
        /// Gets a service used to watch definition files
        /// </summary>
        protected FileSystemWatcher FileSystemWatcher { get; private set; } = null!;

        /// <inheritdoc/>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (string.IsNullOrWhiteSpace(this.Options.DefinitionsDirectory))
                return;
            this.CancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(stoppingToken);
            var directory = new DirectoryInfo(this.Options.DefinitionsDirectory);
            if (!directory.Exists)
                directory.Create();
            this.FileSystemWatcher = new(directory.FullName)
            {
                IncludeSubdirectories = true,
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName,
                EnableRaisingEvents = true
            };
            this.FileSystemWatcher.Created += this.OnFileCreatedOrChangedAsync;
            this.FileSystemWatcher.Changed += this.OnFileCreatedOrChangedAsync;
            foreach (var file in directory.GetFiles("*.*", SearchOption.AllDirectories)
                .Where(f => f.Extension.ToLower() == ".json" || f.Extension.ToLower() == ".yml" || f.Extension.ToLower() == ".yaml"))
            {
                await this.ReadAndCreateWorkflowAsync(file.FullName, true);
            }
        }

        /// <summary>
        /// Handles the creation of a new file in the definitions directory
        /// </summary>
        /// <param name="sender">The sender of the <see cref="FileSystemEventArgs"/></param>
        /// <param name="e">The <see cref="FileSystemEventArgs"/> to handle</param>
        protected virtual async void OnFileCreatedOrChangedAsync(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Created
                && e.ChangeType != WatcherChangeTypes.Changed)
                return;
            switch (Path.GetExtension(e.FullPath.ToLower()))
            {
                case ".json":
                case ".yaml":
                case ".yml":
                    break;
                default:
                    return;
            }
            await this.ReadAndCreateWorkflowAsync(e.FullPath, false);
        }

        /// <summary>
        /// Reads the <see cref="HumanTaskDefinition"/> from the specified file and creates a new <see cref="HumanTaskTemplate"/>, if it does not already exist
        /// </summary>
        /// <param name="filePath">The path to the <see cref="HumanTaskDefinition"/> file to read</param>
        /// <param name="ifNotExists">A boolean indicating to only import read and create a new <see cref="HumanTaskTemplate"/> if it already exists</param>
        /// <returns>A new awaitable <see cref="Task"/></returns>
        protected virtual async Task ReadAndCreateWorkflowAsync(string filePath, bool ifNotExists)
        {
            try
            {
                using var stream = File.OpenRead(filePath);
                var definition = await this.HuamnTaskDefinitionReader.ReadAsync(stream);
                using var scope = this.ServiceProvider.CreateScope();
                await scope.ServiceProvider.GetRequiredService<IMediator>().ExecuteAndUnwrapAsync(new CreateHumanTaskTemplateCommand(definition, ifNotExists), this.CancellationTokenSource.Token);
            }
            catch (IOException ex) when (ex.HResult == -2147024864) { }
            catch (Exception ex)
            {
                this.Logger.LogError("An error occured while reading a valid Serverless Workflow definition from the specified file '{filePath}': {ex}", filePath, ex.ToString());
                return;
            }
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            this.CancellationTokenSource?.Dispose();
            this.FileSystemWatcher?.Dispose();
            base.Dispose();
            GC.SuppressFinalize(this);
        }

    }

}