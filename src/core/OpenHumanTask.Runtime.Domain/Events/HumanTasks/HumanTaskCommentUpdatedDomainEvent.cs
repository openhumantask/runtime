using OpenHumanTask.Runtime.Integration.Events.HumanTasks;

namespace OpenHumanTask.Runtime.Domain.Events.HumanTasks
{
    /// <summary>
    /// Represents the <see cref="IDomainEvent"/> fired when a <see cref="HumanTask"/>'s <see cref="Models.Comment"/> has been updated
    /// </summary>
    [DataTransferObjectType(typeof(CommentAddedToHumanTaskIntegrationEvent))]
    public class HumanTaskCommentUpdatedDomainEvent
        : DomainEvent<HumanTask, string>
    {

        /// <summary>
        /// Initializes a new <see cref="HumanTaskCommentUpdatedDomainEvent"/>
        /// </summary>
        protected HumanTaskCommentUpdatedDomainEvent()
        {

        }

        /// <summary>
        /// Initializes a new <see cref="HumanTaskCommentUpdatedDomainEvent"/>
        /// </summary>
        /// <param name="id">The id of the <see cref="HumanTask"/> which's <see cref="Models.Comment"/> has been updated</param>
        /// <param name="user">The user that has updated the <see cref="Models.Comment"/></param>
        /// <param name="commentId">The id of the <see cref="Models.Comment"/> that has been updated</param>
        /// <param name="content">The updated comment</param>
        public HumanTaskCommentUpdatedDomainEvent(string id, UserReference user, string commentId, string content)
            : base(id)
        {
            this.User = user;
            this.CommentId = commentId;
            this.Content = content;
        }

        /// <summary>
        /// Gets the user that has updated the <see cref="Models.Comment"/>
        /// </summary>
        public virtual UserReference User { get; protected set; } = null!;

        /// <summary>
        /// Gets the id of the <see cref="Models.Comment"/> that has been updated
        /// </summary>
        public virtual string CommentId { get; protected set; } = null!;

        /// <summary>
        /// Gets the updated comment
        /// </summary>
        public virtual string Content { get; protected set; } = null!;

    }

}
