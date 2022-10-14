namespace OpenHumanTask.Runtime.Dashboard.Pages.HumanTaskTemplates.View
{
    /// <summary>
    /// Defines <see cref="HumanTaskTemplateState"/>-related Flux reducers
    /// </summary>
    [Reducer]
    public static class Reducers
    {

        /// <summary>
        /// Handles the specified <see cref="HandleGetHumanTaskTemplateByIdResult"/>
        /// </summary>
        /// <param name="state">The current <see cref="HumanTaskTemplateState"/> to reduce</param>
        /// <param name="action">The <see cref="HandleGetHumanTaskTemplateByIdResult"/> to handle</param>
        /// <returns>The reduced <see cref="HumanTaskTemplateState"/></returns>
        public static HumanTaskTemplateState On(HumanTaskTemplateState state, HandleGetHumanTaskTemplateByIdResult action)
        {
            return state with
            {
                HumanTaskTemplate = action.Result
            };
        }

    }

}
