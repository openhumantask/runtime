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

namespace OpenHumanTask.Runtime
{

    /// <summary>
    /// Enumerates all supported human task statuses
    /// </summary>
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum HumanTaskStatus
    {
        /// <summary>
        /// Indicates that the task has been created and is pending assignment
        /// </summary>
        [EnumMember(Value = "created")]
        Created,
        /// <summary>
        /// Indicates that the task has been assigned to multiple potential owners and is pending a claim
        /// </summary>
        [EnumMember(Value = "ready")]
        Ready,
        /// <summary>
        /// Indicates that the task has been assigned to a specific individual or has been claimed by a potential owner
        /// </summary>
        [EnumMember(Value = "reserved")]
        Reserved,
        /// <summary>
        /// Indicates that the task is in progress
        /// </summary>
        [EnumMember(Value = "inProgress")]
        InProgress,
        /// <summary>
        /// Indicates that the task is in progress
        /// </summary>
        [EnumMember(Value = "suspended")]
        Suspended,
        /// <summary>
        /// Indicates that the task has been skipped
        /// </summary>
        [EnumMember(Value = "obsolete")]
        Obsolete,
        /// <summary>
        /// Indicates that the task has been cancelled
        /// </summary>
        [EnumMember(Value = "cancelled")]
        Cancelled,
        /// <summary>
        /// Indicates that the task has faulted
        /// </summary>
        [EnumMember(Value = "faulted")]
        Faulted,
        /// <summary>
        /// Indicates that the task has been completed
        /// </summary>
        [EnumMember(Value = "completed")]
        Completed
    }

}
