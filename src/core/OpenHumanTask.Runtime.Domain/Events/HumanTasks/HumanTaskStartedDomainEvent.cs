using OpenHumanTask.Runtime.Integration.Events.HumanTasks;

namespace OpenHumanTask.Runtime.Domain.Events.HumanTasks
{
    /// <summary>
    /// Represents the <see cref="IDomainEvent"/> fired when a <see cref="HumanTask"/> has been started
    /// </summary>
    [DataTransferObjectType(typeof(HumanTaskStartedIntegrationEvent))]
    public class HumanTaskStartedDomainEvent
        : DomainEvent<HumanTask, string>
    {

        /// <summary>
        /// Initializes a new <see cref="HumanTaskStartedDomainEvent"/>
        /// </summary>
        protected HumanTaskStartedDomainEvent() { }

        /// <summary>
        /// Initializes a new <see cref="HumanTaskStartedDomainEvent"/>
        /// </summary>
        /// <param name="id">The id of the started <see cref="HumanTask"/></param>
        /// <param name="user">The user that has started the <see cref="HumanTask"/></param>
        public HumanTaskStartedDomainEvent(string id, UserReference user)
            : base(id)
        {
            this.User = user;
        }

        /// <summary>
        /// Gets the user that has started the <see cref="Comment"/>
        /// </summary>
        public virtual UserReference User { get; protected set; } = null!;

    }
    

}
