namespace OpenHumanTask.Runtime.Dashboard.Configuration
{
    /// <summary>
    /// Represents the options used to configure the application's OIDC authentication
    /// </summary>
    public class OidcAuthenticationOptions
    {

        /// <summary>
        /// Gets/sets the <see cref="Uri"/> of the OpenID Connect authority
        /// </summary>
        public virtual string Authority { get; set; } = null!;

        /// <summary>
        /// Gets/sets the id of the dashboard's OpenID Connect client
        /// </summary>
        public virtual string ClientId { get; set; } = null!;

        /// <summary>
        /// Gets/sets a <see cref="List{T}"/> containing the default scopes to request to the OIDC authority when creating tokens
        /// </summary>
        public virtual List<string> DefaultScopes { get; set; } = new();

        /// <summary>
        /// Gets/sets the OIDC response type to use for authorization flows
        /// </summary>
        public virtual string ResponseType { get; set; } = "code";

    }

}
