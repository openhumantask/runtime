using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OpenHumanTask.Runtime.IntegrationTests.Fixtures;
using OpenHumanTask.Runtime.IntegrationTests.Services;
using System.Globalization;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;

namespace OpenHumanTask.Runtime.IntegrationTests.Cases.Api
{

    public abstract class ApiControllerTests<TController>
        : IClassFixture<ApiFactoryFixture>
        where TController : ControllerBase
    {

        protected ApiControllerTests(ApiFactoryFixture apiFixture)
        {
            this.ApiFixture = apiFixture;
            this.LinkGenerator = this.Services.GetRequiredService<LinkGenerator>();
            this.IntegrationTestData = this.Services.GetRequiredService<IntegrationTestData>(); ;
        }

        protected ApiFactoryFixture ApiFixture { get; }

        protected IServiceProvider Services => this.ApiFixture.Services;

        protected LinkGenerator LinkGenerator { get; }

        protected IntegrationTestData IntegrationTestData { get; }

        protected ClaimsPrincipal? User { get; private set; }

        protected HttpClient CreateClient() => this.User == null ? this.ApiFixture.CreateClient() : this.ApiFixture.CreateAuthenticatedClient(this.User);

        protected virtual ApiControllerTests<TController> ActAs(ClaimsPrincipal user) 
        {
            this.User = user;
            return this;
        }

        internal protected virtual async Task<HttpResponseMessage> PostAsJsonAsync<TValue>(string actionName, TValue value, object? routeValues = null)
        {
            var url = this.BuildUrl<TController>(actionName, routeValues);
            using var httpClient = this.CreateClient();
            var json = JsonSerializer.Serialize(value);
            return await httpClient.PostAsJsonAsync(url, value);
        }

        internal protected virtual async Task<HttpResponseMessage> GetAsync(string actionName, object? routeValues = null)
        {
            var url = this.BuildUrl<TController>(actionName, routeValues);
            using var httpClient = this.CreateClient();
            return await httpClient.GetAsync(url);
        }

        internal protected virtual async Task<TResult?> GetAsync<TResult>(string actionName, object? routeValues = null)
        {
            var url = this.BuildUrl<TController>(actionName, routeValues);
            using var httpClient = this.CreateClient();
            using var response = await httpClient.GetAsync(url);
            var json = await response.Content?.ReadAsStringAsync()!;
            response.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<TResult>(json);
        }

        internal protected virtual async Task<HttpResponseMessage> DeleteAsync(string actionName, object? routeValues = null)
        {
            var url = this.BuildUrl<TController>(actionName, routeValues);
            using var httpClient = this.CreateClient();
            return await httpClient.DeleteAsync(url);
        }

        /// <summary>
        /// Builds a request url based on the specified <see cref="ControllerBase"/>, action and route values
        /// </summary>
        /// <typeparam name="TController">The type of <see cref="ControllerBase"/> to request</typeparam>
        /// <param name="action">The action to request</param>
        /// <param name="routeValues">An object representing the route values of the action to request</param>
        /// <returns>The resulting request url</returns>
        protected string BuildUrl<TController>(string action, object? routeValues = null)
            where TController : ControllerBase
        {
            var url = this.LinkGenerator.GetPathByAction(action, typeof(TController).Name.Replace("Controller", string.Empty), routeValues)!;
            string cultureQueryParam = $"ui-culture={CultureInfo.CurrentCulture.Name}";
            if (!string.IsNullOrWhiteSpace(url))
            {
                if (url.Contains("?"))
                    url += $"&{cultureQueryParam}";
                else
                    url += $"?{cultureQueryParam}";
            }
            return url;
        }


    }

}
