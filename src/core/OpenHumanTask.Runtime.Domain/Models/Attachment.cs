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
    /// Represents an <see cref="Attachment"/>
    /// </summary>
    [DataTransferObjectType(typeof(Integration.Models.Attachment))]
    public class Attachment
        : Entity<string>
    {

        /// <summary>
        /// Initializes a new <see cref="Attachment"/>
        /// </summary>
        protected Attachment() { }

        /// <summary>
        /// Initializes a new <see cref="Attachment"/>
        /// </summary>
        /// <param name="author">The user that has created the <see cref="Attachment"/></param>
        /// <param name="name">The <see cref="Attachment"/>'s name</param>
        /// <param name="contentType">The <see cref="Attachment"/>'s content type</param>
        public Attachment(UserReference author, string name, string contentType)
            : base(Guid.NewGuid().ToString())
        {
            this.Author = author;
            this.Name = name;
            this.ContentType = contentType;
        }

        /// <summary>
        /// Gets the id of the user that has created the <see cref="Attachment"/>
        /// </summary>
        public virtual UserReference Author { get; protected set; } = null!;

        /// <summary>
        /// Gets the <see cref="Attachment"/>'s name
        /// </summary>
        public virtual string Name { get; protected set; } = null!;

        /// <summary>
        /// Gets the <see cref="Attachment"/>'s content type
        /// </summary>
        public virtual string ContentType { get; protected set; } = null!;

    }

}
