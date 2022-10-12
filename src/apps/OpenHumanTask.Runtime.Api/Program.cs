using Microsoft.OpenApi.Models;
using OpenHumanTask.Runtime.Application.Mapping;
using OpenHumanTask.Runtime.Integration.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

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
builder.Services.AddSwaggerGen(builder =>
{
    builder.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    builder.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Open Human Task Runtime API",
        Version = "v1",
        Description = "The Open API documentation for the Open Human Task Runtime API",
        Contact = new()
        {
            Name = "The Open Human Task Specification Authors",
            Url = new Uri("https://openhumantask.io/")
        }
    });
    builder.IncludeXmlComments(typeof(MappingProfile).Assembly.Location.Replace(".dll", ".xml"));
    builder.IncludeXmlComments(typeof(HumanTask).Assembly.Location.Replace(".dll", ".xml"));
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.UseWebAssemblyDebugging();
else
    app.UseExceptionHandler("/error");
app.UseCloudEvents();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseODataRouteDebug();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger(builder =>
{
    builder.RouteTemplate = "api/{documentName}/doc/oas.{json|yaml}";
});
app.UseSwaggerUI(builder =>
{
    builder.DocExpansion(DocExpansion.None);
    builder.SwaggerEndpoint("/api/v1/doc/oas.json", "Open Human Task  Runtime API v1");
    builder.RoutePrefix = "api/doc";
});
app.MapControllers()
    .RequireAuthorization();
app.MapFallbackToFile("index.html");
app.MapFallbackToFile("/tasks/{param?}", "index.html");
app.MapFallbackToFile("/tasks/definitions/{param?}", "index.html");
await app.RunAsync();