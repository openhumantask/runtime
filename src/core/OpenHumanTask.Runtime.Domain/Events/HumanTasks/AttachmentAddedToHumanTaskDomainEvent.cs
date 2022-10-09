using OpenHumanTask.Runtime.Integration.Events.HumanTasks;

namespace OpenHumanTask.Runtime.Domain.Events.HumanTasks
{

    /// <summary>
    /// Represents the <see cref="IDomainEvent"/> fired whenever a new <see cref="Models.Attachment"/> has been attached to a <see cref="HumanTask"/>
    /// </summary>
    [DataTransferObjectType(typeof(AttachmentAddedToHumanTaskIntegrationEvent))]
    public class AttachmentAddedToHumanTaskDomainEvent
        : DomainEvent<HumanTask, string>
    {

        /// <summary>
        /// Initializes a new <see cref="AttachmentAddedToHumanTaskDomainEvent"/>
        /// </summary>
        protected AttachmentAddedToHumanTaskDomainEvent()
        {

        }

        /// <summary>
        /// Initializes a new <see cref="AttachmentAddedToHumanTaskDomainEvent"/>
        /// </summary>
        /// <param name="id">The id of the <see cref="HumanTask"/> a new <see cref="Models.Attachment"/> has been attached to</param>
        /// <param name="user">The user that has added the <see cref="Models.Attachment"/></param>
        /// <param name="attachment">The newly added <see cref="Models.Attachment"/></param>
        public AttachmentAddedToHumanTaskDomainEvent(string id, UserReference user, Attachment attachment)
            : base(id)
        {
            this.User = user;
            this.Attachment = attachment;
        }

        /// <summary>
        /// Gets the user that has added the <see cref="Attachment"/>
        /// </summary>
        public virtual UserReference User { get; protected set; } = null!;

        /// <summary>
        /// Gets the newly added <see cref="Models.Attachment"/>
        /// </summary>
        public virtual Attachment Attachment { get; protected set; } = null!;

    }

}
