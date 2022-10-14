namespace OpenHumanTask.Runtime.Dashboard.Pages.HumanTaskTemplates.View
{

    /// <summary>
    /// Represents the Flux state used to hold information about a specific <see cref="Integration.Models.HumanTaskTemplate"/>
    /// </summary>
    [Feature]
    public record HumanTaskTemplateState
    {

        /// <summary>
        /// Gets/sets the current <see cref="Integration.Models.HumanTaskTemplate"/>
        /// </summary>
        public HumanTaskTemplate? HumanTaskTemplate { get; set; }

    }

}
