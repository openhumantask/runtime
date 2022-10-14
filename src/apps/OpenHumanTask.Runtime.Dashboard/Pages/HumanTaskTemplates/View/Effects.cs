namespace OpenHumanTask.Runtime.Dashboard.Pages.HumanTaskTemplates.View
{
    /// <summary>
    /// Defines <see cref="HumanTaskTemplateState"/>-related Flux effects
    /// </summary>
    [Effect]
    public static class Effects
    {

        /// <summary>
        /// Queries <see cref="HumanTaskTemplate"/>s
        /// </summary>
        /// <param name="action">The Flux action the effect applies to</param>
        /// <param name="context">The current <see cref="IEffectContext"/></param>
        /// <returns>A new awaitable <see cref="Task"/></returns>
        public static async Task On(GetHumanTaskTemplateById action, IEffectContext context)
        {
            try
            {
                var api = context.Services.GetRequiredService<IOpenHumanTaskRuntimeApiClient>();
                var result = await api.HumanTaskTemplates.GetByIdAsync(action.Id);
                context.Dispatcher.Dispatch(new HandleGetHumanTaskTemplateByIdResult(result));
            }
            catch (Exception ex)
            {
                var logger = context.Services.GetRequiredService<ILogger<GetHumanTaskTemplateById>>();
                logger.LogError(ex.ToString());
            }
        }

    }

}
