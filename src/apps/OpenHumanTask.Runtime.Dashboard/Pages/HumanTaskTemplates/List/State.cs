namespace OpenHumanTask.Runtime.Dashboard.Pages.HumanTaskTemplates.List
{

    /// <summary>
    /// Represents the state of the <see cref="View">list view</see>
    /// </summary>
    [Feature]
    public record ListState
    {

        /// <summary>
        /// Gets/sets a <see cref="List{T}"/> containing all available <see cref="HumanTaskTemplate"/>s in the current context (sort, filter, search, etc.)
        /// </summary>
        public List<HumanTaskTemplate>? Items { get; set; }

    }

}
