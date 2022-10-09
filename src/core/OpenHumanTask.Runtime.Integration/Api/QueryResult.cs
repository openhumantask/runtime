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
