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

namespace OpenHumanTask.Runtime.Domain.Models
{

    /// <summary>
    /// Represents assignments of users to a <see cref="HumanTask"/>'s roles
    /// </summary>
    [DataTransferObjectType(typeof(Integration.Models.PeopleAssignments))]
    public class PeopleAssignments
    {

        /// <summary>
        /// Initializes a new <see cref="PeopleAssignments"/>
        /// </summary>
        protected PeopleAssignments() { }

        /// <summary>
        /// Initializes a new <see cref="PeopleAssignments"/>
        /// </summary>
        /// <param name="potentialInitiators">An <see cref="IEnumerable{T}"/> containing the <see cref="HumanTask"/>'s potential initiators</param>
        /// <param name="actualInitiator">The <see cref="HumanTask"/>'s actual initiator</param>
        /// <param name="potentialOwners">An <see cref="IEnumerable{T}"/> containing the <see cref="HumanTask"/>'s potential owners</param>
        /// <param name="actualOwner">The <see cref="HumanTask"/>'s actual owner</param>
        /// <param name="excludedOwners">An <see cref="IEnumerable{T}"/> containing the <see cref="HumanTask"/>'s excluded owners</param>
        /// <param name="stakeholders">An <see cref="IEnumerable{T}"/> containing the <see cref="HumanTask"/>'s stakeholders</param>
        /// <param name="businessAdministrators">An <see cref="IEnumerable{T}"/> containing the <see cref="HumanTask"/>'s business administrators</param>
        /// <param name="notificationRecipients">An <see cref="IEnumerable{T}"/> containing the <see cref="HumanTask"/>'s notification recipients</param>
        public PeopleAssignments(UserReference actualInitiator, IEnumerable<UserReference>? potentialInitiators = null, IEnumerable<UserReference>? potentialOwners = null, UserReference? actualOwner = null, IEnumerable<UserReference>? excludedOwners = null,
            IEnumerable<UserReference>? stakeholders = null, IEnumerable<UserReference>? businessAdministrators = null, IEnumerable<UserReference>? notificationRecipients = null)
        {
            if(actualInitiator == null) throw DomainException.ArgumentNull(nameof(actualInitiator));
            this._potentialInitiators = potentialInitiators?.ToList();
            this.ActualInitiator = actualInitiator;
            this._potentialOwners = potentialOwners?.ToList();
            this.ActualOwner = actualOwner;
            this._excludedOwners = excludedOwners?.ToList();
            this._stakeholders = stakeholders?.ToList();
            this._businessAdministrators = businessAdministrators?.ToList();
            this._notificationRecipients = notificationRecipients?.ToList();
        }

        [Newtonsoft.Json.JsonProperty(nameof(PotentialInitiators))]
        [JsonPropertyName(nameof(PotentialInitiators))]
        private List<UserReference>? _potentialInitiators;
        /// <summary>
        /// Gets an <see cref="IReadOnlyCollection{T}"/> containing the <see cref="HumanTask"/>'s potential initiators
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [JsonIgnore]
        public virtual IReadOnlyCollection<UserReference>? PotentialInitiators => this._potentialInitiators?.AsReadOnly();

        /// <summary>
        /// Gets the <see cref="HumanTask"/>'s actual initiator
        /// </summary>
        public virtual UserReference ActualInitiator { get; protected set; } = null!;

        [Newtonsoft.Json.JsonProperty(nameof(PotentialOwners))]
        [JsonPropertyName(nameof(PotentialOwners))]
        private List<UserReference>? _potentialOwners;
        /// <summary>
        /// Gets an <see cref="IReadOnlyCollection{T}"/> containing the <see cref="HumanTask"/>'s potential owners
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [JsonIgnore]
        public virtual IReadOnlyCollection<UserReference>? PotentialOwners => this._potentialOwners?.AsReadOnly();

        /// <summary>
        /// Gets the <see cref="HumanTask"/>'s actual owner
        /// </summary>
        public virtual UserReference? ActualOwner { get; protected set; }

        [Newtonsoft.Json.JsonProperty(nameof(ExcludedOwners))]
        [JsonPropertyName(nameof(ExcludedOwners))]
        private List<UserReference>? _excludedOwners;
        /// <summary>
        /// Gets an <see cref="IReadOnlyCollection{T}"/> containing the <see cref="HumanTask"/>'s excluded owners
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [JsonIgnore]
        public virtual IReadOnlyCollection<UserReference>? ExcludedOwners => this._excludedOwners?.AsReadOnly();

        [Newtonsoft.Json.JsonProperty(nameof(Stakeholders))]
        [JsonPropertyName(nameof(Stakeholders))]
        private List<UserReference>? _stakeholders;
        /// <summary>
        /// Gets an <see cref="IReadOnlyCollection{T}"/> containing the <see cref="HumanTask"/>'s stakeholders
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [JsonIgnore]
        public virtual IReadOnlyCollection<UserReference>? Stakeholders => this._stakeholders?.AsReadOnly();

        [Newtonsoft.Json.JsonProperty(nameof(BusinessAdministrators))]
        [JsonPropertyName(nameof(BusinessAdministrators))]
        private List<UserReference>? _businessAdministrators;
        /// <summary>
        /// Gets an <see cref="IReadOnlyCollection{T}"/> containing the <see cref="HumanTask"/>'s business administrators
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [JsonIgnore]
        public virtual IReadOnlyCollection<UserReference>? BusinessAdministrators => this._businessAdministrators?.AsReadOnly();

        [Newtonsoft.Json.JsonProperty(nameof(NotificationRecipients))]
        [JsonPropertyName(nameof(NotificationRecipients))]
        private List<UserReference>? _notificationRecipients;
        /// <summary>
        /// Gets an <see cref="IReadOnlyCollection{T}"/> containing the <see cref="HumanTask"/>'s notification recipients
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [JsonIgnore]
        public virtual IReadOnlyCollection<UserReference>? NotificationRecipients => this._notificationRecipients?.AsReadOnly();

        /// <summary>
        /// Sets the <see cref="HumanTask"/>'s actual owner 
        /// </summary>
        /// <param name="user">The <see cref="HumanTask"/>'s actual owner </param>
        /// <returns>A boolean indicating whether or not the operation was successfull</returns>
        internal protected virtual bool SetActualOwner(UserReference? user)
        {
            if (this.ActualOwner == user) return false;
            this.ActualOwner = user;
            return true;
        }

        /// <summary>
        /// Adds the specified users to <see cref="PotentialOwners"/>
        /// </summary>
        /// <param name="users">An array containing the users to add</param>
        /// <returns>A boolean indicating whether or not the operation was successfull</returns>
        internal protected virtual bool AddUsersToPotentialOwners(params UserReference[] users)
        {
            if (users == null) throw DomainException.ArgumentNull(nameof(users));
            if (users.Length < 1) return false;
            if (this._potentialOwners == null) this._potentialOwners = new();
            var updated = false;
            foreach (var user in users)
            {
                if (this._potentialOwners.Contains(user)) continue;
                this._potentialOwners.Add(user);
                updated = true;
            }
            return updated;
        }

        /// <summary>
        /// Removes the specified users from <see cref="PotentialOwners"/>
        /// </summary>
        /// <param name="users">An array containing the users to remove</param>
        /// <returns>A boolean indicating whether or not the operation was successfull</returns>
        internal protected virtual bool RemoveUsersFromPotentialOwners(params UserReference[] users)
        {
            if (users == null) throw DomainException.ArgumentNull(nameof(users));
            if (this._potentialOwners == null || users.Length < 1) return false;
            var updated = false;
            foreach (var user in users)
            {
                if (!this._potentialOwners.Contains(user)) continue;
                this._potentialOwners.Remove(user);
                updated = true;
            }
            return updated;
        }

    }

}
