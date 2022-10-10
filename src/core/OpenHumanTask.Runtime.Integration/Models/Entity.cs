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

namespace OpenHumanTask.Runtime.Integration.Models
{

    /// <summary>
    /// Represents the base class of all entity Data Transfer Objects (DTOs)
    /// </summary>
    public abstract class Entity
        : IIdentifiable<string>, IDataTransferObject
    {

        /// <summary>
        /// Gets the entity's unique identifier
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// Gets the date and time the entity has been created at
        /// </summary>
        public virtual DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Gets the date and time the entity has been last modified at
        /// </summary>
        public virtual DateTimeOffset LastModified { get; set; }

        /// <inheritdoc/>
        object IIdentifiable.Id => this.Id;

    }

}
