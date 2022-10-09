using OpenHumanTask.Runtime.Domain.Models;

namespace OpenHumanTask.Runtime.IntegrationTests.Data
{
    internal static class PeopleAssignmentsFactory
    {

        internal static PeopleAssignments Create()
        {
            var actualInitiator = UserReferenceFactory.Create();
            var potentialInitiators = Array.Empty<UserReference>();
            var potentialOwners = new UserReference[] { UserReferenceFactory.Create() };
            var actualOwner = (UserReference?)null;
            var excludedOwners = Array.Empty<UserReference>();
            var stakeholders = Array.Empty<UserReference>();
            var businessAdministrators = Array.Empty<UserReference>();
            var notificationRecipients = Array.Empty<UserReference>();
            return new(actualInitiator, potentialInitiators, potentialOwners, actualOwner, excludedOwners, stakeholders, businessAdministrators, notificationRecipients);
        }

    }

}
