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
    /// Represents a fault
    /// </summary>
    [DataTransferObjectType(typeof(Integration.Models.Fault))]
    public class Fault
        : Entity<string>
    {

        /// <summary>
        /// Initializes a new <see cref="Fault"/>
        /// </summary>
        protected Fault() : base(string.Empty) { }

        /// <summary>
        /// Initializes a new <see cref="Fault"/>
        /// </summary>
        /// <param name="name">The <see cref="Fault"/>'s name</param>
        /// <param name="description">The <see cref="Fault"/>'s description</param>
        public Fault(string name, string description)
            : base(Guid.NewGuid().ToString().ToLowerInvariant())
        {
            if (string.IsNullOrWhiteSpace(name)) throw DomainException.ArgumentNullOrWhitespace(nameof(name));
            if (string.IsNullOrWhiteSpace(description)) throw DomainException.ArgumentNullOrWhitespace(nameof(description));
            this.Name = name;
            this.Description = description;
        }

        /// <summary>
        /// Gets the <see cref="Fault"/>'s name
        /// </summary>
        public virtual string Name { get; protected set; } = null!;

        /// <summary>
        /// Gets the <see cref="Fault"/>'s description
        /// </summary>
        public virtual string Description { get; protected set; } = null!;

    }

}
