using OpenHumanTask.Runtime.Integration.Events.HumanTasks;

namespace OpenHumanTask.Runtime.Domain.Events.HumanTasks
{
    /// <summary>
    /// Represents the <see cref="IDomainEvent"/> fired when a <see cref="HumanTask"/> has been cancelled
    /// </summary>
    [DataTransferObjectType(typeof(HumanTaskCancelledIntegrationEvent))]
    public class HumanTaskCancelledDomainEvent
        : DomainEvent<HumanTask, string>
    {

        /// <summary>
        /// Initializes a new <see cref="HumanTaskCancelledDomainEvent"/>
        /// </summary>
        protected HumanTaskCancelledDomainEvent() { }

        /// <summary>
        /// Initializes a new <see cref="HumanTaskCancelledDomainEvent"/>
        /// </summary>
        /// <param name="id">The id of the cancelled <see cref="HumanTask"/></param>
        /// <param name="user">The user that has cancelled the <see cref="HumanTask"/></param>
        /// <param name="reason">The reason why the <see cref="HumanTask"/> has been cancelled</param>
        public HumanTaskCancelledDomainEvent(string id, UserReference user, string? reason)
            : base(id)
        {
            this.User = user;
            this.Reason = reason;
        }

        /// <summary>
        /// Gets the user that has skipped the <see cref="Comment"/>
        /// </summary>
        public virtual UserReference User { get; protected set; } = null!;

        /// <summary>
        /// Gets the reason why the <see cref="HumanTask"/> has been cancelled
        /// </summary>
        public virtual string? Reason { get; protected set; } = null!;

    }
    

}
