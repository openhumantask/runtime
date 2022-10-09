using OpenHumanTask.Runtime.Integration.Events.HumanTasks;

namespace OpenHumanTask.Runtime.Domain.Events.HumanTasks
{
    /// <summary>
    /// Represents the <see cref="IDomainEvent"/> fired whenever a <see cref="HumanTask"/> has been claimed
    /// </summary>
    [DataTransferObjectType(typeof(HumanTaskClaimedIntegrationEvent))]
    public class HumanTaskClaimedDomainEvent
        : DomainEvent<HumanTask, string>
    {

        /// <summary>
        /// Initializes a new <see cref="HumanTaskClaimedDomainEvent"/>
        /// </summary>
        protected HumanTaskClaimedDomainEvent()
        {

        }

        /// <summary>
        /// Initializes a new <see cref="HumanTaskClaimedDomainEvent"/>
        /// </summary>
        /// <param name="id">The id of the claimed <see cref="HumanTask"/></param>
        /// <param name="user">The user that has claimed the <see cref="HumanTask"/></param>
        public HumanTaskClaimedDomainEvent(string id, UserReference user)
            : base(id)
        {
            this.User = user;
        }

        /// <summary>
        /// Gets the user that has claimed the <see cref="HumanTask"/>
        /// </summary>
        public virtual UserReference User { get; protected set; } = null!;

    }
    

}
