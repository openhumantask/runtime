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
    /// Represents the <see cref="IDomainEvent"/> fired whenever a <see cref="HumanTask"/> has been delegated
    /// </summary>
    [DataTransferObjectType(typeof(HumanTaskDelegatedIntegrationEvent))]
    public class HumanTaskDelegatedDomainEvent
  : DomainEvent<HumanTask, string>
    {

        /// <summary>
        /// Initializes a new <see cref="HumanTaskDelegatedDomainEvent"/>
        /// </summary>
        protected HumanTaskDelegatedDomainEvent() { }

        /// <summary>
        /// Initializes a new <see cref="HumanTaskDelegatedDomainEvent"/>
        /// </summary>
        /// <param name="id">The id of the delegated <see cref="HumanTask"/></param>
        /// <param name="user">The user that has delegated the <see cref="HumanTask"/></param>
        /// <param name="delegatedTo">The user the <see cref="HumanTask"/> has been delegated to</param>
        public HumanTaskDelegatedDomainEvent(string id, UserReference user, UserReference delegatedTo)
            : base(id)
        {
            this.User = user;
            this.DelegatedTo = delegatedTo;
        }

        /// <summary>
        /// Gets the user that has delegated the <see cref="HumanTask"/>
        /// </summary>
        public virtual UserReference User { get; protected set; } = null!;

        /// <summary>
        /// Gets the user the <see cref="HumanTask"/> has been delegated to
        /// </summary>
        public virtual UserReference DelegatedTo { get; protected set; } = null!;

    }

}
