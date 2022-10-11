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

namespace OpenHumanTask.Runtime.Integration.Commands.PeopleAssignments
{

	/// <summary>
	/// Represents the ICommand used to resolve PeopleAssignmentsDefinitions
	/// </summary>
	[DataContract]
	public partial class ResolvePeopleAssignmentsCommand
		: Command
	{

		/// <summary>
		/// The PeopleAssignmentsDefinition to resolve
		/// </summary>
		[DataMember(Name = "peopleAssignments", Order = 1)]
		[Description("The PeopleAssignmentsDefinition to resolve")]
		public virtual PeopleAssignmentsDefinition PeopleAssignments { get; set; }

		/// <summary>
		/// The HumanTaskRuntimeContext for which to resolve the specified PeopleAssignmentsDefinition
		/// </summary>
		[DataMember(Name = "context", Order = 2)]
		[Description("The HumanTaskRuntimeContext for which to resolve the specified PeopleAssignmentsDefinition")]
		public virtual HumanTaskRuntimeContext Context { get; set; }

    }

}
