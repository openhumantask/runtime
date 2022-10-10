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

using OpenHumanTask.Runtime.Integration.Events.HumanTasks;

namespace OpenHumanTask.Runtime.Domain.Events.HumanTasks
{
    /// <summary>
    /// Represents the <see cref="IDomainEvent"/> fired whenever a <see cref="HumanTask"/>'s status has changed
    /// </summary>
    [DataTransferObjectType(typeof(HumanTaskStatusChangedIntegrationEvent))]
    public class HumanTaskStatusChangedDomainEvent
        : DomainEvent<HumanTask, string>
    {

        /// <summary>
        /// Initializes a new <see cref="HumanTaskStatusChangedDomainEvent"/>
        /// </summary>
        protected HumanTaskStatusChangedDomainEvent()
        {

        }

        /// <summary>
        /// Initializes a new <see cref="HumanTaskStatusChangedDomainEvent"/>
        /// </summary>
        /// <param name="id">The id of the <see cref="HumanTask"/> which's status has changed</param>
        /// <param name="status">The <see cref="HumanTask"/>'s updated status</param>
        public HumanTaskStatusChangedDomainEvent(string id, HumanTaskStatus status)
            : base(id)
        {
            this.Status = status;
        }

        /// <summary>
        /// Gets the <see cref="HumanTask"/>'s updated status
        /// </summary>
        public virtual HumanTaskStatus Status { get; protected set; }

    }

}
