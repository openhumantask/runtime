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
        /// <param name="definition">The <see cref="HumanTask"/>'s <see cref="HumanTaskTemplate"/></param>
        /// <param name="key">The <see cref="HumanTask"/>'s key</param>
        /// <param name="peopleAssignments">The people assigned to the <see cref="HumanTask"/></param>
        /// <param name="priority">The <see cref="HumanTask"/>'s priority</param>
        /// <param name="title">An <see cref="IDictionary{TKey, TValue}"/> that contains the <see cref="HumanTask"/>'s localized title language/value pairs</param>
        /// <param name="subject">An <see cref="IDictionary{TKey, TValue}"/> that contains the <see cref="HumanTask"/>'s localized subject language/value pairs</param>
        /// <param name="description">An <see cref="IDictionary{TKey, TValue}"/> that contains the <see cref="HumanTask"/>'s localized description language/value pairs</param>
        /// <param name="input">The <see cref="HumanTask"/>'s input</param>
        /// <param name="subtasks">An <see cref="IEnumerable{T}"/> containing the <see cref="HumanTask"/>'s <see cref="Subtask"/>s</param>
        /// <param name="attachments">An <see cref="IEnumerable{T}"/> containg the <see cref="HumanTask"/>'s <see cref="Attachment"/>s</param>
        /// <param name="comments">An <see cref="IEnumerable{T}"/> containg the <see cref="HumanTask"/>'s <see cref="Comment"/>s</param>
        public HumanTask(HumanTaskTemplate definition, string key, PeopleAssignments peopleAssignments, int priority, IDictionary<string, string>? title = null, IDictionary<string, string>? subject = null, 
            IDictionary<string, string>? description = null, object? input = null, IEnumerable<Subtask>? subtasks = null, IEnumerable<Attachment>? attachments = null, IEnumerable<Comment>? comments = null)
            : base(BuildId(definition?.Id!, key))
        {
            if (definition == null) throw DomainException.ArgumentNull(nameof(definition));
            if(peopleAssignments == null) throw DomainException.ArgumentNull(nameof(peopleAssignments));
            if (string.IsNullOrWhiteSpace(key)) throw DomainException.ArgumentNullOrWhitespace(nameof(key));
            this.On(this.RegisterEvent(new HumanTaskCreatedDomainEvent(this.Id, definition.Id, key, peopleAssignments, priority, definition.Definition.Skipable, title, subject, description, input, subtasks, attachments, comments)));
        }

        /// <summary>
        /// Gets the id of the <see cref="HumanTask"/>'s <see cref="HumanTaskTemplate"/>
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

        /// <summary>
        /// Gets the <see cref="HumanTask"/>'s priority
        /// </summary>
        public virtual int Priority { get; protected set; }

        /// <summary>
        /// Gets a boolean indicating whether or not the <see cref="HumanTask"/> can be skipped
        /// </summary>
        public virtual bool Skipable { get; protected set; } = false;

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

        /// <summary>
        /// Gets the <see cref="HumanTask"/>'s outcome
        /// </summary>
        public virtual object? Outcome { get; protected set; }

        /// <summary>
        /// Gets the name of the completion behavior that allowed the <see cref="HumanTask"/>'s completion, if any
        /// </summary>
        public virtual string? CompletionBehaviorName { get; protected set; }

        /// <summary>
        /// Gets the date and time at which the <see cref="HumanTask"/> has started
        /// </summary>
        public virtual DateTimeOffset? StartedAt { get; protected set; }

        /// <summary>
        /// Gets the date and time at which the <see cref="HumanTask"/> has been completed
        /// </summary>
        public virtual DateTimeOffset? CompletedAt { get; protected set; }

        [Newtonsoft.Json.JsonProperty(nameof(Subtasks))]
        [JsonPropertyName(nameof(Subtasks))]
        private List<Subtask>? _subtasks;
        /// <summary>
        /// Gets an <see cref="IReadOnlyCollection{T}"/> containing the <see cref="HumanTask"/>'s <see cref="Subtask"/>s
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [JsonIgnore]
        public virtual IReadOnlyCollection<Subtask>? Subtasks => this._subtasks?.AsReadOnly();

        [Newtonsoft.Json.JsonProperty(nameof(Attachments))]
        [JsonPropertyName(nameof(Attachments))]
        private List<Attachment>? _attachments;
        /// <summary>
        /// Gets an <see cref="IReadOnlyCollection{T}"/> containing the <see cref="HumanTask"/>'s <see cref="Attachment"/>s
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [JsonIgnore]
        public virtual IReadOnlyCollection<Attachment>? Attachments => this._attachments?.AsReadOnly();

        [Newtonsoft.Json.JsonProperty(nameof(Comments))]
        [JsonPropertyName(nameof(Comments))]
        private List<Comment>? _comments;
        /// <summary>
        /// Gets an <see cref="IReadOnlyCollection{T}"/> containing the <see cref="HumanTask"/>'s <see cref="Comment"/>s
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [JsonIgnore]
        public virtual IReadOnlyCollection<Comment>? Comments => this._comments?.AsReadOnly();

        [Newtonsoft.Json.JsonProperty(nameof(Faults))]
        [JsonPropertyName(nameof(Faults))]
        private List<Fault>? _faults;
        /// <summary>
        /// Gets an <see cref="IReadOnlyCollection{T}"/> containing the <see cref="HumanTask"/>'s <see cref="Models.Fault"/>s
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [JsonIgnore]
        public virtual IReadOnlyCollection<Fault>? Faults => this._faults?.AsReadOnly();

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
        /// Sets the <see cref="HumanTask"/>'s priority
        /// </summary>
        /// <param name="user">The user setting the <see cref="HumanTask"/>'s priority</param>
        /// <param name="priority">The <see cref="HumanTask"/>'s priority</param>
        /// <returns></returns>
        public virtual bool SetPriority(UserReference user, int priority)
        {
            if (user == null) throw DomainException.ArgumentNull(nameof(user));
            var roles = GenericHumanRole.ActualOwner | GenericHumanRole.BusinessAdministrator | GenericHumanRole.Initiator | GenericHumanRole.Stakeholder;
            if (!this.DefinesRoleFor(roles, user))
            {
                if (this.DefinesRoleFor(GenericHumanRole.PotentialOwner, user))
                {
                    if (this.Status != HumanTaskStatus.Ready) throw DomainException.UnexpectedState(typeof(HumanTask), this.Id, this.Status);
                }
                else throw HumanTaskDomainExceptions.UserNotInRoles(user, nameof(SetPriority), roles);
            }
            if (this.Status != HumanTaskStatus.Created && this.Status != HumanTaskStatus.Ready && this.Status != HumanTaskStatus.Reserved)
                throw DomainException.UnexpectedState(typeof(HumanTask), this.Id, this.Status);
            if (this.Priority == priority) return false;
            this.On(this.RegisterEvent(new HumanTaskPriorityChangedDomainEvent(this.Id, user, priority)));
            return true;
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
            if (this.Status >= HumanTaskStatus.InProgress) throw DomainException.UnexpectedState(typeof(HumanTask), this.Id, this.Status);
            var roles = GenericHumanRole.BusinessAdministrator | GenericHumanRole.Stakeholder;
            if (!this.DefinesRoleFor(roles, user))
            {
                if (this.DefinesRoleFor(GenericHumanRole.Initiator, user))
                {
                    if (this.Status > HumanTaskStatus.Ready) throw DomainException.UnexpectedState(typeof(HumanTask), this.Id, this.Status);
                }
                else throw HumanTaskDomainExceptions.UserNotInRoles(user, nameof(Assign), roles);
            }
            if (this.Status == HumanTaskStatus.Reserved || this.Status == HumanTaskStatus.InProgress) this.Release(user);
            this.On(this.RegisterEvent(new HumanTaskReassignedDomainEvent(this.Id, user, assignments)));
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
            if (!this.DefinesRoleFor(roles, user)) throw HumanTaskDomainExceptions.UserNotInRoles(user, nameof(Delegate), roles);
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
                {
                    if (this.Status != HumanTaskStatus.Ready) throw DomainException.UnexpectedState(typeof(HumanTask), this.Id, this.Status);
                } 
                else throw HumanTaskDomainExceptions.UserNotInRoles(user, nameof(Forward), roles);
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
            if (this.Status >= HumanTaskStatus.Obsolete) throw DomainException.UnexpectedState(typeof(HumanTask), this.Id, this.Status);
            var roles = GenericHumanRole.ActualOwner | GenericHumanRole.BusinessAdministrator | GenericHumanRole.Stakeholder;
            if (!this.DefinesRoleFor(roles, user))
            {
                if (this.DefinesRoleFor(GenericHumanRole.PotentialOwner, user))
                {
                    if (this.Status != HumanTaskStatus.Ready) throw DomainException.UnexpectedState(typeof(HumanTask), this.Id, this.Status);
                }
                else throw HumanTaskDomainExceptions.UserNotInRoles(user, nameof(AddAttachment), roles);
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
            if (this.Status >= HumanTaskStatus.Obsolete) throw DomainException.UnexpectedState(typeof(HumanTask), this.Id, this.Status);
            var roles = GenericHumanRole.ActualOwner | GenericHumanRole.BusinessAdministrator | GenericHumanRole.Stakeholder;
            if (!this.DefinesRoleFor(roles, user))
            {
                if (this.DefinesRoleFor(GenericHumanRole.PotentialOwner, user))
                {
                    if (this.Status != HumanTaskStatus.Ready) throw DomainException.UnexpectedState(typeof(HumanTask), this.Id, this.Status);
                }
                else throw HumanTaskDomainExceptions.UserNotInRoles(user, nameof(RemoveAttachment), roles);
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
            if (this.Status >= HumanTaskStatus.Obsolete) throw DomainException.UnexpectedState(typeof(HumanTask), this.Id, this.Status);
            var roles = GenericHumanRole.ActualOwner | GenericHumanRole.BusinessAdministrator | GenericHumanRole.Stakeholder;
            if (!this.DefinesRoleFor(roles, author))
            {
                if (this.DefinesRoleFor(GenericHumanRole.PotentialOwner, author))
                {
                    if (this.Status != HumanTaskStatus.Ready) throw DomainException.UnexpectedState(typeof(HumanTask), this.Id, this.Status);
                }
                else throw HumanTaskDomainExceptions.UserNotInRoles(author, nameof(AddComment), roles);
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
            if (this.Status >= HumanTaskStatus.Obsolete) throw DomainException.UnexpectedState(typeof(HumanTask), this.Id, this.Status);
            if (string.IsNullOrWhiteSpace(content)) throw DomainException.ArgumentNull(nameof(content));
            var roles = GenericHumanRole.ActualOwner | GenericHumanRole.BusinessAdministrator | GenericHumanRole.Stakeholder;
            if (!this.DefinesRoleFor(roles, user))
            {
                if (this.DefinesRoleFor(GenericHumanRole.PotentialOwner, user))
                {
                    if (this.Status != HumanTaskStatus.Ready) throw DomainException.UnexpectedState(typeof(HumanTask), this.Id, this.Status);
                }
                else throw HumanTaskDomainExceptions.UserNotInRoles(user, nameof(UpdateComment), roles);
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
            if (this.Status >= HumanTaskStatus.Obsolete) throw DomainException.UnexpectedState(typeof(HumanTask), this.Id, this.Status);
            var roles = GenericHumanRole.ActualOwner | GenericHumanRole.BusinessAdministrator | GenericHumanRole.Stakeholder;
            if (!this.DefinesRoleFor(roles, user))
            {
                if (this.DefinesRoleFor(GenericHumanRole.PotentialOwner, user))
                {
                    if (this.Status != HumanTaskStatus.Ready) throw DomainException.UnexpectedState(typeof(HumanTask), this.Id, this.Status);
                }
                else throw HumanTaskDomainExceptions.UserNotInRoles(user, nameof(RemoveComment), roles);
            }
            if (this._comments?.Any(c => c.Id.Equals(comment.Id, StringComparison.InvariantCultureIgnoreCase)) != true) return false;
            this.On(this.RegisterEvent(new CommentRemovedFromHumanTaskDomainEvent(this.Id, user, comment.Id)));
            return true;
        }

        /// <summary>
        /// Starts the <see cref="HumanTask"/>
        /// </summary>
        /// <param name="user">The <see cref="UserReference"/> starting the <see cref="HumanTask"/></param>
        public virtual void Start(UserReference user)
        {
            if (user == null) throw DomainException.ArgumentNull(nameof(user));
            if (this.Status != HumanTaskStatus.Reserved) throw DomainException.UnexpectedState(typeof(HumanTask), this.Id, this.Status);
            if (!this.DefinesRoleFor(GenericHumanRole.ActualOwner, user)) throw HumanTaskDomainExceptions.UserNotInRoles(user, nameof(Start), GenericHumanRole.ActualOwner);
            this.On(this.RegisterEvent(new HumanTaskStartedDomainEvent(this.Id, user)));
        }

        /// <summary>
        /// Suspends the <see cref="HumanTask"/>
        /// </summary>
        /// <param name="user">The user suspending the <see cref="HumanTask"/></param>
        /// <param name="until">The date and time until which to supsend the <see cref="HumanTask"/></param>
        /// <param name="reason">The reason why to suspend the <see cref="HumanTask"/></param>
        public virtual void Suspend(UserReference user, DateTimeOffset? until = null, string? reason = null)
        {
            if (user == null) throw DomainException.ArgumentNull(nameof(user));
            if (this.Status != HumanTaskStatus.InProgress) throw DomainException.UnexpectedState(typeof(HumanTask), this.Id, this.Status);
            var roles = GenericHumanRole.ActualOwner | GenericHumanRole.BusinessAdministrator | GenericHumanRole.Stakeholder;
            if (!this.DefinesRoleFor(roles, user)) throw HumanTaskDomainExceptions.UserNotInRoles(user, nameof(Suspend), roles);
            this.On(this.RegisterEvent(new HumanTaskSuspendedDomainEvent(this.Id, user, reason)));
        }

        /// <summary>
        /// Resumes the <see cref="HumanTask"/>
        /// </summary>
        /// <param name="user">The user resuming the <see cref="HumanTask"/></param>
        public virtual void Resume(UserReference user)
        {
            if (user == null) throw DomainException.ArgumentNull(nameof(user));
            if (this.Status != HumanTaskStatus.Suspended) throw DomainException.UnexpectedState(typeof(HumanTask), this.Id, this.Status);
            var roles = GenericHumanRole.ActualOwner | GenericHumanRole.BusinessAdministrator | GenericHumanRole.Stakeholder;
            if (!this.DefinesRoleFor(roles, user)) throw HumanTaskDomainExceptions.UserNotInRoles(user, nameof(Resume), roles);
            this.On(this.RegisterEvent(new HumanTaskResumedDomainEvent(this.Id, user)));
        }

        /// <summary>
        /// Skips the <see cref="HumanTask"/>
        /// </summary>
        /// <param name="user">The user skipping the <see cref="HumanTask"/></param>
        /// <param name="reason">The reason why to skip the <see cref="HumanTask"/></param>
        public virtual void Skip(UserReference user, string? reason = null)
        {
            if (user == null) throw DomainException.ArgumentNull(nameof(user));
            if (!this.Skipable) throw HumanTaskDomainExceptions.UnskipableHumanTask(this);
            if (this.Status > HumanTaskStatus.Suspended) throw DomainException.UnexpectedState(typeof(HumanTask), this.Id, this.Status);
            var roles = GenericHumanRole.Initiator | GenericHumanRole.ActualOwner | GenericHumanRole.BusinessAdministrator | GenericHumanRole.Stakeholder;
            if (!this.DefinesRoleFor(roles, user)) throw HumanTaskDomainExceptions.UserNotInRoles(user, nameof(Skip), roles);
            this.On(this.RegisterEvent(new HumanTaskSkippedDomainEvent(this.Id, user, reason)));
        }

        /// <summary>
        /// Cancels the <see cref="HumanTask"/>
        /// </summary>
        /// <param name="user">The user cancelling the <see cref="HumanTask"/></param>
        /// <param name="reason">The reason why to cancel the <see cref="HumanTask"/></param>
        public virtual void Cancel(UserReference user, string? reason = null)
        {
            if (user == null) throw DomainException.ArgumentNull(nameof(user));
            if(this.Status >= HumanTaskStatus.Obsolete) throw DomainException.UnexpectedState(typeof(HumanTask), this.Id, this.Status);
            var roles = GenericHumanRole.Initiator | GenericHumanRole.ActualOwner | GenericHumanRole.BusinessAdministrator | GenericHumanRole.Stakeholder;
            if (!this.DefinesRoleFor(roles, user)) throw HumanTaskDomainExceptions.UserNotInRoles(user, nameof(Cancel), roles);
            this.On(this.RegisterEvent(new HumanTaskCancelledDomainEvent(this.Id, user, reason)));
        }

        /// <summary>
        /// Faults the <see cref="HumanTask"/>
        /// </summary>
        /// <param name="user">The user faulting the <see cref="HumanTask"/></param>
        /// <param name="faults">An <see cref="IEnumerable{T}"/> containing the <see cref="Models.Fault"/>s to add</param>
        public virtual void Fault(UserReference user, params Fault[] faults)
        {
            if (user == null) throw DomainException.ArgumentNull(nameof(user));
            if (faults == null) throw DomainException.ArgumentNull(nameof(faults));
            if (faults.Length < 1) throw DomainException.ArgumentMustHaveMinimumLengthOf(nameof(faults), 1);
            if (this.Status >= HumanTaskStatus.Obsolete) throw DomainException.UnexpectedState(typeof(HumanTask), this.Id, this.Status);
            var roles = GenericHumanRole.ActualOwner | GenericHumanRole.BusinessAdministrator | GenericHumanRole.Stakeholder;
            if (!this.DefinesRoleFor(roles, user)) throw HumanTaskDomainExceptions.UserNotInRoles(user, nameof(Fault), roles);
            this.On(this.RegisterEvent(new HumanTaskFaultedDomainEvent(this.Id, user, faults)));
        }

        /// <summary>
        /// Adds <see cref="Models.Fault"/>(s) to the <see cref="HumanTask"/>
        /// </summary>
        /// <param name="user">The user adding the <see cref="Models.Fault"/>s to the <see cref="HumanTask"/></param>
        /// <param name="faults">An <see cref="IEnumerable{T}"/> containing the <see cref="Models.Fault"/>s to add</param>
        public virtual void AddFault(UserReference user, params Fault[] faults)
        {
            if (user == null) throw DomainException.ArgumentNull(nameof(user));
            if (faults == null) throw DomainException.ArgumentNull(nameof(faults));
            if (faults.Length < 1) throw DomainException.ArgumentMustHaveMinimumLengthOf(nameof(faults), 1);
            if (this.Status >= HumanTaskStatus.Obsolete) throw DomainException.UnexpectedState(typeof(HumanTask), this.Id, this.Status);
            var roles = GenericHumanRole.BusinessAdministrator | GenericHumanRole.Stakeholder;
            if (!this.DefinesRoleFor(roles, user)) throw HumanTaskDomainExceptions.UserNotInRoles(user, nameof(AddFault), roles);
            this.On(this.RegisterEvent(new FaultsAddedToHumanTaskDomainEvent(this.Id, user, faults)));
        }

        /// <summary>
        /// Removes the specified <see cref="Models.Fault"/> from the <see cref="HumanTask"/>
        /// </summary>
        /// <param name="user">The user removing the <see cref="Models.Fault"/>s from the <see cref="HumanTask"/></param>
        /// <param name="fault">The <see cref="Models.Fault"/>s to remove</param>
        /// <returns>A boolean indicating whether or not the operation was successfull</returns>
        public virtual bool RemoveFault(UserReference user, Fault fault)
        {
            if (user == null) throw DomainException.ArgumentNull(nameof(user));
            if (fault == null) throw DomainException.ArgumentNull(nameof(fault));
            if (this.Status >= HumanTaskStatus.Obsolete) throw DomainException.UnexpectedState(typeof(HumanTask), this.Id, this.Status);
            var roles = GenericHumanRole.BusinessAdministrator | GenericHumanRole.Stakeholder;
            if (!this.DefinesRoleFor(roles, user)) throw HumanTaskDomainExceptions.UserNotInRoles(user, nameof(RemoveFault), roles);
            if (this._faults?.Any(f => f.Id.Equals(fault.Id, StringComparison.InvariantCultureIgnoreCase)) == false) return false;
            this.On(this.RegisterEvent(new FaultRemovedFromHumanTaskDomainEvent(this.Id, user, fault.Id)));
            return true;
        }

        /// <summary>
        /// Completes the <see cref="HumanTask"/>
        /// </summary>
        /// <param name="user">The user completing the <see cref="HumanTask"/></param>
        /// <param name="outcome">The <see cref="HumanTask"/>'s outcome</param>
        /// <param name="output">The <see cref="HumanTask"/>'s output</param>
        /// <param name="completionBehaviorName">The name of the <see cref="HumanTask"/>'s completion behavior</param>
        public virtual void Complete(UserReference user, object outcome, object? output = null, string? completionBehaviorName = null)
        {
            if (user == null) throw DomainException.ArgumentNull(nameof(user));
            if (outcome == null) throw DomainException.ArgumentNull(nameof(outcome));
            if (this.Status >= HumanTaskStatus.Obsolete) throw DomainException.UnexpectedState(typeof(HumanTask), this.Id, this.Status);
            var roles = GenericHumanRole.ActualOwner | GenericHumanRole.BusinessAdministrator | GenericHumanRole.Stakeholder;
            if (!this.DefinesRoleFor(roles, user)) throw HumanTaskDomainExceptions.UserNotInRoles(user, nameof(RemoveFault), roles);
            this.On(this.RegisterEvent(new HumanTaskCompletedDomainEvent(this.Id, user, outcome, output, completionBehaviorName)));
        }

        /// <summary>
        /// Deletes the <see cref="HumanTask"/>
        /// </summary>
        /// <param name="user">The user deleting the <see cref="HumanTask"/></param>
        public virtual void Delete(UserReference user)
        {
            if (user == null) throw DomainException.ArgumentNull(nameof(user));
            if (!this.DefinesRoleFor(GenericHumanRole.BusinessAdministrator, user)) throw HumanTaskDomainExceptions.UserNotInRoles(user, nameof(Delete), GenericHumanRole.BusinessAdministrator);
            this.On(this.RegisterEvent(new HumanTaskDeletedDomainEvent(this.Id, user)));
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
            this.Priority = e.Priority;
            this.Skipable = e.Skipable;
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
        /// Handles the specified <see cref="HumanTaskPriorityChangedDomainEvent"/>
        /// </summary>
        /// <param name="e">The <see cref="HumanTaskPriorityChangedDomainEvent"/> to handle</param>
        protected virtual void On(HumanTaskPriorityChangedDomainEvent e)
        {
            this.LastModified = e.CreatedAt;
            this.Priority = e.Priority;
        }

        /// <summary>
        /// Handles the specified <see cref="HumanTaskReassignedDomainEvent"/>
        /// </summary>
        /// <param name="e">The <see cref="HumanTaskReassignedDomainEvent"/> to handle</param>
        protected virtual void On(HumanTaskReassignedDomainEvent e)
        {
            this.LastModified = e.CreatedAt;
            this.PeopleAssignments = e.Assignments;
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
        /// Handles the specified <see cref="HumanTaskStartedDomainEvent"/>
        /// </summary>
        /// <param name="e">The <see cref="HumanTaskStartedDomainEvent"/> to handle</param>
        protected virtual void On(HumanTaskStartedDomainEvent e)
        {
            this.StartedAt = DateTimeOffset.Now;
            this.On(this.RegisterEvent(new HumanTaskStatusChangedDomainEvent(this.Id, HumanTaskStatus.InProgress)));
        }

        /// <summary>
        /// Handles the specified <see cref="HumanTaskSuspendedDomainEvent"/>
        /// </summary>
        /// <param name="e">The <see cref="HumanTaskSuspendedDomainEvent"/> to handle</param>
        protected virtual void On(HumanTaskSuspendedDomainEvent e)
        {
            this.On(this.RegisterEvent(new HumanTaskStatusChangedDomainEvent(this.Id, HumanTaskStatus.Suspended)));
        }

        /// <summary>
        /// Handles the specified <see cref="HumanTaskResumedDomainEvent"/>
        /// </summary>
        /// <param name="e">The <see cref="HumanTaskResumedDomainEvent"/> to handle</param>
        protected virtual void On(HumanTaskResumedDomainEvent e)
        {
            this.On(this.RegisterEvent(new HumanTaskStatusChangedDomainEvent(this.Id, HumanTaskStatus.InProgress)));
        }

        /// <summary>
        /// Handles the specified <see cref="HumanTaskSkippedDomainEvent"/>
        /// </summary>
        /// <param name="e">The <see cref="HumanTaskSkippedDomainEvent"/> to handle</param>
        protected virtual void On(HumanTaskSkippedDomainEvent e)
        {
            this.On(this.RegisterEvent(new HumanTaskStatusChangedDomainEvent(this.Id, HumanTaskStatus.Obsolete)));
        }

        /// <summary>
        /// Handles the specified <see cref="HumanTaskCancelledDomainEvent"/>
        /// </summary>
        /// <param name="e">The <see cref="HumanTaskCancelledDomainEvent"/> to handle</param>
        protected virtual void On(HumanTaskCancelledDomainEvent e)
        {
            this.On(this.RegisterEvent(new HumanTaskStatusChangedDomainEvent(this.Id, HumanTaskStatus.Cancelled)));
        }

        /// <summary>
        /// Handles the specified <see cref="HumanTaskFaultedDomainEvent"/>
        /// </summary>
        /// <param name="e">The <see cref="HumanTaskFaultedDomainEvent"/> to handle</param>
        protected virtual void On(HumanTaskFaultedDomainEvent e)
        {
            this._faults ??= new();
            this._faults.AddRange(e.Faults);
            this.On(this.RegisterEvent(new HumanTaskStatusChangedDomainEvent(this.Id, HumanTaskStatus.Faulted)));
        }

        /// <summary>
        /// Handles the specified <see cref="FaultsAddedToHumanTaskDomainEvent"/>
        /// </summary>
        /// <param name="e">The <see cref="FaultsAddedToHumanTaskDomainEvent"/> to handle</param>
        protected virtual void On(FaultsAddedToHumanTaskDomainEvent e)
        {
            this.LastModified = e.CreatedAt;
            this._faults ??= new();
            this._faults.AddRange(e.Faults);
        }

        /// <summary>
        /// Handles the specified <see cref="FaultRemovedFromHumanTaskDomainEvent"/>
        /// </summary>
        /// <param name="e">The <see cref="FaultRemovedFromHumanTaskDomainEvent"/> to handle</param>
        protected virtual void On(FaultRemovedFromHumanTaskDomainEvent e)
        {
            this.LastModified = e.CreatedAt;
            if (this._faults == null) return;
            var fault = this._faults.FirstOrDefault(f => f.Id.Equals(e.FaultId, StringComparison.InvariantCultureIgnoreCase));
            if(fault == null) return;
            this._faults.Remove(fault);
        }

        /// <summary>
        /// Handles the specified <see cref="HumanTaskCompletedDomainEvent"/>
        /// </summary>
        /// <param name="e">The <see cref="HumanTaskCompletedDomainEvent"/> to handle</param>
        protected virtual void On(HumanTaskCompletedDomainEvent e)
        {
            this.CompletedAt = e.CreatedAt;
            this.Outcome = e.Outcome;
            this.Output = e.Output;
            this.CompletionBehaviorName = e.CompletionBehaviorName;
            this.On(this.RegisterEvent(new HumanTaskStatusChangedDomainEvent(this.Id, HumanTaskStatus.Completed)));
        }

        /// <summary>
        /// Handles the specified <see cref="HumanTaskDeletedDomainEvent"/>
        /// </summary>
        /// <param name="e">The <see cref="HumanTaskDeletedDomainEvent"/> to handle</param>
        protected virtual void On(HumanTaskDeletedDomainEvent e)
        {
            this.LastModified = e.CreatedAt;
        }

        /// <summary>
        /// Builds a new unique identifier for the specified <see cref="HumanTask"/>
        /// </summary>
        /// <param name="definitionId">The id of the <see cref="HumanTaskTemplate"/> of the <see cref="HumanTask"/> to build a new id for</param>
        /// <param name="key">The key of the <see cref="HumanTask"/> to build a new id for</param>
        /// <returns>A new <see cref="HumanTask"/> unique identifier</returns>
        public static string BuildId(string definitionId, string key)
        {
            if (string.IsNullOrEmpty(definitionId)) throw DomainException.ArgumentNullOrWhitespace(nameof(definitionId));
            if (string.IsNullOrEmpty(key)) throw DomainException.ArgumentNullOrWhitespace(nameof(key));
            return $"{definitionId}.{key}";
        }

    }

}
