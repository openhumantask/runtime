using OpenHumanTask.Runtime.Domain.Models;

namespace OpenHumanTask.Runtime.IntegrationTests.Data
{
    public static class CommentFactory
    {

        public static Comment Create()
        {
            return new(UserReferenceFactory.Create(), "fake-comment");
        }

    }

}
