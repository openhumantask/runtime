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

namespace OpenHumanTask.Runtime.Domain.Models
{
    /// <summary>
    /// Holds contextual, runtime information about a <see cref="HumanTask"/>
    /// </summary>
    [DataTransferObjectType(typeof(Integration.Models.HumanTaskRuntimeContext))]
    public class HumanTaskRuntimeContext
    {

        /// <summary>
        /// Gets/sets the <see cref="HumanTask"/>'s runtime expression language
        /// </summary>
        public virtual string ExpressionLanguage { get; set; } = null!;

        /// <summary>
        /// Gets/sets the <see cref="HumanTask"/>'s id
        /// </summary>
        public virtual string Id { get; set; } = null!;

        /// <summary>
        /// Gets/sets the <see cref="HumanTask"/>'s key
        /// </summary>
        public virtual string Key { get; set; } = null!;

        /// <summary>
        /// Gets/sets the id of the <see cref="HumanTask"/>'s <see cref="HumanTaskDefinition"/>
        /// </summary>
        public virtual string DefinitionId { get; set; } = null!;

        /// <summary>
        /// Gets/sets the <see cref="HumanTask"/>'s status
        /// </summary>
        public virtual HumanTaskStatus Status { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="HumanTask"/>'s priority
        /// </summary>
        public virtual int? Priority { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="HumanTask"/>'s localized titles mapped by their language two-letter ISO 639-1 code
        /// </summary>
        public virtual Dictionary<string, string> Title { get; set; } = new();

        /// <summary>
        /// Gets/sets the <see cref="HumanTask"/>'s localized subjects mapped by their language two-letter ISO 639-1 code
        /// </summary>
        public virtual Dictionary<string, string> Subject { get; set; } = new();

        /// <summary>
        /// Gets/sets the <see cref="HumanTask"/>'s localized descriptions mapped by their language two-letter ISO 639-1 code
        /// </summary>
        public virtual Dictionary<string, string> Description { get; set; } = new();

        /// <summary>
        /// Gets/sets the <see cref="HumanTask"/>'s <see cref="Integration.Models.PeopleAssignments"/>
        /// </summary>
        public virtual PeopleAssignments PeopleAssignments { get; set; } = null!;

        /// <summary>
        /// Gets/sets the <see cref="HumanTask"/>'s input data
        /// </summary>
        public virtual object? InputData { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="HumanTask"/>'s output data
        /// </summary>
        public virtual object? OutputData { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="HumanTask"/>'s <see cref="Comment"/>s
        /// </summary>
        public virtual List<Comment> Comments { get; set; } = new();

        /// <summary>
        /// Gets/sets the <see cref="HumanTask"/>'s <see cref="Attachment"/>s
        /// </summary>
        public virtual List<Attachment> Attachments { get; set; } = new();

    }

}
