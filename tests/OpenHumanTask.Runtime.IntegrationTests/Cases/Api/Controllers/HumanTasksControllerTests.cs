using FluentAssertions;
using OpenHumanTask.Runtime.Api.Controllers;
using OpenHumanTask.Runtime.Integration.Commands.HumanTasks;
using OpenHumanTask.Runtime.IntegrationTests.Fixtures;
using System.Net;
using System.Text.Json;

namespace OpenHumanTask.Runtime.IntegrationTests.Cases.Api.Controllers
{

    public class HumanTasksControllerTests
        : ApiControllerTests<HumanTasksController>
    {

        public HumanTasksControllerTests(ApiFactoryFixture apiFixture) : base(apiFixture) { }

        [Fact]
        public async Task Create_Shoudl_Work()
        {
            //arrange
            var command = new CreateHumanTaskCommand()
            {
                DefinitionReference = this.IntegrationTestData.HumanTaskTemplates.First(),
                Key = "fake-key",
                Priority = 69,
                PeopleAssignments = PeopleAssignmentsDefinitionFactory.Create(),
                Input = new { foo = new { bar = "baz" }.ToExpandoObject() }.ToExpandoObject()
            };

            //act
            using var response = await this.ActAs(Users.Admin).PostAsJsonAsync(nameof(HumanTasksController.Create), command);
            var json = await response.Content?.ReadAsStringAsync()!;

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            json.Should().NotBeNullOrWhiteSpace();
            var task = JsonSerializer.Deserialize<HumanTask>(json)!;
            task.DefinitionId.Should().Be(command.DefinitionReference!);
            task.Key.Should().Be(command.Key);
            task.Priority.Should().Be(command.Priority);
            task.PeopleAssignments.Should().BeEquivalentTo(command.PeopleAssignments);
            task.Input.Should().BeEquivalentTo(command.Input);
        }

        [Fact]
        public async Task GetById_Should_Work()
        {
            //act
            using var response = await this.ActAs(Users.Admin).GetAsync(nameof(HumanTasksController.GetById), new { id = this.IntegrationTestData.HumanTasks.First() });
            var json = await response.Content?.ReadAsStringAsync()!;

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            json.Should().NotBeNullOrWhiteSpace();
            var template = JsonSerializer.Deserialize<HumanTask>(json)!;
            template.Id.Should().NotBeNullOrWhiteSpace();
            template.CreatedAt.Should().NotBe(default);
            template.LastModified.Should().NotBe(default);
            template.PeopleAssignments.ActualInitiator.Should().NotBeNull();
        }

        [Fact]
        public async Task Get_Should_Work()
        {
            //act
            using var response = await this.ActAs(Users.Admin).GetAsync(nameof(HumanTasksController.Get));
            var json = await response.Content?.ReadAsStringAsync()!;

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            json.Should().NotBeNullOrWhiteSpace();
            var count = JsonSerializer.Deserialize<int>(json)!;
            count.Should().BeGreaterThanOrEqualTo(this.IntegrationTestData.HumanTasks.Count);
        }

        [Fact]
        public async Task Delete_Should_Work()
        {
            //arrange
            var id = this.IntegrationTestData.HumanTasks.Last();
            var getById = async () => await this.ActAs(Users.Admin).GetAsync<HumanTaskTemplate>(nameof(HumanTasksController.GetById), new { id });

            //act
            using var response = await this.ActAs(Users.Admin).DeleteAsync(nameof(HumanTasksController.Delete), new { id });
            var json = await response.Content?.ReadAsStringAsync()!;
            var action = async () => await this.ActAs(Users.Admin).GetAsync(id);

            //assert
            await getById.Should().ThrowAsync<HttpRequestException>();

            this.IntegrationTestData.HumanTasks.Remove(id);
        }

    }

}
