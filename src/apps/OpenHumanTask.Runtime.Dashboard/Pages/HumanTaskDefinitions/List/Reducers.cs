namespace OpenHumanTask.Runtime.Dashboard.Pages.HumanTaskDefinitions.List
{
    /// <summary>
    /// Defines the Flux reducers applying to <see cref="ListState"/>-related actions
    /// </summary>
    [Reducer]
    public static class Reducers
    {

        /// <summary>
        /// Handles the specified <see cref="HandleHumanTaskDefinitionQueryResults"/>
        /// </summary>
        /// <param name="state">The current <see cref="ListState"/> to reduce</param>
        /// <param name="action">The <see cref="HandleHumanTaskDefinitionQueryResults"/> to handle</param>
        /// <returns>The reduced <see cref="ListState"/></returns>
        public static ListState On(ListState state, HandleHumanTaskDefinitionQueryResults action)
        {
            return state with
            {
                Items = action.Results
            };
        }

    }

}
