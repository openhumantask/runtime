﻿// Copyright © 2022-Present The Open Human Task Specification Authors. All rights reserved.
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
    /// Represents the <see cref="IDomainEvent"/> fired when a <see cref="HumanTask"/>'s <see cref="Models.Comment"/> has been updated
    /// </summary>
    [DataTransferObjectType(typeof(CommentAddedToHumanTaskIntegrationEvent))]
    public class HumanTaskCommentUpdatedDomainEvent
        : DomainEvent<HumanTask, string>
    {

        /// <summary>
        /// Initializes a new <see cref="HumanTaskCommentUpdatedDomainEvent"/>
        /// </summary>
        protected HumanTaskCommentUpdatedDomainEvent()
        {

        }

        /// <summary>
        /// Initializes a new <see cref="HumanTaskCommentUpdatedDomainEvent"/>
        /// </summary>
        /// <param name="id">The id of the <see cref="HumanTask"/> which's <see cref="Models.Comment"/> has been updated</param>
        /// <param name="user">The user that has updated the <see cref="Models.Comment"/></param>
        /// <param name="commentId">The id of the <see cref="Models.Comment"/> that has been updated</param>
        /// <param name="content">The updated comment</param>
        public HumanTaskCommentUpdatedDomainEvent(string id, UserReference user, string commentId, string content)
            : base(id)
        {
            this.User = user;
            this.CommentId = commentId;
            this.Content = content;
        }

        /// <summary>
        /// Gets the user that has updated the <see cref="Models.Comment"/>
        /// </summary>
        public virtual UserReference User { get; protected set; } = null!;

        /// <summary>
        /// Gets the id of the <see cref="Models.Comment"/> that has been updated
        /// </summary>
        public virtual string CommentId { get; protected set; } = null!;

        /// <summary>
        /// Gets the updated comment
        /// </summary>
        public virtual string Content { get; protected set; } = null!;

    }

}