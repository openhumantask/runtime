namespace OpenHumanTask.Runtime.Application.Commands.TaskDefinitions
{

    /// <summary>
    /// Represents the <see cref="ICommand"/> used to create a new <see cref="HumanTaskDefinition"/>
    /// </summary>
    [DataTransferObjectType(typeof(Integration.Commands.TaskDefinitions.CreateTaskDefinitionCommand))]
    public class CreateTaskDefinitionCommand
        : Command<HumanTaskDefinition>
    {

        /// <summary>
        /// Initializes a new <see cref="CreateTaskDefinitionCommand"/>
        /// </summary>
        /// <param name="definition">The <see cref="HumanTaskDefinition"/> of the <see cref="HumanTask"/> to create</param>
        public CreateTaskDefinitionCommand(HumanTaskDefinition definition)
        {
            this.Definition = definition;
        }

        /// <summary>
        /// Gets the <see cref="HumanTaskDefinition"/> of the <see cref="HumanTask"/> to create
        /// </summary>
        public virtual HumanTaskDefinition Definition { get; protected set; }

    }

}
