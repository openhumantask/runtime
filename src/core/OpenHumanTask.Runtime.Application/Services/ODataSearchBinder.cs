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

using Microsoft.AspNetCore.OData.Query.Expressions;
using Microsoft.OData.UriParser;
using System.Linq.Expressions;
using System.Reflection;

namespace OpenHumanTask.Runtime.Application.Services
{
    /// <summary>
    /// Represents the default <see cref="ISearchBinder"/> implementation
    /// </summary>
    public partial class ODataSearchBinder
        : QueryBinder, ISearchBinder
    {

        private static readonly Dictionary<BinaryOperatorKind, ExpressionType> BinaryOperatorMapping = new Dictionary<BinaryOperatorKind, ExpressionType>
        {
            { BinaryOperatorKind.And, ExpressionType.AndAlso },
            { BinaryOperatorKind.Or, ExpressionType.OrElse },
        };

        private static readonly MethodInfo FilterHumanTaskTemplateMethod = typeof(ODataSearchBinder).GetMethod(nameof(FilterHumanTaskTemplate), BindingFlags.Static | BindingFlags.NonPublic)!;

        /// <inheritdoc/>
        public Expression BindSearch(SearchClause searchClause, QueryBinderContext context)
        {
            return Expression.Lambda(this.BindSingleValueNode(searchClause.Expression, context), context.CurrentParameter);
        }

        /// <summary>
        /// Binds the specified <see cref="SingleValueCastNode"/>
        /// </summary>
        /// <param name="node">The <see cref="SingleValueCastNode"/> to bind</param>
        /// <param name="context">The current <see cref="QueryBinderContext"/></param>
        /// <returns>A new <see cref="Expression"/></returns>
        public override Expression BindSingleValueNode(SingleValueNode node, QueryBinderContext context)
        {
            return node switch
            {
                BinaryOperatorNode binaryOperatorNode => this.BindBinaryOperatorNode(binaryOperatorNode, context),
                SearchTermNode searchTermNode => this.BindSearchTerm(searchTermNode, context),
                UnaryOperatorNode unaryOperatorNode => this.BindUnaryOperatorNode(unaryOperatorNode, context),
                _ => throw new NotSupportedException($"The specified {nameof(SingleValueNode)} type '{node.GetType().Name}' is not supported")
            };
        }

        /// <summary>
        /// Binds the specified <see cref="BinaryOperatorNode"/>
        /// </summary>
        /// <param name="binaryOperatorNode">The <see cref="BinaryOperatorNode"/> to bind</param>
        /// <param name="context">The current <see cref="QueryBinderContext"/></param>
        /// <returns>A new <see cref="Expression"/></returns>
        public override Expression BindBinaryOperatorNode(BinaryOperatorNode binaryOperatorNode, QueryBinderContext context)
        {
            var left = this.Bind(binaryOperatorNode.Left, context);
            var right = this.Bind(binaryOperatorNode.Right, context);
            if (!BinaryOperatorMapping.TryGetValue(binaryOperatorNode.OperatorKind, out ExpressionType binaryExpressionType))
                throw new NotImplementedException($"Binary operator '{binaryOperatorNode.OperatorKind}' is not supported!");
            return Expression.MakeBinary(binaryExpressionType, left, right);
        }

        /// <summary>
        /// Binds the specified <see cref="SearchTermNode"/>
        /// </summary>
        /// <param name="term">The <see cref="SearchTermNode"/> to bind</param>
        /// <param name="context">The current <see cref="QueryBinderContext"/></param>
        /// <returns>A new <see cref="Expression"/></returns>
        public Expression BindSearchTerm(SearchTermNode term, QueryBinderContext context)
        {
            if (term == null)
                throw new ArgumentNullException(nameof(term));
            if (context.ElementClrType == typeof(Integration.Models.HumanTaskTemplate))
                return this.BindHumanTaskTemplateSearchTerm(term, context);
            else
                throw new NotSupportedException($"Search is not allowed on element type '{context.ElementClrType.Name}'");
        }

        /// <summary>
        /// Binds the specified <see cref="Domain.Models.HumanTaskTemplate"/> <see cref="SearchTermNode"/>
        /// </summary>
        /// <param name="searchTermNode">The <see cref="Domain.Models.HumanTaskTemplate"/> <see cref="SearchTermNode"/> to bind</param>
        /// <param name="context">The current <see cref="QueryBinderContext"/></param>
        /// <returns>A new <see cref="Expression"/></returns>
        protected virtual Expression BindHumanTaskTemplateSearchTerm(SearchTermNode searchTermNode, QueryBinderContext context)
        {
            var searchTerm = searchTermNode.Text.ToLowerInvariant();
            var searchQuery = Expression.IsTrue(Expression.Call(null, FilterHumanTaskTemplateMethod, context.CurrentParameter, Expression.Constant(searchTerm)));
            return searchQuery;
        }

        static bool FilterHumanTaskTemplate(Integration.Models.HumanTaskTemplate workflow, string searchTerm)
        {
            return workflow.Id.ToLower().Contains(searchTerm);
        }

    }

}
