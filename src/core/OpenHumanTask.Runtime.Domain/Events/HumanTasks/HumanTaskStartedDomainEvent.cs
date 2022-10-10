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
    /// Represents the <see cref="IDomainEvent"/> fired when a <see cref="HumanTask"/> has been started
    /// </summary>
    [DataTransferObjectType(typeof(HumanTaskStartedIntegrationEvent))]
    public class HumanTaskStartedDomainEvent
        : DomainEvent<HumanTask, string>
    {

        /// <summary>
        /// Initializes a new <see cref="HumanTaskStartedDomainEvent"/>
        /// </summary>
        protected HumanTaskStartedDomainEvent() { }

        /// <summary>
        /// Initializes a new <see cref="HumanTaskStartedDomainEvent"/>
        /// </summary>
        /// <param name="id">The id of the started <see cref="HumanTask"/></param>
        /// <param name="user">The user that has started the <see cref="HumanTask"/></param>
        public HumanTaskStartedDomainEvent(string id, UserReference user)
            : base(id)
        {
            this.User = user;
        }

        /// <summary>
        /// Gets the user that has started the <see cref="Comment"/>
        /// </summary>
        public virtual UserReference User { get; protected set; } = null!;

    }
    

}
