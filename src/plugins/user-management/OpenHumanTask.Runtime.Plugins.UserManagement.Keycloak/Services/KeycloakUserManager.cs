// Copyright © 2022-Present The Open Human Task Specification Authors. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License")
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using IdentityModel;
using Keycloak.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenHumanTask.Runtime.Infrastructure.Services;
using OpenHumanTask.Runtime.Plugins.UserManagement.Keycloak.Configuration;
using System.Security.Claims;

namespace OpenHumanTask.Runtime.Plugins.UserManagement.Keycloak
{

    /// <summary>
    /// Represents the Keycloak implementation of the <see cref="IUserManager"/> interface
    /// </summary>
    public class KeycloakUserManager
        : IUserManager
    {

        /// <summary>
        /// Initializes a new <see cref="KeycloakUserManager"/>
        /// </summary>
        /// <param name="logger">The service used to perform logging</param>
        /// <param name="options">The service used to access the current <see cref="KeycloakClientOptions"/></param>
        /// <param name="keycloakClient">The service used to interact with the Keycloak API</param>
        public KeycloakUserManager(ILogger<KeycloakUserManager> logger, IOptions<KeycloakClientOptions> options, KeycloakClient keycloakClient)
        {
            this.Logger = logger;
            this.Options = options.Value;
            this.KeycloakClient = keycloakClient;
        }

        /// <summary>
        /// Gets the service used to perform logging
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Gets the current <see cref="KeycloakClientOptions"/>
        /// </summary>
        protected KeycloakClientOptions Options { get; }

        /// <summary>
        /// Gets the service used to interact with the Keycloak API
        /// </summary>
        protected KeycloakClient KeycloakClient { get; }

        /// <inheritdoc/>
        public virtual async Task<List<ClaimsIdentity>> ListUsersAsync(CancellationToken cancellationToken = default)
        {
            var users = new List<ClaimsIdentity>();
            foreach (var user in await this.KeycloakClient.GetUsersAsync(this.Options.Realm))
            {
                var claims = new List<Claim>()
                {
                    new(JwtClaimTypes.Subject, user.Id),
                    new(JwtClaimTypes.Name, user.UserName),
                    new(JwtClaimTypes.Email, user.Email)
                };
                if (user.EmailVerified.HasValue) claims.Add(new(JwtClaimTypes.EmailVerified, user.EmailVerified.ToString()!));
                if (!string.IsNullOrWhiteSpace(user.FirstName)) claims.Add(new(JwtClaimTypes.GivenName, user.FirstName));
                if (!string.IsNullOrWhiteSpace(user.LastName)) claims.Add(new(JwtClaimTypes.FamilyName, user.LastName));
                if (user.Attributes != null)
                {
                    foreach (var attribute in user.Attributes)
                    {
                        claims.Add(new(attribute.Key, string.Join(", ", attribute.Value)));
                    }
                }
                users.Add(new(claims, "Bearer", JwtClaimTypes.Name, JwtClaimTypes.Role));
            }
            return users;
        }

    }

}
