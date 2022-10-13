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

using OpenHumanTask.Runtime.Integration.Commands.HumanTaskTemplates;
using System.Net.Http.Json;

namespace OpenHumanTask.Runtime.Integration.Api
{
    /// <summary>
    /// Represents the default implementation of the <see cref="IHumanTaskTemplateApi"/> inteface
    /// </summary>
    public class HumanTaskTemplateApi
        : IHumanTaskTemplateApi
    {

        /// <summary>
        /// Gets the path prefix for the human task template API
        /// </summary>
        public const string PathPrefix = OpenHumanTaskRuntimeApiClient.PathPrefix + "/tasks/templates";

        /// <summary>
        /// Initializes a new <see cref="HumanTaskTemplateApi"/>
        /// </summary>
        /// <param name="httpClientFactory">The service used to creater <see cref="System.Net.Http.HttpClient"/>s</param>
        public HumanTaskTemplateApi(IHttpClientFactory httpClientFactory)
        {
            this.HttpClient = httpClientFactory.CreateClient(nameof(IHumanTaskTemplateApi));
        }

        /// <summary>
        /// Gets the <see cref="System.Net.Http.HttpClient"/> used to perform requests
        /// </summary>
        protected virtual HttpClient HttpClient { get; }

        /// <inheritdoc/>
        public virtual async Task<HumanTaskTemplate> CreateAsync(CreateHumanTaskTemplateCommand command, CancellationToken cancellationToken = default)
        {
            if(command == null) throw new ArgumentNullException(nameof(command));
            var uri = PathPrefix;
            return await this.HttpClient.PostAsJsonAsync<CreateHumanTaskTemplateCommand, HumanTaskTemplate>(uri, command, cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<HumanTaskTemplate> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));
            var uri = PathPrefix + $"/{id}";
            return await this.HttpClient.GetFromJsonAsync<HumanTaskTemplate>(uri, cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<List<HumanTaskTemplate>> GetAsync(string query = null, CancellationToken cancellationToken = default)
        {
            var uri = PathPrefix;
            if (!string.IsNullOrEmpty(query)) uri += $"?{query}";
            return await this.HttpClient.GetFromJsonAsync<List<HumanTaskTemplate>>(uri, cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));
            var uri = PathPrefix + $"/{id}";
            await this.HttpClient.DeleteAsync(uri, cancellationToken);
        }

    }

}
