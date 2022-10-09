using OpenHumanTask.Runtime.Integration.Events.HumanTasks;

namespace OpenHumanTask.Runtime.Domain.Events.HumanTasks
{
    /// <summary>
    /// Represents the <see cref="IDomainEvent"/> fired when a <see cref="HumanTask"/> has faulted
    /// </summary>
    [DataTransferObjectType(typeof(HumanTaskFaultedIntegrationEvent))]
    public class HumanTaskFaultedDomainEvent
        : DomainEvent<HumanTask, string>
    {

        /// <summary>
        /// Initializes a new <see cref="HumanTaskFaultedDomainEvent"/>
        /// </summary>
        protected HumanTaskFaultedDomainEvent() { }

        /// <summary>
        /// Initializes a new <see cref="HumanTaskFaultedDomainEvent"/>
        /// </summary>
        /// <param name="id">The id of the faulted <see cref="HumanTask"/></param>
        /// <param name="user">The user that has faulted the <see cref="HumanTask"/></param>
        /// <param name="faults">An <see cref="IEnumerable{T}"/> containing the <see cref="HumanTask"/>'s <see cref="Fault"/>s</param>
        public HumanTaskFaultedDomainEvent(string id, UserReference user, IEnumerable<Fault> faults)
            : base(id)
        {
            this.User = user;
            this.Faults = faults;
        }

        /// <summary>
        /// Gets the user that has faulted the <see cref="HumanTask"/>
        /// </summary>
        public virtual UserReference User { get; protected set; } = null!;

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> containing the <see cref="HumanTask"/>'s <see cref="Fault"/>s
        /// </summary>
        public virtual IEnumerable<Fault> Faults { get; protected set; } = null!;

    }

    /// <summary>
    /// Represents the <see cref="IDomainEvent"/> fired when new <see cref="Fault"/>s have been added to a <see cref="HumanTask"/>
    /// </summary>
    [DataTransferObjectType(typeof(FaultsAddedToHumanTaskIntegrationEvent))]
    public class FaultsAddedToHumanTaskDomainEvent
        : DomainEvent<HumanTask, string>
    {

        /// <summary>
        /// Initializes a new <see cref="FaultsAddedToHumanTaskDomainEvent"/>
        /// </summary>
        protected FaultsAddedToHumanTaskDomainEvent() { }

        /// <summary>
        /// Initializes a new <see cref="FaultsAddedToHumanTaskDomainEvent"/>
        /// </summary>
        /// <param name="id">The id of the <see cref="HumanTask"/> new <see cref="Fault"/>s have been added to</param>
        /// <param name="user">The user that has added the <see cref="Fault"/>s</param>
        /// <param name="faults">An <see cref="IEnumerable{T}"/> containing the added <see cref="Fault"/>s</param>
        public FaultsAddedToHumanTaskDomainEvent(string id, UserReference user, IEnumerable<Fault> faults)
            : base(id)
        {
            this.User = user;
            this.Faults = faults;
        }

        /// <summary>
        /// Gets the user that has added the <see cref="Fault"/>s
        /// </summary>
        public virtual UserReference User { get; protected set; } = null!;

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> containing the added <see cref="Fault"/>s
        /// </summary>
        public virtual IEnumerable<Fault> Faults { get; protected set; } = null!;

    }

}
