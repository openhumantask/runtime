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
    /// Represents the <see cref="IDomainEvent"/> fired when a <see cref="HumanTask"/> has been completed
    /// </summary>
    [DataTransferObjectType(typeof(HumanTaskCompletedIntegrationEvent))]
    public class HumanTaskCompletedDomainEvent
        : DomainEvent<HumanTask, string>
    {

        /// <summary>
        /// Initializes a new <see cref="HumanTaskCompletedDomainEvent"/>
        /// </summary>
        protected HumanTaskCompletedDomainEvent() { }

        /// <summary>
        /// Initializes a new <see cref="HumanTaskCompletedDomainEvent"/>
        /// </summary>
        /// <param name="id">The id of the completed <see cref="HumanTask"/></param>
        /// <param name="user">The user that has completed the <see cref="HumanTask"/></param>
        /// <param name="outcome">The <see cref="HumanTask"/>'s outcome</param>
        /// <param name="output">The <see cref="HumanTask"/>'s output</param>
        /// <param name="completionBehaviorName">The name of the <see cref="HumanTask"/>'s matching completion behavior</param>
        public HumanTaskCompletedDomainEvent(string id, UserReference user, object outcome, object? output, string? completionBehaviorName)
            : base(id)
        {
            this.User = user;
            this.Outcome = outcome;
            this.Output = output;
            this.CompletionBehaviorName = completionBehaviorName;
        }

        /// <summary>
        /// Gets the user that has completed the <see cref="HumanTask"/>
        /// </summary>
        public virtual UserReference User { get; protected set; } = null!;

        /// <summary>
        /// Gets the <see cref="HumanTask"/>'s outcome
        /// </summary>
        public virtual object Outcome { get; protected set; } = null!;

        /// <summary>
        /// Gets the <see cref="HumanTask"/>'s output
        /// </summary>
        public virtual object? Output { get; protected set; }

        /// <summary>
        /// Gets the name of the <see cref="HumanTask"/>'s matching completion behavior
        /// </summary>
        public virtual string? CompletionBehaviorName { get; protected set; }

    }
    

}
