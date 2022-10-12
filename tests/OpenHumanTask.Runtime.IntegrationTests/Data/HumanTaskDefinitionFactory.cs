using OpenHumanTask.Sdk.Services.FluentBuilders;

namespace OpenHumanTask.Runtime.IntegrationTests.Data
{

    internal static class HumanTaskDefinitionFactory
    {

        internal static HumanTaskDefinition Create()
        {
            var definition = new HumanTaskDefinitionBuilder()
                .WithName(Guid.NewGuid().ToShortString())
                .WithNamespace("oht.sdk.integration-tests")
                .WithVersion("1.0.0")
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
                .UseStartDeadline("fake-start-deadline", deadline =>
                    deadline
                        .ElapsesAfter(TimeSpan.FromMinutes(30))
                        .Escalates(then =>
                            then.WithName("fake-escalation").Reassign()))
                .UseCompletionDeadline("fake-completion-deadline", deadline =>
                    deadline
                        .ElapsesAt(new(2023, 4, 4, 12, 30, 00, TimeSpan.Zero))
                        .Escalates(then =>
                            then.WithName("fake-escalation").Reassign()))
                .AnnotateWith("fake-annotation-key", "fake-annotation-value")
                .CanBeSkipped()
                .Build();
            return definition;
        }

    }

}
