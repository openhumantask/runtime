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

using Microsoft.Extensions.Options;
using OpenHumanTask.Runtime.Integration.Api;

namespace OpenHumanTask.Runtime.Client
{

    /// <summary>
    /// Defines extensions for <see cref="IServiceCollection"/>s
    /// </summary>
    public static class IServiceCollectionExtensions
    {

        /// <summary>
        /// Adds and configures a new <see cref="IOpenHumanTaskRuntimeApiClient"/> service
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to configure</param>
        /// <param name="setupAction">An <see cref="Action{T}"/> used to setup the <see cref="OpenHumanTaskRuntimeApiClientOptions"/> to use</param>
        /// <returns>The configured <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddOpenHumanTaskRuntimeApi(this IServiceCollection services, Action<OpenHumanTaskRuntimeApiClientOptions> setupAction)
        {
            var options = new OpenHumanTaskRuntimeApiClientOptions();
            setupAction(options);
            services.Configure(setupAction);
            var httpConfig = (IServiceProvider provider, HttpClient http) =>
            {
                var options = provider.GetRequiredService<IOptions<OpenHumanTaskRuntimeApiClientOptions>>().Value;
                http.BaseAddress = options.Server.BaseAddress;
            };
            services.AddSingleton<IOpenHumanTaskRuntimeApiClient, OpenHumanTaskRuntimeApiClient>();
            services.AddSingleton<IHumanTaskTemplateApi, HumanTaskTemplateApi>();
            services.AddSingleton<IHumanTaskApi, HumanTaskApi>();
            var httpClientBuilder = services.AddHttpClient(nameof(IHumanTaskTemplateApi), httpConfig);
            options.HttpClientSetup?.Invoke(httpClientBuilder);
            httpClientBuilder = services.AddHttpClient(nameof(IHumanTaskApi), httpConfig);
            options.HttpClientSetup?.Invoke(httpClientBuilder);
            return services;
        }

    }

}
