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

namespace OpenHumanTask.Runtime.Application.Configuration
{
    /// <summary>
    /// Represents the options used to configure the application's JWT Bearer authentication
    /// </summary>
    public class JwtBearerAuthenticationOptions
    {

        /// <summary>
        /// Gets/sets the JWT Bearer authority
        /// </summary>
        public virtual string Authority { get; set; } = null!;

        /// <summary>
        /// Gets/sets the required audience for tokens consumed by the application
        /// </summary>
        public virtual string? Audience { get; set; } = "api";

        /// <summary>
        /// Gets/sets the base 64 encoded signing key of the issuer of tokens consumed by the application
        /// </summary>
        public virtual string IssuerSigningKey { get; set; } = null!;

    }

}
