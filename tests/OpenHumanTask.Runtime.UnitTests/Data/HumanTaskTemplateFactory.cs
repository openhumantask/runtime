namespace OpenHumanTask.Runtime.UnitTests.Data
{
    internal static class HumanTaskTemplateFactory
    {

        internal static HumanTaskTemplate Create()
        {
            var template = new HumanTaskTemplate(UserReferenceFactory.Create(), HumanTaskDefinitionFactory.Create());
            template.ClearPendingEvents();
            return template;
        }

    }

}
