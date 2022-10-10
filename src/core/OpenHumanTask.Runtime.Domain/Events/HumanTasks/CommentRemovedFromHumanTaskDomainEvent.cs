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
    /// Represents the <see cref="IDomainEvent"/> fired when a new <see cref="Comment"/> has been removed from a <see cref="HumanTask"/>
    /// </summary>
    [DataTransferObjectType(typeof(CommentRemovedFromHumanTaskIntegrationEvent))]
    public class CommentRemovedFromHumanTaskDomainEvent
        : DomainEvent<HumanTask, string>
    {

        /// <summary>
        /// Initializes a new <see cref="CommentRemovedFromHumanTaskDomainEvent"/>
        /// </summary>
        protected CommentRemovedFromHumanTaskDomainEvent() { }

        /// <summary>
        /// Initializes a new <see cref="CommentRemovedFromHumanTaskDomainEvent"/>
        /// </summary>
        /// <param name="id">The id of the <see cref="HumanTask"/> the <see cref="Comment"/> has been removed from</param>
        /// <param name="user">The user that has removed the <see cref="Comment"/></param>
        /// <param name="commentId">The id of the removed <see cref="Comment"/></param>
        public CommentRemovedFromHumanTaskDomainEvent(string id, UserReference user, string commentId)
            : base(id)
        {
            this.User = user;
            this.CommentId = commentId;
        }

        /// <summary>
        /// Gets the user that has removed the <see cref="Comment"/>
        /// </summary>
        public virtual UserReference User { get; protected set; } = null!;

        /// <summary>
        /// Gets the id of the removed <see cref="Comment"/>
        /// </summary>
        public virtual string CommentId { get; protected set; } = null!;

    }

}
