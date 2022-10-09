﻿
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
	/// Represents the IDomainEvent created whenever a new HumanTask has been created
	/// </summary>
	[DataContract]
	public partial class HumanTaskCreatedIntegrationEvent
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
		/// The id of the HumanTask's HumanTaskDefinition
		/// </summary>
		[DataMember(Name = "definitionId", Order = 3)]
		[Description("The id of the HumanTask's HumanTaskDefinition")]
		public virtual string DefinitionId { get; set; }

		/// <summary>
		/// The HumanTask's key
		/// </summary>
		[DataMember(Name = "key", Order = 4)]
		[Description("The HumanTask's key")]
		public virtual string Key { get; set; }

		/// <summary>
		/// The people assigned to the HumanTask
		/// </summary>
		[DataMember(Name = "peopleAssignments", Order = 5)]
		[Description("The people assigned to the HumanTask")]
		public virtual PeopleAssignments PeopleAssignments { get; set; }

		/// <summary>
		/// The HumanTask's priority
		/// </summary>
		[DataMember(Name = "priority", Order = 6)]
		[Description("The HumanTask's priority")]
		public virtual int Priority { get; set; }

		/// <summary>
		/// A boolean indicating whether or not the HumanTask can be skipped
		/// </summary>
		[DataMember(Name = "skipable", Order = 7)]
		[Description("A boolean indicating whether or not the HumanTask can be skipped")]
		public virtual bool Skipable { get; set; }

		/// <summary>
		/// A Dictionary that contains the HumanTask's localized title language/value pairs
		/// </summary>
		[DataMember(Name = "title", Order = 8)]
		[Description("A Dictionary that contains the HumanTask's localized title language/value pairs")]
		public virtual IDictionary<string, string> Title { get; set; }

		/// <summary>
		/// A Dictionary that contains the HumanTask's localized subject language/value pairs
		/// </summary>
		[DataMember(Name = "subject", Order = 9)]
		[Description("A Dictionary that contains the HumanTask's localized subject language/value pairs")]
		public virtual IDictionary<string, string> Subject { get; set; }

		/// <summary>
		/// A Dictionary that contains the HumanTask's localized description language/value pairs
		/// </summary>
		[DataMember(Name = "description", Order = 10)]
		[Description("A Dictionary that contains the HumanTask's localized description language/value pairs")]
		public virtual IDictionary<string, string> Description { get; set; }

		/// <summary>
		/// The HumanTask's input
		/// </summary>
		[DataMember(Name = "input", Order = 11)]
		[Description("The HumanTask's input")]
		public virtual object Input { get; set; }

		/// <summary>
		/// An IEnumerable containing the HumanTask's Subtasks
		/// </summary>
		[DataMember(Name = "subtasks", Order = 12)]
		[Description("An IEnumerable containing the HumanTask's Subtasks")]
		public virtual IEnumerable<Subtask> Subtasks { get; set; }

		/// <summary>
		/// An IEnumerable containing the HumanTask's Attachments
		/// </summary>
		[DataMember(Name = "attachments", Order = 13)]
		[Description("An IEnumerable containing the HumanTask's Attachments")]
		public virtual IEnumerable<Attachment> Attachments { get; set; }

		/// <summary>
		/// An IEnumerable containing the HumanTask's Comments
		/// </summary>
		[DataMember(Name = "comments", Order = 14)]
		[Description("An IEnumerable containing the HumanTask's Comments")]
		public virtual IEnumerable<Comment> Comments { get; set; }

    }

}
