namespace OpenHumanTask.Runtime.UnitTests.Data
{
    internal static class HumanTaskFactory
    {

        internal static HumanTask Create()
        {
            var template = HumanTaskTemplateFactory.Create();
            var key = "fake-key";
            var peopleAssignments = PeopleAssignmentsFactory.Create();
            var priority = 1;
            var title = template.Definition.Title?.ToDictionary<string>();
            var subject = template.Definition.Subject?.ToDictionary<string>();
            var description = template.Definition.Description?.ToDictionary<string>();
            var input = new { foo = "bar", baz = new { bar = "foo" } };
            var subtasks = new Subtask[] { new(HumanTaskTemplateFactory.Create()) };
            var attachments = new Attachment[] { AttachmentFactory.Create() };
            var comments = new Comment[] { CommentFactory.Create() };
            var task = new HumanTask(template, key, peopleAssignments, priority, title, subject, description, input, subtasks, attachments, comments);
            task.ClearPendingEvents();
            return task;
        }

    }

}
