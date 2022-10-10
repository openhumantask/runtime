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

namespace OpenHumanTask.Runtime.Application.Events
{

    /// <summary>
    /// Represents the base class for all <see cref="IDomainEvent"/> handlers
    /// </summary>
    public abstract class DomainEventHandlerBase
    {

        /// <summary>
        /// Initializes a new <see cref="DomainEventHandlerBase"/>
        /// </summary>
        /// <param name="loggerFactory">The service used to create <see cref="ILogger"/>s</param>
        /// <param name="mapper">The service used to map objects</param>
        /// <param name="mediator">The service used to mediate calls</param>
        /// <param name="cloudEventBus">The service used to publish and subscribe to <see cref="CloudEvent"/>s</param>
        protected DomainEventHandlerBase(ILoggerFactory loggerFactory, IMapper mapper, IMediator mediator, ICloudEventBus cloudEventBus)
        {
            this.Logger = loggerFactory.CreateLogger(this.GetType());
            this.Mapper = mapper;
            this.Mediator = mediator;
            this.CloudEventBus = cloudEventBus;
        }

        /// <summary>
        /// Gets the service used to perform logging
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Gets the service used to map objects
        /// </summary>
        protected IMapper Mapper { get; }

        /// <summary>
        /// Gets the service used to mediate calls
        /// </summary>
        protected IMediator Mediator { get; }

        /// <summary>
        /// Gets the service used to publish and subscribe to <see cref="CloudEvent"/>s
        /// </summary>
        protected ICloudEventBus CloudEventBus { get; }

        /// <summary>
        /// Publishes the specified a <see cref="CloudEvent"/> for the specified <see cref="IDomainEvent"/>
        /// </summary>
        /// <param name="e">The <see cref="IDomainEvent"/> to publish a new <see cref="CloudEvent"/> for</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new awaitable <see cref="Task"/></returns>
        protected virtual async Task PublishCloudEventAsync<TEvent>(TEvent e, CancellationToken cancellationToken)
            where TEvent : class, IDomainEvent
        {
            if (!e.GetType().TryGetCustomAttribute(out DataTransferObjectTypeAttribute dataTransferObjectTypeAttribute))
                return;
            var aggregateName = e.GetType().GetGenericType(typeof(DomainEvent<,>)).GetGenericArguments()[0].Name.ToLower();
            var actionName = e.GetType().Name.Replace(aggregateName, string.Empty, StringComparison.OrdinalIgnoreCase).Replace("DomainEvent", string.Empty, StringComparison.OrdinalIgnoreCase).ToLower();
            var integrationEvent = (Integration.Events.IntegrationEvent)this.Mapper.Map(e, e.GetType(), dataTransferObjectTypeAttribute.Type);
            CloudEvent cloudEvent = new()
            {
                Id = Guid.NewGuid().ToString(),
                Source = new(Environment.GetEnvironmentVariable("CLOUDEVENTS_SOURCE")!),
                Type = $"com.cisco.mozart.widget-manager/{aggregateName}/{actionName}/v1",
                Time = integrationEvent.CreatedAt,
                Subject = integrationEvent.AggregateId.ToString(),
                DataSchema = new($"{Environment.GetEnvironmentVariable("SCHEMA_REGISTRY_URI")}/{aggregateName}/{actionName}/v1", UriKind.RelativeOrAbsolute),
                DataContentType = MediaTypeNames.Application.Json,
                Data = integrationEvent
            };
            await this.CloudEventBus.PublishAsync(cloudEvent, cancellationToken);
        }

    }

    /// <summary>
    /// Represents the base class for all <see cref="IDomainEvent"/> handlers
    /// </summary>
    /// <typeparam name="TAggregate">The type of <see cref="IAggregateRoot"/> to handle the <see cref="IDomainEvent"/>s of</typeparam>
    /// <typeparam name="TProjection">The type of projection managed by the <see cref="DomainEventHandlerBase"/></typeparam>
    /// <typeparam name="TKey">The type of key used to uniquely authenticate the <see cref="IAggregateRoot"/>s that produce handled <see cref="IDomainEvent"/>s</typeparam>
    public abstract class DomainEventHandlerBase<TAggregate, TProjection, TKey>
        : DomainEventHandlerBase
        where TAggregate : class, IAggregateRoot<TKey>
        where TProjection : class, IIdentifiable<TKey>
        where TKey : IEquatable<TKey>
    {

        /// <summary>
        /// Initializes a new <see cref="DomainEventHandlerBase"/>
        /// </summary>
        /// <param name="loggerFactory">The service used to create <see cref="ILogger"/>s</param>
        /// <param name="mapper">The service used to map objects</param>
        /// <param name="mediator">The service used to mediate calls</param>
        /// <param name="cloudEventBus">The service used to publish and subscribe to <see cref="CloudEvent"/>s</param>
        /// <param name="aggregates">The <see cref="IRepository"/> used to manage the <see cref="IAggregateRoot"/>s to handle the <see cref="IDomainEvent"/>s of</param>
        /// <param name="projections">The <see cref="IRepository"/> used to manage the <see cref="DomainEventHandlerBase"/>'s projections</param>
        protected DomainEventHandlerBase(ILoggerFactory loggerFactory, IMapper mapper, IMediator mediator, ICloudEventBus cloudEventBus, IRepository<TAggregate, TKey> aggregates, IRepository<TProjection, TKey> projections)
            : base(loggerFactory, mapper, mediator, cloudEventBus)
        {
            this.Aggregates = aggregates;
            this.Projections = projections;
        }

        /// <summary>
        /// Gets the <see cref="IRepository"/> used to manage the <see cref="IAggregateRoot"/>s to handle the <see cref="IDomainEvent"/>s of
        /// </summary>
        protected IRepository<TAggregate, TKey> Aggregates { get; }

        /// <summary>
        /// Gets the <see cref="IRepository"/> used to manage the <see cref="DomainEventHandlerBase"/>'s projections
        /// </summary>
        protected IRepository<TProjection, TKey> Projections { get; }

        /// <summary>
        /// Gets or reconciles the projection for the <see cref="IAggregateRoot"/> with the specified key
        /// </summary>
        /// <param name="aggregateKey">The id of the <see cref="IAggregateRoot"/> to get the projection for</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>The projection for the <see cref="IAggregateRoot"/> with the specified key</returns>
        protected virtual async Task<TProjection> GetOrReconcileProjectionAsync(TKey aggregateKey, CancellationToken cancellationToken)
        {
            TProjection projection = await this.Projections.FindAsync(aggregateKey, cancellationToken);
            if (projection == null)
            {
                TAggregate aggregate = await this.Aggregates.FindAsync(aggregateKey, cancellationToken);
                if (aggregate == null)
                {
                    this.Logger.LogError("Failed to find an aggregate of type '{aggregateType}' with the specified key '{key}'", typeof(TAggregate), aggregateKey);
                    throw new Exception($"Failed to find an aggregate of type '{typeof(TAggregate)}' with the specified key '{aggregateKey}'");
                }
                projection = await this.ProjectAsync(aggregate, cancellationToken);
                projection = await this.Projections.AddAsync(projection, cancellationToken);
                await this.Projections.SaveChangesAsync(cancellationToken);
            }
            return projection;
        }

        /// <summary>
        /// Projects the specified <see cref="IAggregateRoot"/>
        /// </summary>
        /// <param name="aggregate">The <see cref="IAggregateRoot"/> to project</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>The projected <see cref="IAggregateRoot"/></returns>
        protected virtual async Task<TProjection> ProjectAsync(TAggregate aggregate, CancellationToken cancellationToken)
        {
            return await Task.FromResult(this.Mapper.Map<TProjection>(aggregate));
        }

    }

}
