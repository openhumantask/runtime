/*
 * Copyright © 2022-Present The Open Human Task Runtime Authors
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

namespace OpenHumanTask.Runtime.Application
{
    /// <summary>
    /// Defines extensions for <see cref="ODataQueryOptions"/>
    /// </summary>
    public static class ODataQueryOptionsExtensions
    {

        /// <summary>
        /// Creates a new <see cref="ODataQueryOptions{TEntity}"/> without paging-related clauses (skip and top)
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to query</typeparam>
        /// <param name="queryOptions">The <see cref="ODataQueryOptions{TEntity}"/> to clone</param>
        /// <returns>A new <see cref="ODataQueryOptions{TEntity}"/> without paging-related clauses (skip and top)</returns>
        public static ODataQueryOptions<TEntity> WithoutPaging<TEntity>(this ODataQueryOptions<TEntity> queryOptions)
        {
            var clauses = new List<string>();
            if (!string.IsNullOrWhiteSpace(queryOptions.Apply.RawValue)) clauses.Add($"$apply={queryOptions.Apply.RawValue}");
            if (!string.IsNullOrWhiteSpace(queryOptions.Compute.RawValue)) clauses.Add($"$compute={queryOptions.Compute.RawValue}");
            if (!string.IsNullOrWhiteSpace(queryOptions.Count.RawValue)) clauses.Add($"$count={queryOptions.Count.RawValue}");
            if (!string.IsNullOrWhiteSpace(queryOptions.Filter.RawValue)) clauses.Add($"$filter={queryOptions.Filter.RawValue}");
            if (!string.IsNullOrWhiteSpace(queryOptions.OrderBy.RawValue)) clauses.Add($"$orderby={queryOptions.OrderBy.RawValue}");
            if (!string.IsNullOrWhiteSpace(queryOptions.Search.RawValue)) clauses.Add($"$search={queryOptions.Search.RawValue}");
            var context = new DefaultHttpContext();
            context.Request.QueryString = new($"?{string.Join('&', clauses)}");
            return new(queryOptions.Context, context.Request);
        }

        /// <summary>
        /// Creates a new <see cref="ODataQueryOptions{TEntity}"/> with paging-related clauses (skip and top) only
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to query</typeparam>
        /// <param name="queryOptions">The <see cref="ODataQueryOptions{TEntity}"/> to clone</param>
        /// <returns>A new <see cref="ODataQueryOptions{TEntity}"/> with paging-related clauses (skip and top) only</returns>
        public static ODataQueryOptions<TEntity> WithPagingOnly<TEntity>(this ODataQueryOptions<TEntity> queryOptions)
        {
            var clauses = new List<string>();
            if (!string.IsNullOrWhiteSpace(queryOptions.Skip.RawValue)) clauses.Add($"$skip={queryOptions.Skip.RawValue}");
            if (!string.IsNullOrWhiteSpace(queryOptions.Top.RawValue)) clauses.Add($"$top={queryOptions.Top.RawValue}");
            var context = new DefaultHttpContext();
            context.Request.QueryString = new($"?{string.Join('&', clauses)}");
            return new(queryOptions.Context, context.Request);
        }

    }

}
