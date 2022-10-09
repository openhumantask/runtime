using Neuroglia;
using OpenHumanTask.Runtime.Domain.Models;

namespace OpenHumanTask.Runtime.IntegrationTests.Data
{
    internal static class HumanTaskFactory
    {

        internal static HumanTask Create()
        {
            var definition = HumanTaskDefinitionFactory.Create();
            var key = "fake-key";
            var peopleAssignments = PeopleAssignmentsFactory.Create();
            var title = definition.Title?.ToDictionary<string>();
            var subject = definition.Subject?.ToDictionary<string>();
            var description = definition.Description?.ToDictionary<string>();
            var input = new { foo = "bar", baz = new { bar = "foo" } };
            var subtasks = new Subtask[] { new(HumanTaskDefinitionFactory.Create()) };
            var attachments = new Attachment[] { AttachmentFactory.Create() };
            var comments = new Comment[] { CommentFactory.Create() };
            var task = new HumanTask(definition, key, peopleAssignments, title, subject, description, input, subtasks, attachments, comments);
            task.ClearPendingEvents();
            return task;
        }

    }

}
