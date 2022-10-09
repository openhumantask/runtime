using OpenHumanTask.Runtime.Integration.Events.HumanTasks;

namespace OpenHumanTask.Runtime.Domain.Events.HumanTasks
{

    /// <summary>
    /// Represents the <see cref="IDomainEvent"/> created whenever a new <see cref="HumanTask"/> has been created
    /// </summary>
    [DataTransferObjectType(typeof(HumanTaskCreatedIntegrationEvent))]
    public class HumanTaskCreatedDomainEvent
        : DomainEvent<HumanTask, string>
    {

        /// <summary>
        /// Initializes a new <see cref="HumanTaskCreatedDomainEvent"/>
        /// </summary>
        protected HumanTaskCreatedDomainEvent()
        {

        }

        /// <summary>
        /// Initializes a new <see cref="HumanTaskCreatedDomainEvent"/>
        /// </summary>
        /// <param name="id">The id of the newly created <see cref="HumanTask"/></param>
        /// <param name="definitionId">The id of the <see cref="HumanTask"/>'s <see cref="HumanTaskDefinition"/></param>
        /// <param name="key">The <see cref="HumanTask"/>'s key</param>
        /// <param name="peopleAssignments">The people assigned to the <see cref="HumanTask"/></param>
        /// <param name="priority">The <see cref="HumanTask"/>'s priority</param>
        /// <param name="skipable">A boolean indicating whether or not the <see cref="HumanTask"/> can be skipped</param>
        /// <param name="title">An <see cref="IDictionary{TKey, TValue}"/> that contains the <see cref="HumanTask"/>'s localized title language/value pairs</param>
        /// <param name="subject">An <see cref="IDictionary{TKey, TValue}"/> that contains the <see cref="HumanTask"/>'s localized subject language/value pairs</param>
        /// <param name="description">An <see cref="IDictionary{TKey, TValue}"/> that contains the <see cref="HumanTask"/>'s localized description language/value pairs</param>
        /// <param name="input">The <see cref="HumanTask"/>'s input</param>
        /// <param name="subtasks">An <see cref="IEnumerable{T}"/> containing the <see cref="HumanTask"/>'s <see cref="Subtask"/>s</param>
        /// <param name="attachments">An <see cref="IEnumerable{T}"/> containg the <see cref="HumanTask"/>'s <see cref="Attachment"/>s</param>
        /// <param name="comments">An <see cref="IEnumerable{T}"/> containg the <see cref="HumanTask"/>'s <see cref="Comment"/>s</param>
        public HumanTaskCreatedDomainEvent(string id, string definitionId, string key, PeopleAssignments peopleAssignments, int priority, bool skipable, IDictionary<string, string>? title, IDictionary<string, string>? subject, 
            IDictionary<string, string>? description, object? input, IEnumerable<Subtask>? subtasks, IEnumerable<Attachment>? attachments, IEnumerable<Comment>? comments)
            : base(id)
        {
            this.DefinitionId = definitionId;
            this.Key = key;
            this.PeopleAssignments = peopleAssignments;
            this.Priority = priority;
            this.Skipable = skipable;
            this.Title = title;
            this.Subject = subject;
            this.Description = description;
            this.Input = input;
            this.Subtasks = subtasks;
            this.Attachments = attachments;
            this.Comments = comments;
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
        public virtual bool Skipable { get; protected set; }

        /// <summary>
        /// Gets a <see cref="Dictionary{TKey, TValue}"/> that contains the <see cref="HumanTask"/>'s localized title language/value pairs
        /// </summary>
        public virtual IDictionary<string, string>? Title { get; protected set; }

        /// <summary>
        /// Gets a <see cref="Dictionary{TKey, TValue}"/> that contains the <see cref="HumanTask"/>'s localized subject language/value pairs
        /// </summary>
        public virtual IDictionary<string, string>? Subject { get; protected set; }

        /// <summary>
        /// Gets a <see cref="Dictionary{TKey, TValue}"/> that contains the <see cref="HumanTask"/>'s localized description language/value pairs
        /// </summary>
        public virtual IDictionary<string, string>? Description { get; protected set; }

        /// <summary>
        /// Gets the <see cref="HumanTask"/>'s input
        /// </summary>
        public virtual object? Input { get; protected set; }

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> containing the <see cref="HumanTask"/>'s <see cref="Subtask"/>s
        /// </summary>
        public virtual IEnumerable<Subtask>? Subtasks { get; protected set; }

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> containing the <see cref="HumanTask"/>'s <see cref="Attachment"/>s
        /// </summary>
        public virtual IEnumerable<Attachment>? Attachments { get; protected set; }

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> containing the <see cref="HumanTask"/>'s <see cref="Comment"/>s
        /// </summary>
        public virtual IEnumerable<Comment>? Comments { get; protected set; }

    }

}
