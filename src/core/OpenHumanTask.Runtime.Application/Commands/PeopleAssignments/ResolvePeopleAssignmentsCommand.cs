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

using Neuroglia.Data.Expressions;
using OpenHumanTask.Runtime.Application.Services;
using OpenHumanTask.Runtime.Domain;
using OpenHumanTask.Sdk;
using System.Security.Claims;

namespace OpenHumanTask.Runtime.Application.Commands.PeopleAssignments
{

    /// <summary>
    /// Represents the <see cref="ICommand"/> used to resolve <see cref="PeopleAssignmentsDefinition"/>s
    /// </summary>
    [DataTransferObjectType(typeof(Integration.Commands.PeopleAssignments.ResolvePeopleAssignmentsCommand))]
    public class ResolvePeopleAssignmentsCommand
        : Command<Domain.Models.PeopleAssignments>
    {

        /// <summary>
        /// Initializes a new <see cref="ResolvePeopleAssignmentsCommand"/>
        /// </summary>
        protected ResolvePeopleAssignmentsCommand() { }

        /// <summary>
        /// Initializes a new <see cref="ResolvePeopleAssignmentsCommand"/>
        /// </summary>
        /// <param name="peopleAssignments">The <see cref="PeopleAssignmentsDefinition"/> to resolve</param>
        /// <param name="context">The <see cref="HumanTaskRuntimeContext"/> for which to resolve the specified <see cref="PeopleAssignmentsDefinition"/></param>
        public ResolvePeopleAssignmentsCommand(PeopleAssignmentsDefinition? peopleAssignments, HumanTaskRuntimeContext context)
        {
            this.PeopleAssignments = peopleAssignments;
            this.Context = context;
        }

        /// <summary>
        /// Gets the <see cref="PeopleAssignmentsDefinition"/> to resolve
        /// </summary>
        public virtual PeopleAssignmentsDefinition? PeopleAssignments { get; protected set; } = null!;

        /// <summary>
        /// Gets the <see cref="HumanTaskRuntimeContext"/> for which to resolve the specified <see cref="PeopleAssignmentsDefinition"/>
        /// </summary>
        public virtual HumanTaskRuntimeContext Context { get; protected set; } = null!;

    }

    /// <summary>
    /// Represents the service used to handle <see cref="ResolvePeopleAssignmentsCommand"/>s
    /// </summary>
    public class ResolvePeopleAssignmentsCommandHandler
        : CommandHandlerBase,
        ICommandHandler<ResolvePeopleAssignmentsCommand, Domain.Models.PeopleAssignments>
    {

        /// <summary>
        /// Initializes a new <see cref="ResolvePeopleAssignmentsCommandHandler"/>
        /// </summary>
        /// <param name="loggerFactory">The service used to create <see cref="ILogger"/>s.</param>
        /// <param name="mediator">The service used to mediate calls.</param>
        /// <param name="mapper">The service used to map objects.</param>
        /// <param name="userAccessor">The service used to access the current user</param>
        /// <param name="userManager">The service used to manage users</param>
        /// <param name="expressionEvaluatorProvider">The service used to provide <see cref="IExpressionEvaluator"/>s</param>
        public ResolvePeopleAssignmentsCommandHandler(ILoggerFactory loggerFactory, IMediator mediator, IMapper mapper, IUserAccessor userAccessor, 
            IUserManager userManager, IExpressionEvaluatorProvider expressionEvaluatorProvider) 
            : base(loggerFactory, mediator, mapper, userAccessor)
        {
            this.UserManager = userManager;
            this.ExpressionEvaluatorProvider = expressionEvaluatorProvider;
        }

        /// <summary>
        /// Gets the service used to manage users
        /// </summary>
        protected IUserManager UserManager { get; }

        /// <summary>
        /// Gets the service used to provide <see cref="IExpressionEvaluator"/>s
        /// </summary>
        protected IExpressionEvaluatorProvider ExpressionEvaluatorProvider { get; }

        /// <inheritdoc/>
        public virtual async Task<IOperationResult<Domain.Models.PeopleAssignments>> HandleAsync(ResolvePeopleAssignmentsCommand command, CancellationToken cancellationToken = default)
        {
            var user = this.UserAccessor.User;
            if (user == null || !user.Identity?.IsAuthenticated == true) return this.Forbid();
            var availableUsers = await this.UserManager.ListUsersAsync(cancellationToken);
            var resolvedUsers = new Dictionary<GenericHumanRole, List<UserReference>>() { { GenericHumanRole.Initiator, new() { user } } };
            if (command.PeopleAssignments == null) return this.Ok(new Domain.Models.PeopleAssignments(user));
            var expressionEvaluator = this.ExpressionEvaluatorProvider.GetEvaluator(command.Context.ExpressionLanguage);
            if (expressionEvaluator == null) throw HumanTaskDomainExceptions.RuntimeExpressionLanguageNotSupported(command.Context.ExpressionLanguage);
            var resolvedGroups = this.ResolveLogicalGroups(command.Context, expressionEvaluator, availableUsers, resolvedUsers, command.PeopleAssignments.Groups);
            this.ResolveAssignmentsTo(command.Context, expressionEvaluator, availableUsers, resolvedUsers, resolvedGroups, GenericHumanRole.PotentialInitiator, command.PeopleAssignments.PotentialInitiators);
            this.ResolveAssignmentsTo(command.Context, expressionEvaluator, availableUsers, resolvedUsers, resolvedGroups, GenericHumanRole.PotentialInitiator, command.PeopleAssignments.PotentialOwners);
            this.ResolveAssignmentsTo(command.Context, expressionEvaluator, availableUsers, resolvedUsers, resolvedGroups, GenericHumanRole.PotentialInitiator, command.PeopleAssignments.ExcludedOwners);
            this.ResolveAssignmentsTo(command.Context, expressionEvaluator, availableUsers, resolvedUsers, resolvedGroups, GenericHumanRole.PotentialInitiator, command.PeopleAssignments.Stakeholders);
            this.ResolveAssignmentsTo(command.Context, expressionEvaluator, availableUsers, resolvedUsers, resolvedGroups, GenericHumanRole.PotentialInitiator, command.PeopleAssignments.BusinessAdministrators);
            this.ResolveAssignmentsTo(command.Context, expressionEvaluator, availableUsers, resolvedUsers, resolvedGroups, GenericHumanRole.PotentialInitiator, command.PeopleAssignments.NotificationRecipients);
            return this.Ok(new Domain.Models.PeopleAssignments
            (
                user, 
                resolvedUsers[GenericHumanRole.PotentialInitiator], 
                resolvedUsers[GenericHumanRole.PotentialOwner], 
                null, 
                resolvedUsers[GenericHumanRole.ExcludedOwner], 
                resolvedUsers[GenericHumanRole.Stakeholder], 
                resolvedUsers[GenericHumanRole.BusinessAdministrator], 
                resolvedUsers[GenericHumanRole.NotificationRecipient],
                resolvedGroups));
        }

        /// <summary>
        /// Resolves the specified <see cref="LogicalPeopleGroupDefinition"/>s
        /// </summary>
        /// <param name="context">The <see cref="HumanTaskRuntimeContext"/> for which to resolve the specified <see cref="PeopleAssignmentsDefinition"/></param>
        /// <param name="expressionEvaluator">The service used to evaluate runtime expressions</param>
        /// <param name="availableUsers">A <see cref="List{T}"/> containing all known users</param>
        /// <param name="resolvedUsers">An <see cref="IDictionary{TKey, TValue}"/> containing the resolved users for each <see cref="GenericHumanRole"/></param>
        /// <param name="groups">A <see cref="List{T}"/> containing the <see cref="LogicalPeopleGroupDefinition"/>s to resolve</param>
        /// <returns>A new <see cref="Dictionary{TKey, TValue}"/> containing the resolved users per group</returns>
        protected virtual Dictionary<string, List<UserReference>> ResolveLogicalGroups(HumanTaskRuntimeContext context, IExpressionEvaluator expressionEvaluator, List<ClaimsIdentity> availableUsers, IDictionary<GenericHumanRole, List<UserReference>> resolvedUsers, List<LogicalPeopleGroupDefinition>? groups)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (expressionEvaluator == null) throw new ArgumentNullException(nameof(expressionEvaluator));
            if (availableUsers == null) throw new ArgumentNullException(nameof(availableUsers));
            if (resolvedUsers == null) throw new ArgumentNullException(nameof(resolvedUsers));
            var resolvedGroups = new Dictionary<string, List<UserReference>>();
            if (groups == null) return resolvedGroups;
            foreach (var group in groups)
            {
                var users = new List<UserReference>();
                foreach (var peopleReference in group.Members)
                {
                    users.AddRange(peopleReference.Resolve(context, expressionEvaluator, availableUsers, resolvedUsers, resolvedGroups));
                }
                resolvedGroups.Add(group.Name, users);
            }
            return resolvedGroups;
        }

        /// <summary>
        /// Resolves the specified <see cref="PeopleReferenceDefinition"/>s
        /// </summary>
        /// <param name="context">The <see cref="HumanTaskRuntimeContext"/> for which to resolve the specified <see cref="PeopleAssignmentsDefinition"/></param>
        /// <param name="expressionEvaluator">The service used to evaluate runtime expressions</param>
        /// <param name="availableUsers">A <see cref="List{T}"/> containing all known users</param>
        /// <param name="resolvedUsers">An <see cref="IDictionary{TKey, TValue}"/> containing the resolved users for each <see cref="GenericHumanRole"/></param>
        /// <param name="resolvedGroups">An <see cref="IDictionary{TKey, TValue}"/> containing the name/users mappings of the resolved user groups</param>
        /// <param name="role">The <see cref="GenericHumanRole"/> to resolve the specified <see cref="PeopleReferenceDefinition"/>s for</param>
        /// <param name="peopleReferences">A <see cref="List{T}"/> containing the <see cref="PeopleReferenceDefinition"/>s to resolve</param>
        protected virtual void ResolveAssignmentsTo(HumanTaskRuntimeContext context, IExpressionEvaluator expressionEvaluator, List<ClaimsIdentity> availableUsers, IDictionary<GenericHumanRole, List<UserReference>> resolvedUsers, Dictionary<string, List<UserReference>> resolvedGroups, GenericHumanRole role, List<PeopleReferenceDefinition>? peopleReferences)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (expressionEvaluator == null) throw new ArgumentNullException(nameof(expressionEvaluator));
            if (availableUsers == null) throw new ArgumentNullException(nameof(availableUsers));
            if (resolvedUsers == null) throw new ArgumentNullException(nameof(resolvedUsers));
            if (resolvedGroups == null) throw new ArgumentNullException(nameof(resolvedGroups));
            if (peopleReferences == null) return;
            var users = new List<UserReference>();
            foreach (var peopleReference in peopleReferences)
            {
                users.AddRange(peopleReference.Resolve(context, expressionEvaluator, availableUsers, resolvedUsers, resolvedGroups));
            }
            if(!resolvedUsers.TryGetValue(role, out var usersPerRole))
            {
                usersPerRole = new();
                resolvedUsers[role] = usersPerRole;
            }
            usersPerRole.AddRange(users);
        }

    }

}
