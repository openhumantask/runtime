namespace OpenHumanTask.Runtime.Dashboard.Pages.HumanTaskDefinitions.List
{

    /// <summary>
    /// Represents the state of the <see cref="View">list view</see>
    /// </summary>
    [Feature]
    public record ListState
    {

        /// <summary>
        /// Gets/sets a <see cref="List{T}"/> containing all available <see cref="HumanTaskDefinition"/>s in the current context (sort, filter, search, etc.)
        /// </summary>
        public List<HumanTaskDefinition>? Items { get; set; }

    }

}
