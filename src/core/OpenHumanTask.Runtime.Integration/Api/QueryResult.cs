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
    /// Describes the results of a query
    /// </summary>
    public class QueryResult
    {

        /// <summary>
        /// Gets the index of the current page
        /// </summary>
        [DataMember(Name = "page")]
        [JsonPropertyName("page")]
        public int Page { get; set; }

        /// <summary>
        /// Gets the total amount of pages
        /// </summary>
        [DataMember(Name = "totalPages")]
        [JsonPropertyName("totalPages")]
        public int TotalPages { get; set; }

        /// <summary>
        /// Gets the maximum amount of results per page
        /// </summary>
        [DataMember(Name = "resultsPerPage")]
        [JsonPropertyName("resultsPerPage")]
        public short ResultsPerPage { get; set; }

        /// <summary>
        /// Gets the current page's results count
        /// </summary>
        [DataMember(Name = "resultsCount")]
        [JsonPropertyName("resultsCount")]
        public short ResultsCount { get; set; }

        /// <summary>
        /// Gets the total result count
        /// </summary>
        [DataMember(Name = "totalResultsCount")]
        [JsonPropertyName("totalResultsCount")]
        public long TotalResultsCount { get; set; }

        /// <summary>
        /// Gets the total count
        /// </summary>
        [DataMember(Name = "totalCount")]
        [JsonPropertyName("totalCount")]
        public long TotalCount { get; set; }

        /// <summary>
        /// Gets a <see cref="List{T}"/> containing the query results
        /// </summary>
        [DataMember(Name = "results")]
        [JsonPropertyName("results")]
        public List<object> Results { get; set; } = null!;

    }

    /// <summary>
    /// Describes the results of a query
    /// </summary>
    /// <typeparam name="TResult">The expect type of the query results</typeparam>
    public class QueryResult<TResult>
        : QueryResult
    {

        /// <summary>
        /// Gets a <see cref="List{T}"/> containing the query results
        /// </summary>
        [DataMember(Name = "results")]
        [JsonPropertyName("results")]
        public new List<TResult> Results { get; set; } = null!;

    }

}
