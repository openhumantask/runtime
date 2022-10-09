using OpenHumanTask.Runtime.Integration.Events.HumanTasks;

namespace OpenHumanTask.Runtime.Domain.Events.HumanTasks
{
    /// <summary>
    /// Represents the <see cref="IDomainEvent"/> fired when a <see cref="HumanTask"/> has been resumed
    /// </summary>
    [DataTransferObjectType(typeof(HumanTaskResumedIntegrationEvent))]
    public class HumanTaskResumedDomainEvent
        : DomainEvent<HumanTask, string>
    {

        /// <summary>
        /// Initializes a new <see cref="HumanTaskResumedDomainEvent"/>
        /// </summary>
        protected HumanTaskResumedDomainEvent() { }

        /// <summary>
        /// Initializes a new <see cref="HumanTaskResumedDomainEvent"/>
        /// </summary>
        /// <param name="id">The id of the resumed <see cref="HumanTask"/></param>
        /// <param name="user">The user that has resumed the <see cref="HumanTask"/></param>
        public HumanTaskResumedDomainEvent(string id, UserReference user)
            : base(id)
        {
            this.User = user;
        }

        /// <summary>
        /// Gets the user that has suspended the <see cref="Comment"/>
        /// </summary>
        public virtual UserReference User { get; protected set; } = null!;

    }

}
