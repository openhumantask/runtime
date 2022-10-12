using Microsoft.AspNetCore.Authentication.JwtBearer;
using OpenHumanTask.Runtime.IntegrationTests.Services;
using System.Security.Claims;

namespace OpenHumanTask.Runtime.IntegrationTests.Fixtures
{

    public class ApiFactoryFixture
        : ApiFactory<ApiConfiguration>
    {

        public HttpClient CreateAuthenticatedClient(ClaimsPrincipal user)
        {
            var httpClient = this.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new(JwtBearerDefaults.AuthenticationScheme, JwtTokenFactory.GenerateFor(user));
            return httpClient;
        }

    }

}
