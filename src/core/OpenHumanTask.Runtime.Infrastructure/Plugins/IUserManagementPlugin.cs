﻿/*
 * Copyright © 2022-Present The Open Human Task Runtime Authors
 * <p>
 * Licensed under the Apache License, Version 2.0 (the "License");
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
 *
 */

using OpenHumanTask.Runtime.Infrastructure.Services;

namespace OpenHumanTask.Runtime.Infrastructure.Plugins
{

    /// <summary>
    /// Defines the fundamentals of an <see cref="IPlugin"/> used to provide user management related services
    /// </summary>
    public interface IUserManagementPlugin
        : IPlugin
    {

        /// <summary>
        /// Creates a new <see cref="IUserManager"/>
        /// </summary>
        /// <returns>A new <see cref="IUserManager"/></returns>
        IUserManager CreateUserManager();

    }

}
