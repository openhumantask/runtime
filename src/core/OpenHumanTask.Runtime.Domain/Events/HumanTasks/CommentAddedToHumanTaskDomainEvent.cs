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
    /// Represents the <see cref="IDomainEvent"/> fired when a new <see cref="Models.Comment"/> has been added to a <see cref="HumanTask"/>
    /// </summary>
    [DataTransferObjectType(typeof(CommentAddedToHumanTaskIntegrationEvent))]
    public class CommentAddedToHumanTaskDomainEvent
        : DomainEvent<HumanTask, string>
    {

        /// <summary>
        /// Initializes a new <see cref="CommentAddedToHumanTaskDomainEvent"/>
        /// </summary>
        protected CommentAddedToHumanTaskDomainEvent() { }

        /// <summary>
        /// Initializes a new <see cref="CommentAddedToHumanTaskDomainEvent"/>
        /// </summary>
        /// <param name="id">The id of the <see cref="HumanTask"/> a new <see cref="Models.Comment"/> has been added to</param>
        /// <param name="author">The user that has authored the <see cref="Models.Comment"/></param>
        /// <param name="comment">The newly added <see cref="Models.Comment"/></param>
        public CommentAddedToHumanTaskDomainEvent(string id, UserReference author, Comment comment)
            : base(id)
        {
            this.User = author;
            this.Comment = comment;
        }

        /// <summary>
        /// Gets the user that has authored the <see cref="Models.Comment"/>
        /// </summary>
        public virtual UserReference User { get; protected set; } = null!;

        /// <summary>
        /// Gets the newly added <see cref="Models.Comment"/>
        /// </summary>
        public virtual Comment Comment { get; protected set; } = null!;

    }

}
