using FluentAssertions;
using OpenHumanTask.Runtime.Api.Controllers;
using OpenHumanTask.Runtime.Integration.Commands.HumanTaskTemplates;
using OpenHumanTask.Runtime.IntegrationTests.Fixtures;
using System.Net;
using System.Text.Json;

namespace OpenHumanTask.Runtime.IntegrationTests.Cases.Api.Controllers
{

    public class HumanTaskTemplatesControllerTests
        : ApiControllerTests<HumanTaskTemplatesController>
    {

        public HumanTaskTemplatesControllerTests(ApiFactoryFixture apiFixture) : base(apiFixture) { }

        [Fact]
        public async Task Create_Should_Work()
        {
            //arrange
            var command = new CreateHumanTaskTemplateCommand()
            {
                Definition = HumanTaskDefinitionFactory.Create()
            };

            //act
            using var response = await this.ActAs(Users.Admin).PostAsJsonAsync(nameof(HumanTaskTemplatesController.Create), command);
            var json = await response.Content?.ReadAsStringAsync()!;

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            json.Should().NotBeNullOrWhiteSpace();
            var template = JsonSerializer.Deserialize<HumanTaskTemplate>(json)!;
            template.Definition.Should().BeEquivalentTo(command.Definition);
        }

        [Fact]
        public async Task GetById_Should_Work()
        {
            //act
            using var response = await this.ActAs(Users.Admin).GetAsync(nameof(HumanTaskTemplatesController.GetById), new { id = this.IntegrationTestData.HumanTaskTemplates.First() });
            var json = await response.Content?.ReadAsStringAsync()!;

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            json.Should().NotBeNullOrWhiteSpace();
            var template = JsonSerializer.Deserialize<HumanTaskTemplate>(json)!;
            template.Id.Should().NotBeNullOrWhiteSpace();
            template.CreatedAt.Should().NotBe(default);
            template.LastModified.Should().NotBe(default);
            template.CreatedBy.Should().NotBeNull();
        }

        [Fact]
        public  async Task Get_Should_Work()
        {
            //act
            using var response = await this.ActAs(Users.Admin).GetAsync(nameof(HumanTaskTemplatesController.Get));
            var json = await response.Content?.ReadAsStringAsync()!;

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            json.Should().NotBeNullOrWhiteSpace();
            var count = JsonSerializer.Deserialize<int>(json)!;
            count.Should().BeGreaterThanOrEqualTo(this.IntegrationTestData.HumanTaskTemplates.Count);
        }

        [Fact]
        public async Task Delete_Should_Work()
        {
            //arrange
            var id = this.IntegrationTestData.HumanTaskTemplates.Last();
            var getById = async () => await this.ActAs(Users.Admin).GetAsync<HumanTaskTemplate>(nameof(HumanTaskTemplatesController.GetById), new { id });

            //act
            using var response = await this.ActAs(Users.Admin).DeleteAsync(nameof(HumanTaskTemplatesController.Delete), new { id });
            var json = await response.Content?.ReadAsStringAsync()!;
            var action = async () => await this.ActAs(Users.Admin).GetAsync(id);

            //assert
            await getById.Should().ThrowAsync<HttpRequestException>();

            this.IntegrationTestData.HumanTaskTemplates.Remove(id);
        }

    }

}
