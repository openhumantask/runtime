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

using OpenHumanTask.Runtime.Domain.Properties;
using System.Data;

namespace OpenHumanTask.Runtime.Domain
{

    /// <summary>
    /// Defines human task related <see cref="DomainException"/>s
    /// </summary>
    public static class HumanTaskDomainExceptions
    {

        /// <summary>
        /// Creates a new <see cref="DomainException"/> thrown when a user is not in the specified <see cref="GenericHumanRole"/>(s)
        /// </summary>
        /// <param name="user">The <see cref="UserReference"/> that is trying to perform an operation for which she requires to be in the specified <see cref="GenericHumanRole"/>(s)</param>
        /// <param name="roles">An <see cref="IEnumerable{T}"/> containing the <see cref="GenericHumanRole"/> alternative(s) the specified user needs to be in</param>
        /// <param name="operation">The operation that is forbidden to uses that are not in the specified <see cref="GenericHumanRole"/>(s)</param>
        /// <returns>A new <see cref="DomainException"/></returns>
        public static DomainException UserNotInRoles(UserReference user, string operation, GenericHumanRole roles)
        {
            if(user == null) throw new ArgumentNullException(nameof(user));
            return new DomainException(Neuroglia.StringExtensions.Format
            (
                DomainExceptionResources.user_not_in_role, 
                user.Name, 
                string.Join(", ", EnumHelper.GetFlags(roles)
                    .Select(r => EnumHelper.Stringify(r))
                    .Except(new string[] { EnumHelper.Stringify(GenericHumanRole.None) })),
                operation
            ));
        }

        /// <summary>
        /// Creates a new <see cref="DomainException"/> thrown when trying to skip an unskipable <see cref="HumanTask"/>
        /// </summary>
        /// <param name="task">The ownerless <see cref="HumanTask"/></param>
        /// <returns>A new <see cref="DomainException"/></returns>
        public static DomainException UnskipableHumanTask(HumanTask task)
        {
            if (task == null) throw new ArgumentNullException(nameof(task));
            return new DomainException(Neuroglia.StringExtensions.Format(DomainExceptionResources.unskipable_human_task, task.Id));
        }

        /// <summary>
        /// Creates a new <see cref="DomainException"/> thrown when trying to perform an owner operation on an ownerless <see cref="HumanTask"/>
        /// </summary>
        /// <param name="task">The ownerless <see cref="HumanTask"/></param>
        /// <returns>A new <see cref="DomainException"/></returns>
        public static DomainException OwnerlessHumanTask(HumanTask task)
        {
            if (task == null) throw new ArgumentNullException(nameof(task));
            return new DomainException(Neuroglia.StringExtensions.Format(DomainExceptionResources.ownerless_human_task, task.Id));
        }

    }

}
