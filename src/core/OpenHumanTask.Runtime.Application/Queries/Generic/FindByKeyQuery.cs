namespace OpenHumanTask.Runtime.Application.Queries.Generic
{

    /// <summary>
    /// Represents an <see cref="IQuery"/> used to get an entity by id
    /// </summary>
    /// <typeparam name="TEntity">The type of entity to query</typeparam>
    /// <typeparam name="TKey">The type of id used to uniquely identify the entity to get</typeparam>
    public class FindByKeyQuery<TEntity, TKey>
        : Query<TEntity>
        where TEntity : class, IIdentifiable<TKey>
        where TKey : IEquatable<TKey>
    {

        /// <summary>
        /// Initializes a new <see cref="FindByKeyQuery{TEntity, TKey}"/>
        /// </summary>
        protected FindByKeyQuery()
        {
            this.Id = default!;
        }

        /// <summary>
        /// Initializes a new <see cref="FindByKeyQuery{TEntity, TKey}"/>
        /// </summary>
        /// <param name="id">The id of the entity to find</param>
        public FindByKeyQuery(TKey id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets the id of the entity to find
        /// </summary>
        public virtual TKey Id { get; }

    }

    /// <summary>
    /// Represents the service used to handle <see cref="FindByKeyQuery{TEntity, TKey}"/> instances
    /// </summary>
    /// <typeparam name="TEntity">The type of entity to find</typeparam>
    /// <typeparam name="TKey">The type of key used to uniquely identify the entity to find</typeparam>
    public class FindByKeyQueryHandler<TEntity, TKey>
        : QueryHandlerBase<TEntity, TKey>,
        IQueryHandler<FindByKeyQuery<TEntity, TKey>, TEntity>
        where TEntity : class, IIdentifiable<TKey>
        where TKey : IEquatable<TKey>
    {

        /// <inheritdoc/>
        public FindByKeyQueryHandler(ILoggerFactory loggerFactory, IMediator mediator, IMapper mapper, IRepository<TEntity, TKey> repository)
            : base(loggerFactory, mediator, mapper, repository)
        {
        }

        /// <inheritdoc/>
        public virtual async Task<IOperationResult<TEntity>> HandleAsync(FindByKeyQuery<TEntity, TKey> query, CancellationToken cancellationToken = default)
        {
            var entity = await this.Repository.FindAsync(query.Id, cancellationToken);
            if (entity == null)
                throw DomainException.NullReference(typeof(TEntity), query.Id, nameof(query.Id));
            return this.Ok(entity);
        }

    }

}
