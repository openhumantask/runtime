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

using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using OpenHumanTask.Runtime.Application.Commands.PeopleAssignments;
using OpenHumanTask.Runtime.Application.Queries.HumanTaskTemplates;
using OpenHumanTask.Runtime.Application.Services;
using OpenHumanTask.Runtime.Domain;
using OpenHumanTask.Sdk;

namespace OpenHumanTask.Runtime.Application.Commands.HumanTasks
{

    /// <summary>
    /// Represents the <see cref="ICommand"/> used to create a new <see cref="HumanTask"/>
    /// </summary>
    [DataTransferObjectType(typeof(Integration.Commands.HumanTasks.CreateHumanTaskCommand))]
    public class CreateHumanTaskCommand
        : Command<Integration.Models.HumanTask>
    {

        /// <summary>
        /// Initializes a new <see cref="CreateHumanTaskCommand"/>
        /// </summary>
        protected CreateHumanTaskCommand() { }

        /// <summary>
        /// Initializes a new <see cref="CreateHumanTaskCommand"/>
        /// </summary>
        /// <param name="definitionReference">An object used to reference the <see cref="HumanTaskDefinition"/> to instanciate</param>
        /// <param name="key">The key of the <see cref="HumanTask"/>. Overrides possible values set by the <see cref="HumanTaskDefinition"/></param>
        /// <param name="peopleAssignments">An object used to configure the people assignments of the <see cref="HumanTask"/> to create. Overrides possible values set by the <see cref="HumanTaskDefinition"/></param>
        /// <param name="priority">The priority of the <see cref="HumanTask"/> to create</param>
        /// <param name="input">The input of the <see cref="HumanTask"/> to create</param>
        public CreateHumanTaskCommand(HumanTaskDefinitionReference definitionReference, string? key, PeopleAssignmentsDefinition? peopleAssignments, int? priority, object? input)
        {
            this.DefinitionReference = definitionReference;
            this.Key = key;
            this.PeopleAssignments = peopleAssignments;
            this.Priority = priority;
            this.Input = input;
        }

        /// <summary>
        /// Gets an object used to reference the <see cref="HumanTaskDefinition"/> to instanciate
        /// </summary>
        public virtual HumanTaskDefinitionReference DefinitionReference { get; protected set; } = null!;

        /// <summary>
        /// Gets the key of the <see cref="HumanTask"/>. Overrides possible values set by the <see cref="HumanTaskDefinition"/>.
        /// </summary>
        public virtual string? Key { get; protected set; }

        /// <summary>
        /// Gets an object used to configure the people assignments of the <see cref="HumanTask"/> to create. Overrides possible values set by the <see cref="HumanTaskDefinition"/>
        /// </summary>
        public virtual PeopleAssignmentsDefinition? PeopleAssignments { get; protected set; }

        /// <summary>
        /// Gets the <see cref="HumanTask"/>'s priority. Overrides possible values set by the <see cref="HumanTaskDefinition"/>
        /// </summary>
        public virtual int? Priority { get; protected set; }

        /// <summary>
        /// Gets the input of the <see cref="HumanTask"/> to create
        /// </summary>
        public virtual object? Input { get; protected set; }

    }

    /// <summary>
    /// Represents the service used to handle <see cref="CreateHumanTaskCommand"/>s
    /// </summary>
    public class CreateHumanTaskCommandHandler
        : CommandHandlerBase,
        ICommandHandler<CreateHumanTaskCommand, Integration.Models.HumanTask>
    {

        /// <summary>
        /// Initializes a new <see cref="CommandHandlerBase"/>
        /// </summary>
        /// <param name="loggerFactory">The service used to create <see cref="ILogger"/>s.</param>
        /// <param name="mediator">The service used to mediate calls.</param>
        /// <param name="mapper">The service used to map objects.</param>
        /// <param name="userAccessor">The service used to access the current user</param>
        /// <param name="expressionEvaluatorProvider">The service used to provide <see cref="Neuroglia.Data.Expressions.IExpressionEvaluator"/>s</param>
        /// <param name="humanTaskTemplates">The <see cref="IRepository"/> used to manage <see cref="HumanTaskTemplate"/>s</param>
        /// <param name="humanTasks">The <see cref="IRepository"/> used to manage <see cref="HumanTask"/>s</param>
        public CreateHumanTaskCommandHandler(ILoggerFactory loggerFactory, IMediator mediator, IMapper mapper, IUserAccessor userAccessor,
            Neuroglia.Data.Expressions.IExpressionEvaluatorProvider expressionEvaluatorProvider, IRepository<HumanTaskTemplate, string> humanTaskTemplates, IRepository<HumanTask, string> humanTasks) 
            : base(loggerFactory, mediator, mapper, userAccessor)
        {
            this.ExpressionEvaluatorProvider = expressionEvaluatorProvider;
            this.HumanTaskTemplates = humanTaskTemplates;
            this.HumanTasks = humanTasks;
        }

        /// <summary>
        /// Gets the service used to provide <see cref="Neuroglia.Data.Expressions.IExpressionEvaluator"/>s
        /// </summary>
        protected Neuroglia.Data.Expressions.IExpressionEvaluatorProvider ExpressionEvaluatorProvider { get; }

        /// <summary>
        /// Gets the <see cref="IRepository"/> used to manage <see cref="HumanTaskTemplate"/>s
        /// </summary>
        protected IRepository<HumanTaskTemplate, string> HumanTaskTemplates { get; }

        /// <summary>
        /// Gets the <see cref="IRepository"/> used to manage <see cref="HumanTask"/>s
        /// </summary>
        protected IRepository<HumanTask, string> HumanTasks { get; }

        /// <inheritdoc/>
        public virtual async Task<IOperationResult<Integration.Models.HumanTask>> HandleAsync(CreateHumanTaskCommand command, CancellationToken cancellationToken = default)
        {
            if (this.UserAccessor.User == null || !this.UserAccessor.User.Identity?.IsAuthenticated == true) return this.Forbid();
            var templateId = (await this.Mediator.ExecuteAndUnwrapAsync(new GetHumanTaskTemplateByIdQuery(command.DefinitionReference), cancellationToken)).Id;
            var template = await this.HumanTaskTemplates.FindAsync(templateId, cancellationToken);
            if (template == null) throw DomainException.NullReference(typeof(HumanTaskTemplate), templateId);
            var expressionEvaluator = this.ExpressionEvaluatorProvider.GetEvaluator(template.Definition.ExpressionLanguage);
            if (expressionEvaluator == null) throw HumanTaskDomainExceptions.RuntimeExpressionLanguageNotSupported(template.Definition.ExpressionLanguage);
            var context = new HumanTaskRuntimeContext()
            {
                ExpressionLanguage = template.Definition.ExpressionLanguage,
                DefinitionId = template.Definition.Id,
                PeopleAssignments = new(this.UserAccessor.User)
            };
            var inputData = command.Input;
            if (inputData != null && template.Definition.InputData?.State != null)
                inputData = expressionEvaluator.Evaluate(template.Definition.InputData.State, inputData, context);
            if(inputData != null && template.Definition.InputData?.Schema != null)
            {
                var inputToken = JObject.FromObject(inputData);
                if (!inputToken.IsValid(template.Definition.InputData.Schema, out IList<string> errors))
                    return this.Invalid(nameof(command.Input), string.Join('\n', errors));
            }
            context.InputData = inputData;
            var key = command.Key;
            if (string.IsNullOrWhiteSpace(key)
                && !string.IsNullOrWhiteSpace(template.Definition.Key) 
                && template.Definition.Key.IsRuntimeExpression())
                    key = expressionEvaluator.Evaluate<string>(template.Definition.Key, new { }, context);
            if (string.IsNullOrWhiteSpace(key))
                key = Guid.NewGuid().ToShortString();
            context.Key = key;
            context.Id = HumanTask.BuildId(template.Id, key);
            int priority = 0;
            if (command.Priority.HasValue) priority = command.Priority.Value;
            else if (template.Definition.Priority is int intBasedPriority) priority = intBasedPriority;
            else if(template.Definition.Priority is string expressionBasedPriority && !string.IsNullOrWhiteSpace(expressionBasedPriority))
                priority = expressionEvaluator.Evaluate<int>(expressionBasedPriority, new { }, context);
            context.Priority = priority;
            var peopleAssignments = await this.Mediator.ExecuteAndUnwrapAsync(new ResolvePeopleAssignmentsCommand(command.PeopleAssignments == null ? template.Definition.PeopleAssignments : command.PeopleAssignments, context), cancellationToken);
            context.PeopleAssignments = peopleAssignments;
            var title = template.Definition.Title == null ? null : expressionEvaluator.EvaluateLocalizedStrings(template.Definition.Title, new { }, context);
            context.Title = title ?? new();
            var subject = template.Definition.Subject == null ? null : expressionEvaluator.EvaluateLocalizedStrings(template.Definition.Subject, new { }, context);
            context.Subject = subject ?? new();
            var description = template.Definition.Description == null ? null : expressionEvaluator.EvaluateLocalizedStrings(template.Definition.Description, new { }, context);
            context.Description = description ?? new();
            var task = await this.HumanTasks.AddAsync(new(template, key, peopleAssignments, priority, title, subject, description, inputData), cancellationToken);
            await this.HumanTasks.SaveChangesAsync(cancellationToken);
            return this.Ok(this.Mapper.Map<Integration.Models.HumanTask>(task));
        }

    }

}
