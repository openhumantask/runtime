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

using System.Net.Http.Json;
using System.Text.Json;

namespace OpenHumanTask.Runtime
{
    /// <summary>
    /// Defines extensions for <see cref="HttpClient"/>s
    /// </summary>
    public static class HttpClientExtensions
    {

        /// <summary>
        /// Posts the specified value as JSON
        /// </summary>
        /// <typeparam name="TValue">The type of value to post</typeparam>
        /// <typeparam name="TResult">The expected type of result</typeparam>
        /// <param name="httpClient">The extended <see cref="HttpClient"/></param>
        /// <param name="uri">The uri to post the specified value at</param>
        /// <param name="value">The value to post as JSON</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>The expected result, if the request succeeded</returns>
        public static async Task<TResult> PostAsJsonAsync<TValue, TResult>(this HttpClient httpClient, string uri, TValue value, CancellationToken cancellationToken = default)
        {
            if(httpClient == null) throw new ArgumentNullException(nameof(httpClient));
            if(string.IsNullOrEmpty(uri)) throw new ArgumentNullException(nameof(uri));
            using var response = await httpClient.PostAsJsonAsync(uri, value, cancellationToken).ConfigureAwait(false);
            var json = await response.Content?.ReadAsStringAsync(cancellationToken);
            response.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<TResult>(json);
        }

    }

}
