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

using OpenHumanTask.Runtime.Domain.Events.HumanTaskTemplates;

namespace OpenHumanTask.Runtime.Domain.Models
{

    /// <summary>
    /// Represents a <see cref="HumanTask"/> template
    /// </summary>
    [DataTransferObjectType(typeof(Integration.Models.HumanTaskTemplate))]
    public class HumanTaskTemplate
        : AggregateRoot<string>, IDeletable
    {

        /// <summary>
        /// Initializes a new <see cref="HumanTaskTemplate"/>
        /// </summary>
        protected HumanTaskTemplate() : base(string.Empty) { }

        /// <summary>
        /// Initializes a new <see cref="HumanTaskTemplate"/>
        /// </summary>
        /// <param name="user">The user that has creating <see cref="HumanTaskTemplate"/></param>
        /// <param name="definition">The <see cref="HumanTaskDefinition"/> of templated <see cref="HumanTask"/>s</param>
        public HumanTaskTemplate(UserReference user, HumanTaskDefinition definition)
            : base(definition?.Id!)
        {
            if (user == null) throw DomainException.ArgumentNull(nameof(user));
            if (definition == null) throw DomainException.ArgumentNull(nameof(definition));
            this.On(this.RegisterEvent(new HumanTaskTemplateCreatedDomainEvent(this.Id, user, definition)));
        }

        /// <summary>
        /// Gets the user that has created the <see cref="HumanTaskTemplate"/>
        /// </summary>
        public virtual UserReference CreatedBy { get; protected set; } = null!;

        /// <summary>
        /// Gets the <see cref="HumanTaskDefinition"/> of templated <see cref="HumanTask"/>s
        /// </summary>
        public virtual HumanTaskDefinition Definition { get; protected set; } = null!;

        /// <summary>
        /// Deletes the <see cref="HumanTaskTemplate"/>
        /// </summary>
        /// <param name="user">The user deleting the <see cref="HumanTaskTemplate"/></param>
        public virtual void Delete(UserReference user)
        {
            if (user == null) throw DomainException.ArgumentNull(nameof(user));
            this.On(this.RegisterEvent(new HumanTaskTemplateDeletedDomainEvent(this.Id, user)));
        }

        /// <summary>
        /// Handles the specified <see cref="HumanTaskTemplateCreatedDomainEvent"/>
        /// </summary>
        /// <param name="e">The <see cref="HumanTaskTemplateCreatedDomainEvent"/> to handle</param>
        protected virtual void On(HumanTaskTemplateCreatedDomainEvent e)
        {
            this.Id = e.AggregateId;
            this.CreatedAt = e.CreatedAt;
            this.LastModified = e.CreatedAt;
            this.CreatedBy = e.CreatedBy;
            this.Definition = e.Definition;
        }

        /// <summary>
        /// Handles the specified <see cref="HumanTaskTemplateDeletedDomainEvent"/>
        /// </summary>
        /// <param name="e">The <see cref="HumanTaskTemplateDeletedDomainEvent"/> to handle</param>
        protected virtual void On(HumanTaskTemplateDeletedDomainEvent e)
        {
            this.LastModified = e.CreatedAt;
        }

    }

}
