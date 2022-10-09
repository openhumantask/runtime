using OpenHumanTask.Runtime.Integration.Events.HumanTasks;

namespace OpenHumanTask.Runtime.Domain.Events.HumanTasks
{
    /// <summary>
    /// Represents the <see cref="IDomainEvent"/> fired when a new <see cref="Models.Comment"/> has been added to a <see cref="HumanTask"/>
    /// </summary>
    [DataTransferObjectType(typeof(CommentAddedToHumanTaskIntegrationEvent))]
    public class CommentAddedToHumanTaskDomainEvent
        : DomainEvent<HumanTask, string>
    {

        /// <summary>
        /// Initializes a new <see cref="CommentAddedToHumanTaskDomainEvent"/>
        /// </summary>
        protected CommentAddedToHumanTaskDomainEvent() { }

        /// <summary>
        /// Initializes a new <see cref="CommentAddedToHumanTaskDomainEvent"/>
        /// </summary>
        /// <param name="id">The id of the <see cref="HumanTask"/> a new <see cref="Models.Comment"/> has been added to</param>
        /// <param name="author">The user that has authored the <see cref="Models.Comment"/></param>
        /// <param name="comment">The newly added <see cref="Models.Comment"/></param>
        public CommentAddedToHumanTaskDomainEvent(string id, UserReference author, Comment comment)
            : base(id)
        {
            this.User = author;
            this.Comment = comment;
        }

        /// <summary>
        /// Gets the user that has authored the <see cref="Models.Comment"/>
        /// </summary>
        public virtual UserReference User { get; protected set; } = null!;

        /// <summary>
        /// Gets the newly added <see cref="Models.Comment"/>
        /// </summary>
        public virtual Comment Comment { get; protected set; } = null!;

    }

}
