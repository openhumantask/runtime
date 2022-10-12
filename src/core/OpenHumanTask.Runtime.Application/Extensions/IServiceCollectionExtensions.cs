/*
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

using CloudNative.CloudEvents.SystemTextJson;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Adapters;
using Neuroglia.Data.Expressions.JQ;
using Neuroglia.Serialization;
using OpenHumanTask.Runtime.Application.Commands.Generic;
using OpenHumanTask.Runtime.Application.Mapping;
using OpenHumanTask.Runtime.Application.Queries.Generic;
using OpenHumanTask.Runtime.Application.Services;
using OpenHumanTask.Sdk;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text.Json.Serialization;

namespace OpenHumanTask.Runtime.Application
{

    /// <summary>
    /// Defines extensions for <see cref="IServiceCollection"/>s
    /// </summary>
    public static class IServiceCollectionExtensions
    {

        /// <summary>
        /// Adds and configures the services required by the application
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to configure</param>
        /// <param name="configuration">The current <see cref="IConfiguration"/></param>
        /// <returns>The configured <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            var mapperAssemblies = new Assembly[] { typeof(MappingProfile).Assembly };
            var writeModelTypes = TypeCacheUtil.FindFilteredTypes("syn:models-write", t => t.IsClass && !t.IsAbstract && typeof(IAggregateRoot).IsAssignableFrom(t), typeof(HumanTask).Assembly).ToList();
            var readModelTypes = writeModelTypes
                .Where(t => t.TryGetCustomAttribute<DataTransferObjectTypeAttribute>(out _))
                .Select(t => t.GetCustomAttribute<DataTransferObjectTypeAttribute>()!.Type)
                .ToList();
            readModelTypes.AddRange(TypeCacheUtil.FindFilteredTypes("syn:models-read", t => t.IsClass && !t.IsAbstract && t.TryGetCustomAttribute<ReadModelAttribute>(out _)));
            readModelTypes = readModelTypes.Distinct().ToList();

            services.Configure<ApplicationOptions>(configuration);
            services.AddLogging();
            services.AddMediator(builder =>
            {
                builder.ScanAssembly(typeof(IServiceCollectionExtensions).Assembly);
                builder.UseDefaultPipelineBehavior(typeof(DomainExceptionHandlingMiddleware<,>));
                builder.UseDefaultPipelineBehavior(typeof(FluentValidationMiddleware<,>));
            });
            services.AddMapper(mapperAssemblies.ToArray());
            services.AddSingleton<IJsonPatchMetadataProvider, JsonPatchMetadataProvider>();
            services.AddScoped<IObjectAdapter, AggregateObjectAdapter>();
            services.AddTransient<IEdmModelBuilder, EdmModelBuilder>();
            services.AddTransient(provider => provider.GetRequiredService<IEdmModelBuilder>().Build());
            services.AddTransient<IODataQueryOptionsParser, ODataQueryOptionsParser>();
            services.AddJsonSerializer(json =>
            {
                json.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
            });
            services.AddGenericQueryHandlers();
            services.AddGenericCommandHandlers();
            services.AddSingleton<CloudEventFormatter, JsonEventFormatter>();
            services.AddCloudEventBus(builder =>
            {
                builder.WithBrokerUri(new("https://test.com")); //todo
            });
            services.AddOpenHumanTask();
            services.AddAuthorization();
            services.AddJQExpressionEvaluator(options => options.UseSerializer<JsonSerializer>());
            services.AddHttpContextAccessor();
            services.AddScoped<IUserAccessor, HttpContextUserAccessor>();
            services.AddSingleton<PluginManager>();
            services.AddSingleton<IPluginManager>(provider => provider.GetRequiredService<PluginManager>());
            services.AddSingleton<IHostedService>(provider => provider.GetRequiredService<PluginManager>());
            services.AddSingleton<IUserManager, InMemoryUserManager>();
            services.AddScoped<IRepositoryFactory, PluginBasedRepositoryFactory>();
            services.AddRepositories(writeModelTypes, ServiceLifetime.Scoped, ApplicationDataModelType.WriteModel);
            services.AddRepositories(readModelTypes, ServiceLifetime.Scoped,  ApplicationDataModelType.ReadModel);

            return services;
        }

        private static IServiceCollection AddGenericQueryHandlers(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            foreach (Type queryableType in TypeCacheUtil.FindFilteredTypes("nqt", t => t.TryGetCustomAttribute<QueryableAttribute>(out _), typeof(QueryableAttribute).Assembly))
            {
                var keyType = queryableType.GetGenericType(typeof(IIdentifiable<>)).GetGenericArguments().First();
                var queryType = typeof(FindByKeyQuery<,>).MakeGenericType(queryableType, keyType);
                var resultType = typeof(IOperationResult<>).MakeGenericType(queryableType);
                var handlerServiceType = typeof(IRequestHandler<,>).MakeGenericType(queryType, resultType);
                var handlerImplementationType = typeof(FindByKeyQueryHandler<,>).MakeGenericType(queryableType, keyType);
                services.Add(new ServiceDescriptor(handlerServiceType, handlerImplementationType, serviceLifetime));
                services.Add(new ServiceDescriptor(typeof(IMiddleware<,>).MakeGenericType(queryType, resultType), typeof(DomainExceptionHandlingMiddleware<,>).MakeGenericType(queryType, resultType), serviceLifetime));

                queryType = typeof(FilterQuery<>).MakeGenericType(queryableType);
                resultType = typeof(IOperationResult<>).MakeGenericType(typeof(List<>).MakeGenericType(queryableType));
                handlerServiceType = typeof(IRequestHandler<,>).MakeGenericType(queryType, resultType);
                handlerImplementationType = typeof(FilterQueryHandler<>).MakeGenericType(queryableType);
                services.Add(new ServiceDescriptor(handlerServiceType, handlerImplementationType, serviceLifetime));
                services.Add(new ServiceDescriptor(typeof(IMiddleware<,>).MakeGenericType(queryType, resultType), typeof(DomainExceptionHandlingMiddleware<,>).MakeGenericType(queryType, resultType), serviceLifetime));
            }
            return services;
        }

        private static IServiceCollection AddGenericCommandHandlers(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            foreach (Type aggregateType in TypeCacheUtil.FindFilteredTypes("domain:aggregates", t => t.IsClass && !t.IsAbstract && typeof(IAggregateRoot).IsAssignableFrom(t), typeof(HumanTask).Assembly))
            {
                Type dtoType = aggregateType.GetCustomAttribute<DataTransferObjectTypeAttribute>()!.Type;
                Type keyType = aggregateType.GetGenericType(typeof(IIdentifiable<>)).GetGenericArguments().First();
                Type commandType;
                Type resultType;
                Type handlerServiceType;
                Type handlerImplementationType;

                //if (aggregateType.TryGetCustomAttribute(out PatchableAttribute patchableAttribute))
                //{
                //    commandType = typeof(PatchCommand<,,>).MakeGenericType(aggregateType, dtoType, keyType);
                //    resultType = typeof(IOperationResult<>).MakeGenericType(dtoType);
                //    handlerServiceType = typeof(IRequestHandler<,>).MakeGenericType(commandType, resultType);
                //    handlerImplementationType = typeof(PatchCommandHandler<,,>).MakeGenericType(aggregateType, dtoType, keyType);
                //    services.Add(new ServiceDescriptor(handlerServiceType, handlerImplementationType, serviceLifetime));
                //    services.Add(new ServiceDescriptor(typeof(IMiddleware<,>).MakeGenericType(commandType, resultType), typeof(DomainExceptionHandlingMiddleware<,>).MakeGenericType(commandType, resultType), serviceLifetime));
                //}

                if (typeof(IDeletable).IsAssignableFrom(aggregateType))
                {
                    commandType = typeof(DeleteCommand<,>).MakeGenericType(aggregateType, keyType);
                    resultType = typeof(IOperationResult);
                    handlerServiceType = typeof(IRequestHandler<,>).MakeGenericType(commandType, resultType);
                    handlerImplementationType = typeof(DeleteCommandHandler<,>).MakeGenericType(aggregateType, keyType);
                    services.Add(new ServiceDescriptor(handlerServiceType, handlerImplementationType, serviceLifetime));
                    services.Add(new ServiceDescriptor(typeof(IMiddleware<,>).MakeGenericType(commandType, resultType), typeof(DomainExceptionHandlingMiddleware<,>).MakeGenericType(commandType, resultType), serviceLifetime));
                }
            }
            return services;
        }

        private static IServiceCollection AddRepository(this IServiceCollection services, Type entityType, ServiceLifetime lifetime, ApplicationDataModelType modelType)
        {
            var keyType = entityType.GetGenericType(typeof(IIdentifiable<>)).GetGenericArguments()[0];
            var genericRepositoryType = typeof(IRepository<,>).MakeGenericType(entityType, keyType);
            services.Add(new(genericRepositoryType, provider => provider.GetRequiredService<IRepositoryFactory>().CreateRepository(entityType, modelType), lifetime));
            services.Add(new(typeof(IRepository<>).MakeGenericType(entityType), provider => provider.GetRequiredService(genericRepositoryType), lifetime));
            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services, IEnumerable<Type> entityTypes, ServiceLifetime lifetime, ApplicationDataModelType modelType)
        {
            foreach (Type entityType in entityTypes)
            {
                services.AddRepository(entityType, lifetime, modelType);
            }
            return services;
        }

    }

}
