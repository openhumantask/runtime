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

namespace OpenHumanTask.Runtime.Application.Queries
{

    /// <summary>
    /// Represents the base class for all service used to handle queries
    /// </summary>
    /// <typeparam name="TEntity">The type of entity to query</typeparam>
    public abstract class QueryHandlerBase<TEntity>
        where TEntity : class, IIdentifiable
    {

        /// <summary>
        /// Initializes a new <see cref="QueryHandlerBase{TEntity, TKey}"/>
        /// </summary>
        /// <param name="loggerFactory">The service used to create <see cref="ILogger"/>s</param>
        /// <param name="mediator">The service used to mediate calls</param>
        /// <param name="mapper">The service used to map objects</param>
        /// <param name="repository">The <see cref="IRepository{TEntity}"/> to query</param>
        protected QueryHandlerBase(ILoggerFactory loggerFactory, IMediator mediator, IMapper mapper,  IRepository<TEntity> repository)
        {
            this.Logger = loggerFactory.CreateLogger(this.GetType());
            this.Mediator = mediator;
            this.Mapper = mapper;
         
            this.Repository = repository;
        }

        /// <summary>
        /// Gets the service used to perform logging
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Gets the service used to mediate calls
        /// </summary>
        protected IMediator Mediator { get; }

        /// <summary>
        /// Gets the service used to map objects
        /// </summary>
        protected IMapper Mapper { get; }

        /// <summary>
        /// Gets the <see cref="IRepository{TEntity}"/> to query
        /// </summary>
        protected IRepository<TEntity> Repository { get; }

    }

    /// <summary>
    /// Represents the base class for all service used to handle queries
    /// </summary>
    /// <typeparam name="TEntity">The type of entity to query</typeparam>
    /// <typeparam name="TKey">The type of the key of the entity to query</typeparam>
    public abstract class QueryHandlerBase<TEntity, TKey>
        : QueryHandlerBase<TEntity>
        where TEntity : class, IIdentifiable<TKey>
        where TKey : IEquatable<TKey>
    {

        /// <summary>
        /// Initializes a new <see cref="QueryHandlerBase{TEntity, TKey}"/>
        /// </summary>
        /// <param name="loggerFactory">The service used to create <see cref="ILogger"/>s</param>
        /// <param name="mediator">The service used to mediate calls</param>
        /// <param name="mapper">The service used to map objects</param>
        /// <param name="repository">The <see cref="IRepository{TEntity, TKey}"/> to query</param>
        protected QueryHandlerBase(ILoggerFactory loggerFactory, IMediator mediator, IMapper mapper, IRepository<TEntity, TKey> repository)
            : base(loggerFactory, mediator, mapper, repository)
        {

        }

        /// <summary>
        /// Gets the <see cref="IRepository{TEntity, TKey}"/> to query
        /// </summary>
        protected new IRepository<TEntity, TKey> Repository => (IRepository<TEntity, TKey>)base.Repository;

    }

}
