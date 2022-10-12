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

namespace OpenHumanTask.Runtime.Integration.Api
{
    /// <summary>
    /// Represents the default implementation of the <see cref="IOpenHumanTaskRuntimeApi"/>
    /// </summary>
    public class OpenHumanTaskRuntimeApiClient
        : IOpenHumanTaskRuntimeApiClient
    {

        /// <summary>
        /// Gets the path prefix for all APIs
        /// </summary>
        public const string PathPrefix = "/api/v1";

        /// <summary>
        /// Initializes a new <see cref="OpenHumanTaskRuntimeApiClient"/>
        /// </summary>
        /// <param name="humanTaskTemplates">The API used to manage <see cref="HumanTaskTemplate"/>s</param>
        /// <param name="humanTasks">The API used to manage <see cref="HumanTask"/>s</param>
        public OpenHumanTaskRuntimeApiClient(IHumanTaskTemplateApi humanTaskTemplates, IHumanTaskApi humanTasks)
        {
            this.HumanTaskTemplates = humanTaskTemplates;
            this.HumanTasks = humanTasks;
        }

        /// <inheritdoc/>
        public IHumanTaskTemplateApi HumanTaskTemplates { get; }

        /// <inheritdoc/>
        public IHumanTaskApi HumanTasks { get; }

    }

}
