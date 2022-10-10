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
    /// Represents the <see cref="IDomainEvent"/> fired whenever an <see cref="Attachment"/> has been removed from a <see cref="HumanTask"/>
    /// </summary>
    [DataTransferObjectType(typeof(AttachmentRemovedFromHumanTaskIntegrationEvent))]
    public class AttachmentRemovedFromHumanTaskDomainEvent
        : DomainEvent<HumanTask, string>
    {

        /// <summary>
        /// Initializes a new <see cref="AttachmentRemovedFromHumanTaskDomainEvent"/>
        /// </summary>
        protected AttachmentRemovedFromHumanTaskDomainEvent() { }

        /// <summary>
        /// Initializes a new <see cref="AttachmentRemovedFromHumanTaskDomainEvent"/>
        /// </summary>
        /// <param name="id">The id of the <see cref="HumanTask"/> the <see cref="Attachment"/> has been removed from</param>
        /// <param name="user">The user that has removed the <see cref="Attachment"/></param>
        /// <param name="attachmentId">The id of the <see cref="Attachment"/> that gas been removed</param>
        public AttachmentRemovedFromHumanTaskDomainEvent(string id, UserReference user, string attachmentId)
            : base(id)
        {
            this.User = user;
            this.AttachmentId = attachmentId;
        }

        /// <summary>
        /// Gets the user that has removed the <see cref="Attachment"/>
        /// </summary>
        public virtual UserReference User { get; protected set; } = null!;

        /// <summary>
        /// Gets the id of the <see cref="Attachment"/> that has been removed
        /// </summary>
        public virtual string AttachmentId { get; protected set; } = null!;

    }

}
