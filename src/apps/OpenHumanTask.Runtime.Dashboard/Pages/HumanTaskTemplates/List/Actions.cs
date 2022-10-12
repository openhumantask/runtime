using OData.QueryBuilder.Builders;
using OData.QueryBuilder.Conventions.AddressingEntities.Query;

namespace OpenHumanTask.Runtime.Dashboard.Pages.HumanTaskTemplates.List
{

    /// <summary>
    /// Represents the action used to query <see cref="HumanTaskTemplate"/>s
    /// </summary>
    public class QueryHumanTaskTemplates
    {

        /// <summary>
        /// Initializes a new <see cref="QueryHumanTaskTemplates"/>
        /// </summary>
        public QueryHumanTaskTemplates()
        {

        }

        /// <summary>
        /// Initializes a new <see cref="QueryHumanTaskTemplates"/>
        /// </summary>
        /// <param name="searchTerm">The term to search for</param>
        /// <param name="querySetup">An <see cref="Action{T}"/> used to setup the query to perform</param>
        public QueryHumanTaskTemplates(string? searchTerm, Action<IODataQueryCollection<HumanTaskTemplate>> querySetup)
        {
            var builder = new ODataQueryBuilder(new Uri("https://test.com"))
                .For<HumanTaskTemplate>("HumanTaskTemplates")
                .ByList();
            querySetup(builder);
            this.Query = builder.ToUri(UriKind.Absolute).Query;
            if (!string.IsNullOrWhiteSpace(searchTerm)) this.Query = $"$search={searchTerm}&{Query}";
        }

        /// <summary>
        /// Initializes a new <see cref="QueryHumanTaskTemplates"/>
        /// </summary>
        /// <param name="searchTerm">The term to search for</param>
        public QueryHumanTaskTemplates(string searchTerm) : this(searchTerm, _ => { }) { }

        /// <summary>
        /// Initializes a new <see cref="QueryHumanTaskTemplates"/>
        /// </summary>
        /// <param name="querySetup">An <see cref="Action{T}"/> used to setup the query to perform</param>
        public QueryHumanTaskTemplates(Action<IODataQueryCollection<HumanTaskTemplate>> querySetup) : this(null, querySetup) { }

        /// <summary>
        /// Gets the query to perform
        /// </summary>
        public string? Query { get; }

    }

    /// <summary>
    /// Represents the action used to handle the differed results of a <see cref="QueryHumanTaskTemplates"/> action
    /// </summary>
    public class HandleHumanTaskTemplateQueryResults
    {

        /// <summary>
        /// Initializes a new <see cref="HandleHumanTaskTemplateQueryResults"/>
        /// </summary>
        /// <param name="results">A <see cref="List{T}"/> that contains the differed results of the <see cref="QueryHumanTaskTemplates"/> action</param>
        public HandleHumanTaskTemplateQueryResults(List<HumanTaskTemplate> results)
        {
            this.Results = results;
        }

        /// <summary>
        /// Gets a <see cref="List{T}"/> that contains the differed results of the <see cref="QueryHumanTaskTemplates"/> action
        /// </summary>
        public List<HumanTaskTemplate> Results { get; }

    }

}
