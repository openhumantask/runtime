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

using AutoMapper;
using System.Reflection;

namespace OpenHumanTask.Runtime.Application.Mapping
{

    /// <summary>
    /// Represents the application's mapping profile
    /// </summary>
    public class MappingProfile
        : Profile
    {

        /// <summary>
        /// Initializes a new <see cref="MappingProfile"/>
        /// </summary>
        public MappingProfile()
        {
            this.AllowNullCollections = true;
            this.MappingConfigurationTypes = new List<Type>();
            this.Initialize();
            this.ConfigureEntities();
            this.ConfigureCommands();
            this.ConfigureQueries();
            this.ConfigureEvents();
        }

        /// <summary>
        /// Gets a <see cref="List{T}"/> containing the types of all existing <see cref="IMappingConfiguration"/>s
        /// </summary>
        protected List<Type> MappingConfigurationTypes { get; }

        /// <summary>
        /// Initializes the <see cref="MappingProfile"/>
        /// </summary>
        protected void Initialize()
        {
            foreach (var mappingConfigurationType in this.GetType().Assembly
                .GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface && t.IsClass && typeof(IMappingConfiguration).IsAssignableFrom(t)))
            {
                this.MappingConfigurationTypes.Add(mappingConfigurationType);
                this.ApplyConfiguration((IMappingConfiguration)Activator.CreateInstance(mappingConfigurationType, new object[] { })!);
            }
        }

        /// <summary>
        /// Configures entity mappings
        /// </summary>
        protected void ConfigureEntities()
        {
            foreach (Type entityType in new Assembly[] { typeof(HumanTask).Assembly }
                .SelectMany(a => a.GetTypes())
                .Where(t => !t.IsAbstract && !t.IsInterface && t.IsClass))
            {
                var dtoTypeAttribute = entityType.GetCustomAttribute<DataTransferObjectTypeAttribute>();
                if (dtoTypeAttribute != null && !this.MappingConfigurationTypes.Any(t => typeof(IMappingConfiguration<,>).MakeGenericType(entityType, dtoTypeAttribute.Type).IsAssignableFrom(t)))
                    this.CreateMap(entityType, dtoTypeAttribute.Type);
            }
        }

        /// <summary>
        /// Configures command mappings
        /// </summary>
        protected void ConfigureCommands()
        {
            foreach (Type commandType in typeof(MappingProfile).Assembly
                .GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface && t.IsClass && typeof(ICommand).IsAssignableFrom(t)))
            {
                var dtoTypeAttribute = commandType.GetCustomAttribute<DataTransferObjectTypeAttribute>();
                if (dtoTypeAttribute != null && !this.MappingConfigurationTypes.Any(t => typeof(IMappingConfiguration<,>).MakeGenericType(dtoTypeAttribute.Type, commandType).IsAssignableFrom(t)))
                    this.CreateMap(dtoTypeAttribute.Type, commandType);
            }
        }

        /// <summary>
        /// Configures query mappings
        /// </summary>
        protected void ConfigureQueries()
        {
            foreach (Type queryType in typeof(MappingProfile).Assembly
                .GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface && t.IsClass && typeof(IQuery).IsAssignableFrom(t)))
            {
                var dtoTypeAttribute = queryType.GetCustomAttribute<DataTransferObjectTypeAttribute>();
                if (dtoTypeAttribute != null && !this.MappingConfigurationTypes.Any(t => typeof(IMappingConfiguration<,>).MakeGenericType(dtoTypeAttribute.Type, queryType).IsAssignableFrom(t)))
                    this.CreateMap(dtoTypeAttribute.Type, queryType);
            }
        }

        /// <summary>
        /// Configures event mappings
        /// </summary>
        protected void ConfigureEvents()
        {
            foreach (Type eventType in typeof(HumanTask).Assembly
               .GetTypes()
               .Where(t => !t.IsAbstract && !t.IsInterface && t.IsClass && typeof(IDomainEvent).IsAssignableFrom(t)))
            {
                var dtoTypeAttribute = eventType.GetCustomAttribute<DataTransferObjectTypeAttribute>();
                if (dtoTypeAttribute != null && !this.MappingConfigurationTypes.Any(t => typeof(IMappingConfiguration<,>).MakeGenericType(eventType, dtoTypeAttribute.Type).IsAssignableFrom(t)))
                    this.CreateMap(eventType, dtoTypeAttribute.Type);
            }
            this.CreateMap(typeof(IDomainEvent), typeof(IIntegrationEvent))
                .IncludeAllDerived();
        }

    }

}
