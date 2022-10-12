using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Query.Expressions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Neuroglia.Caching;
using OpenHumanTask.Runtime.Api.Controllers;
using OpenHumanTask.Runtime.Application;
using OpenHumanTask.Runtime.Application.Services;
using OpenHumanTask.Runtime.Infrastructure.Services;
using OpenHumanTask.Runtime.IntegrationTests.Services;

namespace OpenHumanTask.Runtime.IntegrationTests
{
    public class ApiConfiguration
    {

        private readonly IConfiguration _configuration;

        public ApiConfiguration(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            IdentityModelEventSource.ShowPII = true;
            var searchBinder = new ODataSearchBinder();

            services.AddSingleton<ISearchBinder>(searchBinder);
            services.AddApplicationServices(this._configuration);
            services.AddMemoryDistributedCache();
            services.AddHostedService<IntegrationTestDbInitializer>();
            services.AddSingleton<IntegrationTestData>();
            services.AddControllers()
                .AddOData((options, provider) =>
                {
                    var builder = provider.GetRequiredService<IEdmModelBuilder>();
                    options.AddRouteComponents("api/odata", builder.Build(), services => services.AddSingleton<ISearchBinder>(searchBinder))
                        .EnableQueryFeatures(50);
                    options.RouteOptions.EnableControllerNameCaseInsensitive = true;
                    options.RouteOptions.EnableActionNameCaseInsensitive = true;
                    options.RouteOptions.EnablePropertyNameCaseInsensitive = true;
                })
                .AddApplicationPart(typeof(HumanTasksController).Assembly);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = JwtClaimTypes.Name,
                        RoleClaimType = JwtClaimTypes.Role,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = JwtTokenFactory.Issuer,
                        ValidAudience = JwtTokenFactory.Issuer,
                        IssuerSigningKey = JwtTokenFactory.IssuerSigningKey
                    };
                });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers()
                    .RequireAuthorization();
            });
        }

    }

}
