using OpenHumanTask.Runtime.Integration.Commands.HumanTasks;

namespace OpenHumanTask.Runtime.Dashboard.Pages.HumanTasks.Create
{

    /// <summary>
    /// Represents the Flux action used to initialize the <see cref="HumanTaskState"/>
    /// </summary>
    public class InitializeState
    {

        /// <summary>
        /// Initializes a new <see cref="InitializeState"/>
        /// </summary>
        /// <param name="templateId">The id of the <see cref="HumanTaskTemplate"/> to create a new <see cref="HumanTask"/> for</param>
        public InitializeState(string? templateId)
        {
            this.TemplateId = templateId;
        }

        /// <summary>
        /// Gets the id of the <see cref="HumanTaskTemplate"/> to create a new <see cref="HumanTask"/> for
        /// </summary>
        public string? TemplateId { get; }

    }

    /// <summary>
    /// Represents the Flux action used to patch the <see cref="CreateHumanTaskCommand"/>
    /// </summary>
    public class PatchCommand
    {

        /// <summary>
        /// Initializes a new <see cref="PatchCommand"/>
        /// </summary>
        /// <param name="patch">An <see cref="Action{T}"/> that represents the patch to apply</param>
        public PatchCommand(Action<CreateHumanTaskCommand> patch)
        {
            this.Patch = patch;
        }

        /// <summary>
        /// Gets the <see cref="Action{T}"/> that represents the patch to apply
        /// </summary>
        public Action<CreateHumanTaskCommand> Patch { get; }

    }

    /// <summary>
    /// Represents the Flux action used to create a new <see cref="HumanTask"/>
    /// </summary>
    public class CreateHumanTask
    {

        /// <summary>
        /// Initializes a new <see cref="CreateHumanTask"/>
        /// </summary>
        /// <param name="command">The command that describes the <see cref="HumanTask"/> to create</param>
        public CreateHumanTask(CreateHumanTaskCommand command)
        {
            this.Command = command;
        }

        /// <summary>
        /// Gets the command that describes the <see cref="HumanTask"/> to create
        /// </summary>
        public CreateHumanTaskCommand Command { get; }

    }

    /// <summary>
    /// Represents the Flux action used to handle the result of a <see cref="CreateHumanTask"/> action
    /// </summary>
    public class HandleCreateHumanTaskResult
    {

        /// <summary>
        /// Initializes a new <see cref="HandleCreateHumanTaskResult"/>
        /// </summary>
        /// <param name="humanTask">The newly created <see cref="Integration.Models.HumanTask"/></param>
        public HandleCreateHumanTaskResult(HumanTask humanTask)
        {
            this.HumanTask = humanTask;
        }

        /// <summary>
        /// Gets the newly created <see cref="Integration.Models.HumanTask"/>
        /// </summary>
        public HumanTask HumanTask { get; }

    }

}
