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
    /// Represents the <see cref="IDomainEvent"/> fired whenever a <see cref="HumanTask"/>'s priority has changed
    /// </summary>
    [DataTransferObjectType(typeof(HumanTaskPriorityChangedIntegrationEvent))]
    public class HumanTaskPriorityChangedDomainEvent
        : DomainEvent<HumanTask, string>
    {

        /// <summary>
        /// Initializes a new <see cref="HumanTaskPriorityChangedDomainEvent"/>
        /// </summary>
        protected HumanTaskPriorityChangedDomainEvent() { }

        /// <summary>
        /// Initializes a new <see cref="HumanTaskPriorityChangedDomainEvent"/>
        /// </summary>
        /// <param name="id">The id of the <see cref="HumanTask"/> which's priority has changed</param>
        /// <param name="user">The user that has changed the <see cref="HumanTask"/>'s priority</param>
        /// <param name="priority">The <see cref="HumanTask"/>'s updated priority</param>
        public HumanTaskPriorityChangedDomainEvent(string id, UserReference user, int priority)
            : base(id)
        {
            this.User = user;
            this.Priority = priority;
        }

        /// <summary>
        /// Gets the user that has changed the <see cref="HumanTask"/>'s priority
        /// </summary>
        public virtual UserReference User { get; protected set; } = null!;

        /// <summary>
        /// Gets the <see cref="HumanTask"/>'s updated priority
        /// </summary>
        public virtual int Priority { get; protected set; }

    }

}
