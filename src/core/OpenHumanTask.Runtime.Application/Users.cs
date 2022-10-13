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
using System.Security.Claims;

namespace OpenHumanTask.Runtime.Application
{

    /// <summary>
    /// Exposes default users and defines helper methods to handle users
    /// </summary>
    internal static class Users
    {

        /// <summary>
        /// Gets the 'System' user
        /// </summary>
        internal static readonly ClaimsPrincipal System;

        static Users()
        {
            var claims = new Claim[] 
            { 
                new(JwtClaimTypes.Name, "System"),
                new(JwtClaimTypes.Subject, "ohtr-system"),
                new(JwtClaimTypes.PreferredUserName, "System")
            };
            var identity = new ClaimsIdentity(claims, "Bearer", JwtClaimTypes.Name, JwtClaimTypes.Role);
            System = new(identity);
        }

    }

}
