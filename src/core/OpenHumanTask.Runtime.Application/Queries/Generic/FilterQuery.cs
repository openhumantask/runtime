﻿/*
 * Copyright © 2022-Present The Synapse Authors
 * <p>
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * <p>
 * http://www.apache.org/licenses/LICENSE-2.0
 * <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Query.Expressions;
using Microsoft.OData.Edm;
using OpenHumanTask.Runtime.Application.Queries;
using System.Linq.Expressions;

namespace OpenHumanTask.Runtime.Application.Queries.Generic
{

    /// <summary>
    /// Represents the <see cref="IQuery"/> used to filter the entities of the specified type
    /// </summary>
    /// <typeparam name="TEntity">The type of <see cref="IEntity"/>The type of entities to query</typeparam>
    public class FilterQuery<TEntity>
        : Query<List<TEntity>>
        where TEntity : class, IIdentifiable
    {

        /// <summary>
        /// Initializes a new <see cref="FilterQuery{TEntity}"/>
        /// </summary>
        protected FilterQuery()
        {
            this.Options = null!;
        }

        /// <summary>
        /// Initializes a new <see cref="FilterQuery{TEntity}"/>
        /// </summary>
        /// <param name="options">The <see cref="ODataQueryOptions"/> used to filter the entities</param>
        public FilterQuery(ODataQueryOptions<TEntity> options)
        {
            this.Options = options;
        }

        /// <summary>
        /// Gets the <see cref="ODataQueryOptions"/> used to filter the entities
        /// </summary>
        public ODataQueryOptions<TEntity> Options { get; protected set; }

    }

    /// <summary>
    /// Represents the service used to handle <see cref="FilterQuery{TEntity}"/> instances
    /// </summary>
    /// <typeparam name="TEntity">The type of entity to filter</typeparam>
    public class V1FilterQueryHandler<TEntity>
        : QueryHandlerBase<TEntity>,
        IQueryHandler<FilterQuery<TEntity>, List<TEntity>>
        where TEntity : class, IIdentifiable
    {

        /// <inheritdoc/>
        public V1FilterQueryHandler(ILoggerFactory loggerFactory, IMediator mediator, IMapper mapper, IRepository<TEntity> repository, ISearchBinder searchBinder, IEdmModel edmModel) 
            : base(loggerFactory, mediator, mapper, repository)
        {
            this.SearchBinder = searchBinder;
            this.EdmModel = edmModel;
        }

        /// <summary>
        /// Gets the service used to bind ODATA searches
        /// </summary>
        protected ISearchBinder SearchBinder { get; }

        /// <summary>
        /// Gets the current <see cref="IEdmModel"/>
        /// </summary>
        protected IEdmModel EdmModel { get; }

        /// <inheritdoc/>
        public virtual async Task<IOperationResult<List<TEntity>>> HandleAsync(FilterQuery<TEntity> query, CancellationToken cancellationToken = default)
        {
            var toFilter = (await this.Repository.ToListAsync(cancellationToken)).AsQueryable();
            if (query.Options?.Search != null)
            {
                var searchExpression = (Expression<Func<TEntity, bool>>)this.SearchBinder.BindSearch(query.Options.Search.SearchClause, new(this.EdmModel, new(), typeof(TEntity)));
                toFilter = toFilter.Where(searchExpression);
            }
            var filtered = query.Options?.ApplyTo(toFilter);
            if (filtered == null)
                filtered = toFilter;
            return this.Ok(filtered.OfType<TEntity>().ToList());
        }

    }

}
