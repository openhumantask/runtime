using OpenHumanTask.Runtime.Domain.Models;

namespace OpenHumanTask.Runtime.UnitTests.Data
{
    internal static class UserReferenceFactory
    {

        internal static UserReference Create()
        {
            return new(Guid.NewGuid().ToString().ToLowerInvariant(), "fake-user");
        }

    }

}
