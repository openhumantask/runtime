using OpenHumanTask.Runtime.Integration.Events.HumanTasks;

namespace OpenHumanTask.Runtime.Domain.Events.HumanTasks
{

    /// <summary>
    /// Represents the <see cref="IDomainEvent"/> fired when a <see cref="HumanTask"/> has been deleted
    /// </summary>
    [DataTransferObjectType(typeof(HumanTaskDeletedIntegrationEvent))]
    public class HumanTaskDeletedDomainEvent
        : DomainEvent<HumanTask, string>
    {

        /// <summary>
        /// Initializes a new <see cref="HumanTaskDeletedDomainEvent"/>
        /// </summary>
        protected HumanTaskDeletedDomainEvent() { }

        /// <summary>
        /// Initializes a new <see cref="HumanTaskDeletedDomainEvent"/>
        /// </summary>
        /// <param name="id">The id of the deleted <see cref="HumanTask"/></param>
        /// <param name="user">The user that has deleted the <see cref="HumanTask"/></param>
        public HumanTaskDeletedDomainEvent(string id, UserReference user)
            : base(id)
        {
            this.User = user;
        }

        /// <summary>
        /// Gets the user that has deleted the <see cref="HumanTask"/>
        /// </summary>
        public virtual UserReference User { get; protected set; } = null!;

    }
    

}
