// Copyright © 2022-Present The Open Human Task Specification Authors. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License")
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using OpenHumanTask.Runtime.Domain.Events.HumanTasks;

namespace OpenHumanTask.Runtime.Application.Events.Domain
{

    /// <summary>
    /// Represents the service used to handle <see cref="HumanTask"/>-related <see cref="IDomainEvent"/>s
    /// </summary>
    public class HumanTaskDomainEventHandler
        : DomainEventHandlerBase<HumanTask, Integration.Models.HumanTask, string>,
        INotificationHandler<HumanTaskCreatedDomainEvent>,
        INotificationHandler<HumanTaskPriorityChangedDomainEvent>,
        INotificationHandler<HumanTaskReassignedDomainEvent>,
        INotificationHandler<HumanTaskClaimedDomainEvent>,
        INotificationHandler<HumanTaskReleasedDomainEvent>,
        INotificationHandler<HumanTaskDelegatedDomainEvent>,
        INotificationHandler<HumanTaskForwardedDomainEvent>,
        INotificationHandler<AttachmentAddedToHumanTaskDomainEvent>,
        INotificationHandler<AttachmentRemovedFromHumanTaskDomainEvent>,
        INotificationHandler<CommentAddedToHumanTaskDomainEvent>,
        INotificationHandler<HumanTaskCommentUpdatedDomainEvent>,
        INotificationHandler<CommentRemovedFromHumanTaskDomainEvent>,
        INotificationHandler<HumanTaskStartedDomainEvent>,
        INotificationHandler<HumanTaskSuspendedDomainEvent>,
        INotificationHandler<HumanTaskResumedDomainEvent>,
        INotificationHandler<HumanTaskSkippedDomainEvent>,
        INotificationHandler<HumanTaskCancelledDomainEvent>,
        INotificationHandler<HumanTaskFaultedDomainEvent>,
        INotificationHandler<FaultsAddedToHumanTaskDomainEvent>,
        INotificationHandler<FaultRemovedFromHumanTaskDomainEvent>,
        INotificationHandler<HumanTaskCompletedDomainEvent>,
        INotificationHandler<HumanTaskDeletedDomainEvent>
    {

        /// <inheritdoc/>
        public HumanTaskDomainEventHandler(ILoggerFactory loggerFactory, IMapper mapper, IMediator mediator, ICloudEventBus cloudEventBus, 
            IRepository<HumanTask, string> aggregates, IRepository<Integration.Models.HumanTask, string> projections) 
            : base(loggerFactory, mapper, mediator, cloudEventBus, aggregates, projections)
        {

        }

        /// <inheritdoc/>
        public virtual async Task HandleAsync(HumanTaskCreatedDomainEvent e, CancellationToken cancellationToken = default)
        {
            await this.GetOrReconcileProjectionAsync(e.AggregateId, cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task HandleAsync(HumanTaskPriorityChangedDomainEvent e, CancellationToken cancellationToken = default)
        {
            var task = await this.GetOrReconcileProjectionAsync(e.AggregateId, cancellationToken);
            task.LastModified = e.CreatedAt;
            task.Priority = e.Priority;
            await this.Projections.UpdateAsync(task, cancellationToken);
            await this.Projections.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task HandleAsync(HumanTaskReassignedDomainEvent e, CancellationToken cancellationToken = default)
        {
            var task = await this.GetOrReconcileProjectionAsync(e.AggregateId, cancellationToken);
            task.LastModified = e.CreatedAt;
            task.PeopleAssignments = this.Mapper.Map<Integration.Models.PeopleAssignments>(e.Assignments);
            await this.Projections.UpdateAsync(task, cancellationToken);
            await this.Projections.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task HandleAsync(HumanTaskClaimedDomainEvent e, CancellationToken cancellationToken = default)
        {
            var task = await this.GetOrReconcileProjectionAsync(e.AggregateId, cancellationToken);
            task.LastModified = e.CreatedAt;
            task.PeopleAssignments.ActualOwner = this.Mapper.Map<Integration.Models.UserReference>(e.User);
            await this.Projections.UpdateAsync(task, cancellationToken);
            await this.Projections.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task HandleAsync(HumanTaskReleasedDomainEvent e, CancellationToken cancellationToken = default)
        {
            var task = await this.GetOrReconcileProjectionAsync(e.AggregateId, cancellationToken);
            task.LastModified = e.CreatedAt;
            task.PeopleAssignments.ActualOwner = null;
            await this.Projections.UpdateAsync(task, cancellationToken);
            await this.Projections.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task HandleAsync(HumanTaskDelegatedDomainEvent e, CancellationToken cancellationToken = default)
        {
            var task = await this.GetOrReconcileProjectionAsync(e.AggregateId, cancellationToken);
            task.LastModified = e.CreatedAt;
            task.PeopleAssignments.ActualOwner = this.Mapper.Map<Integration.Models.UserReference>(e.DelegatedTo);
            await this.Projections.UpdateAsync(task, cancellationToken);
            await this.Projections.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task HandleAsync(HumanTaskForwardedDomainEvent e, CancellationToken cancellationToken = default)
        {
            var task = await this.GetOrReconcileProjectionAsync(e.AggregateId, cancellationToken);
            task.LastModified = e.CreatedAt;
            task.PeopleAssignments.ActualOwner = null;
            var user = this.Mapper.Map<Integration.Models.UserReference>(e.User);
            task.PeopleAssignments.PotentialOwners.Remove(user);
            foreach(var forwardTo in e.ForwardedTo.Select(u => this.Mapper.Map<Integration.Models.UserReference>(u)))
            {
                if (task.PeopleAssignments.PotentialOwners.Contains(forwardTo)) continue;
                task.PeopleAssignments.PotentialOwners.Add(forwardTo);

            }
            await this.Projections.UpdateAsync(task, cancellationToken);
            await this.Projections.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task HandleAsync(AttachmentAddedToHumanTaskDomainEvent e, CancellationToken cancellationToken = default)
        {
            var task = await this.GetOrReconcileProjectionAsync(e.AggregateId, cancellationToken);
            task.LastModified = e.CreatedAt;
            task.Attachments ??= new();
            if (task.Attachments.Any(a => a.Id.Equals(e.Attachment.Id, StringComparison.InvariantCultureIgnoreCase))) return;
            task.Attachments.Add(this.Mapper.Map<Integration.Models.Attachment>(e.Attachment));
            await this.Projections.UpdateAsync(task, cancellationToken);
            await this.Projections.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task HandleAsync(AttachmentRemovedFromHumanTaskDomainEvent e, CancellationToken cancellationToken = default)
        {
            var task = await this.GetOrReconcileProjectionAsync(e.AggregateId, cancellationToken);
            task.LastModified = e.CreatedAt;
            if (task.Attachments == null) return;
            var attachment = task.Attachments.SingleOrDefault(a => a.Id.Equals(e.AttachmentId, StringComparison.InvariantCultureIgnoreCase));
            if (attachment == null) return;
            task.Attachments.Remove(attachment);
            await this.Projections.UpdateAsync(task, cancellationToken);
            await this.Projections.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task HandleAsync(CommentAddedToHumanTaskDomainEvent e, CancellationToken cancellationToken = default)
        {
            var task = await this.GetOrReconcileProjectionAsync(e.AggregateId, cancellationToken);
            task.LastModified = e.CreatedAt;
            task.Comments ??= new();
            if (task.Comments.Any(a => a.Id.Equals(e.Comment.Id, StringComparison.InvariantCultureIgnoreCase))) return;
            task.Comments.Add(this.Mapper.Map<Integration.Models.Comment>(e.Comment));
            await this.Projections.UpdateAsync(task, cancellationToken);
            await this.Projections.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task HandleAsync(HumanTaskCommentUpdatedDomainEvent e, CancellationToken cancellationToken = default)
        {
            var task = await this.GetOrReconcileProjectionAsync(e.AggregateId, cancellationToken);
            task.LastModified = e.CreatedAt;
            if (task.Comments == null) return;
            var comment = task.Comments.SingleOrDefault(a => a.Id.Equals(e.CommentId, StringComparison.InvariantCultureIgnoreCase));
            if (comment == null) return;
            comment.LastModified = e.CreatedAt;
            comment.LastModifiedBy = this.Mapper.Map<Integration.Models.UserReference>(e.User);
            comment.Content = e.Content;
            await this.Projections.UpdateAsync(task, cancellationToken);
            await this.Projections.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task HandleAsync(CommentRemovedFromHumanTaskDomainEvent e, CancellationToken cancellationToken = default)
        {
            var task = await this.GetOrReconcileProjectionAsync(e.AggregateId, cancellationToken);
            task.LastModified = e.CreatedAt;
            if (task.Comments == null) return;
            var comment = task.Comments.SingleOrDefault(a => a.Id.Equals(e.CommentId, StringComparison.InvariantCultureIgnoreCase));
            if (comment == null) return;
            task.Comments.Remove(comment);
            await this.Projections.UpdateAsync(task, cancellationToken);
            await this.Projections.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task HandleAsync(HumanTaskStartedDomainEvent e, CancellationToken cancellationToken = default)
        {
            var task = await this.GetOrReconcileProjectionAsync(e.AggregateId, cancellationToken);
            task.LastModified = e.CreatedAt;
            task.StartedAt = e.CreatedAt;
            await this.Projections.UpdateAsync(task, cancellationToken);
            await this.Projections.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task HandleAsync(HumanTaskSuspendedDomainEvent e, CancellationToken cancellationToken = default)
        {
            var task = await this.GetOrReconcileProjectionAsync(e.AggregateId, cancellationToken);

            await this.Projections.UpdateAsync(task, cancellationToken);
            await this.Projections.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task HandleAsync(HumanTaskResumedDomainEvent e, CancellationToken cancellationToken = default)
        {
            var task = await this.GetOrReconcileProjectionAsync(e.AggregateId, cancellationToken);

            await this.Projections.UpdateAsync(task, cancellationToken);
            await this.Projections.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task HandleAsync(HumanTaskSkippedDomainEvent e, CancellationToken cancellationToken = default)
        {
            var task = await this.GetOrReconcileProjectionAsync(e.AggregateId, cancellationToken);

            await this.Projections.UpdateAsync(task, cancellationToken);
            await this.Projections.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task HandleAsync(HumanTaskCancelledDomainEvent e, CancellationToken cancellationToken = default)
        {
            var task = await this.GetOrReconcileProjectionAsync(e.AggregateId, cancellationToken);
            task.LastModified = e.CreatedAt;

            await this.Projections.UpdateAsync(task, cancellationToken);
            await this.Projections.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task HandleAsync(HumanTaskFaultedDomainEvent e, CancellationToken cancellationToken = default)
        {
            var task = await this.GetOrReconcileProjectionAsync(e.AggregateId, cancellationToken);
            task.LastModified = e.CreatedAt;
            task.Faults ??= new();
            foreach (var fault in e.Faults.Select(f => this.Mapper.Map<Integration.Models.Fault>(f)))
            {
                if (task.Faults.Any(f => f.Id.Equals(fault.Id, StringComparison.InvariantCultureIgnoreCase))) continue;
                task.Faults.Add(fault);
            }
            await this.Projections.UpdateAsync(task, cancellationToken);
            await this.Projections.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task HandleAsync(FaultsAddedToHumanTaskDomainEvent e, CancellationToken cancellationToken = default)
        {
            var task = await this.GetOrReconcileProjectionAsync(e.AggregateId, cancellationToken);
            task.LastModified = e.CreatedAt;
            task.Faults ??= new();
            foreach(var fault in e.Faults.Select(f => this.Mapper.Map<Integration.Models.Fault>(f)))
            {
                if (task.Faults.Any(f => f.Id.Equals(fault.Id, StringComparison.InvariantCultureIgnoreCase))) continue;
                task.Faults.Add(fault);
            }
            await this.Projections.UpdateAsync(task, cancellationToken);
            await this.Projections.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task HandleAsync(FaultRemovedFromHumanTaskDomainEvent e, CancellationToken cancellationToken = default)
        {
            var task = await this.GetOrReconcileProjectionAsync(e.AggregateId, cancellationToken);
            task.LastModified = e.CreatedAt;
            if (task.Faults == null) return;
            var fault = task.Faults.SingleOrDefault(a => a.Id.Equals(e.FaultId, StringComparison.InvariantCultureIgnoreCase));
            if (fault == null) return;
            task.Faults.Remove(fault);
            await this.Projections.UpdateAsync(task, cancellationToken);
            await this.Projections.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task HandleAsync(HumanTaskCompletedDomainEvent e, CancellationToken cancellationToken = default)
        {
            var task = await this.GetOrReconcileProjectionAsync(e.AggregateId, cancellationToken);
            task.LastModified = e.CreatedAt;
            task.CompletedAt = e.CreatedAt;
            task.Outcome = e.Outcome;
            task.Output = e.Output;
            task.CompletionBehaviorName = e.CompletionBehaviorName;
            await this.Projections.UpdateAsync(task, cancellationToken);
            await this.Projections.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task HandleAsync(HumanTaskDeletedDomainEvent e, CancellationToken cancellationToken = default)
        {
            await this.Projections.RemoveAsync(e.AggregateId, cancellationToken);
        }

    }

}
