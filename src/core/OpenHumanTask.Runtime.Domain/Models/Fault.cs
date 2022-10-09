namespace OpenHumanTask.Runtime.Domain.Models
{

    /// <summary>
    /// Represents a fault
    /// </summary>
    [DataTransferObjectType(typeof(Integration.Models.Fault))]
    public class Fault
        : Entity<string>
    {

        /// <summary>
        /// Initializes a new <see cref="Fault"/>
        /// </summary>
        protected Fault() : base(string.Empty) { }

        /// <summary>
        /// Initializes a new <see cref="Fault"/>
        /// </summary>
        /// <param name="name">The <see cref="Fault"/>'s name</param>
        /// <param name="description">The <see cref="Fault"/>'s description</param>
        public Fault(string name, string description)
            : base(Guid.NewGuid().ToString().ToLowerInvariant())
        {
            if (string.IsNullOrWhiteSpace(name)) throw DomainException.ArgumentNullOrWhitespace(nameof(name));
            if (string.IsNullOrWhiteSpace(description)) throw DomainException.ArgumentNullOrWhitespace(nameof(description));
            this.Name = name;
            this.Description = description;
        }

        /// <summary>
        /// Gets the <see cref="Fault"/>'s name
        /// </summary>
        public virtual string Name { get; protected set; } = null!;

        /// <summary>
        /// Gets the <see cref="Fault"/>'s description
        /// </summary>
        public virtual string Description { get; protected set; } = null!;

    }

}
