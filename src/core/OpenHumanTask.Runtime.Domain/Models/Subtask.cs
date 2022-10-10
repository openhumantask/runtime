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

using OpenHumanTask.Sdk.Models;

namespace OpenHumanTask.Runtime.Domain.Models
{
    /// <summary>
    /// Represents a <see cref="HumanTask"/>'s subtask
    /// </summary>
    [DataTransferObjectType(typeof(Integration.Models.Subtask))]
    public class Subtask
        : Entity<string>
    {

        /// <summary>
        /// Initializes a new <see cref="Subtask"/>
        /// </summary>
        protected Subtask()
            : base(string.Empty)
        {

        }

        /// <summary>
        /// Initializes a new <see cref="Subtask"/>
        /// </summary>
        /// <param name="definition">The <see cref="Subtask"/>'s <see cref="HumanTaskTemplate"/></param>
        public Subtask(HumanTaskTemplate definition)
        {
            if(definition == null) throw DomainException.ArgumentNull(nameof(definition));
            this.DefinitionId = definition.Id;
        }

        /// <summary>
        /// Gets the id of the <see cref="Subtask"/>'s <see cref="HumanTaskTemplate"/>
        /// </summary>
        public virtual string DefinitionId { get; protected set; } = null!;

    }

}
