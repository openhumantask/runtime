using OpenHumanTask.Runtime.Integration.Events.HumanTasks;

namespace OpenHumanTask.Runtime.Domain.Events.HumanTasks
{
    /// <summary>
    /// Represents the <see cref="IDomainEvent"/> fired whenever a <see cref="HumanTask"/>'s status has changed
    /// </summary>
    [DataTransferObjectType(typeof(HumanTaskStatusChangedIntegrationEvent))]
    public class HumanTaskStatusChangedDomainEvent
        : DomainEvent<HumanTask, string>
    {

        /// <summary>
        /// Initializes a new <see cref="HumanTaskStatusChangedDomainEvent"/>
        /// </summary>
        protected HumanTaskStatusChangedDomainEvent()
        {

        }

        /// <summary>
        /// Initializes a new <see cref="HumanTaskStatusChangedDomainEvent"/>
        /// </summary>
        /// <param name="id">The id of the <see cref="HumanTask"/> which's status has changed</param>
        /// <param name="status">The <see cref="HumanTask"/>'s updated status</param>
        public HumanTaskStatusChangedDomainEvent(string id, HumanTaskStatus status)
            : base(id)
        {
            this.Status = status;
        }

        /// <summary>
        /// Gets the <see cref="HumanTask"/>'s updated status
        /// </summary>
        public virtual HumanTaskStatus Status { get; protected set; }

    }

}
