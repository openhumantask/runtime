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

using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using OpenHumanTask.Runtime.Integration.Models;
using System.Dynamic;

namespace OpenHumanTask.Runtime.Application.Services
{
    /// <summary>
    /// Represents the default implementation of the <see cref="IEdmModelBuilder"/>
    /// </summary>
    public class EdmModelBuilder
        : IEdmModelBuilder
    {

        /// <inheritdoc/>
        public virtual IEdmModel Build()
        {
            ODataConventionModelBuilder builder = new();
            builder.EnableLowerCamelCase();

            builder.EntitySet<Integration.Models.HumanTaskTemplate>("HumanTaskTemplates").EntityType.HasKey(e => e.Id);
            builder.EntitySet<Integration.Models.HumanTask>("HumanTasks").EntityType.HasKey(e => e.Id);

            builder.AddComplexType(typeof(ExpandoObject));
            builder.AddComplexType(typeof(HumanTaskDefinition));
            builder.AddComplexType(typeof(List<object>));

            return builder.GetEdmModel();
        }

    }

}
