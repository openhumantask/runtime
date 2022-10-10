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

using Semver;

namespace OpenHumanTask.Runtime.Application.Queries.HumanTaskTemplates
{

    /// <summary>
    /// Represents the <see cref="IQuery"/> used to retrieve a <see cref="HumanTaskTemplate"/> by id
    /// </summary>
    public class GetHumanTaskTemplateByIdQuery
        : Query<Integration.Models.HumanTaskTemplate>
    {

        /// <summary>
        /// Initializes a new <see cref="GetHumanTaskTemplateByIdQuery"/>
        /// </summary>
        protected GetHumanTaskTemplateByIdQuery()
        {

        }

        /// <summary>
        /// Initializes a new <see cref="GetHumanTaskTemplateByIdQuery"/>
        /// </summary>
        /// <param name="reference">The reference of the <see cref="HumanTaskTemplate"/> to get</param>
        public GetHumanTaskTemplateByIdQuery(TaskDefinitionReference reference)
        {
            this.Reference = reference;
        }

        /// <summary>
        /// Gets the reference of the <see cref="HumanTaskTemplate"/> to get
        /// </summary>
        public virtual TaskDefinitionReference Reference { get; protected set; } = null!;

    }

    /// <summary>
    /// Represents the service used to handle <see cref="GetHumanTaskTemplateByIdQuery"/> instances
    /// </summary>
    public class GetHumanTaskTemplateByIdQueryHandler
        : QueryHandlerBase<Integration.Models.HumanTaskTemplate, string>,
        IQueryHandler<GetHumanTaskTemplateByIdQuery, Integration.Models.HumanTaskTemplate>
    {

        /// <inheritdoc/>
        public GetHumanTaskTemplateByIdQueryHandler(ILoggerFactory loggerFactory, IMapper mapper, IMediator mediator, IRepository<Integration.Models.HumanTaskTemplate, string> repository) 
            : base(loggerFactory, mapper, mediator, repository)
        {

        }

        /// <inheritdoc/>
        public virtual async Task<IOperationResult<Integration.Models.HumanTaskTemplate>> HandleAsync(GetHumanTaskTemplateByIdQuery query, CancellationToken cancellationToken = default)
        {
            Integration.Models.HumanTaskTemplate? template;
            if (string.IsNullOrWhiteSpace(query.Reference.Version)
                || query.Reference.Version == "latest")
            {
                template = this.Repository.AsQueryable()
                    .Where(htt => htt.Definition.Namespace.Equals(query.Reference.Namespace, StringComparison.OrdinalIgnoreCase) && htt.Definition.Name.Equals(query.Reference.Name, StringComparison.OrdinalIgnoreCase))
                    .ToList()
                    .OrderByDescending(wf => SemVersion.Parse(wf.Definition.Version, SemVersionStyles.Any))
                    .FirstOrDefault()!;
            }
            else
            {
                template = await this.Repository.FindAsync(query.Reference.ToString(), cancellationToken);
            }
            if (template == null) throw DomainException.NullReference(typeof(HumanTaskTemplate), query.Reference.ToString());
            return this.Ok(template);
        }

    }

}
