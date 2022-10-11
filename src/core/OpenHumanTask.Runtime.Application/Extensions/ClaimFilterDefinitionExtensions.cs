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

using OpenHumanTask.Sdk;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace OpenHumanTask.Runtime.Application
{

    /// <summary>
    /// Defines extensions for <see cref="ClaimFilterDefinition"/>s
    /// </summary>
    public static class ClaimFilterDefinitionExtensions
    {

        /// <summary>
        /// Determines whether or not the specified <see cref="ClaimsIdentity"/> is filtered by the <see cref="ClaimFilterDefinition"/>s
        /// </summary>
        /// <param name="context">The <see cref="HumanTaskRuntimeContext"/> to resolve the specified <see cref="ClaimFilterDefinition"/>s for</param>
        /// <param name="expressionEvaluator">The service used to evaluate runtime expressions</param>
        /// <param name="claimFilters">The extended <see cref="ClaimFilterDefinition"/> collection</param>
        /// <param name="user">The <see cref="ClaimsIdentity"/> to check</param>
        /// <returns>A boolean indicating whether or not the specified <see cref="ClaimsIdentity"/> is filtered by the <see cref="ClaimFilterDefinition"/>s</returns>
        public static bool Filters(this IEnumerable<ClaimFilterDefinition> claimFilters, HumanTaskRuntimeContext context, Neuroglia.Data.Expressions.IExpressionEvaluator expressionEvaluator, ClaimsIdentity user)
        {
            if (claimFilters == null) throw new ArgumentNullException(nameof(claimFilters));
            if (user == null) throw new ArgumentNullException(nameof(user));
            foreach(var filter in claimFilters)
            {
                if (!filter.Filters(context, expressionEvaluator, user)) return false;
            }
            return true;
        }

        /// <summary>
        /// Determines whether or not the specified <see cref="ClaimsIdentity"/> is filtered by the <see cref="ClaimFilterDefinition"/>
        /// </summary>
        /// <param name="context">The <see cref="HumanTaskRuntimeContext"/> to resolve the specified <see cref="ClaimFilterDefinition"/> for</param>
        /// <param name="expressionEvaluator">The service used to evaluate runtime expressions</param>
        /// <param name="claimFilter">The extended <see cref="ClaimFilterDefinition"/></param>
        /// <param name="user">The <see cref="ClaimsIdentity"/> to check</param>
        /// <returns>A boolean indicating whether or not the specified <see cref="ClaimsIdentity"/> is filtered by the <see cref="ClaimFilterDefinition"/></returns>
        public static bool Filters(this ClaimFilterDefinition claimFilter, HumanTaskRuntimeContext context, Neuroglia.Data.Expressions.IExpressionEvaluator expressionEvaluator, ClaimsIdentity user)
        {
            if (claimFilter == null) throw new ArgumentNullException(nameof(claimFilter));
            if (user == null) throw new ArgumentNullException(nameof(user));
            var type = claimFilter.Type;
            if(!string.IsNullOrEmpty(type) && type.IsRuntimeExpression()) type = expressionEvaluator.Evaluate<string>(type, new { }, BuildArguments(context));
            var value = claimFilter.Value;
            if (!string.IsNullOrEmpty(value) && value.IsRuntimeExpression()) value = expressionEvaluator.Evaluate<string>(value, new { }, BuildArguments(context));
            if (string.IsNullOrWhiteSpace(type))
            {
                if (string.IsNullOrWhiteSpace(value)) 
                    return true;
                if (!user.Claims.Any(c => Regex.IsMatch(c.Value, value))) 
                    return false;
            }
            else if(string.IsNullOrWhiteSpace(value))
            {
                if (!user.Claims.Any(c => Regex.IsMatch(c.Type, type))) 
                    return false;
            }
            else
            {
                if (!user.Claims.Any(c => Regex.IsMatch(c.Type, type) && Regex.IsMatch(c.Value, value))) 
                    return false;
            }
            return true;
        }

        private static IDictionary<string, object> BuildArguments(HumanTaskRuntimeContext context)
        {
            return new Dictionary<string, object>()
            {
                { "CONTEXT", context }
            };
        }

    }

}
