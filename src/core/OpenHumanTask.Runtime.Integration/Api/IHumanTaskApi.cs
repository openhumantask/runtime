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

using OpenHumanTask.Runtime.Integration.Commands.HumanTasks;

namespace OpenHumanTask.Runtime.Integration.Api
{
    /// <summary>
    /// Defines the fundamentals of the API used to manage <see cref="HumanTask"/>s
    /// </summary>
    public interface IHumanTaskApi
    {

        /// <summary>
        /// Gets the referenced <see cref="HumanTask"/>
        /// </summary>
        /// <param name="id">The id of the <see cref="HumanTask"/> to get</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>The <see cref="HumanTask"/> with the specified id</returns>
        Task<HumanTask> GetByIdAsync(string id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the <see cref="HumanTask"/>s matching the specified ODATA query
        /// </summary>
        /// <param name="query">The ODATA query to perform</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new object that describes the result of the query</returns>
        Task<List<HumanTask>> GetAsync(string? query = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a new <see cref="HumanTask"/>
        /// </summary>
        /// <param name="command">An object that describes the command to execute</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>The newly created <see cref="HumanTask"/></returns>
        Task<HumanTask> CreateAsync(CreateHumanTaskCommand command, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes the referenced <see cref="HumanTask"/>
        /// </summary>
        /// <param name="id">The id of the <see cref="HumanTask"/> to delete</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new awaitable <see cref="Task"/></returns>
        Task DeleteAsync(string id, CancellationToken cancellationToken = default);

    }

}
