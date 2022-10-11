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

namespace OpenHumanTask.Runtime.Infrastructure.Plugins
{

    /// <summary>
    /// Defines the base contract of all plugins
    /// </summary>
    public interface IPlugin
        : IDisposable, IAsyncDisposable
    {

        /// <summary>
        /// Initialzes the <see cref="IPlugin"/>
        /// </summary>
        /// <param name="stoppingToken">A <see cref="CancellationToken"/> used to manage the <see cref="IPlugin"/>'s lifetime</param>
        /// <returns>A new awaitable <see cref="ValueTask"/></returns>
        ValueTask InitializeAsync(CancellationToken stoppingToken);

    }

}
