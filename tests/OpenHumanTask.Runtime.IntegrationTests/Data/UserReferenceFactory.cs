namespace OpenHumanTask.Runtime.IntegrationTests.Data
{
    internal static class UserReferenceFactory
    {

        internal static Domain.Models.UserReference Create()
        {
            return new(Guid.NewGuid().ToShortString(), "fake-user");
        }

    }

}
