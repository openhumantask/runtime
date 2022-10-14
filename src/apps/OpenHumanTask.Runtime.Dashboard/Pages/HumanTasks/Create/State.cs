using OpenHumanTask.Runtime.Integration.Commands.HumanTasks;

namespace OpenHumanTask.Runtime.Dashboard.Pages.HumanTasks.Create;

/// <summary>
/// Represents the Flux state that holds information about a new <see cref="Integration.Models.HumanTask"/>
/// </summary>
[Feature]
public record HumanTaskState
{

    /// <summary>
    /// Gets/sets the command used to create a new <see cref="Integration.Models.HumanTask"/>
    /// </summary>
    public CreateHumanTaskCommand? Command { get; set; }

    /// <summary>
    /// Gets/sets the newly created <see cref="Integration.Models.HumanTask"/>
    /// </summary>
    public HumanTask? HumanTask { get; set; }

}
