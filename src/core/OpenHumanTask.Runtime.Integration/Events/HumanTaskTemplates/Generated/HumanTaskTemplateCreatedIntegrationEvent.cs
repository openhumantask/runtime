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

namespace OpenHumanTask.Runtime.Integration.Events.HumanTaskTemplates
{

	/// <summary>
	/// Represents the IDomainEvent fired whenever a new HumanTaskTemplate has been created
	/// </summary>
	[DataContract]
	public partial class HumanTaskTemplateCreatedIntegrationEvent
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
		/// The user that has created the HumanTaskTemplate
		/// </summary>
		[DataMember(Name = "createdBy", Order = 3)]
		[Description("The user that has created the HumanTaskTemplate")]
		public virtual UserReference CreatedBy { get; set; }

		/// <summary>
		/// The HumanTaskDefinition of the newly created HumanTaskTemplate
		/// </summary>
		[DataMember(Name = "definition", Order = 4)]
		[Description("The HumanTaskDefinition of the newly created HumanTaskTemplate")]
		public virtual HumanTaskDefinition Definition { get; set; }

    }

}
