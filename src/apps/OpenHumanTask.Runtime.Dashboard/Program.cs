using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Neuroglia.Serialization;
using OpenHumanTask.Runtime.Client;
using OpenHumanTask.Runtime.Dashboard;
using OpenHumanTask.Runtime.Dashboard.Configuration;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var httpClient = new HttpClient()
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
};
builder.Services.AddScoped(sp => httpClient);
using var response = await httpClient.GetAsync("appsettings.json");
using var stream = await response.Content.ReadAsStreamAsync();
builder.Configuration.AddJsonStream(stream);

var applicationOptions = new ApplicationOptions();
builder.Configuration.Bind(applicationOptions);

builder.Services.AddJsonSerializer();
builder.Services.AddOpenHumanTaskRuntimeApi(options =>
{
    options.Server.BaseAddress = new(builder.HostEnvironment.BaseAddress);
    options.HttpClientSetup = (http) => http.AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
});
builder.Services.AddOidcAuthentication(options =>
{
    options.ProviderOptions.Authority = applicationOptions.Authentication.Authority;
    options.ProviderOptions.ClientId = applicationOptions.Authentication.ClientId;
    applicationOptions.Authentication.DefaultScopes?.ForEach(s => options.ProviderOptions.DefaultScopes.Add(s));
    options.ProviderOptions.ResponseType = applicationOptions.Authentication.ResponseType;
});
builder.Services.AddFlux(flux =>
{
    flux
        .ScanMarkupTypeAssembly<App>();
});
builder.Services.AddScoped<ILayoutManager, LayoutManager>();
builder.Services.AddScoped<IBreadcrumbManager, BreadcrumbManager>();
builder.Services.AddScoped<IApplicationStatusMonitor, ApplicationStatusMonitor>();

await builder.Build().RunAsync();
