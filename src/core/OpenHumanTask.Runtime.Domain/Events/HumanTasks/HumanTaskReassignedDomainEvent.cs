using OpenHumanTask.Runtime.Integration.Events.HumanTasks;

namespace OpenHumanTask.Runtime.Domain.Events.HumanTasks
{
    /// <summary>
    /// Represents the <see cref="IDomainEvent"/> fired whenever a <see cref="HumanTask"/>'s has been reassigned
    /// </summary>
    [DataTransferObjectType(typeof(HumanTaskReassignedIntegrationEvent))]
    public class HumanTaskReassignedDomainEvent
        : DomainEvent<HumanTask, string>
    {

        /// <summary>
        /// Initializes a new <see cref="HumanTaskReassignedDomainEvent"/>
        /// </summary>
        protected HumanTaskReassignedDomainEvent() { }

        /// <summary>
        /// Initializes a new <see cref="HumanTaskReassignedDomainEvent"/>
        /// </summary>
        /// <param name="id">The id of the reassigned <see cref="HumanTask"/></param>
        /// <param name="user">The user that has reassigned the <see cref="HumanTask"/></param>
        /// <param name="assignments">The <see cref="HumanTask"/>'s updated assignments</param>
        public HumanTaskReassignedDomainEvent(string id, UserReference user, PeopleAssignments assignments)
            : base(id)
        {
            this.User = user;
            this.Assignments = assignments;
        }

        /// <summary>
        /// Gets the user that has changed the <see cref="HumanTask"/>'s priority
        /// </summary>
        public virtual UserReference User { get; protected set; } = null!;

        /// <summary>
        /// Gets the <see cref="HumanTask"/>'s updated assignments
        /// </summary>
        public virtual PeopleAssignments Assignments { get; protected set; } = null!;

    }

}
