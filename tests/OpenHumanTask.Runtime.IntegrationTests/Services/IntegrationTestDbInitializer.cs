using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Neuroglia.Data;

namespace OpenHumanTask.Runtime.IntegrationTests.Services
{

    public class IntegrationTestDbInitializer
        : BackgroundService
    {

        readonly IServiceProvider _serviceProvider;
        readonly IntegrationTestData _integrationTestData;

        public IntegrationTestDbInitializer(IServiceProvider serviceProvider, IntegrationTestData integrationTestData)
        {
            this._serviceProvider = serviceProvider;
            this._integrationTestData = integrationTestData;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await this.SeedHumanTaskTemplatesAsync(stoppingToken);
            await this.SeedHumanTasksAsync(stoppingToken);
        }

        async Task SeedHumanTaskTemplatesAsync(CancellationToken cancellationToken)
        {
            var templates = this._serviceProvider.GetRequiredService<IRepository<Domain.Models.HumanTaskTemplate, string>>();
            var user = Users.Admin;

            for (int i = 0; i < 5; i++)
            {
                var template = await templates.AddAsync(new(user, HumanTaskDefinitionFactory.Create()), cancellationToken);
                this._integrationTestData.HumanTaskTemplates.Add(template.Id);
            }

            await templates.SaveChangesAsync(cancellationToken);
        }

        async Task SeedHumanTasksAsync(CancellationToken cancellationToken)
        {
            var templates = this._serviceProvider.GetRequiredService<IRepository<Domain.Models.HumanTaskTemplate, string>>();
            var tasks = this._serviceProvider.GetRequiredService<IRepository<Domain.Models.HumanTask, string>>();
            var user = Users.Admin;

            for (int i = 0; i < 5; i++)
            {
                var template = await templates.FindAsync(this._integrationTestData.HumanTaskTemplates[i], cancellationToken);
                var task = await tasks.AddAsync(new(template, Guid.NewGuid().ToShortString(), new(user, businessAdministrators: new Domain.Models.UserReference[] { user }), 10), cancellationToken);
                this._integrationTestData.HumanTasks.Add(task.Id);
            }

            await tasks.SaveChangesAsync(cancellationToken);
        }

    }

}
