/*
 * Copyright © 2022-Present The Open Human Task Runtime Authors
 * <p>
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * <p>
 * http://www.apache.org/licenses/LICENSE-2.0
 * <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

namespace OpenHumanTask.Runtime.Plugins.UserManagement.Keycloak.Configuration
{

    /// <summary>
    /// Represents the options used to configure a <see cref="KeycloakClient"/>
    /// </summary>
    public class KeycloakClientOptions
    {

        /// <summary>
        /// Gets/sets the url of the Keycloak API
        /// </summary>
        public virtual string Url { get; set; } = null!;

        /// <summary>
        /// Gets/sets the secret of the client to use when contacting the Keycloak API
        /// </summary>
        public virtual string ClientSecret { get; set; } = null!;

        /// <summary>
        /// Gets/sets the realm to use
        /// </summary>
        public virtual string Realm { get; set; } = null!;

    }


}
