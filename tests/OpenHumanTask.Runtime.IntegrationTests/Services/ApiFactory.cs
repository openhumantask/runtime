using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace OpenHumanTask.Runtime.IntegrationTests.Services
{
    public class ApiFactory<TStartup>
        : WebApplicationFactory<TStartup>
        where TStartup : class
    {

        public new IServiceProvider Services
        {
            get
            {
                if (this.Server == null)
                    this.CreateClient();
                return this.Server!.Host.Services;
            }
        }

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            return WebHost.CreateDefaultBuilder()
                .UseContentRoot(AppContext.BaseDirectory)
                .UseWebRoot(AppContext.BaseDirectory)
                .UseSolutionRelativeContentRoot(AppContext.BaseDirectory)
                .UseConfiguration(configuration)
                .UseStartup<TStartup>();
        }

        protected override TestServer CreateServer(IWebHostBuilder builder)
        {
            builder.UseSolutionRelativeContentRoot(new Uri(AppDomain.CurrentDomain.BaseDirectory).LocalPath);
            return base.CreateServer(builder);
        }

    }

}
