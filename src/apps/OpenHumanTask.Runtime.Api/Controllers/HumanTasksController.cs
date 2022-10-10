using Microsoft.AspNetCore.OData.Query;
using System.Net;

namespace OpenHumanTask.Runtime.Api.Controllers
{

    /// <summary>
    /// Represents the API controller used to manage human tasks
    /// </summary>
    [Route("api/v1/tasks/templates")]
    public class HumanTasksController
        : ApiController
    {

        /// <inheritdoc/>
        public HumanTasksController(ILoggerFactory loggerFactory, IMediator mediator, IMapper mapper) : base(loggerFactory, mediator, mapper) { }

        /// <summary>
        /// Creates a new human task
        /// </summary>
        /// <param name="command">An object that represents the command to execute</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new <see cref="IActionResult"/></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Integration.Models.HumanTask), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public virtual async Task<IActionResult> Create([FromBody] Integration.Commands.HumanTaskTemplates.CreateHumanTaskCommand command, CancellationToken cancellationToken)
        {
            if (!this.ModelState.IsValid) return this.BadRequest(this.ModelState);
            return this.Process(await this.Mediator.ExecuteAsync(this.Mapper.Map<Application.Commands.HumanTaskTemplates.CreateHumanTaskCommand>(command), cancellationToken));
        }

        /// <summary>
        /// Gets the human task with the specified id
        /// </summary>
        /// <param name="id">The id of the human task to get</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new <see cref="IActionResult"/></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Integration.Models.HumanTask), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public virtual async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
        {
            return this.Process(await this.Mediator.ExecuteAsync(new Application.Queries.Generic.FindByKeyQuery<Integration.Models.HumanTask>(id!), cancellationToken));
        }

        /// <summary>
        /// Queries available human tasks
        /// </summary>
        /// <param name="queryOptions">The options of the ODATA query to perform</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new <see cref="IActionResult"/></returns>
        [HttpGet, EnableQuery]
        [ProducesResponseType(typeof(IEnumerable<Integration.Models.HumanTask>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<IActionResult> Get(ODataQueryOptions<Integration.Models.HumanTask> queryOptions, CancellationToken cancellationToken)
        {
            return this.Process(await this.Mediator.ExecuteAsync(new Application.Queries.Generic.FilterQuery<Integration.Models.HumanTask>(queryOptions), cancellationToken));
        }

    }

}
