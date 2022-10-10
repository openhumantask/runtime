using OpenHumanTask.Runtime.Domain.Models;
using System.Net.Mime;

namespace OpenHumanTask.Runtime.UnitTests.Data
{
    public static class AttachmentFactory
    {

        public static Attachment Create()
        {
            return new(UserReferenceFactory.Create(), "fake.pdf", MediaTypeNames.Application.Pdf);
        }

    }

}
