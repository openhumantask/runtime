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

using IdentityModel;
using Neuroglia.Mediation;
using OpenHumanTask.Sdk;
using System.Security.Claims;

namespace OpenHumanTask.Runtime.Application
{

    /// <summary>
    /// Defines extensions for <see cref="PeopleAssignmentsDefinition"/>s
    /// </summary>
    public static class PeopleReferenceDefinitionExtensions
    {

        /// <summary>
        /// Resolves the <see cref="PeopleReferenceDefinition"/>
        /// </summary>
        /// <param name="peopleReference">The <see cref="PeopleReferenceDefinition"/> to resolve</param>
        /// <param name="context">The <see cref="HumanTaskRuntimeContext"/> to resolve the specified <see cref="PeopleReferenceDefinition"/> for</param>
        /// <param name="expressionEvaluator">The service used to evaluate runtime expressions</param>
        /// <param name="availableUsers">A <see cref="List{T}"/> containing all known users</param>
        /// <param name="resolvedUsers">An <see cref="IDictionary{TKey, TValue}"/> containing the resolved users for each <see cref="GenericHumanRole"/></param>
        /// <param name="resolvedGroups">An <see cref="IDictionary{TKey, TValue}"/> containing the name/users mappings of the resolved user groups</param>
        /// <returns></returns>
        public static List<UserReference> Resolve(this PeopleReferenceDefinition peopleReference, HumanTaskRuntimeContext context, Neuroglia.Data.Expressions.IExpressionEvaluator expressionEvaluator, List<ClaimsIdentity> availableUsers, IDictionary<GenericHumanRole, List<UserReference>> resolvedUsers, IDictionary<string, List<UserReference>> resolvedGroups)
        {
            if (peopleReference == null) throw new ArgumentNullException(nameof(peopleReference));
            if (availableUsers == null) throw new ArgumentNullException(nameof(availableUsers));
            if (resolvedUsers == null) throw new ArgumentNullException(nameof(resolvedUsers));
            if (resolvedGroups == null) throw new ArgumentNullException(nameof(resolvedGroups));
            var users = new List<UserReference>();
            if (peopleReference.User != null)
            {
                var userId = peopleReference.User;
                if (userId.IsRuntimeExpression()) userId = expressionEvaluator.Evaluate<string>(userId, new { }, BuildArguments(context));
                var user = availableUsers.FirstOrDefault(u => u.FindFirst(JwtClaimTypes.Subject)?.Value.Equals(userId, StringComparison.InvariantCultureIgnoreCase) == true);
                if (user != null)
                    users.Add(user);
            }
            else if (peopleReference.Users != null)
            {
                if (peopleReference.Users.InGenericRole != null)
                {
                    users = resolvedUsers[peopleReference.Users.InGenericRole.Value];
                }
                else if (!string.IsNullOrWhiteSpace(peopleReference.Users.InGroup))
                {
                    var group = peopleReference.Users.InGroup;
                    if (group.IsRuntimeExpression()) group = expressionEvaluator.Evaluate<string>(group, new { }, BuildArguments(context));
                    if (!resolvedGroups.TryGetValue(group!, out users)) users = new();
                }
                else if (peopleReference.Users.WithClaims != null)
                {
                    foreach (var user in availableUsers)
                    {
                        if (peopleReference.Users.WithClaims.Filters(context, expressionEvaluator, user)) users.Add(user);
                    }
                }
            }
            return users;
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
