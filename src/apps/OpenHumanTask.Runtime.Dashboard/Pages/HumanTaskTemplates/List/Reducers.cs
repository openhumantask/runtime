namespace OpenHumanTask.Runtime.Dashboard.Pages.HumanTaskTemplates.List
{

    /// <summary>
    /// Defines the Flux reducers applying to <see cref="ListState"/>-related actions
    /// </summary>
    [Reducer]
    public static class Reducers
    {

        /// <summary>
        /// Handles the specified <see cref="HandleHumanTaskTemplateQueryResults"/>
        /// </summary>
        /// <param name="state">The current <see cref="ListState"/> to reduce</param>
        /// <param name="action">The <see cref="HandleHumanTaskTemplateQueryResults"/> to handle</param>
        /// <returns>The reduced <see cref="ListState"/></returns>
        public static ListState On(ListState state, HandleHumanTaskTemplateQueryResults action)
        {
            return state with
            {
                Items = action.Results
            };
        }

    }

}
