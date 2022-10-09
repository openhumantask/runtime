using OpenHumanTask.Runtime.Integration.Events.HumanTasks;

namespace OpenHumanTask.Runtime.Domain.Events.HumanTasks
{
    /// <summary>
    /// Represents the <see cref="IDomainEvent"/> fired when a <see cref="Fault"/> has been removed from a <see cref="HumanTask"/>
    /// </summary>
    [DataTransferObjectType(typeof(FaultRemovedFromHumanTaskIntegrationEvent))]
    public class FaultRemovedFromHumanTaskDomainEvent
        : DomainEvent<HumanTask, string>
    {

        /// <summary>
        /// Initializes a new <see cref="FaultRemovedFromHumanTaskDomainEvent"/>
        /// </summary>
        protected FaultRemovedFromHumanTaskDomainEvent() { }

        /// <summary>
        /// Initializes a new <see cref="FaultsAddedToHumanTaskDomainEvent"/>
        /// </summary>
        /// <param name="id">The id of the <see cref="HumanTask"/> a <see cref="Fault"/> has been removed from</param>
        /// <param name="user">The user that has removed the <see cref="Fault"/></param>
        /// <param name="faultId">The id of the removed <see cref="Fault"/>s</param>
        public FaultRemovedFromHumanTaskDomainEvent(string id, UserReference user, string faultId)
            : base(id)
        {
            this.User = user;
            this.FaultId = faultId;
        }

        /// <summary>
        /// Gets the user that has removed the <see cref="Fault"/>
        /// </summary>
        public virtual UserReference User { get; protected set; } = null!;

        /// <summary>
        /// Gets the id of the removed <see cref="Fault"/>s
        /// </summary>
        public virtual string FaultId { get; protected set; } = null!;

    }

}
