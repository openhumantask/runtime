using Microsoft.AspNetCore.OData.Query;
using System.Net;

namespace OpenHumanTask.Runtime.Api.Controllers
{

    /// <summary>
    /// Represents the API controller used to manage human task templates
    /// </summary>
    [Route("api/v1/tasks/templates")]
    public class HumanTaskTemplatesController
        : ApiController
    {

        /// <inheritdoc/>
        public HumanTaskTemplatesController(ILoggerFactory loggerFactory, IMediator mediator, IMapper mapper) : base(loggerFactory, mediator, mapper) { }

        /// <summary>
        /// Creates a new human task template
        /// </summary>
        /// <param name="command">An object that represents the command to execute</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new <see cref="IActionResult"/></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Integration.Models.HumanTaskTemplate), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public virtual async Task<IActionResult> Create([FromBody] Integration.Commands.HumanTaskTemplates.CreateHumanTaskTemplateCommand command, CancellationToken cancellationToken)
        {
            if (!this.ModelState.IsValid) return this.BadRequest(this.ModelState);
            return this.Process(await this.Mediator.ExecuteAsync(this.Mapper.Map<Application.Commands.HumanTaskTemplates.CreateHumanTaskTemplateCommand>(command), cancellationToken), (int)HttpStatusCode.Created);
        }

        /// <summary>
        /// Gets the human task template with the specified id
        /// </summary>
        /// <param name="id">The id of the human task template to get</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new <see cref="IActionResult"/></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Integration.Models.HumanTaskTemplate), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public virtual async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
        {
            return this.Process(await this.Mediator.ExecuteAsync(new Application.Queries.HumanTaskTemplates.GetHumanTaskTemplateByIdQuery(id!), cancellationToken));
        }

        /// <summary>
        /// Queries available human task templates
        /// </summary>
        /// <param name="queryOptions">The options of the ODATA query to perform</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new <see cref="IActionResult"/></returns>
        [HttpGet, EnableQuery]
        [ProducesResponseType(typeof(IEnumerable<Integration.Models.HumanTaskTemplate>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<IActionResult> Get(ODataQueryOptions<Integration.Models.HumanTaskTemplate> queryOptions, CancellationToken cancellationToken)
        {
            return this.Process(await this.Mediator.ExecuteAsync(new Application.Queries.Generic.FilterQuery<Integration.Models.HumanTaskTemplate>(queryOptions), cancellationToken));
        }

        /// <summary>
        /// Deletes the human task template with the specified id
        /// </summary>
        /// <param name="id">The id of the human task template to delete</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new <see cref="IActionResult"/></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Integration.Models.HumanTaskTemplate), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            return this.Process(await this.Mediator.ExecuteAsync(new Application.Commands.Generic.DeleteCommand<Domain.Models.HumanTaskTemplate, string>(id), cancellationToken));
        }

    }

}
