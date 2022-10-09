using OpenHumanTask.Runtime.Integration.Events.HumanTasks;

namespace OpenHumanTask.Runtime.Domain.Events.HumanTasks
{
    /// <summary>
    /// Represents the <see cref="IDomainEvent"/> fired whenever a <see cref="HumanTask"/> has been released
    /// </summary>
    [DataTransferObjectType(typeof(HumanTaskReleasedIntegrationEvent))]
    public class HumanTaskReleasedDomainEvent
        : DomainEvent<HumanTask, string>
    {

        /// <summary>
        /// Initializes a new <see cref="HumanTaskReleasedDomainEvent"/>
        /// </summary>
        protected HumanTaskReleasedDomainEvent() { }

        /// <summary>
        /// Initializes a new <see cref="HumanTaskReleasedDomainEvent"/>
        /// </summary>
        /// <param name="id">The id of the released <see cref="HumanTask"/></param>
        /// <param name="user">The user that has released the <see cref="HumanTask"/></param>
        public HumanTaskReleasedDomainEvent(string id, UserReference user)
            : base(id)
        {
            this.User = user;
        }

        /// <summary>
        /// Gets the user that has released the <see cref="HumanTask"/>
        /// </summary>
        public virtual UserReference User { get; protected set; } = null!;

    }
}
