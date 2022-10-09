using OpenHumanTask.Runtime.Integration.Events.HumanTasks;

namespace OpenHumanTask.Runtime.Domain.Events.HumanTasks
{

    /// <summary>
    /// Represents the <see cref="IDomainEvent"/> fired whenever a <see cref="HumanTask"/> has been forwarded
    /// </summary>
    [DataTransferObjectType(typeof(HumanTaskForwardedIntegrationEvent))]
    public class HumanTaskForwardedDomainEvent
        : DomainEvent<HumanTask, string>
    {

        /// <summary>
        /// Initializes a new <see cref="HumanTaskForwardedDomainEvent"/>
        /// </summary>
        protected HumanTaskForwardedDomainEvent() { }

        /// <summary>
        /// Initializes a new <see cref="HumanTaskForwardedDomainEvent"/>
        /// </summary>
        /// <param name="id">The id of the forwarded <see cref="HumanTask"/></param>
        /// <param name="user">The user that has forwarded the <see cref="HumanTask"/></param>
        /// <param name="forwardedTo">An <see cref="IEnumerable{T}"/> containing the users the <see cref="HumanTask"/> has been forwarded to</param>
        public HumanTaskForwardedDomainEvent(string id, UserReference user, IEnumerable<UserReference> forwardedTo)
            : base(id)
        {
            this.User = user;
            this.ForwardedTo = forwardedTo;
        }

        /// <summary>
        /// Gets the user that has forwarded the <see cref="HumanTask"/>
        /// </summary>
        public virtual UserReference User { get; protected set; } = null!;

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> containing the users the <see cref="HumanTask"/> has been forwarded to
        /// </summary>
        public virtual IEnumerable<UserReference> ForwardedTo { get; protected set; } = null!;

    }

}
