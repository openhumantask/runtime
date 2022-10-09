using OpenHumanTask.Runtime.Integration.Events.HumanTasks;

namespace OpenHumanTask.Runtime.Domain.Events.HumanTasks
{
    /// <summary>
    /// Represents the <see cref="IDomainEvent"/> fired when a <see cref="HumanTask"/> has been suspended
    /// </summary>
    [DataTransferObjectType(typeof(HumanTaskSuspendedIntegrationEvent))]
    public class HumanTaskSuspendedDomainEvent
        : DomainEvent<HumanTask, string>
    {

        /// <summary>
        /// Initializes a new <see cref="HumanTaskSuspendedDomainEvent"/>
        /// </summary>
        protected HumanTaskSuspendedDomainEvent() { }

        /// <summary>
        /// Initializes a new <see cref="HumanTaskSuspendedDomainEvent"/>
        /// </summary>
        /// <param name="id">The id of the suspended <see cref="HumanTask"/></param>
        /// <param name="user">The user that has suspended the <see cref="HumanTask"/></param>
        /// <param name="reason">The reason why the <see cref="HumanTask"/> has been suspended</param>
        public HumanTaskSuspendedDomainEvent(string id, UserReference user, string? reason)
            : base(id)
        {
            this.User = user;
            this.Reason = reason;
        }

        /// <summary>
        /// Gets the user that has suspended the <see cref="Comment"/>
        /// </summary>
        public virtual UserReference User { get; protected set; } = null!;

        /// <summary>
        /// Gets the reason why the <see cref="HumanTask"/> has been suspended
        /// </summary>
        public virtual string? Reason { get; protected set; }

    }

}
