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

using IdentityModel;
using OpenHumanTask.Runtime.Application.Services;
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
        public ResolvePeopleAssignmentsCommand(PeopleAssignmentsDefinition peopleAssignments)
        {
            this.PeopleAssignments = peopleAssignments;
        }

        /// <summary>
        /// Gets the <see cref="PeopleAssignmentsDefinition"/> to resolve
        /// </summary>
        public virtual PeopleAssignmentsDefinition PeopleAssignments { get; protected set; } = null!;

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
        public ResolvePeopleAssignmentsCommandHandler(ILoggerFactory loggerFactory, IMediator mediator, IMapper mapper, IUserAccessor userAccessor, IUserManager userManager) 
            : base(loggerFactory, mediator, mapper, userAccessor)
        {
            this.UserManager = userManager;
        }

        /// <summary>
        /// Gets the service used to manage users
        /// </summary>
        protected IUserManager UserManager { get; }

        /// <inheritdoc/>
        public virtual async Task<IOperationResult<Domain.Models.PeopleAssignments>> HandleAsync(ResolvePeopleAssignmentsCommand command, CancellationToken cancellationToken = default)
        {
            var user = this.UserAccessor.User;
            if (user == null || !user.Identity?.IsAuthenticated == true) return this.Forbid();
            var availableUsers = await this.UserManager.ListUsersAsync(cancellationToken);
            var resolvedUsers = new Dictionary<GenericHumanRole, List<UserReference>>() { { GenericHumanRole.Initiator, new() { user } } };
            await this.ResolveUsersAsync(availableUsers, resolvedUsers, command.PeopleAssignments.PotentialInitiators, cancellationToken);
            await this.ResolveUsersAsync(availableUsers, resolvedUsers, command.PeopleAssignments.PotentialInitiators, cancellationToken);
            await this.ResolveUsersAsync(availableUsers, resolvedUsers, command.PeopleAssignments.PotentialOwners, cancellationToken);
            await this.ResolveUsersAsync(availableUsers, resolvedUsers, command.PeopleAssignments.ExcludedOwners, cancellationToken);
            await this.ResolveUsersAsync(availableUsers, resolvedUsers, command.PeopleAssignments.Stakeholders, cancellationToken);
            await this.ResolveUsersAsync(availableUsers, resolvedUsers, command.PeopleAssignments.BusinessAdministrators, cancellationToken);
            await this.ResolveUsersAsync(availableUsers, resolvedUsers, command.PeopleAssignments.NotificationRecipients, cancellationToken);
            return this.Ok(new Domain.Models.PeopleAssignments
            (
                user, 
                resolvedUsers[GenericHumanRole.PotentialInitiator], 
                resolvedUsers[GenericHumanRole.PotentialOwner], 
                null, 
                resolvedUsers[GenericHumanRole.ExcludedOwner], 
                resolvedUsers[GenericHumanRole.Stakeholder], 
                resolvedUsers[GenericHumanRole.BusinessAdministrator], 
                resolvedUsers[GenericHumanRole.NotificationRecipient]));
        }

        protected virtual async Task ResolveUsersAsync(List<ClaimsIdentity> availableUsers, IDictionary<GenericHumanRole, List<UserReference>> resolvedUsers, List<PeopleReferenceDefinition>? peopleReferences, CancellationToken cancellationToken)
        {
            if (availableUsers == null) throw new ArgumentNullException(nameof(availableUsers));
            if (peopleReferences == null) return;
            var users = new List<UserReference>();
            foreach (var peopleReference in peopleReferences)
            {
                users.AddRange(await this.ResolveUsersAsync(availableUsers, peopleReference, cancellationToken));
            }
        }

        protected virtual async Task ResolveUsersAsync(List<ClaimsIdentity> availableUsers, IDictionary<GenericHumanRole, List<UserReference>> resolvedUsers, PeopleReferenceDefinition peopleReference, CancellationToken cancellationToken)
        {
            if (availableUsers == null) throw new ArgumentNullException(nameof(availableUsers));
            if (peopleReference == null) throw new ArgumentNullException(nameof(peopleReference));
            var users = new List<UserReference>();
            if (peopleReference.User != null)
            {
                var user = availableUsers.FirstOrDefault(u => u.FindFirst(JwtClaimTypes.Subject)?.Value.Equals(peopleReference.User, StringComparison.InvariantCultureIgnoreCase) == true);
                if (user != null)
                    users.Add(user);
            }
            else if(peopleReference.Users != null)
            {
                if (peopleReference.Users.InGenericRole != null)
                {

                }
                else if(peopleReference.Users.InGroup != null)
                {

                }
                else if (peopleReference.Users.WithClaims != null)
                {
                    foreach(var user in availableUsers)
                    {
                        if (peopleReference.Users.WithClaims.Filters(user)) users.Add(user);
                    }
                }
            }
            return users;
        }

    }

}
