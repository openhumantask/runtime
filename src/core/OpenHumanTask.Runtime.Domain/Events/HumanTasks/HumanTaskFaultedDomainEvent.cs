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
    /// Represents the <see cref="IDomainEvent"/> fired when a <see cref="HumanTask"/> has faulted
    /// </summary>
    [DataTransferObjectType(typeof(HumanTaskFaultedIntegrationEvent))]
    public class HumanTaskFaultedDomainEvent
        : DomainEvent<HumanTask, string>
    {

        /// <summary>
        /// Initializes a new <see cref="HumanTaskFaultedDomainEvent"/>
        /// </summary>
        protected HumanTaskFaultedDomainEvent() { }

        /// <summary>
        /// Initializes a new <see cref="HumanTaskFaultedDomainEvent"/>
        /// </summary>
        /// <param name="id">The id of the faulted <see cref="HumanTask"/></param>
        /// <param name="user">The user that has faulted the <see cref="HumanTask"/></param>
        /// <param name="faults">An <see cref="IEnumerable{T}"/> containing the <see cref="HumanTask"/>'s <see cref="Fault"/>s</param>
        public HumanTaskFaultedDomainEvent(string id, UserReference user, IEnumerable<Fault> faults)
            : base(id)
        {
            this.User = user;
            this.Faults = faults;
        }

        /// <summary>
        /// Gets the user that has faulted the <see cref="HumanTask"/>
        /// </summary>
        public virtual UserReference User { get; protected set; } = null!;

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> containing the <see cref="HumanTask"/>'s <see cref="Fault"/>s
        /// </summary>
        public virtual IEnumerable<Fault> Faults { get; protected set; } = null!;

    }

    /// <summary>
    /// Represents the <see cref="IDomainEvent"/> fired when new <see cref="Fault"/>s have been added to a <see cref="HumanTask"/>
    /// </summary>
    [DataTransferObjectType(typeof(FaultsAddedToHumanTaskIntegrationEvent))]
    public class FaultsAddedToHumanTaskDomainEvent
        : DomainEvent<HumanTask, string>
    {

        /// <summary>
        /// Initializes a new <see cref="FaultsAddedToHumanTaskDomainEvent"/>
        /// </summary>
        protected FaultsAddedToHumanTaskDomainEvent() { }

        /// <summary>
        /// Initializes a new <see cref="FaultsAddedToHumanTaskDomainEvent"/>
        /// </summary>
        /// <param name="id">The id of the <see cref="HumanTask"/> new <see cref="Fault"/>s have been added to</param>
        /// <param name="user">The user that has added the <see cref="Fault"/>s</param>
        /// <param name="faults">An <see cref="IEnumerable{T}"/> containing the added <see cref="Fault"/>s</param>
        public FaultsAddedToHumanTaskDomainEvent(string id, UserReference user, IEnumerable<Fault> faults)
            : base(id)
        {
            this.User = user;
            this.Faults = faults;
        }

        /// <summary>
        /// Gets the user that has added the <see cref="Fault"/>s
        /// </summary>
        public virtual UserReference User { get; protected set; } = null!;

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> containing the added <see cref="Fault"/>s
        /// </summary>
        public virtual IEnumerable<Fault> Faults { get; protected set; } = null!;

    }

}
