
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
	/// Represents an Attachment
	/// </summary>
	[DataContract]
	public partial class Attachment
		: Entity
	{

		/// <summary>
		/// The id of the user that has created the Attachment
		/// </summary>
		[DataMember(Name = "author", Order = 1)]
		[Description("The id of the user that has created the Attachment")]
		public virtual UserReference Author { get; set; }

		/// <summary>
		/// The Attachment's name
		/// </summary>
		[DataMember(Name = "name", Order = 2)]
		[Description("The Attachment's name")]
		public virtual string Name { get; set; }

		/// <summary>
		/// The Attachment's content type
		/// </summary>
		[DataMember(Name = "contentType", Order = 3)]
		[Description("The Attachment's content type")]
		public virtual string ContentType { get; set; }

    }

}
