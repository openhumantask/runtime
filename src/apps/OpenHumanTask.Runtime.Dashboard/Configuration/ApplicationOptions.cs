namespace OpenHumanTask.Runtime.Dashboard.Configuration
{

    /// <summary>
    /// Represents the options used to configure the Open Human Task Runtime Dashboard application
    /// </summary>
    public class ApplicationOptions
    {

        /// <summary>
        /// Gets/sets the options used to configure the application's OIDC authentication
        /// </summary>
        public virtual OidcAuthenticationOptions Authentication { get; set; } = new();

    }

}
