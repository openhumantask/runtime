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

using Neuroglia.Data;

namespace OpenHumanTask.Runtime.Infrastructure.Services
{
    /// <summary>
    /// Defines the fundamentals of a service used to create <see cref="IRepository"/> instances
    /// </summary>
    public interface IRepositoryFactory
    {

        /// <summary>
        /// Creates a new <see cref="IRepository"/>
        /// </summary>
        /// <param name="entityType">The type of entity to create the <see cref="IRepository"/> for</param>
        /// <param name="keyType">The type of key used to uniquely identify the entities managed by the <see cref="IRepository"/> to create</param>
        /// <param name="dataModelType">The type of the application data model to create a new <see cref="IRepository"/> for</param>
        /// <returns>A new <see cref="IRepository"/></returns>
        IRepository CreateRepository(Type entityType, Type keyType, ApplicationDataModelType dataModelType);

    }

}
