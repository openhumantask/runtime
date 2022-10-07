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
    /// Represents the base class for all services used to handle <see cref="IDomainEvent"/>s
    /// </summary>
    public abstract class DomainEventHandlerBase
    {

        /// <summary>
        /// Initializes a new <see cref="DomainEventHandlerBase"/>
        /// </summary>
        /// <param name="loggerFactory">The service used to create <see cref="ILogger"/>s.</param>
        /// <param name="mediator">The service used to mediate calls.</param>
        /// <param name="mapper">The service used to map objects.</param>
        protected DomainEventHandlerBase(ILoggerFactory loggerFactory, IMediator mediator, IMapper mapper)
        {
            this.Logger = loggerFactory.CreateLogger(this.GetType());
            this.Mediator = mediator;
            this.Mapper = mapper;
        }

        /// <summary>
        /// Gets the service used to perform logging.
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Gets the service used to mediate calls.
        /// </summary>
        protected IMediator Mediator { get; }

        /// <summary>
        /// Gets the service used to map objects.
        /// </summary>
        protected IMapper Mapper { get; }

    }

}
