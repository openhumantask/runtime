namespace OpenHumanTask.Runtime.Application.Queries
{

    /// <summary>
    /// Represents the <see cref="IQuery"/> used to list entities of the specified type
    /// </summary>
    /// <typeparam name="TEntity">The type of entity to list</typeparam>
    public class ListQuery<TEntity>
        : Query<IEnumerable<TEntity>>
        where TEntity : class, IIdentifiable
    {



    }

    /// <summary>
    /// Represents the service used to handle <see cref="ListQuery{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">The type of entity to list</typeparam>
    public class ListQueryHandler<TEntity>
        : QueryHandlerBase<TEntity>,
        IQueryHandler<ListQuery<TEntity>, IEnumerable<TEntity>>
        where TEntity : class, IIdentifiable
    {

        /// <summary>
        /// Initializes a new <see cref="QueryHandlerBase{TEntity}"/>
        /// </summary>
        /// <param name="loggerFactory">The service used to create <see cref="ILogger"/>s</param>
        /// <param name="mapper">The service used to map objects</param>
        /// <param name="mediator">The service used to mediate calls</param>
        /// <param name="repository">The <see cref="IRepository{TEntity}"/> to query</param>
        public ListQueryHandler(ILoggerFactory loggerFactory, IMediator mediator, IMapper mapper,  IRepository<TEntity> repository) 
            : base(loggerFactory, mediator, mapper, repository)
        {

        }

        /// <inheritdoc/>
        public virtual async Task<IOperationResult<IEnumerable<TEntity>>> HandleAsync(ListQuery<TEntity> query, CancellationToken cancellationToken = default)
        {
            return this.Ok(await this.Repository.ToListAsync(cancellationToken));
        }

    }

}
