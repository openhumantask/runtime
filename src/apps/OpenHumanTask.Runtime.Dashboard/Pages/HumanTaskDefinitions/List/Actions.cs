using OData.QueryBuilder.Builders;
using OData.QueryBuilder.Conventions.AddressingEntities.Query;

namespace OpenHumanTask.Runtime.Dashboard.Pages.HumanTaskDefinitions.List
{

    /// <summary>
    /// Represents the action used to query <see cref="HumanTaskDefinition"/>s
    /// </summary>
    public class QueryHumanTaskDefinitions
    {

        /// <summary>
        /// Initializes a new <see cref="QueryHumanTaskDefinitions"/>
        /// </summary>
        public QueryHumanTaskDefinitions()
        {

        }

        /// <summary>
        /// Initializes a new <see cref="QueryHumanTaskDefinitions"/>
        /// </summary>
        /// <param name="searchTerm">The term to search for</param>
        /// <param name="querySetup">An <see cref="Action{T}"/> used to setup the query to perform</param>
        public QueryHumanTaskDefinitions(string? searchTerm, Action<IODataQueryCollection<HumanTaskDefinition>> querySetup)
        {
            var builder = new ODataQueryBuilder(new Uri("https://test.com"))
                .For<HumanTaskDefinition>("V1FunctionDefinitionCollections")
                .ByList();
            querySetup(builder);
            Query = builder.ToUri(UriKind.Absolute).Query;
            if (!string.IsNullOrWhiteSpace(searchTerm))
                Query = $"$search={searchTerm}&{Query}";
        }

        /// <summary>
        /// Initializes a new <see cref="QueryHumanTaskDefinitions"/>
        /// </summary>
        /// <param name="searchTerm">The term to search for</param>
        public QueryHumanTaskDefinitions(string searchTerm)
            : this(searchTerm, _ => { })
        {

        }

        /// <summary>
        /// Initializes a new <see cref="QueryHumanTaskDefinitions"/>
        /// </summary>
        /// <param name="querySetup">An <see cref="Action{T}"/> used to setup the query to perform</param>
        public QueryHumanTaskDefinitions(Action<IODataQueryCollection<HumanTaskDefinition>> querySetup)
            : this(null, querySetup)
        {

        }

        /// <summary>
        /// Gets the query to perform
        /// </summary>
        public string? Query { get; }

    }

    /// <summary>
    /// Represents the action used to handle the differed results of a <see cref="QueryHumanTaskDefinitions"/> action
    /// </summary>
    public class HandleHumanTaskDefinitionQueryResults
    {

        /// <summary>
        /// Initializes a new <see cref="HandleHumanTaskDefinitionQueryResults"/>
        /// </summary>
        /// <param name="results">A <see cref="List{T}"/> that contains the differed results of the <see cref="QueryHumanTaskDefinitions"/> action</param>
        public HandleHumanTaskDefinitionQueryResults(List<HumanTaskDefinition> results)
        {
            this.Results = results;
        }

        /// <summary>
        /// Gets a <see cref="List{T}"/> that contains the differed results of the <see cref="QueryHumanTaskDefinitions"/> action
        /// </summary>
        public List<HumanTaskDefinition> Results { get; }

    }

}
