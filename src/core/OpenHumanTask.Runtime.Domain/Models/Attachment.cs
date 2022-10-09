namespace OpenHumanTask.Runtime.Domain.Models
{
    /// <summary>
    /// Represents an <see cref="Attachment"/>
    /// </summary>
    [DataTransferObjectType(typeof(Integration.Models.Attachment))]
    public class Attachment
        : Entity<string>
    {

        /// <summary>
        /// Initializes a new <see cref="Attachment"/>
        /// </summary>
        protected Attachment() { }

        /// <summary>
        /// Initializes a new <see cref="Attachment"/>
        /// </summary>
        /// <param name="author">The user that has created the <see cref="Attachment"/></param>
        /// <param name="name">The <see cref="Attachment"/>'s name</param>
        /// <param name="contentType">The <see cref="Attachment"/>'s content type</param>
        public Attachment(UserReference author, string name, string contentType)
            : base(Guid.NewGuid().ToString())
        {
            this.Author = author;
            this.Name = name;
            this.ContentType = contentType;
        }

        /// <summary>
        /// Gets the id of the user that has created the <see cref="Attachment"/>
        /// </summary>
        public virtual UserReference Author { get; protected set; } = null!;

        /// <summary>
        /// Gets the <see cref="Attachment"/>'s name
        /// </summary>
        public virtual string Name { get; protected set; } = null!;

        /// <summary>
        /// Gets the <see cref="Attachment"/>'s content type
        /// </summary>
        public virtual string ContentType { get; protected set; } = null!;

    }

}
