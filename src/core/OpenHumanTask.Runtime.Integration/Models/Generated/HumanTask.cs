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

namespace OpenHumanTask.Runtime.Integration.Models
{

	/// <summary>
	/// Represents a human-computer interaction
	/// </summary>
	[DataContract]
	[Queryable]
	public partial class HumanTask
		: AggregateRoot
	{

		/// <summary>
		/// The id of the HumanTask's HumanTaskDefinition
		/// </summary>
		[DataMember(Name = "definitionId", Order = 1)]
		[Description("The id of the HumanTask's HumanTaskDefinition")]
		public virtual string DefinitionId { get; set; }

		/// <summary>
		/// The HumanTask's key
		/// </summary>
		[DataMember(Name = "key", Order = 2)]
		[Description("The HumanTask's key")]
		public virtual string Key { get; set; }

		/// <summary>
		/// The HumanTask's status
		/// </summary>
		[DataMember(Name = "status", Order = 3)]
		[Description("The HumanTask's status")]
		public virtual HumanTaskStatus Status { get; set; }

		/// <summary>
		/// The people assigned to the HumanTask
		/// </summary>
		[DataMember(Name = "peopleAssignments", Order = 4)]
		[Description("The people assigned to the HumanTask")]
		public virtual PeopleAssignments PeopleAssignments { get; set; }

		/// <summary>
		/// The HumanTask's input
		/// </summary>
		[DataMember(Name = "input", Order = 5)]
		[Description("The HumanTask's input")]
		public virtual object Input { get; set; }

		/// <summary>
		/// The HumanTask's output
		/// </summary>
		[DataMember(Name = "output", Order = 6)]
		[Description("The HumanTask's output")]
		public virtual object Output { get; set; }

		/// <summary>
		/// An IReadOnlyCollection containing the HumanTask's Subtasks
		/// </summary>
		[DataMember(Name = "subtasks", Order = 7)]
		[Description("An IReadOnlyCollection containing the HumanTask's Subtasks")]
		public virtual IReadOnlyCollection<Subtask> Subtasks { get; set; }

		/// <summary>
		/// An IReadOnlyCollection containing the HumanTask's Attachments
		/// </summary>
		[DataMember(Name = "attachments", Order = 8)]
		[Description("An IReadOnlyCollection containing the HumanTask's Attachments")]
		public virtual IReadOnlyCollection<Attachment> Attachments { get; set; }

		/// <summary>
		/// An IReadOnlyCollection containing the HumanTask's Comments
		/// </summary>
		[DataMember(Name = "comments", Order = 9)]
		[Description("An IReadOnlyCollection containing the HumanTask's Comments")]
		public virtual IReadOnlyCollection<Comment> Comments { get; set; }

    }

}
