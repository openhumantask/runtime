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

using OpenHumanTask.Runtime.Integration.Events.HumanTaskTemplates;

namespace OpenHumanTask.Runtime.Domain.Events.HumanTaskTemplates
{

    /// <summary>
    /// Represents the <see cref="IDomainEvent"/> fired whenever a new <see cref="HumanTaskTemplate"/> has been created
    /// </summary>
    [DataTransferObjectType(typeof(HumanTaskTemplateCreatedIntegrationEvent))]
    public class HumanTaskTemplateCreatedDomainEvent
        : DomainEvent<HumanTaskTemplate, string>
    {

        /// <summary>
        /// Initializes a new <see cref="HumanTaskTemplateCreatedDomainEvent"/>
        /// </summary>
        protected HumanTaskTemplateCreatedDomainEvent() { }

        /// <summary>
        /// Initializes a new <see cref="HumanTaskTemplateCreatedDomainEvent"/>
        /// </summary>
        /// <param name="id">The id of the newly created <see cref="HumanTask"/></param>
        /// <param name="createdBy">The user that has created the <see cref="HumanTaskTemplate"/></param>
        /// <param name="definition">The <see cref="HumanTaskDefinition"/> of the newly created <see cref="HumanTaskTemplate"/></param>
        public HumanTaskTemplateCreatedDomainEvent(string id, UserReference createdBy, HumanTaskDefinition definition)
            : base(id)
        {
            this.CreatedBy = createdBy;
            this.Definition = definition;
        }

        /// <summary>
        /// Gets the user that has created the <see cref="HumanTaskTemplate"/>
        /// </summary>
        public virtual UserReference CreatedBy { get; protected set; } = null!;

        /// <summary>
        /// Gets the <see cref="HumanTaskDefinition"/> of the newly created <see cref="HumanTaskTemplate"/>
        /// </summary>
        public virtual HumanTaskDefinition Definition { get; protected set; } = null!;

    }

}
