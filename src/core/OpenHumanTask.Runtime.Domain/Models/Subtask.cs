using OpenHumanTask.Sdk.Models;

namespace OpenHumanTask.Runtime.Domain.Models
{
    /// <summary>
    /// Represents a <see cref="HumanTask"/>'s subtask
    /// </summary>
    [DataTransferObjectType(typeof(Integration.Models.Subtask))]
    public class Subtask
        : Entity<string>
    {

        /// <summary>
        /// Initializes a new <see cref="Subtask"/>
        /// </summary>
        protected Subtask()
            : base(string.Empty)
        {

        }

        /// <summary>
        /// Initializes a new <see cref="Subtask"/>
        /// </summary>
        /// <param name="definition">The <see cref="Subtask"/>'s <see cref="HumanTaskDefinition"/></param>
        public Subtask(HumanTaskDefinition definition)
        {
            if(definition == null) throw DomainException.ArgumentNull(nameof(definition));
            this.DefinitionId = definition.Id;
        }

        /// <summary>
        /// Gets the id of the <see cref="Subtask"/>'s <see cref="HumanTaskDefinition"/>
        /// </summary>
        public virtual string DefinitionId { get; protected set; } = null!;

    }

}
