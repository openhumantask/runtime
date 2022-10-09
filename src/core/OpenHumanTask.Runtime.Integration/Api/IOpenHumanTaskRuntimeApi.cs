namespace OpenHumanTask.Runtime.Integration.Api
{
    /// <summary>
    /// Defines the fundamentals of the Open Human Task Runtime API
    /// </summary>
    public interface IOpenHumanTaskRuntimeApi
    {

        /// <summary>
        /// Gets the API used to manage <see cref="HumanTaskDefinition"/>s
        /// </summary>
        ITaskDefinitionApi TaskDefinitions { get; }

    }

}
