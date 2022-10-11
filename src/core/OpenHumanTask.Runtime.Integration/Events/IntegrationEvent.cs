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

namespace OpenHumanTask.Runtime.Integration.Events
{

    /// <summary>
    /// Represents the base class for all integration event <see cref="IDataTransferObject"/>s
    /// </summary>
    public abstract class IntegrationEvent
        : IDataTransferObject
    {

        /// <summary>
        /// Gets the id of the aggregate the integration event applies to
        /// </summary>
        public virtual object AggregateId { get; set; }

        /// <summary>
        /// Gets the date and time at which the integration event has been created
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

    }

}
