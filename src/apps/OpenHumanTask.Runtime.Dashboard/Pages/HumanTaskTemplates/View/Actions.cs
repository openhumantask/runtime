namespace OpenHumanTask.Runtime.Dashboard.Pages.HumanTaskTemplates.View
{

    /// <summary>
    /// Represents a Flux action used to get a <see cref="HumanTaskTemplate"/> by id
    /// </summary>
    public class GetHumanTaskTemplateById
    {

        /// <summary>
        /// Initializes a new <see cref="GetHumanTaskTemplateById"/>
        /// </summary>
        /// <param name="id">The id of the <see cref="HumanTaskTemplate"/> to get</param>
        public GetHumanTaskTemplateById(string id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets the id of the <see cref="HumanTaskTemplate"/> to get
        /// </summary>
        public string Id { get; }

    }

    /// <summary>
    /// Represents the Flux action used to handle the result of a <see cref="GetHumanTaskTemplateById"/> action
    /// </summary>
    public class HandleGetHumanTaskTemplateByIdResult
    {

        /// <summary>
        /// Initializes a new <see cref="HandleGetHumanTaskTemplateByIdResult"/>
        /// </summary>
        /// <param name="result">The <see cref="GetHumanTaskTemplateById"/>'s result</param>
        public HandleGetHumanTaskTemplateByIdResult(HumanTaskTemplate result)
        {
            this.Result = result;
        }

        /// <summary>
        /// Gets the <see cref="GetHumanTaskTemplateById"/>'s result
        /// </summary>
        public HumanTaskTemplate Result { get; set; }

    }

}
