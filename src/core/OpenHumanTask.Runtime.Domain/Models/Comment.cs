namespace OpenHumanTask.Runtime.Domain.Models
{
    /// <summary>
    /// Represents a comment
    /// </summary>
    [DataTransferObjectType(typeof(Integration.Models.Comment))]
    public class Comment
        : Entity<string>
    {

        /// <summary>
        /// Initializes a new <see cref="Comment"/>
        /// </summary>
        protected Comment() { }

        /// <summary>
        /// Initializes a new <see cref="Comment"/>
        /// </summary>
        /// <param name="author">The <see cref="Comment"/>'s author</param>
        /// <param name="content">The <see cref="Comment"/>'s Markdown (MD) content</param>
        public Comment(UserReference author, string content)
            : base(Guid.NewGuid().ToString())
        {
            this.AuthorId = author;
            this.Content = content;
        }

        /// <summary>
        /// Gets the <see cref="Comment"/>'s author
        /// </summary>
        public virtual UserReference AuthorId { get; protected set; } = null!;

        /// <summary>
        /// Gets the user that has last updated the <see cref="Comment"/>'s content
        /// </summary>
        public virtual UserReference? LastUpdatedBy { get; protected set; } = null!;

        /// <summary>
        /// Gets the <see cref="Comment"/>'s Markdown (MD) content
        /// </summary>
        public virtual string Content { get; protected set; } = null!;

        /// <summary>
        /// Sets the <see cref="Comment"/>'s content
        /// </summary>
        /// <param name="dateTime">The date and time at which the <see cref="Comment"/> has been updated</param>
        /// <param name="user">The user that has updated the <see cref="Comment"/>'s content</param>
        /// <param name="content">The updated content</param>
        /// <returns>A boolean indicating whether or not the operation was successfull</returns>
        internal protected virtual bool SetContent(DateTimeOffset dateTime, UserReference user, string content)
        {
            if (string.IsNullOrWhiteSpace(content)) throw DomainException.ArgumentNullOrWhitespace(nameof(content));
            if (this.Content.Equals(content, StringComparison.InvariantCultureIgnoreCase)) return false;
            this.Content = content;
            this.LastUpdatedBy = user;
            this.LastModified = dateTime;
            return true;
        }

    }

}
