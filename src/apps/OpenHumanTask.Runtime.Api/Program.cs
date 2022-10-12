using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Query.Expressions;
using OpenHumanTask.Runtime.Application;
using OpenHumanTask.Runtime.Application.Services;
using OpenHumanTask.Runtime.Infrastructure.Services;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var searchBinder = new ODataSearchBinder();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddSingleton<ISearchBinder>(searchBinder);
builder.Services.AddControllers()
    .AddJsonOptions(json =>
    {
        json.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
        json.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    })
    .AddOData((options, provider) =>
    {
        var builder = provider.GetRequiredService<IEdmModelBuilder>();
        options.AddRouteComponents("api/odata", builder.Build(), services => services.AddSingleton<ISearchBinder>(searchBinder))
            .EnableQueryFeatures(50);
        options.RouteOptions.EnableControllerNameCaseInsensitive = true;
        options.RouteOptions.EnableActionNameCaseInsensitive = true;
        options.RouteOptions.EnablePropertyNameCaseInsensitive = true;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers().RequireAuthorization();
await app.RunAsync();