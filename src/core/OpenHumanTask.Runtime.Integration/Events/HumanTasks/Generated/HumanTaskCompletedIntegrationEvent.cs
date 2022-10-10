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

/*
 * Copyright © 2022-Present The Open Human Task Specification Authors. All rights reserved.
 * <p>
 * Licensed under the Apache License, Version 2.0(the "License");
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
 */

/* -----------------------------------------------------------------------
 * This file has been automatically generated by a tool
 * -----------------------------------------------------------------------
 */

namespace OpenHumanTask.Runtime.Integration.Events.HumanTasks
{

	/// <summary>
	/// Represents the IDomainEvent fired when a HumanTask has been completed
	/// </summary>
	[DataContract]
	public partial class HumanTaskCompletedIntegrationEvent
		: IntegrationEvent
	{

		/// <summary>
		/// Gets the id of the aggregate that has produced the event
		/// </summary>
		[DataMember(Name = "aggregateId", Order = 1)]
		[Description("Gets the id of the aggregate that has produced the event")]
		public virtual string AggregateId { get; set; }

		/// <summary>
		/// Gets the date and time at which the event has been produced
		/// </summary>
		[DataMember(Name = "createdAt", Order = 2)]
		[Description("Gets the date and time at which the event has been produced")]
		public virtual DateTime CreatedAt { get; set; }

		/// <summary>
		/// The user that has completed the HumanTask
		/// </summary>
		[DataMember(Name = "user", Order = 3)]
		[Description("The user that has completed the HumanTask")]
		public virtual UserReference User { get; set; }

		/// <summary>
		/// The HumanTask's outcome
		/// </summary>
		[DataMember(Name = "outcome", Order = 4)]
		[Description("The HumanTask's outcome")]
		public virtual object Outcome { get; set; }

		/// <summary>
		/// The HumanTask's output
		/// </summary>
		[DataMember(Name = "output", Order = 5)]
		[Description("The HumanTask's output")]
		public virtual object Output { get; set; }

		/// <summary>
		/// The name of the HumanTask's matching completion behavior
		/// </summary>
		[DataMember(Name = "completionBehaviorName", Order = 6)]
		[Description("The name of the HumanTask's matching completion behavior")]
		public virtual string CompletionBehaviorName { get; set; }

    }

}
