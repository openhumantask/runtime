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
	/// Represents assignments of users to a HumanTask's roles
	/// </summary>
	[DataContract]
	public partial class PeopleAssignments
	{

		/// <summary>
		/// An IReadOnlyCollection containing the HumanTask's potential initiators
		/// </summary>
		[DataMember(Name = "potentialInitiators", Order = 1)]
		[JsonPropertyName("potentialInitiators")]
		[YamlMember(Alias = "potentialInitiators")]
		[Description("An IReadOnlyCollection containing the HumanTask's potential initiators")]
		public virtual List<UserReference> PotentialInitiators { get; set; }

		/// <summary>
		/// The HumanTask's actual initiator
		/// </summary>
		[DataMember(Name = "actualInitiator", Order = 2)]
		[JsonPropertyName("actualInitiator")]
		[YamlMember(Alias = "actualInitiator")]
		[Description("The HumanTask's actual initiator")]
		public virtual UserReference ActualInitiator { get; set; }

		/// <summary>
		/// An IReadOnlyCollection containing the HumanTask's potential owners
		/// </summary>
		[DataMember(Name = "potentialOwners", Order = 3)]
		[JsonPropertyName("potentialOwners")]
		[YamlMember(Alias = "potentialOwners")]
		[Description("An IReadOnlyCollection containing the HumanTask's potential owners")]
		public virtual List<UserReference> PotentialOwners { get; set; }

		/// <summary>
		/// The HumanTask's actual owner
		/// </summary>
		[DataMember(Name = "actualOwner", Order = 4)]
		[JsonPropertyName("actualOwner")]
		[YamlMember(Alias = "actualOwner")]
		[Description("The HumanTask's actual owner")]
		public virtual UserReference ActualOwner { get; set; }

		/// <summary>
		/// An IReadOnlyCollection containing the HumanTask's excluded owners
		/// </summary>
		[DataMember(Name = "excludedOwners", Order = 5)]
		[JsonPropertyName("excludedOwners")]
		[YamlMember(Alias = "excludedOwners")]
		[Description("An IReadOnlyCollection containing the HumanTask's excluded owners")]
		public virtual List<UserReference> ExcludedOwners { get; set; }

		/// <summary>
		/// An IReadOnlyCollection containing the HumanTask's stakeholders
		/// </summary>
		[DataMember(Name = "stakeholders", Order = 6)]
		[JsonPropertyName("stakeholders")]
		[YamlMember(Alias = "stakeholders")]
		[Description("An IReadOnlyCollection containing the HumanTask's stakeholders")]
		public virtual List<UserReference> Stakeholders { get; set; }

		/// <summary>
		/// An IReadOnlyCollection containing the HumanTask's business administrators
		/// </summary>
		[DataMember(Name = "businessAdministrators", Order = 7)]
		[JsonPropertyName("businessAdministrators")]
		[YamlMember(Alias = "businessAdministrators")]
		[Description("An IReadOnlyCollection containing the HumanTask's business administrators")]
		public virtual List<UserReference> BusinessAdministrators { get; set; }

		/// <summary>
		/// An IReadOnlyCollection containing the HumanTask's notification recipients
		/// </summary>
		[DataMember(Name = "notificationRecipients", Order = 8)]
		[JsonPropertyName("notificationRecipients")]
		[YamlMember(Alias = "notificationRecipients")]
		[Description("An IReadOnlyCollection containing the HumanTask's notification recipients")]
		public virtual List<UserReference> NotificationRecipients { get; set; }

		/// <summary>
		/// An IReadOnlyDictionary containing the name/users pairs of the HumanTask's logical people groups
		/// </summary>
		[DataMember(Name = "groups", Order = 9)]
		[JsonPropertyName("groups")]
		[YamlMember(Alias = "groups")]
		[Description("An IReadOnlyDictionary containing the name/users pairs of the HumanTask's logical people groups")]
		public virtual Dictionary<string, List<UserReference>> Groups { get; set; }

    }

}
