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

namespace OpenHumanTask.Runtime.Application.Configuration
{
    /// <summary>
    /// Represents the options used to configure the application's cloud eventss
    /// </summary>
    public class CloudEventOptions
    {

        /// <summary>
        /// Gets/sets the options used to configure the sink to post cloud events to
        /// </summary>
        public virtual CloudEventSinkOptions Sink { get; set; } = new();

    }

}
