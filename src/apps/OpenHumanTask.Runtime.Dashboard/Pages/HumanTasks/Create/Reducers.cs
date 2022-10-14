namespace OpenHumanTask.Runtime.Dashboard.Pages.HumanTasks.Create;

/// <summary>
/// Represents <see cref="HumanTaskState"/>-related Flux reducers
/// </summary>
[Reducer]
public static class Reducers
{

    /// <summary>
    /// Handles the specified <see cref="InitializeState"/>
    /// </summary>
    /// <param name="state">The current <see cref="HumanTaskState"/> to reduce</param>
    /// <param name="action">The <see cref="InitializeState"/> to handle</param>
    /// <returns>The reduced <see cref="HumanTaskState"/></returns>
    public static HumanTaskState On(HumanTaskState state, InitializeState action)
    {
        return state with
        {
            Command = new() { TemplateReference = action.TemplateId! }
        };
    }

    /// <summary>
    /// Handles the specified <see cref="PatchCommand"/>
    /// </summary>
    /// <param name="state">The current <see cref="HumanTaskState"/> to reduce</param>
    /// <param name="action">The <see cref="PatchCommand"/> to handle</param>
    /// <returns>The reduced <see cref="HumanTaskState"/></returns>
    public static HumanTaskState On(HumanTaskState state, PatchCommand action)
    {
        var command = state.Command;
        if (command == null) return state;
        action.Patch(command);
        return state with
        {
            Command = command
        };
    }

    /// <summary>
    /// Handles the specified <see cref="HandleCreateHumanTaskResult"/>
    /// </summary>
    /// <param name="state">The current <see cref="HumanTaskState"/> to reduce</param>
    /// <param name="action">The <see cref="HandleCreateHumanTaskResult"/> to handle</param>
    /// <returns>The reduced <see cref="HumanTaskState"/></returns>
    public static HumanTaskState On(HumanTaskState state, HandleCreateHumanTaskResult action)
    {
        return state with
        {
            HumanTask = action.HumanTask
        };
    }

}
