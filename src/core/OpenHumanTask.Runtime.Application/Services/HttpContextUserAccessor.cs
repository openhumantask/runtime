﻿// Copyright © 2022-Present The Open Human Task Specification Authors. All rights reserved.
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

using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace OpenHumanTask.Runtime.Application.Services
{
    /// <summary>
    /// Represents the default implementation of the <see cref="IUserAccessor"/> interface
    /// </summary>
    public class HttpContextUserAccessor
        : IUserAccessor
    {

        /// <summary>
        /// Initializes a new <see cref="HttpContextUserAccessor"/>
        /// </summary>
        /// <param name="httpContextAccessor">The service used to access the current <see cref="Microsoft.AspNetCore.Http.HttpContext"/></param>
        public HttpContextUserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            this.HttpContext = httpContextAccessor.HttpContext;
        }

        /// <summary>
        /// Gets the current <see cref="Microsoft.AspNetCore.Http.HttpContext"/>
        /// </summary>
        public HttpContext HttpContext { get; }

        /// <inheritdoc/>
        public ClaimsPrincipal User => this.HttpContext.User;

    }

}
