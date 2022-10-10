namespace OpenHumanTask.Runtime.Application.Queries.Generic
{

    /// <summary>
    /// Represents the query used to find an entity by key
    /// </summary>
    /// <typeparam name="TEntity">The type of entity to find</typeparam>
    public class FindByKeyQuery<TEntity>
        : Query<TEntity>
        where TEntity : class, IIdentifiable
    {

        /// <summary>
        /// Initializes a new <see cref="FindByKeyQuery{TEntity}"/>
        /// </summary>
        /// <param name="key">The key of the entity to find</param>
        public FindByKeyQuery(object key)
        {
            this.Key = key;
        }

        /// <summary>
        /// Gets the key of the entity to find
        /// </summary>
        public object Key { get; }

    }

    /// <summary>
    /// Represents the query used to handle <see cref="FindByKeyQuery{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">The type of entity to find</typeparam>
    public class FindByKeyQueryHandler<TEntity>
        : QueryHandlerBase<TEntity>,
        IQueryHandler<FindByKeyQuery<TEntity>, TEntity>
        where TEntity : class, IIdentifiable
    {

        /// <summary>
        /// Initializes a new <see cref="QueryHandlerBase{TEntity}"/>
        /// </summary>
        /// <param name="loggerFactory">The service used to create <see cref="ILogger"/>s</param>
        /// <param name="mapper">The service used to map objects</param>
        /// <param name="mediator">The service used to mediate calls</param>
        /// <param name="repository">The <see cref="IRepository{TEntity}"/> to query</param>
        public FindByKeyQueryHandler(ILoggerFactory loggerFactory, IMediator mediator, IMapper mapper, IRepository<TEntity> repository) 
            : base(loggerFactory, mediator, mapper, repository)
        {

        }

        /// <inheritdoc/>
        public virtual async Task<IOperationResult<TEntity>> HandleAsync(FindByKeyQuery<TEntity> query, CancellationToken cancellationToken = default)
        {
            TEntity entity = await this.Repository.FindAsync(query.Key, cancellationToken);
            if (entity == null)
                throw DomainException.NullReference(typeof(TEntity), query.Key);
            return this.Ok(entity);
        }

    }

}
