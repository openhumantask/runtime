namespace OpenHumanTask.Runtime.Dashboard.Pages.HumanTaskTemplates.List
{

    /// <summary>
    /// Defines the Flux effects applying to <see cref="ListState"/>-related actions
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
        public static async Task On(QueryHumanTaskTemplates action, IEffectContext context)
        {
            var api = context.Services.GetRequiredService<IOpenHumanTaskRuntimeApiClient>();
            var results = await api.HumanTaskTemplates.GetAsync(action.Query);
            context.Dispatcher.Dispatch(new HandleHumanTaskTemplateQueryResults(results));
        }

    }

}
