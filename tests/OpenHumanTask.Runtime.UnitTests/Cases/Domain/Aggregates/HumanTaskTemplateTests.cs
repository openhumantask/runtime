using OpenHumanTask.Runtime.Domain.Events.HumanTaskTemplates;

namespace OpenHumanTask.Runtime.UnitTests.Cases.Domain.Aggregates
{
    public class HumanTaskTemplateTests
    {

        #region Create

        [Fact]
        public void Create_Should_Work()
        {
            //arrange
            var user = UserReferenceFactory.Create();
            var definition = HumanTaskDefinitionFactory.Create();

            //act
            var template = new HumanTaskTemplate(user, definition);

            //assert
            template.PendingEvents.Should().NotBeNullOrEmpty();
            template.PendingEvents.Should().ContainSingle();
            template.PendingEvents.OfType<HumanTaskTemplateCreatedDomainEvent>().SingleOrDefault().Should().NotBeNull();
            template.Id.Should().Be(definition.Id);
            template.CreatedBy.Should().BeEquivalentTo(user);
            template.Definition.Should().BeEquivalentTo(definition);
        }

        [Fact]
        public void Create_With_Null_User_Should_Throw()
        {
            //arrange
            var definition = HumanTaskDefinitionFactory.Create();
            var action = () => new HumanTaskTemplate(null!, definition);

            //assert
            action.Should().Throw<DomainArgumentException>();
        }

        [Fact]
        public void Create_With_Null_Definition_Should_Throw()
        {
            //arrange
            var user = UserReferenceFactory.Create();
            var action = () => new HumanTaskTemplate(user, null!);

            //assert
            action.Should().Throw<DomainArgumentException>();
        }

        #endregion

    }

}
