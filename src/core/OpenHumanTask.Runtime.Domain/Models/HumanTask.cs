using OpenHumanTask.Runtime.Domain.Events.HumanTasks;
using OpenHumanTask.Sdk;
using System.Data;

namespace OpenHumanTask.Runtime.Domain.Models
{

    /// <summary>
    /// Represents a human-computer interaction
    /// </summary>
    [DataTransferObjectType(typeof(Integration.Models.HumanTask))]
    public class HumanTask
        : AggregateRoot<string>
    {

        /// <summary>
        /// Initializes a new <see cref="HumanTask"/>
        /// </summary>
        protected HumanTask() : base(string.Empty) { }

        /// <summary>
        /// Initializes a new <see cref="HumanTask"/>
        /// </summary>
        /// <param name="definition">The <see cref="HumanTask"/>'s <see cref="HumanTaskDefinition"/></param>
        /// <param name="key">The <see cref="HumanTask"/>'s key</param>
        /// <param name="peopleAssignments">The people assigned to the <see cref="HumanTask"/></param>
        /// <param name="title">An <see cref="IDictionary{TKey, TValue}"/> that contains the <see cref="HumanTask"/>'s localized title language/value pairs</param>
        /// <param name="subject">An <see cref="IDictionary{TKey, TValue}"/> that contains the <see cref="HumanTask"/>'s localized subject language/value pairs</param>
        /// <param name="description">An <see cref="IDictionary{TKey, TValue}"/> that contains the <see cref="HumanTask"/>'s localized description language/value pairs</param>
        /// <param name="input">The <see cref="HumanTask"/>'s input</param>
        /// <param name="subtasks">An <see cref="IEnumerable{T}"/> containing the <see cref="HumanTask"/>'s <see cref="Subtask"/>s</param>
        /// <param name="attachments">An <see cref="IEnumerable{T}"/> containg the <see cref="HumanTask"/>'s <see cref="Attachment"/>s</param>
        /// <param name="comments">An <see cref="IEnumerable{T}"/> containg the <see cref="HumanTask"/>'s <see cref="Comment"/>s</param>
        public HumanTask(HumanTaskDefinition definition, string key, PeopleAssignments peopleAssignments, IDictionary<string, string>? title = null, IDictionary<string, string>? subject = null, 
            IDictionary<string, string>? description = null, object? input = null, IEnumerable<Subtask>? subtasks = null, IEnumerable<Attachment>? attachments = null, IEnumerable<Comment>? comments = null)
            : base(BuildId(definition?.Id!, key))
        {
            if (definition == null) throw DomainException.ArgumentNull(nameof(definition));
            if(peopleAssignments == null) throw DomainException.ArgumentNull(nameof(peopleAssignments));
            if (string.IsNullOrWhiteSpace(key)) throw DomainException.ArgumentNullOrWhitespace(nameof(key));
            this.On(this.RegisterEvent(new HumanTaskCreatedDomainEvent(this.Id, definition.Id, key, peopleAssignments, title, subject, description, input, subtasks, attachments, comments)));
        }

        /// <summary>
        /// Gets the id of the <see cref="HumanTask"/>'s <see cref="HumanTaskDefinition"/>
        /// </summary>
        public virtual string DefinitionId { get; protected set; } = null!;

        /// <summary>
        /// Gets the <see cref="HumanTask"/>'s key
        /// </summary>
        public virtual string Key { get; protected set; } = null!;

        /// <summary>
        /// Gets the <see cref="HumanTask"/>'s status
        /// </summary>
        public virtual HumanTaskStatus Status { get; protected set; } = HumanTaskStatus.Created;

        /// <summary>
        /// Gets the people assigned to the <see cref="HumanTask"/>
        /// </summary>
        public virtual PeopleAssignments PeopleAssignments { get; protected set; } = null!;

        [Newtonsoft.Json.JsonProperty(nameof(Title))]
        [JsonPropertyName(nameof(Title))]
        private IDictionary<string, string>? _title;
        /// <summary>
        /// Gets an <see cref="IReadOnlyDictionary{TKey, TValue}"/> that contains the <see cref="HumanTask"/>'s localized title language/value pairs
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [JsonIgnore]
        public virtual IReadOnlyDictionary<string, string>? Title => this._title?.AsReadOnly();

        [Newtonsoft.Json.JsonProperty(nameof(Subject))]
        [JsonPropertyName(nameof(Subject))]
        private IDictionary<string, string>? _subject;
        /// <summary>
        /// Gets an <see cref="IReadOnlyDictionary{TKey, TValue}"/> that contains the <see cref="HumanTask"/>'s localized subject language/value pairs
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [JsonIgnore]
        public virtual IReadOnlyDictionary<string, string>? Subject => this._subject?.AsReadOnly();

        [Newtonsoft.Json.JsonProperty(nameof(Description))]
        [JsonPropertyName(nameof(Description))]
        private IDictionary<string, string>? _description;
        /// <summary>
        /// Gets an <see cref="IReadOnlyDictionary{TKey, TValue}"/> that contains the <see cref="HumanTask"/>'s localized description language/value pairs
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [JsonIgnore]
        public virtual IReadOnlyDictionary<string, string>? Description => this._description?.AsReadOnly();

        /// <summary>
        /// Gets the <see cref="HumanTask"/>'s input
        /// </summary>
        public virtual object? Input { get; protected set; }

        /// <summary>
        /// Gets the <see cref="HumanTask"/>'s output
        /// </summary>
        public virtual object? Output { get; protected set; }

        private List<Subtask>? _subtasks;
        /// <summary>
        /// Gets an <see cref="IReadOnlyCollection{T}"/> containing the <see cref="HumanTask"/>'s <see cref="Subtask"/>s
        /// </summary>
        public virtual IReadOnlyCollection<Subtask>? Subtasks => this._subtasks?.AsReadOnly();

        private List<Attachment>? _attachments;
        /// <summary>
        /// Gets an <see cref="IReadOnlyCollection{T}"/> containing the <see cref="HumanTask"/>'s <see cref="Attachment"/>s
        /// </summary>
        public virtual IReadOnlyCollection<Attachment>? Attachments => this._attachments?.AsReadOnly();

        private List<Comment>? _comments;
        /// <summary>
        /// Gets an <see cref="IReadOnlyCollection{T}"/> containing the <see cref="HumanTask"/>'s <see cref="Comment"/>s
        /// </summary>
        public virtual IReadOnlyCollection<Comment>? Comments => this._comments?.AsReadOnly();

        /// <summary>
        /// Determines whether or not the user is in the specified role(s)
        /// </summary>
        /// <param name="roles">The <see cref="GenericHumanRole"/>(s) the user must have</param>
        /// <param name="user">The user to check</param>
        /// <returns>A boolean indicating whether or not the user is in the specified role(s)</returns>
        public virtual bool DefinesRoleFor(GenericHumanRole roles, UserReference user)
        {
            if (user == null) throw DomainException.ArgumentNull(nameof(user));
            foreach(var role in EnumHelper.GetFlags(roles).Except(new GenericHumanRole[] { GenericHumanRole.None }))
            {
                if (role switch
                {
                    GenericHumanRole.ActualOwner => this.PeopleAssignments.ActualOwner == user,
                    GenericHumanRole.BusinessAdministrator => this.PeopleAssignments.BusinessAdministrators?.Contains(user) == true,
                    GenericHumanRole.ExcludedOwner => this.PeopleAssignments.ExcludedOwners?.Contains(user) == true,
                    GenericHumanRole.Initiator => this.PeopleAssignments.ActualInitiator == user,
                    GenericHumanRole.None => true,
                    GenericHumanRole.NotificationRecipient => this.PeopleAssignments.NotificationRecipients?.Contains(user) == true,
                    GenericHumanRole.PotentialInitiator => this.PeopleAssignments.PotentialInitiators?.Contains(user) == true,
                    GenericHumanRole.PotentialOwner => this.PeopleAssignments.PotentialOwners?.Contains(user) == true,
                    GenericHumanRole.Stakeholder => this.PeopleAssignments.Stakeholders?.Contains(user) == true,
                    _ => throw new NotSupportedException($"The specified {nameof(GenericHumanRole)} '{EnumHelper.Stringify(roles)}' is not supported")
                })
                return true;
            }
            return false;
        }

        /// <summary>
        /// Assigns the specified people to the <see cref="HumanTask"/>
        /// </summary>
        /// <param name="user">The user assigning people to the <see cref="HumanTask"/></param>
        /// <param name="assignments">The people to assign to the <see cref="HumanTask"/></param>
        public virtual void Assign(UserReference user, PeopleAssignments assignments)
        {
            if (user == null) throw DomainException.ArgumentNull(nameof(user));
            if (assignments == null) throw DomainException.ArgumentNull(nameof(assignments));
            var roles = GenericHumanRole.BusinessAdministrator | GenericHumanRole.Initiator | GenericHumanRole.Stakeholder;
            if (!this.DefinesRoleFor(roles, user)) throw HumanTaskDomainExceptions.UserNotInRoles(user, nameof(Assign), roles);
        }

        /// <summary>
        /// Claims the <see cref="HumanTask"/>
        /// </summary>
        /// <param name="user">The user claiming the <see cref="HumanTask"/></param>
        public virtual void Claim(UserReference user)
        {
            if(user == null) throw DomainException.ArgumentNull(nameof(user));
            if (this.Status != HumanTaskStatus.Ready) throw DomainException.UnexpectedState(typeof(HumanTask), this.Id, this.Status);
            if (!this.DefinesRoleFor(GenericHumanRole.PotentialOwner, user)) throw HumanTaskDomainExceptions.UserNotInRoles(user, nameof(Claim), GenericHumanRole.PotentialOwner);
            this.On(this.RegisterEvent(new HumanTaskClaimedDomainEvent(this.Id, user)));
        }

        /// <summary>
        /// Releases the <see cref="HumanTask"/> from its actual owner
        /// </summary>
        /// <param name="user">The user releasing the <see cref="HumanTask"/></param>
        public virtual void Release(UserReference user)
        {
            if (user == null) throw DomainException.ArgumentNull(nameof(user));
            if (this.PeopleAssignments.ActualOwner == null) throw HumanTaskDomainExceptions.OwnerlessHumanTask(this);
            var roles = GenericHumanRole.ActualOwner | GenericHumanRole.BusinessAdministrator;
            if (!this.DefinesRoleFor(roles, user)) throw HumanTaskDomainExceptions.UserNotInRoles(user, nameof(Release), roles);
            if (this.Status != HumanTaskStatus.Reserved && this.Status != HumanTaskStatus.InProgress)
                throw DomainException.UnexpectedState(typeof(HumanTask), this.Id, this.Status);
            this.On(this.RegisterEvent(new HumanTaskReleasedDomainEvent(this.Id, user)));
        }

        /// <summary>
        /// Delegates the <see cref="HumanTask"/> to the specified user
        /// </summary>
        /// <param name="user">The user delegating the <see cref="HumanTask"/></param>
        /// <param name="delegateTo">The user to delegate the <see cref="HumanTask"/> to</param>
        public virtual void Delegate(UserReference user, UserReference delegateTo)
        {
            if (user == null) throw DomainException.ArgumentNull(nameof(user));
            if (delegateTo == null) throw DomainException.ArgumentNull(nameof(delegateTo));
            if (this.Status != HumanTaskStatus.Ready
                && this.Status != HumanTaskStatus.Reserved
                && this.Status != HumanTaskStatus.InProgress)
                throw DomainException.UnexpectedState(typeof(HumanTask), this.Id, this.Status);
            var roles = GenericHumanRole.ActualOwner | GenericHumanRole.BusinessAdministrator;
            if (!this.DefinesRoleFor(roles, user)) throw HumanTaskDomainExceptions.UserNotInRoles(user, nameof(Release), roles);
            if (this.Status == HumanTaskStatus.Reserved || this.Status == HumanTaskStatus.InProgress) this.Release(user);
            this.On(this.RegisterEvent(new HumanTaskDelegatedDomainEvent(this.Id, user, delegateTo)));
        }

        /// <summary>
        /// Forwards the <see cref="HumanTask"/> to the specified group of users
        /// </summary>
        /// <param name="user">The user forwarding the <see cref="HumanTask"/></param>
        /// <param name="forwardTo">An <see cref="IEnumerable{T}"/> containing the users to forward the <see cref="HumanTask"/> to</param>
        public virtual void Forward(UserReference user, IEnumerable<UserReference> forwardTo)
        {
            if (user == null) throw DomainException.ArgumentNull(nameof(user));
            if (forwardTo == null) throw DomainException.ArgumentNull(nameof(forwardTo));
            if (!forwardTo.Any()) throw DomainException.ArgumentMustHaveMinimumLengthOf(nameof(forwardTo), 1);
            if (this.Status != HumanTaskStatus.Reserved
                && this.Status != HumanTaskStatus.InProgress)
                throw DomainException.UnexpectedState(typeof(HumanTask), this.Id, this.Status);
            var roles = GenericHumanRole.ActualOwner | GenericHumanRole.BusinessAdministrator | GenericHumanRole.Stakeholder;
            if (!this.DefinesRoleFor(roles, user))
            {
                if (this.DefinesRoleFor(GenericHumanRole.PotentialOwner, user))
                    if (this.Status != HumanTaskStatus.Ready) throw DomainException.UnexpectedState(typeof(HumanTask), this.Id, this.Status);
                else
                    throw HumanTaskDomainExceptions.UserNotInRoles(user, nameof(Release), roles);
            }
            if (this.Status == HumanTaskStatus.Reserved || this.Status == HumanTaskStatus.InProgress) this.Release(user);
            this.On(this.RegisterEvent(new HumanTaskForwardedDomainEvent(this.Id, user, forwardTo)));
        }

        /// <summary>
        /// Adds the specified <see cref="Attachment"/>
        /// </summary>
        /// <param name="user">The user adding the <see cref="Attachment"/></param>
        /// <param name="attachment">The <see cref="Attachment"/> to add</param>
        public virtual void AddAttachment(UserReference user, Attachment attachment)
        {
            if (user == null) throw DomainException.ArgumentNull(nameof(user));
            if (attachment == null) throw DomainException.ArgumentNull(nameof(attachment));
            var roles = GenericHumanRole.ActualOwner | GenericHumanRole.BusinessAdministrator | GenericHumanRole.Stakeholder;
            if (!this.DefinesRoleFor(roles, user))
            {
                if (this.DefinesRoleFor(GenericHumanRole.PotentialOwner, user))
                    if (this.Status != HumanTaskStatus.Ready) throw DomainException.UnexpectedState(typeof(HumanTask), this.Id, this.Status);
                    else
                        throw HumanTaskDomainExceptions.UserNotInRoles(user, nameof(Release), roles);
            }
            this.On(this.RegisterEvent(new AttachmentAddedToHumanTaskDomainEvent(this.Id, user, attachment)));
        }

        /// <summary>
        /// Removes the specified <see cref="Attachment"/>
        /// </summary>
        /// <param name="user">The user removing the <see cref="Attachment"/></param>
        /// <param name="attachment">The <see cref="Attachment"/> to remove</param>
        /// <returns>A boolean indicating whether or not the operation was successfull</returns>
        public virtual bool RemoveAttachment(UserReference user, Attachment attachment)
        {
            if (user == null) throw DomainException.ArgumentNull(nameof(user));
            if (attachment == null) throw DomainException.ArgumentNull(nameof(attachment));
            var roles = GenericHumanRole.ActualOwner | GenericHumanRole.BusinessAdministrator | GenericHumanRole.Stakeholder;
            if (!this.DefinesRoleFor(roles, user))
            {
                if (this.DefinesRoleFor(GenericHumanRole.PotentialOwner, user))
                    if (this.Status != HumanTaskStatus.Ready) throw DomainException.UnexpectedState(typeof(HumanTask), this.Id, this.Status);
                    else
                        throw HumanTaskDomainExceptions.UserNotInRoles(user, nameof(Release), roles);
            }
            if (this._attachments?.Any(a => a.Id.Equals(attachment.Id, StringComparison.InvariantCultureIgnoreCase)) != true) return false;
            this.On(this.RegisterEvent(new AttachmentRemovedFromHumanTaskDomainEvent(this.Id, user, attachment.Id)));
            return true;
        }

        /// <summary>
        /// Adds the specified <see cref="Comment"/>
        /// </summary>
        /// <param name="author">The user that has authored the <see cref="Comment"/></param>
        /// <param name="content">The content of the <see cref="Comment"/> to add</param>
        public virtual void AddComment(UserReference author, string content)
        {
            if (author == null) throw DomainException.ArgumentNull(nameof(author));
            if (string.IsNullOrWhiteSpace(content)) throw DomainException.ArgumentNullOrWhitespace(nameof(content));
            var roles = GenericHumanRole.ActualOwner | GenericHumanRole.BusinessAdministrator | GenericHumanRole.Stakeholder;
            if (!this.DefinesRoleFor(roles, author))
            {
                if (this.DefinesRoleFor(GenericHumanRole.PotentialOwner, author))
                    if (this.Status != HumanTaskStatus.Ready) throw DomainException.UnexpectedState(typeof(HumanTask), this.Id, this.Status);
                    else
                        throw HumanTaskDomainExceptions.UserNotInRoles(author, nameof(Release), roles);
            }
            this.On(this.RegisterEvent(new CommentAddedToHumanTaskDomainEvent(this.Id, author, new(author, content))));
        }

        /// <summary>
        /// Updates the specified <see cref="Comment"/>
        /// </summary>
        /// <param name="user">The user updating the <see cref="Comment"/></param>
        /// <param name="comment">The <see cref="Comment"/> to update</param>
        /// <param name="content">The updated <see cref="Comment"/></param>
        /// <returns>A boolean indicating whether or not the operation was successfull</returns>
        public virtual bool UpdateComment(UserReference user, Comment comment, string content)
        {
            if (user == null) throw DomainException.ArgumentNull(nameof(user));
            if (comment == null) throw DomainException.ArgumentNull(nameof(comment));
            if(string.IsNullOrWhiteSpace(content)) throw DomainException.ArgumentNull(nameof(content));
            var roles = GenericHumanRole.ActualOwner | GenericHumanRole.BusinessAdministrator | GenericHumanRole.Stakeholder;
            if (!this.DefinesRoleFor(roles, user))
            {
                if (this.DefinesRoleFor(GenericHumanRole.PotentialOwner, user))
                    if (this.Status != HumanTaskStatus.Ready) throw DomainException.UnexpectedState(typeof(HumanTask), this.Id, this.Status);
                    else
                        throw HumanTaskDomainExceptions.UserNotInRoles(user, nameof(Release), roles);
            }
            if (this._comments?.Any(c => c.Id.Equals(comment.Id)) != true) return false;
            this.On(this.RegisterEvent(new HumanTaskCommentUpdatedDomainEvent(this.Id, user, comment.Id, content)));
            return true;
        }

        /// <summary>
        /// Removes the specified <see cref="Comment"/>
        /// </summary>
        /// <param name="user">The user removing the <see cref="Comment"/></param>
        /// <param name="comment">The <see cref="Comment"/> to remove</param>
        /// <returns>A boolean indicating whether or not the operation was successfull</returns>
        public virtual bool RemoveComment(UserReference user, Comment comment)
        {
            if (user == null) throw DomainException.ArgumentNull(nameof(user));
            if (comment == null) throw DomainException.ArgumentNull(nameof(comment));
            var roles = GenericHumanRole.ActualOwner | GenericHumanRole.BusinessAdministrator | GenericHumanRole.Stakeholder;
            if (!this.DefinesRoleFor(roles, user))
            {
                if (this.DefinesRoleFor(GenericHumanRole.PotentialOwner, user))
                    if (this.Status != HumanTaskStatus.Ready) throw DomainException.UnexpectedState(typeof(HumanTask), this.Id, this.Status);
                    else
                        throw HumanTaskDomainExceptions.UserNotInRoles(user, nameof(Release), roles);
            }
            if (this._comments?.Any(c => c.Id.Equals(comment.Id, StringComparison.InvariantCultureIgnoreCase)) != true) return false;
            this.On(this.RegisterEvent(new CommentRemovedFromHumanTaskDomainEvent(this.Id, user, comment.Id)));
            return true;
        }

        /// <summary>
        /// Handles the specified <see cref="HumanTaskCreatedDomainEvent"/>
        /// </summary>
        /// <param name="e">The <see cref="HumanTaskCreatedDomainEvent"/> to handle</param>
        protected virtual void On(HumanTaskCreatedDomainEvent e)
        {
            this.Id = e.AggregateId;
            this.CreatedAt = e.CreatedAt;
            this.LastModified = e.CreatedAt;
            this.DefinitionId = e.DefinitionId;
            this.Key = e.Key;
            this.PeopleAssignments = e.PeopleAssignments;
            this._title = e.Title;
            this._subject = e.Subject;
            this._description = e.Description;
            this.Input = e.Input;
            this._subtasks = e.Subtasks?.ToList();
            this._attachments = e.Attachments?.ToList();
            this._comments = e.Comments?.ToList();
            this.On(this.RegisterEvent(new HumanTaskStatusChangedDomainEvent(this.Id, HumanTaskStatus.Ready)));
        }

        /// <summary>
        /// Handles the specified <see cref="HumanTaskStatusChangedDomainEvent"/>
        /// </summary>
        /// <param name="e">The <see cref="HumanTaskStatusChangedDomainEvent"/> to handle</param>
        protected virtual void On(HumanTaskStatusChangedDomainEvent e)
        {
            this.LastModified = e.CreatedAt;
            this.Status = e.Status;
        }

        /// <summary>
        /// Handles the specified <see cref="HumanTaskClaimedDomainEvent"/>
        /// </summary>
        /// <param name="e">The <see cref="HumanTaskClaimedDomainEvent"/> to handle</param>
        protected virtual void On(HumanTaskClaimedDomainEvent e)
        {
            this.PeopleAssignments.SetActualOwner(e.User);
            this.On(this.RegisterEvent(new HumanTaskStatusChangedDomainEvent(this.Id, HumanTaskStatus.Reserved)));
        }

        /// <summary>
        /// Handles the specified <see cref="HumanTaskReleasedDomainEvent"/>
        /// </summary>
        /// <param name="e">The <see cref="HumanTaskReleasedDomainEvent"/> to handle</param>
        protected virtual void On(HumanTaskReleasedDomainEvent e)
        {
            this.PeopleAssignments.SetActualOwner(null);
            this.On(this.RegisterEvent(new HumanTaskStatusChangedDomainEvent(this.Id, HumanTaskStatus.Ready)));
        }

        /// <summary>
        /// Handles the specified <see cref="HumanTaskDelegatedDomainEvent"/>
        /// </summary>
        /// <param name="e">The <see cref="HumanTaskDelegatedDomainEvent"/> to handle</param>
        protected virtual void On(HumanTaskDelegatedDomainEvent e)
        {
            this.PeopleAssignments.SetActualOwner(e.DelegatedTo);
            this.PeopleAssignments.AddUsersToPotentialOwners(e.DelegatedTo);
        }

        /// <summary>
        /// Handles the specified <see cref="HumanTaskForwardedDomainEvent"/>
        /// </summary>
        /// <param name="e">The <see cref="HumanTaskForwardedDomainEvent"/> to handle</param>
        protected virtual void On(HumanTaskForwardedDomainEvent e)
        {
            this.PeopleAssignments.SetActualOwner(null);
            if (this.PeopleAssignments.PotentialOwners != null
                && this.PeopleAssignments.PotentialOwners.Contains(e.User))
                this.PeopleAssignments.RemoveUsersFromPotentialOwners(e.User);
            this.PeopleAssignments.AddUsersToPotentialOwners(e.ForwardedTo.ToArray());
        }

        /// <summary>
        /// Handles the specified <see cref="AttachmentAddedToHumanTaskDomainEvent"/>
        /// </summary>
        /// <param name="e">The <see cref="AttachmentAddedToHumanTaskDomainEvent"/> to handle</param>
        protected virtual void On(AttachmentAddedToHumanTaskDomainEvent e)
        {
            this.LastModified = e.CreatedAt;
            if (this._attachments == null) this._attachments = new();
            this._attachments.Add(e.Attachment);
        }

        /// <summary>
        /// Handles the specified <see cref="AttachmentRemovedFromHumanTaskDomainEvent"/>
        /// </summary>
        /// <param name="e">The <see cref="AttachmentRemovedFromHumanTaskDomainEvent"/> to handle</param>
        protected virtual void On(AttachmentRemovedFromHumanTaskDomainEvent e)
        {
            this.LastModified = e.CreatedAt;
            if (this._attachments == null) return;
            var attachment = this._attachments.FirstOrDefault(a => a.Id.Equals(e.AttachmentId, StringComparison.InvariantCultureIgnoreCase));
            if (attachment == null) return;
            this._attachments.Remove(attachment);
        }

        /// <summary>
        /// Handles the specified <see cref="CommentAddedToHumanTaskDomainEvent"/>
        /// </summary>
        /// <param name="e">The <see cref="CommentAddedToHumanTaskDomainEvent"/> to handle</param>
        protected virtual void On(CommentAddedToHumanTaskDomainEvent e)
        {
            this.LastModified = e.CreatedAt;
            if (this._comments == null) this._comments = new();
            this._comments.Add(e.Comment);
        }

        /// <summary>
        /// Handles the specified <see cref="HumanTaskCommentUpdatedDomainEvent"/>
        /// </summary>
        /// <param name="e">The <see cref="HumanTaskCommentUpdatedDomainEvent"/> to handle</param>
        protected virtual void On(HumanTaskCommentUpdatedDomainEvent e)
        {
            this.LastModified = e.CreatedAt;
            if (this._comments == null) return;
            var comment = this._comments.FirstOrDefault(a => a.Id.Equals(e.CommentId, StringComparison.InvariantCultureIgnoreCase));
            if (comment == null) return;
            comment.SetContent(e.CreatedAt, e.User, e.Content);
        }

        /// <summary>
        /// Handles the specified <see cref="CommentRemovedFromHumanTaskDomainEvent"/>
        /// </summary>
        /// <param name="e">The <see cref="CommentRemovedFromHumanTaskDomainEvent"/> to handle</param>
        protected virtual void On(CommentRemovedFromHumanTaskDomainEvent e)
        {
            this.LastModified = e.CreatedAt;
            if (this._comments == null) return;
            var comment = this._comments.FirstOrDefault(a => a.Id.Equals(e.CommentId, StringComparison.InvariantCultureIgnoreCase));
            if (comment == null) return;
            this._comments.Remove(comment);
        }

        /// <summary>
        /// Builds a new unique identifier for the specified <see cref="HumanTask"/>
        /// </summary>
        /// <param name="definitionId">The id of the <see cref="HumanTaskDefinition"/> of the <see cref="HumanTask"/> to build a new id for</param>
        /// <param name="key">The key of the <see cref="HumanTask"/> to build a new id for</param>
        /// <returns>A new <see cref="HumanTask"/> unique identifier</returns>
        public static string BuildId(string definitionId, string key)
        {
            if (string.IsNullOrEmpty(definitionId)) throw DomainException.ArgumentNullOrWhitespace(nameof(definitionId));
            if (string.IsNullOrEmpty(key)) throw DomainException.ArgumentNullOrWhitespace(nameof(key));
            return $"{definitionId}-{key}";
        }

    }

}
