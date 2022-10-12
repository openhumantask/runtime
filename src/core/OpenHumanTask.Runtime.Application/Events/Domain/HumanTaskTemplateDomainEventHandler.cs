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

namespace OpenHumanTask.Runtime.Application.Events.Domain
{

    /// <summary>
    /// Represents the service used to handle <see cref="HumanTaskTemplate"/>-related <see cref="IDomainEvent"/>s
    /// </summary>
    public class HumanTaskTemplateDomainEventHandler
        : DomainEventHandlerBase<HumanTaskTemplate, Integration.Models.HumanTaskTemplate, string>,
        INotificationHandler<HumanTaskTemplateCreatedDomainEvent>,
        INotificationHandler<HumanTaskTemplateDeletedDomainEvent>
    {

        /// <inheritdoc/>
        public HumanTaskTemplateDomainEventHandler(ILoggerFactory loggerFactory, IMapper mapper, IMediator mediator, ICloudEventBus cloudEventBus, 
            IRepository<HumanTaskTemplate, string> aggregates, IRepository<Integration.Models.HumanTaskTemplate, string> projections) 
            : base(loggerFactory, mapper, mediator, cloudEventBus, aggregates, projections) { }

        /// <inheritdoc/>
        public virtual async Task HandleAsync(HumanTaskTemplateCreatedDomainEvent e, CancellationToken cancellationToken = default)
        {
            await this.GetOrReconcileProjectionAsync(e.AggregateId, cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async  Task HandleAsync(HumanTaskTemplateDeletedDomainEvent e, CancellationToken cancellationToken = default)
        {
            await this.Projections.RemoveAsync(e.AggregateId, cancellationToken);
            await this.Projections.SaveChangesAsync(cancellationToken);
        }

    }

}
