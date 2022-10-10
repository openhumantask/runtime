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
    /// Represents the <see cref="IDomainEvent"/> fired whenever a <see cref="HumanTaskTemplate"/> has been deleted
    /// </summary>
    [DataTransferObjectType(typeof(HumanTaskTemplateDeletedIntegrationEvent))]
    public class HumanTaskTemplateDeletedDomainEvent
        : DomainEvent<HumanTaskTemplate, string>
    {

        /// <summary>
        /// Initializes a new <see cref="HumanTaskTemplateDeletedDomainEvent"/>
        /// </summary>
        protected HumanTaskTemplateDeletedDomainEvent() { }

        /// <summary>
        /// Initializes a new <see cref="HumanTaskTemplateDeletedDomainEvent"/>
        /// </summary>
        /// <param name="id">The id of the deleted <see cref="HumanTask"/></param>
        public HumanTaskTemplateDeletedDomainEvent(string id)
            : base(id)
        {

        }

    }

}
