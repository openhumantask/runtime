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

using System.Security.Claims;

namespace OpenHumanTask.Runtime.Application.Services
{

    /// <summary>
    /// Defines the fundamentals of a service used to manage users
    /// </summary>
    public interface IUserManager
    {

        /// <summary>
        /// Lists all available users
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new <see cref="List{T}"/> containing all available users</returns>
        Task<List<ClaimsIdentity>> ListUsersAsync(CancellationToken cancellationToken = default);

    }

}
