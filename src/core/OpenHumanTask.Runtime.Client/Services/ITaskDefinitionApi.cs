namespace OpenHumanTask.Runtime.Client.Services
{

    /// <summary>
    /// Defines the fundamentals of the API used to manage <see cref="HumanTaskDefinition"/>s
    /// </summary>
    public interface ITaskDefinitionApi
    {

        /// <summary>
        /// Gets the referenced <see cref="HumanTaskDefinition"/>
        /// </summary>
        /// <param name="reference">The <see cref="TaskDefinitionReference"/> used to reference the <see cref="HumanTaskDefinition"/> to get</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>The referenced <see cref="HumanTaskDefinition"/></returns>
        Task<HumanTaskDefinition> GetByReferenceAsync(TaskDefinitionReference reference, CancellationToken cancellationToken = default);

        Task<List<HumanTaskDefinition>> GetAsync(string? query = null, CancellationToken cancellationToken = default);

        Task<HumanTaskDefinition> CreateAsync(CreateTaskDefinitionCommand command, CancellationToken cancellationToken = default);

        Task DeleteAsync(string id, CancellationToken cancellationToken = default);

    }

}
