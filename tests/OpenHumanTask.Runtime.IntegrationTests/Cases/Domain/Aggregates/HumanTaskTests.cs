using OpenHumanTask.Runtime.Domain.Events.HumanTasks;

namespace OpenHumanTask.Runtime.IntegrationTests.Cases.Domain.Aggregates
{

    public class HumanTaskTests
    {

        #region Create

        [Fact]
        public void Create_Should_Work()
        {
            //arrange
            var definition = HumanTaskDefinitionFactory.Create();
            var key = "fake-key";
            var peopleAssignments = PeopleAssignmentsFactory.Create();
            var priority = 10;
            var title = definition.Title?.ToDictionary<string>();
            var subject = definition.Subject?.ToDictionary<string>();
            var description = definition.Description?.ToDictionary<string>();
            var input = new { foo = "bar", baz = new { bar = "foo" } };
            var subtasks = new Subtask[] { new(HumanTaskDefinitionFactory.Create()) };
            var attachments = new Attachment[] { AttachmentFactory.Create() };
            var comments = new Comment[] { CommentFactory.Create() };
           
            //act
            var task = new HumanTask(definition, key, peopleAssignments, priority, title, subject, description, input, subtasks, attachments, comments);

            //assert
            task.PendingEvents.Should().NotBeNullOrEmpty();
            task.PendingEvents.Should().HaveCount(2);
            task.PendingEvents.OfType<HumanTaskCreatedDomainEvent>().SingleOrDefault().Should().NotBeNull();
            task.PendingEvents.OfType<HumanTaskStatusChangedDomainEvent>().SingleOrDefault().Should().NotBeNull();
            task.Id.Should().Be(HumanTask.BuildId(definition.Id, key));
            task.DefinitionId.Should().Be(definition.Id);
            task.Key.Should().Be(key);
            task.PeopleAssignments.Should().BeEquivalentTo(peopleAssignments);
            task.Priority.Should().Be(priority);
            task.Title.Should().BeEquivalentTo(title);
            task.Subject.Should().BeEquivalentTo(subject);
            task.Description.Should().BeEquivalentTo(description);
            task.Input.Should().BeEquivalentTo(input);
            task.Subtasks.Should().BeEquivalentTo(subtasks);
            task.Attachments.Should().BeEquivalentTo(attachments);
            task.Comments.Should().BeEquivalentTo(comments);
        }

        [Fact]
        public void Create_With_Null_Definition_Should_Throw()
        {
            //arrange
            var key = "fake-key";
            var peopleAssignments = PeopleAssignmentsFactory.Create();
            var priority = 10;
            var action = () => new HumanTask(null!, key, peopleAssignments, priority);

            //assert
            action.Should().Throw<DomainArgumentException>();
        }

        [Fact]
        public void Create_With_NullOrWhiteSpace_Key_Should_Throw()
        {
            //arrange
            var definition = HumanTaskDefinitionFactory.Create();
            var peopleAssignments = PeopleAssignmentsFactory.Create();
            var priority = 10;
            var nullKeyAction = () => new HumanTask(definition, null!, peopleAssignments, priority);
            var emptyKeyAction = () => new HumanTask(definition, string.Empty, peopleAssignments, priority);
            var whitespaceKeyAction = () => new HumanTask(definition, " ", peopleAssignments, priority);

            //assert
            nullKeyAction.Should().Throw<DomainArgumentException>();
            emptyKeyAction.Should().Throw<DomainArgumentException>();
            whitespaceKeyAction.Should().Throw<DomainArgumentException>();
        }

        #endregion

        #region SetPriority

        [Fact]
        public void SetPriority_Should_Work()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            var priority = 99;

            //act
            var succeeded = task.SetPriority(user, priority);

            //assert
            succeeded.Should().BeTrue();
            task.PendingEvents.Should().NotBeNullOrEmpty();
            task.PendingEvents.Should().ContainSingle();
            task.PendingEvents.OfType<HumanTaskPriorityChangedDomainEvent>().SingleOrDefault().Should().NotBeNull();
            task.Priority.Should().Be(priority);
        }

        [Fact]
        public void SetPriority_With_Null_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var priority = 99;
            var action = () => task.SetPriority(null!, priority);

            //assert
            action.Should().Throw<DomainArgumentException>();
        }

        [Fact]
        public void SetPriority_With_UnAuthorized_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = UserReferenceFactory.Create();
            var priority = 99;
            var action = () => task.SetPriority(user, priority);

            //assert
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void SetPriority_With_Same_Priority_Should_Fail()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();

            //act
            var succeeded = task.SetPriority(user, task.Priority);

            //assert
            succeeded.Should().BeFalse();
        }

        #endregion

        #region Assign

        [Fact]
        public void Assign_Should_Work()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.ActualInitiator;
            var assignments = PeopleAssignmentsFactory.Create();

            //act
            task.Assign(user, assignments);

            //assert
            task.PendingEvents.Should().NotBeNullOrEmpty();
            task.PendingEvents.Should().ContainSingle();
            task.PendingEvents.OfType<HumanTaskReassignedDomainEvent>().SingleOrDefault().Should().NotBeNull();
            task.PeopleAssignments.Should().BeEquivalentTo(assignments);
        }

        [Fact]
        public void Assign_With_Null_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var assignments = PeopleAssignmentsFactory.Create();
            var action = () => task.Assign(null!, assignments);

            //assert
            action.Should().Throw<DomainArgumentException>();
        }

        [Fact]
        public void Assign_With_Unauthorized_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = UserReferenceFactory.Create();
            var assignments = PeopleAssignmentsFactory.Create();
            var action = () => task.Assign(user, assignments);

            //assert
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Assign_With_Null_AssignTo_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            var action = () => task.Assign(user, null!);

            //assert
            action.Should().Throw<DomainArgumentException>();
        }

        #endregion

        #region Claim

        [Fact]
        public void Claim_Should_Work()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();

            //act
            task.Claim(user);

            //assert
            task.PendingEvents.Should().NotBeNullOrEmpty();
            task.PendingEvents.Should().HaveCount(2);
            task.PendingEvents.OfType<HumanTaskClaimedDomainEvent>().SingleOrDefault().Should().NotBeNull();
            task.PendingEvents.OfType<HumanTaskStatusChangedDomainEvent>().SingleOrDefault().Should().NotBeNull();
            task.Status.Should().Be(HumanTaskStatus.Reserved);
            task.PeopleAssignments.ActualOwner.Should().BeEquivalentTo(user);
        }

        [Fact]
        public void Claim_With_Null_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var action = () => task.Claim(null!);

            //act
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Claim_With_UnAuthorized_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = UserReferenceFactory.Create();
            var action = () => task.Claim(user);

            //assert
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Claim_When_Not_Ready_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            task.Claim(user);
            task.ClearPendingEvents();
            var action = () => task.Claim(user);

            //act
            action.Should().Throw<DomainException>();
        }

        #endregion

        #region Release

        [Fact]
        public void Release_Should_Work()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            task.Claim(user);
            task.ClearPendingEvents();

            //act
            task.Release(user);

            //assert
            task.PendingEvents.Should().NotBeNullOrEmpty();
            task.PendingEvents.Should().HaveCount(2);
            task.PendingEvents.OfType<HumanTaskReleasedDomainEvent>().Should().ContainSingle();
            task.PendingEvents.OfType<HumanTaskStatusChangedDomainEvent>().Should().ContainSingle();
            task.Status.Should().Be(HumanTaskStatus.Ready);
            task.PeopleAssignments.ActualOwner.Should().BeNull();
        }

        [Fact]
        public void Release_With_Null_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            task.Claim(user);
            task.ClearPendingEvents();
            var action = () => task.Release(null!);

            //assert
            action.Should().Throw<DomainArgumentException>();
        }

        [Fact]
        public void Release_With_Unauthorized_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            task.Claim(user);
            var action = () => task.Release(UserReferenceFactory.Create());

            //assert
            action.Should().Throw<DomainException>();
        }

        #endregion

        #region Delegate

        [Fact]
        public void Delegate_Should_Work()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            var delegateTo = UserReferenceFactory.Create();
            task.Claim(user);
            task.ClearPendingEvents();

            //act
            task.Delegate(user, delegateTo);

            //assert
            task.PendingEvents.Should().NotBeNullOrEmpty();
            task.PendingEvents.Should().HaveCount(3);
            task.PendingEvents.OfType<HumanTaskReleasedDomainEvent>().Should().ContainSingle();
            task.PendingEvents.OfType<HumanTaskDelegatedDomainEvent>().Should().ContainSingle();
            task.PendingEvents.OfType<HumanTaskStatusChangedDomainEvent>().Should().ContainSingle();
            task.Status.Should().Be(HumanTaskStatus.Ready);
            task.PeopleAssignments.ActualOwner.Should().BeEquivalentTo(delegateTo);
            task.PeopleAssignments.PotentialOwners.Should().Contain(delegateTo);
        }

        [Fact]
        public void Delegate_With_Null_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            var delegateTo = UserReferenceFactory.Create();
            task.Claim(user);
            task.ClearPendingEvents();
            var action = () => task.Delegate(null!, delegateTo);

            //assert
            action.Should().Throw<DomainArgumentException>();
        }

        [Fact]
        public void Delegate_With_Null_DelegateTo_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            task.Claim(user);
            task.ClearPendingEvents();
            var action = () => task.Delegate(user, null!);

            //assert
            action.Should().Throw<DomainArgumentException>();
        }

        [Fact]
        public void Delegate_With_Unauthorized_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            var delegateTo = UserReferenceFactory.Create();
            task.Claim(user);
            var action = () => task.Delegate(UserReferenceFactory.Create(), delegateTo);

            //assert
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Delegate_When_With_Non_Active_Status_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            var delegateTo = UserReferenceFactory.Create();
            var action = () => task.Delegate(user, delegateTo);

            //assert
            action.Should().Throw<DomainException>();
        }

        #endregion

        #region Forward

        [Fact]
        public void Forward_Should_Work()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            var forwardTo = new UserReference[] { UserReferenceFactory.Create() };
            task.Claim(user);
            task.ClearPendingEvents();

            //act
            task.Forward(user, forwardTo);

            //assert
            task.PendingEvents.Should().NotBeNullOrEmpty();
            task.PendingEvents.Should().HaveCount(3);
            task.PendingEvents.OfType<HumanTaskReleasedDomainEvent>().Should().ContainSingle();
            task.PendingEvents.OfType<HumanTaskForwardedDomainEvent>().Should().ContainSingle();
            task.PendingEvents.OfType<HumanTaskStatusChangedDomainEvent>().Should().ContainSingle();
            task.Status.Should().Be(HumanTaskStatus.Ready);
            task.PeopleAssignments.ActualOwner.Should().BeNull();
            task.PeopleAssignments.PotentialOwners.Should().Contain(forwardTo);
        }

        [Fact]
        public void Forward_With_Null_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            var forwardTo = new UserReference[] { UserReferenceFactory.Create() };
            task.Claim(user);
            task.ClearPendingEvents();
            var action = () => task.Forward(null!, forwardTo);

            //assert
            action.Should().Throw<DomainArgumentException>();
        }

        [Fact]
        public void Forward_With_Unauthorized_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            var forwardTo = new UserReference[] { UserReferenceFactory.Create() };
            task.Claim(user);
            var action = () => task.Forward(UserReferenceFactory.Create(), forwardTo);

            //assert
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Forward_With_Null_ForwardTo_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            task.Claim(user);
            task.ClearPendingEvents();
            var action = () => task.Forward(user, null!);

            //assert
            action.Should().Throw<DomainArgumentException>();
        }

        [Fact]
        public void Forward_With_Empty_ForwardTo_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            var forwardTo = Array.Empty<UserReference>();
            task.Claim(user);
            task.ClearPendingEvents();
            var action = () => task.Forward(user, null!);

            //assert
            action.Should().Throw<DomainArgumentException>();
        }

        [Fact]
        public void Forward_With_Potential_Author_User_When_Ready_Should_Work()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            var forwardTo = new UserReference[] { UserReferenceFactory.Create() };
            task.Claim(user);
            task.ClearPendingEvents();

            //act
            task.Forward(user, forwardTo);

            //assert
            task.PendingEvents.Should().NotBeNullOrEmpty();
            task.PendingEvents.Should().HaveCount(3);
            task.PendingEvents.OfType<HumanTaskReleasedDomainEvent>().Should().ContainSingle();
            task.PendingEvents.OfType<HumanTaskStatusChangedDomainEvent>().Should().ContainSingle();
            task.PendingEvents.OfType<HumanTaskForwardedDomainEvent>().Should().ContainSingle();
            task.Status.Should().Be(HumanTaskStatus.Ready);
            task.PeopleAssignments.ActualOwner.Should().BeNull();
            task.PeopleAssignments.PotentialOwners.Should().Contain(forwardTo);
        }

        [Fact]
        public void Forward_When_With_Non_Active_Status_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            var forwardTo = new UserReference[] { UserReferenceFactory.Create() };
            var action = () => task.Forward(user, forwardTo);

            //assert
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Forward_With_Potential_Author_User_When_Not_Ready_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            task.Claim(user);
            task.ClearPendingEvents();
            var forwardTo = Array.Empty<UserReference>();
            var action = () => task.Forward(user, forwardTo);

            //assert
            action.Should().Throw<DomainException>();
        }

        #endregion

        #region AddAttachment

        [Fact]
        public void AddAttachment_Should_Work()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            var attachment = AttachmentFactory.Create();

            //act
            task.AddAttachment(user, attachment);

            //assert
            task.PendingEvents.Should().NotBeNullOrEmpty();
            task.PendingEvents.Should().ContainSingle();
            task.PendingEvents.Should().AllBeOfType<AttachmentAddedToHumanTaskDomainEvent>();
            task.Attachments.Should().NotBeNullOrEmpty();
            task.Attachments.Should().HaveCount(2);
            task.Attachments!.Last().Should().BeEquivalentTo(attachment);
        }

        [Fact]
        public void AddAttachment_With_Null_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var attachment = AttachmentFactory.Create();
            var action = () => task.AddAttachment(null!, attachment);

            //assert
            action.Should().Throw<DomainArgumentException>();
        }

        [Fact]
        public void AddAttachment_With_UnAuthorized_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = UserReferenceFactory.Create();
            var attachment = AttachmentFactory.Create();
            var action = () => task.AddAttachment(user, attachment);

            //assert
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void AddAttachment_With_Null_Attachment_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = UserReferenceFactory.Create();
            var action = () => task.AddAttachment(user, null!);

            //assert
            action.Should().Throw<DomainArgumentException>();
        }

        #endregion

        #region RemoveAttachment

        [Fact]
        public void RemoveAttachment_Should_Work()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            var attachment = task.Attachments!.Last();

            //act
            var succeeded = task.RemoveAttachment(user, attachment);

            //assert
            succeeded.Should().BeTrue();
            task.PendingEvents.Should().NotBeNullOrEmpty();
            task.PendingEvents.Should().ContainSingle();
            task.PendingEvents.Should().AllBeOfType<AttachmentRemovedFromHumanTaskDomainEvent>();
            task.Attachments.Should().BeEmpty();
        }

        [Fact]
        public void RemoveAttachment_Not_Previously_Added_Should_Fail()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            var attachment = AttachmentFactory.Create();

            //act
            var succeeded = task.RemoveAttachment(user, attachment);

            //assert
            succeeded.Should().BeFalse();
        }

        [Fact]
        public void RemoveAttachment_With_Null_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var attachment = AttachmentFactory.Create();
            var action = () => task.RemoveAttachment(null!, attachment);

            //assert
            action.Should().Throw<DomainArgumentException>();
        }

        [Fact]
        public void RemoveAttachment_With_UnAuthorized_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = UserReferenceFactory.Create();
            var attachment = AttachmentFactory.Create();
            var action = () => task.RemoveAttachment(user, attachment);

            //assert
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void RemoveAttachment_With_Null_Attachment_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = UserReferenceFactory.Create();
            var attachment = AttachmentFactory.Create();
            var action = () => task.RemoveAttachment(user, null!);

            //assert
            action.Should().Throw<DomainArgumentException>();
        }

        #endregion

        #region AddComment

        [Fact]
        public void AddComment_Should_Work()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            var content = "fake-comment";

            //act
            task.AddComment(user, content);

            //assert
            task.PendingEvents.Should().NotBeNullOrEmpty();
            task.PendingEvents.Should().ContainSingle();
            task.PendingEvents.Should().AllBeOfType<CommentAddedToHumanTaskDomainEvent>();
            task.Comments.Should().NotBeNullOrEmpty();
            task.Comments.Should().HaveCount(2);
            task.Comments!.Last().Content.Should().BeEquivalentTo(content);
        }

        [Fact]
        public void AddComment_With_Null_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var content = "fake-comment";
            var action = () => task.AddComment(null!, content);

            //assert
            action.Should().Throw<DomainArgumentException>();
        }

        [Fact]
        public void AddComment_With_UnAuthorized_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = UserReferenceFactory.Create();
            var content = "fake-comment";
            var action = () => task.AddComment(user, content);

            //assert
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void AddComment_With_Null_Comment_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = UserReferenceFactory.Create();
            var action = () => task.AddComment(user, null!);

            //assert
            action.Should().Throw<DomainArgumentException>();
        }

        #endregion

        #region UpdateComment

        [Fact]
        public void UpdateComment_Should_Work()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            var comment = task.Comments!.First();
            var content = "updated-fake-comment";
            task.Claim(user);
            task.ClearPendingEvents();

            //act
            var success = task.UpdateComment(user, comment, content);

            //assert
            success.Should().BeTrue();
            task.PendingEvents.Should().NotBeNullOrEmpty();
            task.PendingEvents.Should().ContainSingle();
            task.PendingEvents.Should().AllBeOfType<HumanTaskCommentUpdatedDomainEvent>();
            task.Comments!.First().Content.Should().Be(content);
        }

        [Fact]
        public void UpdateComment_With_Null_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var comment = task.Comments!.First();
            var content = "updated-fake-comment";
            var action = () => task.UpdateComment(null!, comment, content);

            //assert
            action.Should().Throw<DomainArgumentException>();
        }

        [Fact]
        public void UpdateComment_With_UnAuthorized_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = UserReferenceFactory.Create();
            var comment = task.Comments!.First();
            var content = "updated-fake-comment";
            var action = () => task.UpdateComment(user, comment, content);

            //assert
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void UpdateComment_With_Null_Comment_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = UserReferenceFactory.Create();
            var content = "updated-fake-comment";
            var action = () => task.UpdateComment(user, null!, content);

            //assert
            action.Should().Throw<DomainArgumentException>();
        }

        [Fact]
        public void UpdateComment_With_NullOrWhiteSpace_Content_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = UserReferenceFactory.Create();
            var comment = task.Comments!.First();
            var nullAction = () => task.UpdateComment(user, comment, null!);
            var emptyAction = () => task.UpdateComment(user, comment, string.Empty);
            var whitespaceAction = () => task.UpdateComment(user, comment, " ");

            //assert
            nullAction.Should().Throw<DomainArgumentException>();
            emptyAction.Should().Throw<DomainArgumentException>();
            whitespaceAction.Should().Throw<DomainArgumentException>();
        }

        [Fact]
        public void UpdateComment_With_NullOrWhiteSpace_Content_Should_Fail()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            var comment = CommentFactory.Create();
            var content = "updated-fake-comment";

            //assert
            task.UpdateComment(user, comment, content);
        }

        #endregion

        #region RemoveComment

        [Fact]
        public void RemoveComment_Should_Work()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            var comment = task.Comments!.Last();

            //act
            var succeeded = task.RemoveComment(user, comment);

            //assert
            succeeded.Should().BeTrue();
            task.PendingEvents.Should().NotBeNullOrEmpty();
            task.PendingEvents.Should().ContainSingle();
            task.PendingEvents.Should().AllBeOfType<CommentRemovedFromHumanTaskDomainEvent>();
            task.Comments.Should().BeEmpty();
        }

        [Fact]
        public void RemoveComment_Not_Previously_Added_Should_Fail()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            var comment = CommentFactory.Create();

            //act
            var succeeded = task.RemoveComment(user, comment);

            //assert
            succeeded.Should().BeFalse();
        }

        [Fact]
        public void RemoveComment_With_Null_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var comment = CommentFactory.Create();
            var action = () => task.RemoveComment(null!, comment);

            //assert
            action.Should().Throw<DomainArgumentException>();
        }

        [Fact]
        public void RemoveComment_With_UnAuthorized_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = UserReferenceFactory.Create();
            var comment = task.Comments!.First();
            var action = () => task.RemoveComment(user, comment);

            //assert
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void RemoveComment_With_Null_Comment_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = UserReferenceFactory.Create();
            var comment = CommentFactory.Create();
            var action = () => task.RemoveComment(user, null!);

            //assert
            action.Should().Throw<DomainArgumentException>();
        }

        #endregion

        #region Start

        [Fact]
        public void Start_Should_Work()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            task.Claim(user);
            task.ClearPendingEvents();

            //act
            task.Start(user);

            //assert
            task.PendingEvents.Should().NotBeNullOrEmpty();
            task.PendingEvents.Should().HaveCount(2);
            task.PendingEvents.OfType<HumanTaskStartedDomainEvent>().Should().ContainSingle();
            task.PendingEvents.OfType<HumanTaskStatusChangedDomainEvent>().Should().ContainSingle();
            task.Status.Should().Be(HumanTaskStatus.InProgress);
            task.StartedAt.Should().NotBeNull();
        }

        [Fact]
        public void Start_When_Not_Reserved_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            task.ClearPendingEvents();
            var action = () => task.Start(user);

            //assert
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Start_With_Null_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            task.Claim(user);
            task.ClearPendingEvents();
            var action = () => task.Start(null!);

            //assert
            action.Should().Throw<DomainArgumentException>();
        }

        [Fact]
        public void Start_With_Unauthorized_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            task.Claim(user);
            var action = () => task.Start(UserReferenceFactory.Create());

            //assert
            action.Should().Throw<DomainException>();
        }

        #endregion

        #region Suspend

        [Fact]
        public void Suspend_Should_Work()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            task.Claim(user);
            task.Start(user);
            task.ClearPendingEvents();

            //act
            task.Suspend(user);

            //assert
            task.PendingEvents.Should().NotBeNullOrEmpty();
            task.PendingEvents.Should().HaveCount(2);
            task.PendingEvents.OfType<HumanTaskSuspendedDomainEvent>().Should().ContainSingle();
            task.PendingEvents.OfType<HumanTaskStatusChangedDomainEvent>().Should().ContainSingle();
            task.Status.Should().Be(HumanTaskStatus.Suspended);
            task.StartedAt.Should().NotBeNull();
        }

        [Fact]
        public void Suspend_When_Not_InProgress_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            task.ClearPendingEvents();
            var action = () => task.Suspend(user);

            //assert
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Suspend_With_Null_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            task.Claim(user);
            task.Start(user);
            task.ClearPendingEvents();
            var action = () => task.Start(null!);

            //assert
            action.Should().Throw<DomainArgumentException>();
        }

        [Fact]
        public void Suspend_With_Unauthorized_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            task.Claim(user);
            task.Start(user);
            task.ClearPendingEvents();
            var action = () => task.Start(UserReferenceFactory.Create());

            //assert
            action.Should().Throw<DomainException>();
        }

        #endregion

        #region Resume

        [Fact]
        public void Resume_Should_Work()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            task.Claim(user);
            task.Start(user);
            task.Suspend(user);
            task.ClearPendingEvents();

            //act
            task.Resume(user);

            //assert
            task.PendingEvents.Should().NotBeNullOrEmpty();
            task.PendingEvents.Should().HaveCount(2);
            task.PendingEvents.OfType<HumanTaskResumedDomainEvent>().Should().ContainSingle();
            task.PendingEvents.OfType<HumanTaskStatusChangedDomainEvent>().Should().ContainSingle();
            task.Status.Should().Be(HumanTaskStatus.InProgress);
            task.StartedAt.Should().NotBeNull();
        }

        [Fact]
        public void Resume_When_Not_Suspended_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            task.ClearPendingEvents();
            var action = () => task.Resume(user);

            //assert
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Resume_With_Null_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            task.Claim(user);
            task.Start(user);
            task.Suspend(user);
            task.ClearPendingEvents();
            var action = () => task.Resume(null!);

            //assert
            action.Should().Throw<DomainArgumentException>();
        }

        [Fact]
        public void Resume_With_Unauthorized_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            task.Claim(user);
            task.Start(user);
            task.Suspend(user);
            task.ClearPendingEvents();
            var action = () => task.Resume(UserReferenceFactory.Create());

            //assert
            action.Should().Throw<DomainException>();
        }

        #endregion

        #region Skip

        [Fact]
        public void Skip_Should_Work()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            task.Claim(user);
            task.ClearPendingEvents();

            //act
            task.Skip(user);

            //assert
            task.PendingEvents.Should().NotBeNullOrEmpty();
            task.PendingEvents.Should().HaveCount(2);
            task.PendingEvents.OfType<HumanTaskSkippedDomainEvent>().Should().ContainSingle();
            task.PendingEvents.OfType<HumanTaskStatusChangedDomainEvent>().Should().ContainSingle();
            task.Status.Should().Be(HumanTaskStatus.Obsolete);
        }

        [Fact]
        public void Skip_When_Not_Active_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            var action = () => task.Skip(user);

            //assert
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Skip_With_Null_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            task.Claim(user);
            task.ClearPendingEvents();
            var action = () => task.Skip(null!);

            //assert
            action.Should().Throw<DomainArgumentException>();
        }

        [Fact]
        public void Skip_With_Unauthorized_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            task.Claim(user);
            task.ClearPendingEvents();
            var action = () => task.Skip(UserReferenceFactory.Create());

            //assert
            action.Should().Throw<DomainException>();
        }

        #endregion

        #region Cancel

        [Fact]
        public void Cancel_Should_Work()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            task.Claim(user);
            task.ClearPendingEvents();

            //act
            task.Cancel(user);

            //assert
            task.PendingEvents.Should().NotBeNullOrEmpty();
            task.PendingEvents.Should().HaveCount(2);
            task.PendingEvents.OfType<HumanTaskCancelledDomainEvent>().Should().ContainSingle();
            task.PendingEvents.OfType<HumanTaskStatusChangedDomainEvent>().Should().ContainSingle();
            task.Status.Should().Be(HumanTaskStatus.Cancelled);
        }

        [Fact]
        public void Cancel_When_Not_Active_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            task.Claim(user);
            task.Cancel(user);
            task.ClearPendingEvents();
            var action = () => task.Cancel(user);

            //assert
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Cancel_With_Null_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            task.Claim(task.PeopleAssignments.PotentialOwners!.First());
            task.ClearPendingEvents();
            var action = () => task.Cancel(null!);

            //assert
            action.Should().Throw<DomainArgumentException>();
        }

        [Fact]
        public void Cancel_With_Unauthorized_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            task.Claim(user);
            task.ClearPendingEvents();
            var action = () => task.Cancel(UserReferenceFactory.Create());

            //assert
            action.Should().Throw<DomainException>();
        }

        #endregion

        #region Fault

        [Fact]
        public void Fault_Should_Work()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            var faults = new Fault[] { FaultFactory.Create() };
            task.Claim(user);
            task.ClearPendingEvents();

            //act
            task.Fault(user, faults);

            //assert
            task.PendingEvents.Should().NotBeNullOrEmpty();
            task.PendingEvents.Should().HaveCount(2);
            task.PendingEvents.OfType<HumanTaskFaultedDomainEvent>().Should().ContainSingle();
            task.PendingEvents.OfType<HumanTaskStatusChangedDomainEvent>().Should().ContainSingle();
            task.Status.Should().Be(HumanTaskStatus.Faulted);
        }

        [Fact]
        public void Fault_When_Not_Active_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            var faults = new Fault[] { FaultFactory.Create() };
            task.Claim(user);
            task.Cancel(user);
            task.ClearPendingEvents();
            var action = () => task.Fault(user, faults);

            //assert
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Fault_With_Null_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var faults = new Fault[] { FaultFactory.Create() };
            task.Claim(task.PeopleAssignments.PotentialOwners!.First());
            task.ClearPendingEvents();
            var action = () => task.Fault(null!, faults);

            //assert
            action.Should().Throw<DomainArgumentException>();
        }

        [Fact]
        public void Fault_With_NullOrEmpty_Faults_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            task.Claim(user);
            task.ClearPendingEvents();
            var nullAction = () => task.Fault(user, null!);
            var emptyAction = () => task.Fault(user, Array.Empty<Fault>());

            //assert
            nullAction.Should().Throw<DomainArgumentException>();
            emptyAction.Should().Throw<DomainArgumentException>();
        }

        [Fact]
        public void Fault_With_Unauthorized_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            var faults = new Fault[] { FaultFactory.Create() };
            task.Claim(user);
            task.ClearPendingEvents();
            var action = () => task.Fault(UserReferenceFactory.Create(), faults);

            //assert
            action.Should().Throw<DomainException>();
        }

        #endregion

        #region AddFault

        [Fact]
        public void AddFault_Should_Work()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.BusinessAdministrators!.First();
            var faults = new Fault[] { FaultFactory.Create() };
            task.Claim(task.PeopleAssignments.PotentialOwners!.First());
            task.ClearPendingEvents();

            //act
            task.AddFault(user, faults);

            //assert
            task.PendingEvents.Should().NotBeNullOrEmpty();
            task.PendingEvents.Should().ContainSingle();
            task.PendingEvents.OfType<FaultsAddedToHumanTaskDomainEvent>().Should().ContainSingle();
        }

        [Fact]
        public void AddFault_When_Not_Active_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            var faults = new Fault[] { FaultFactory.Create() };
            task.Claim(user);
            task.Cancel(user);
            task.ClearPendingEvents();
            var action = () => task.AddFault(user, faults);

            //assert
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void AddFault_With_Null_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var faults = new Fault[] { FaultFactory.Create() };
            task.Claim(task.PeopleAssignments.PotentialOwners!.First());
            task.ClearPendingEvents();
            var action = () => task.AddFault(null!, faults);

            //assert
            action.Should().Throw<DomainArgumentException>();
        }

        [Fact]
        public void AddFault_With_NullOrEmpty_Faults_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            task.Claim(user);
            task.ClearPendingEvents();
            var nullAction = () => task.AddFault(user, null!);
            var emptyAction = () => task.AddFault(user, Array.Empty<Fault>());

            //assert
            nullAction.Should().Throw<DomainArgumentException>();
            emptyAction.Should().Throw<DomainArgumentException>();
        }

        [Fact]
        public void AddFault_With_Unauthorized_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.PotentialOwners!.First();
            var faults = new Fault[] { FaultFactory.Create() };
            task.Claim(user);
            task.ClearPendingEvents();
            var action = () => task.AddFault(UserReferenceFactory.Create(), faults);

            //assert
            action.Should().Throw<DomainException>();
        }

        #endregion

        #region RemoveFault

        [Fact]
        public void RemoveFault_Should_Work()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.BusinessAdministrators!.First();
            var faults = new Fault[] { FaultFactory.Create() };
            task.Claim(task.PeopleAssignments.PotentialOwners!.First());
            task.AddFault(user, faults);
            task.ClearPendingEvents();

            //act
            var succeeded = task.RemoveFault(user, faults.First());

            //assert
            succeeded.Should().BeTrue();
            task.PendingEvents.Should().NotBeNullOrEmpty();
            task.PendingEvents.Should().ContainSingle();
            task.PendingEvents.OfType<FaultRemovedFromHumanTaskDomainEvent>().Should().ContainSingle();
        }

        [Fact]
        public void RemoveFault_When_Not_Active_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.BusinessAdministrators!.First();
            var faults = new Fault[] { FaultFactory.Create() };
            task.Claim(task.PeopleAssignments.PotentialOwners!.First());
            task.AddFault(user, faults);
            task.Cancel(user);
            task.ClearPendingEvents();
            var action = () => task.RemoveFault(user, faults.First());

            //assert
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void RemoveFault_With_Null_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.BusinessAdministrators!.First();
            var faults = new Fault[] { FaultFactory.Create() };
            task.Claim(task.PeopleAssignments.PotentialOwners!.First());
            task.AddFault(user, faults);
            task.ClearPendingEvents();
            var action = () => task.RemoveFault(null!, faults.First());

            //assert
            action.Should().Throw<DomainArgumentException>();
        }

        [Fact]
        public void RemoveFault_With_Unauthorized_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.BusinessAdministrators!.First();
            var fault = FaultFactory.Create();
            task.Claim(task.PeopleAssignments.PotentialOwners!.First());
            task.AddFault(user, fault);
            task.ClearPendingEvents();
            var action = () => task.RemoveFault(UserReferenceFactory.Create(), fault);

            //assert
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void RemoveFault_Non_Existing_Fault_Should_Fail()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.BusinessAdministrators!.First();
            var faults = new Fault[] { FaultFactory.Create() };
            task.Claim(task.PeopleAssignments.PotentialOwners!.First());
            task.AddFault(user, faults);
            task.ClearPendingEvents();

            //act
            var succeeded = task.RemoveFault(user, FaultFactory.Create());

            //assert
            succeeded.Should().BeFalse();
        }

        #endregion

        #region Complete



        #endregion

        #region Delete

        [Fact]
        public void Delete_Should_Work()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.BusinessAdministrators!.First();

            //act
            task.Delete(user);

            //assert
            task.PendingEvents.Should().NotBeNullOrEmpty();
            task.PendingEvents.Should().ContainSingle();
            task.PendingEvents.OfType<HumanTaskDeletedDomainEvent>().Should().ContainSingle();
        }

        [Fact]
        public void Delete_With_Null_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var action = () => task.Delete(null!);

            //assert
            action.Should().Throw<DomainArgumentException>();
        }

        [Fact]
        public void Delete_With_Unauthorized_User_Should_Throw()
        {
            //arrange
            var task = HumanTaskFactory.Create();
            var user = task.PeopleAssignments.BusinessAdministrators!.First();
            var action = () => task.Delete(UserReferenceFactory.Create());

            //assert
            action.Should().Throw<DomainException>();
        }

        #endregion

    }

}
