using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Neuroglia.Serialization;
using OpenHumanTask.Runtime;
using OpenHumanTask.Runtime.Dashboard;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddJsonSerializer();
builder.Services.AddOpenHumanTaskRuntimeApi(options =>
{
    options.Server.BaseAddress = new(builder.HostEnvironment.BaseAddress);
});
builder.Services.AddFlux(flux =>
{
    flux
        .ScanMarkupTypeAssembly<App>();
});
builder.Services.AddScoped<ILayoutManager, LayoutManager>();
builder.Services.AddScoped<IBreadcrumbManager, BreadcrumbManager>();

await builder.Build().RunAsync();
