namespace TnTSoftware.Cqrs.Query
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using TnT.Cqrs.Core;
    using TnT.Cqrs.Core.ServiceFactory;

    public class QueryExecutor : IQueryExecutor
    {
        private readonly ServiceFactory serviceFactory;

        public QueryExecutor(ServiceFactory serviceFactory)
        {
            this.serviceFactory = serviceFactory;
        }

        public Task<ExecutionResult> Execute<TQuery, TUser>(TQuery query, TUser user, CancellationToken cancellationToken = default)
            where TQuery : IQuery
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            return this.serviceFactory
                .GetInstance<PipelineStepExecutor<TQuery, TUser>>().Execute(query, user, cancellationToken);
        }
    }
}