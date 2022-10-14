// Copyright © 2022-Present The Open Human Task Specification Authors. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License")
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Flurl.Http;
using Keycloak.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Neuroglia.Serialization;
/*
 * Copyright © 2022-Present The Open Human Task Runtime Authors
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

using OpenHumanTask.Runtime.Infrastructure.Plugins;
using OpenHumanTask.Runtime.Infrastructure.Services;
using OpenHumanTask.Runtime.Plugins.UserManagement.Keycloak.Configuration;
using OpenHumanTask.Runtime.Plugins.UserManagement.Keycloak.Services;
using System.Text.Json.Serialization;

namespace OpenHumanTask.Runtime.Plugins.UserManagement.Keycloak
{

    /// <summary>
    /// Represents the official <see cref="IUserManagementPlugin"/> implementation for Keycloak
    /// </summary>
    public class KeycloakUserManagementPlugin
        : Plugin, IUserManagementPlugin
    {

        /// <summary>
        /// Gets the <see cref="KeycloakUserManagementPlugin"/>'s <see cref="ISerializerProvider"/>
        /// </summary>
        protected IServiceProvider ServiceProvider { get; set; } = null!;

        /// <inheritdoc/>
        protected override async ValueTask InitializeAsync(CancellationToken stoppingToken)
        {
            await base.InitializeAsync(stoppingToken);
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(Path.GetDirectoryName(typeof(KeycloakUserManagementPlugin).Assembly.Location)!, "settings.plugin.json"), true, true)
                .AddEnvironmentVariables()
                .Build();
            var applicationOptions = new KeycloakClientOptions();
            configuration.Bind(applicationOptions);
            var services = new ServiceCollection();
            services.Configure<KeycloakClientOptions>(configuration);
            services.AddLogging();
            services.AddJsonSerializer(options =>
            {
                options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
            });
            services.AddSingleton(new KeycloakClient(applicationOptions.Url, applicationOptions.ClientSecret));
            this.ServiceProvider = services.BuildServiceProvider();
            FlurlHttp.ConfigureClient(applicationOptions.Url, cli => cli.Settings.HttpClientFactory = new UntrustedCertClientFactory());
        }

        /// <inheritdoc/>
        public virtual IUserManager CreateUserManager()
        {
            return ActivatorUtilities.CreateInstance<KeycloakUserManager>(this.ServiceProvider);
        }

    }

}
