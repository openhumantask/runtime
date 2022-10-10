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

namespace OpenHumanTask.Runtime.Integration.Commands.HumanTaskTemplates
{

	/// <summary>
	/// Represents the ICommand used to create a new HumanTaskTemplate
	/// </summary>
	[DataContract]
	public partial class CreateHumanTaskTemplateCommand
		: Command
	{

		/// <summary>
		/// The HumanTaskDefinition of the HumanTaskTemplate to create
		/// </summary>
		[DataMember(Name = "definition", Order = 1)]
		[Description("The HumanTaskDefinition of the HumanTaskTemplate to create")]
		public virtual HumanTaskDefinition Definition { get; set; }

    }

}
