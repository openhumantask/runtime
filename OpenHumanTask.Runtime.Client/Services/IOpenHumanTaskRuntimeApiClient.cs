namespace OpenHumanTask.Runtime.Client.Services
{

    /// <summary>
    /// Defines the fundamentals of a service used to interact with an Open Human Task Runtime API
    /// </summary>
    public interface IOpenHumanTaskRuntimeApiClient
    {

        /// <summary>
        /// Gets the API used to manage <see cref="UserTaskDefinition"/>s
        /// </summary>
        ITaskDefinitionApi TaskDefinitions { get; }

    }

}
