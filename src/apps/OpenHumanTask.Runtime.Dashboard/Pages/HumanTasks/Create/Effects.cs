namespace OpenHumanTask.Runtime.Dashboard.Pages.HumanTasks.Create;

/// <summary>
/// Represents <see cref="HumanTaskState"/>-related Flux effects
/// </summary>
[Effect]
public static class Effects
{

    /// <summary>
    /// Creates a new <see cref="HumanTask"/>
    /// </summary>
    /// <param name="action">The Flux action the effect applies to</param>
    /// <param name="context">The current <see cref="IEffectContext"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    public static async Task On(CreateHumanTask action, IEffectContext context)
    {
        try
        {
            var api = context.Services.GetRequiredService<IOpenHumanTaskRuntimeApiClient>();
            var result = await api.HumanTasks.CreateAsync(action.Command);
            context.Dispatcher.Dispatch(new HandleCreateHumanTaskResult(result));
        }
        catch (Exception ex)
        {
            var logger = context.Services.GetRequiredService<ILogger<CreateHumanTask>>();
            logger.LogError(ex.ToString());
        }
    }

}