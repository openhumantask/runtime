using OpenHumanTask.Sdk.Services.FluentBuilders;

namespace OpenHumanTask.Runtime.IntegrationTests.Data
{

    internal static class HumanTaskDefinitionFactory
    {

        internal static HumanTaskDefinition Create()
        {
            var definition = new HumanTaskDefinitionBuilder()
                .WithName("fake-task")
                .WithNamespace("oht.sdk.unit-tests")
                .WithVersion("1.0.0-unitTest")
                .UseSpecVersion("0.1.0")
                .UseExpressionLanguage("jq")
                .UseAutomaticCompletionBehavior("reviewed", complete =>
                    complete
                        .When("${ $CONTEXT.form.data }")
                        .SetOutput(new { fakeProperty = "fake-data" }))
                .Assign(assign =>
                    assign
                        .ToPotentialOwners(all =>
                            all
                                .Users()
                                .WithClaim("role", "clerk"))
                        .ToBusinessAdministrators(single =>
                            single.User("fake-user@email.com"))
                        .ToGroup("fake-group", all =>
                            all
                                .Users()
                                .InRole(GenericHumanRole.PotentialOwner))
                        .ToNotificationRecipients(all =>
                            all
                                .Users()
                                .InGroup("fake-group")))
                .AddOutcome("fake-outcome", outcome =>
                    outcome
                        .When("${ $CONTEXT.form.data.reviewed }")
                        .Outputs("en", "fake-en-value")
                        .Outputs("fr", "fake-fr-value"))
                .UseForm(form =>
                    form
                        .WithData("${ $CONTEXT.inputData }")
                        .DisplayUsing(view =>
                            view
                                .OfType("jsonform")
                                .WithTemplate("fake-jsonform-template")))
                .UseStartDeadline(deadline =>
                    deadline
                        .ElapsesAfter(TimeSpan.FromMinutes(30))
                        .Escalates(then =>
                            then.Reassign()))
                .UseCompletionDeadline(deadline =>
                    deadline
                        .ElapsesAt(new(2023, 4, 4, 12, 30, 00, TimeSpan.Zero))
                        .Escalates(then =>
                            then.StartSubtask("fake-subtask-1", subtask =>
                                subtask
                                    .WithDefinition("fake-namespace.fake-other-task:1.0.0-unitTest")
                                    .WithInput("${ $CONTEXT.form.inputData }"))))
                .AddSubtask("fake-subtask-2", subtask =>
                    subtask.WithDefinition("fake-namespace.fake-other-task:1.5.1-unitTest"))
                .AnnotateWith("fake-annotation-key", "fake-annotation-value")
                .Build();
            return definition;
        }

    }

}
