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

namespace OpenHumanTask.Runtime.Domain.Models
{
    /// <summary>
    /// Represents a comment
    /// </summary>
    [DataTransferObjectType(typeof(Integration.Models.Comment))]
    public class Comment
        : Entity<string>
    {

        /// <summary>
        /// Initializes a new <see cref="Comment"/>
        /// </summary>
        protected Comment() { }

        /// <summary>
        /// Initializes a new <see cref="Comment"/>
        /// </summary>
        /// <param name="author">The <see cref="Comment"/>'s author</param>
        /// <param name="content">The <see cref="Comment"/>'s Markdown (MD) content</param>
        public Comment(UserReference author, string content)
            : base(Guid.NewGuid().ToString())
        {
            this.AuthorId = author;
            this.Content = content;
        }

        /// <summary>
        /// Gets the <see cref="Comment"/>'s author
        /// </summary>
        public virtual UserReference AuthorId { get; protected set; } = null!;

        /// <summary>
        /// Gets the user that has last modified the <see cref="Comment"/>
        /// </summary>
        public virtual UserReference? LastModifiedBy { get; protected set; } = null!;

        /// <summary>
        /// Gets the <see cref="Comment"/>'s Markdown (MD) content
        /// </summary>
        public virtual string Content { get; protected set; } = null!;

        /// <summary>
        /// Sets the <see cref="Comment"/>'s content
        /// </summary>
        /// <param name="dateTime">The date and time at which the <see cref="Comment"/> has been updated</param>
        /// <param name="user">The user that has updated the <see cref="Comment"/>'s content</param>
        /// <param name="content">The updated content</param>
        /// <returns>A boolean indicating whether or not the operation was successfull</returns>
        internal protected virtual bool SetContent(DateTimeOffset dateTime, UserReference user, string content)
        {
            if (string.IsNullOrWhiteSpace(content)) throw DomainException.ArgumentNullOrWhitespace(nameof(content));
            if (this.Content.Equals(content, StringComparison.InvariantCultureIgnoreCase)) return false;
            this.Content = content;
            this.LastModifiedBy = user;
            this.LastModified = dateTime;
            return true;
        }

    }

}
