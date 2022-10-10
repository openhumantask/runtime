using OpenHumanTask.Runtime.Domain.Models;

namespace OpenHumanTask.Runtime.UnitTests.Data
{
    public static class CommentFactory
    {

        public static Comment Create()
        {
            return new(UserReferenceFactory.Create(), "fake-comment");
        }

    }

}
