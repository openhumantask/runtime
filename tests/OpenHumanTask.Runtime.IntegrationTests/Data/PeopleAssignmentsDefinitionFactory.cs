namespace OpenHumanTask.Runtime.IntegrationTests.Data
{
    internal static class PeopleAssignmentsDefinitionFactory
    {

        internal static PeopleAssignmentsDefinition Create()
        {
            return new()
            {
                PotentialInitiators = new() { },
                PotentialOwners = new() { },
                ExcludedOwners = new() { },
                Stakeholders = new() { },
                BusinessAdministrators = new() { },
                NotificationRecipients = new() { },
                Groups = new() { }
            };
        }

    }

}
