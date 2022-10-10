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

using IdentityModel;
using System.Security;
using System.Security.Claims;

namespace OpenHumanTask.Runtime.Domain.Models
{

    /// <summary>
    /// Represents a user reference
    /// </summary>
    [DataTransferObjectType(typeof(Integration.Models.UserReference))]
    public class UserReference
    {

        /// <summary>
        /// Initializes a new <see cref="UserReference"/>
        /// </summary>
        protected UserReference() { }

        /// <summary>
        /// Initializes a new <see cref="UserReference"/>
        /// </summary>
        /// <param name="id">The referenced user's id</param>
        /// <param name="name">The referenced user's name</param>
        public UserReference(string id, string? name = null)
        {
            if (string.IsNullOrWhiteSpace(id)) throw DomainException.ArgumentNullOrWhitespace(nameof(id));
            this.Id = id;
            this.Name = name;
        }

        /// <summary>
        /// Gets the referenced user's id
        /// </summary>
        public virtual string Id { get; protected set; } = null!;

        /// <summary>
        /// Gets the referenced user's name
        /// </summary>
        public virtual string? Name { get; protected set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj) => this.Equals(obj as UserReference);

        /// <summary>
        /// Determines whether or not the <see cref="UserReference"/> equals the specified <see cref="UserReference"/>
        /// </summary>
        /// <param name="reference"></param>
        /// <returns>A boolean indicating whether or not the <see cref="UserReference"/> equals the specified <see cref="UserReference"/></returns>
        public bool Equals(UserReference? reference)
        {
            if (reference is null) return false;
            if (object.ReferenceEquals(this, reference)) return true;
            if (this.GetType() != reference.GetType()) return false;
            return this.Id.Equals(reference.Id, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <inheritdoc/>
        public override int GetHashCode() => this.Id.GetHashCode();

        /// <inheritdoc/>
        public override string ToString() => this.Id;

        /// <inheritdoc/>
        public static bool operator ==(UserReference? reference1, UserReference? reference2)
        {
            if (reference1 is null)
            {
                if (reference2 is null) return true;
                return false;
            }
            return reference1.Equals(reference2);
        }

        /// <inheritdoc/>
        public static bool operator !=(UserReference? reference1, UserReference? reference2) => !(reference1 == reference2);

        /// <inheritdoc/>
        public static implicit operator string?(UserReference? reference) => reference?.Id;

        /// <inheritdoc/>
        public static implicit operator UserReference?(string? reference) => string.IsNullOrWhiteSpace(reference) ? null : new(reference);

        /// <summary>
        /// Creates a new <see cref="UserReference"/> based on the specified <see cref="ClaimsPrincipal"/>
        /// </summary>
        /// <param name="user">The <see cref="ClaimsPrincipal"/> to create a new <see cref="UserReference"/> for</param>
        public static implicit operator UserReference(ClaimsPrincipal user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if(user.Identity == null || !user.Identity.IsAuthenticated || user.HasClaim(c => c.Type.Equals(JwtClaimTypes.Subject, StringComparison.InvariantCultureIgnoreCase))) 
                throw new ArgumentException(nameof(user), new SecurityException($"The specified user is not authenticated or does not defined the required claim '{JwtClaimTypes.Subject}"));
            var id = user.FindFirst(JwtClaimTypes.Subject)?.Value;
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException(nameof(user), new SecurityException($"The specified user does not defined the required claim '{JwtClaimTypes.Subject}"));
            var name = user.Identity.Name;
            return new(id, name);
        }

        /// <summary>
        /// Creates a new <see cref="UserReference"/> based on the specified <see cref="ClaimsIdentity"/>
        /// </summary>
        /// <param name="user">The <see cref="ClaimsIdentity"/> to create a new <see cref="UserReference"/> for</param>
        public static implicit operator UserReference(ClaimsIdentity user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (!user.IsAuthenticated || user.HasClaim(c => c.Type.Equals(JwtClaimTypes.Subject, StringComparison.InvariantCultureIgnoreCase)))
                throw new ArgumentException(nameof(user), new SecurityException($"The specified user is not authenticated or does not defined the required claim '{JwtClaimTypes.Subject}"));
            var id = user.FindFirst(JwtClaimTypes.Subject)?.Value;
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException(nameof(user), new SecurityException($"The specified user does not defined the required claim '{JwtClaimTypes.Subject}"));
            var name = user.Name;
            return new(id, name);
        }

    }

}
