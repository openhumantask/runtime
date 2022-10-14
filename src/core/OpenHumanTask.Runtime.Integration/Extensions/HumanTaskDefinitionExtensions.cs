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

namespace OpenHumanTask.Runtime
{
    /// <summary>
    /// Defines extensions for <see cref="HumanTaskDefinition"/>s
    /// </summary>
    public static class HumanTaskDefinitionExtensions
    {

        /// <summary>
        /// Gets the <see cref="HumanTaskDefinition"/>'s versionless identifier
        /// </summary>
        /// <param name="definition">The <see cref="HumanTaskDefinition"/> to get the versionless identifier of</param>
        /// <returns>The <see cref="HumanTaskDefinition"/>'s versionless identifier</returns>
        public static string GetVersionlessIdentifier(this HumanTaskDefinition definition)
        {
            return definition.Id.Split(':', StringSplitOptions.RemoveEmptyEntries).First();
        }

        /// <summary>
        /// Gets all <see cref="SubtaskDefinition"/>s declared in the <see cref="HumanTaskDefinition"/>
        /// </summary>
        /// <param name="definition">The <see cref="HumanTaskDefinition"/> that declares the <see cref="SubtaskDefinition"/>s to get</param>
        /// <returns>A new <see cref="List{T}"/> containing all <see cref="SubtaskDefinition"/>s declared in the <see cref="HumanTaskDefinition"/></returns>
        public static List<SubtaskDefinition> GetSubtaskDefinitions(this HumanTaskDefinition definition)
        {
            var subtasks = definition.Subtasks;
            subtasks ??= new();
            if (definition.Deadlines != null)
                subtasks.AddRange(definition.Deadlines
                    .Where(d => d.Escalations != null)
                    .SelectMany(d => d.Escalations)
                    .Select(e => e.Action)
                    .Where(e => e.Subtask != null)
                    .Select(e => e.Subtask));
            return subtasks;
        }

    }

}
