﻿/*
 * Copyright © 2022-Present The Open Human Task Runtime Authors
 * <p>
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * <p>
 * http://www.apache.org/licenses/LICENSE-2.0
 * <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

using OpenHumanTask.Runtime.Application.Services;

namespace OpenHumanTask.Runtime.Application.Commands.Generic
{

    /// <summary>
    /// Represents the <see cref="ICommand"/> used to delete an existing <see cref="IAggregateRoot"/> by id
    /// </summary>
    /// <typeparam name="TAggregate">The type of the <see cref="IAggregateRoot"/> to delete</typeparam>
    /// <typeparam name="TKey">The type of id used to uniquely identify the <see cref="IAggregateRoot"/> to delete</typeparam>
    public class DeleteCommand<TAggregate, TKey>
        : Command
        where TAggregate : class, IIdentifiable<TKey>, IDeletable
        where TKey : IEquatable<TKey>
    {

        /// <summary>
        /// Initializes a new <see cref="DeleteCommand{TAggregate, TKey}"/>
        /// </summary>
        protected DeleteCommand()
        {
            this.Id = default!;
        }

        /// <summary>
        /// Initializes a new <see cref="DeleteCommand{TAggregate, TKey}"/>
        /// </summary>
        /// <param name="id">The id of the entity to delete</param>
        public DeleteCommand(TKey id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets the id of the entity to delete
        /// </summary>
        public virtual TKey Id { get; protected set; }

    }

    /// <summary>
    /// Represents the service used to handle <see cref="DeleteCommand{TEntity, TKey}"/> instances
    /// </summary>
    /// <typeparam name="TAggregate">The type of the <see cref="IAggregateRoot"/> to delete</typeparam>
    /// <typeparam name="TKey">The key used to uniquely identify the <see cref="IAggregateRoot"/> to delete</typeparam>
    public class DeleteCommandHandler<TAggregate, TKey>
        : CommandHandlerBase,
        ICommandHandler<DeleteCommand<TAggregate, TKey>>
        where TAggregate : class, IIdentifiable<TKey>, IDeletable
        where TKey : IEquatable<TKey>
    {

        /// <summary>
        /// Initializes a new <see cref="DeleteCommandHandler{TAggregate, TKey}"/>
        /// </summary>
        /// <param name="loggerFactory">The service used to create <see cref="ILogger"/>s.</param>
        /// <param name="mediator">The service used to mediate calls.</param>
        /// <param name="mapper">The service used to map objects.</param>
        /// <param name="userAccessor">The service used to access the current user</param>
        /// <param name="repository">The <see cref="IRepository"/> used to manage the entity to delete</param>
        public DeleteCommandHandler(ILoggerFactory loggerFactory, IMediator mediator, IMapper mapper, IUserAccessor userAccessor, IRepository<TAggregate, TKey> repository)
            : base(loggerFactory, mediator, mapper, userAccessor)
        {
            this.Repository = repository;
        }

        /// <summary>
        /// Gets the <see cref="IRepository"/> used to manage the entity to delete
        /// </summary>
        protected IRepository<TAggregate, TKey> Repository { get; }

        /// <inheritdoc/>
        public virtual async Task<IOperationResult> HandleAsync(DeleteCommand<TAggregate, TKey> command, CancellationToken cancellationToken = default)
        {
            if (!await this.Repository.ContainsAsync(command.Id, cancellationToken))
                throw DomainException.NullReference(typeof(TAggregate), command.Id);
            var aggregate = await this.Repository.FindAsync(command.Id, cancellationToken);
            if (aggregate == null)
                throw DomainException.NullReference(typeof(TAggregate), command.Id);
            aggregate.Delete(this.UserAccessor.User);
            await this.Repository.UpdateAsync(aggregate, cancellationToken);
            await this.Repository.RemoveAsync(aggregate, cancellationToken);
            await this.Repository.SaveChangesAsync(cancellationToken);
            return this.Ok();
        }

    }

}
