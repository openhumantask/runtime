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

using OpenHumanTask.Runtime.Application.Queries.HumanTaskTemplates;
using OpenHumanTask.Sdk;

namespace OpenHumanTask.Runtime.Application.Commands.HumanTaskTemplates
{

    /// <summary>
    /// Represents the <see cref="ICommand"/> used to create a new <see cref="HumanTaskTemplate"/>
    /// </summary>
    [DataTransferObjectType(typeof(Integration.Commands.HumanTaskTemplates.CreateHumanTaskTemplateCommand))]
    public class CreateHumanTaskTemplateCommand
        : Command<Integration.Models.HumanTaskTemplate>
    {

        /// <summary>
        /// Initializes a new <see cref="CreateHumanTaskTemplateCommand"/>
        /// </summary>
        protected CreateHumanTaskTemplateCommand() { }

        /// <summary>
        /// Initializes a new <see cref="CreateHumanTaskTemplateCommand"/>
        /// </summary>
        /// <param name="definition">The <see cref="HumanTaskDefinition"/> of the <see cref="HumanTaskTemplate"/> to create</param>
        /// <param name="ifNotExists">A boolean indicating whether the <see cref="HumanTaskTemplate"/> should be created only if it does not already exist. Defaults to false, in which case the <see cref="HumanTaskDefinition"/> is automatically versionned</param>
        public CreateHumanTaskTemplateCommand(HumanTaskDefinition definition, bool ifNotExists)
        {
            this.Definition = definition;
            this.IfNotExists = ifNotExists;
        }

        /// <summary>
        /// Gets the <see cref="HumanTaskDefinition"/> of the <see cref="HumanTaskTemplate"/> to create
        /// </summary>
        public virtual HumanTaskDefinition Definition { get; protected set; } = null!;

        /// <summary>
        /// Gets a boolean indicating whether the <see cref="HumanTaskTemplate"/> should be created only if it does not already exist. Defaults to false, in which case the <see cref="HumanTaskDefinition"/> is automatically versionned
        /// </summary>
        public virtual bool IfNotExists { get; protected set; }

    }

    /// <summary>
    /// Represents the service used to handle <see cref="CreateHumanTaskTemplateCommand"/>s
    /// </summary>
    public class CreateHumanTaskTemplateCommandHandler
        : CommandHandlerBase,
        ICommandHandler<CreateHumanTaskTemplateCommand, Integration.Models.HumanTaskTemplate>
    {

        /// <summary>
        /// Initializes a new <see cref="CreateHumanTaskTemplateCommandHandler"/>
        /// </summary>
        /// <param name="loggerFactory">The service used to create <see cref="ILogger"/>s</param>
        /// <param name="mediator">The service used to mediate calls</param>
        /// <param name="mapper">The service used to map objects</param>
        /// <param name="userAccessor">The service used to access the current user</param>
        /// <param name="humanTaskTemplates">The <see cref="IRepository"/> used to manage <see cref="HumanTaskTemplate"/>s</param>
        public CreateHumanTaskTemplateCommandHandler(ILoggerFactory loggerFactory, IMediator mediator, IMapper mapper, IUserAccessor userAccessor, IRepository<HumanTaskTemplate, string> humanTaskTemplates) 
            : base(loggerFactory, mediator, mapper, userAccessor)
        {
            this.HumanTaskTemplates = humanTaskTemplates;
        }

        /// <summary>
        /// Gets the <see cref="IRepository"/> used to manage <see cref="HumanTaskTemplate"/>s
        /// </summary>
        protected IRepository<HumanTaskTemplate, string> HumanTaskTemplates { get; }

        /// <inheritdoc/>
        public virtual async Task<IOperationResult<Integration.Models.HumanTaskTemplate>> HandleAsync(CreateHumanTaskTemplateCommand command, CancellationToken cancellationToken = default)
        {
            if (this.UserAccessor.User == null || !this.UserAccessor.User.Identity?.IsAuthenticated == true) return this.Forbid();
            command.Definition.Id = HumanTaskDefinition.BuildId(command.Definition.Name, command.Definition.Namespace, command.Definition.Version);
            //todo: validate definition
            foreach (var subtaskDefinition in command.Definition.GetSubtaskDefinitions())
            {
                var subtask = await this.Mediator.ExecuteAndUnwrapAsync(new GetHumanTaskTemplateByIdQuery(subtaskDefinition.Task), cancellationToken);
                if (subtask == null)
                    throw DomainException.NullReference(typeof(HumanTaskTemplate), $"Failed to find the referenced human task template '{subtaskDefinition.Task}'");
            }
            if (command.IfNotExists
              && await this.HumanTaskTemplates.ContainsAsync(command.Definition.Id, cancellationToken))
                return this.NotModified();
            while (await this.HumanTaskTemplates.ContainsAsync(command.Definition.Id, cancellationToken))
            {
                var version = SemanticVersion.Parse(command.Definition.Version);
                version = version.WithPatch(version.Patch + 1);
                command.Definition.Version = version.ToString();
                command.Definition.Id = $"{command.Definition.Namespace}.{command.Definition.Name}:{version}";
            }
            var template = await this.HumanTaskTemplates.AddAsync(new(this.UserAccessor.User, command.Definition), cancellationToken);
            await this.HumanTaskTemplates.SaveChangesAsync(cancellationToken);
            return this.Ok(this.Mapper.Map<Integration.Models.HumanTaskTemplate>(template));
        }

    }

}
