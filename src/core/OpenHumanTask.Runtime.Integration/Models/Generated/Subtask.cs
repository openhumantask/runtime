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
	/// Represents a HumanTask's subtask
	/// </summary>
	[DataContract]
	public partial class Subtask
		: Entity
	{

		/// <summary>
		/// The id of the Subtask's HumanTaskTemplate
		/// </summary>
		[DataMember(Name = "definitionId", Order = 1)]
		[JsonPropertyName("definitionId")]
		[YamlMember(Alias = "definitionId")]
		[Description("The id of the Subtask's HumanTaskTemplate")]
		public virtual string DefinitionId { get; set; }

    }

}
