namespace TnT.Cqrs.Core.Query
{
    using System.Threading;
    using System.Threading.Tasks;

    using TnT.Cqrs.Core.Command;
    using TnT.Cqrs.Core.ServiceFactory;

    using TnTSoftware.Cqrs;
    using TnTSoftware.Cqrs.Query;

    public class QueryHandlerPipelineStep<TRequest, TUser> : HandlerPipelineStep<TRequest, TUser>
        where TRequest : IQuery
    {
        private readonly ServiceFactory serviceFactory;

        public QueryHandlerPipelineStep(ServiceFactory serviceFactory)
        {
            this.serviceFactory = serviceFactory;
        }

        public override async Task<ExecutionResult> Execute(TRequest request, TUser user, CancellationToken cancellationToken, RequestHandlerDelegate<ExecutionResult> next)
        {
            var commandResult = await this.serviceFactory.GetInstance<IQueryHandler<TRequest, TUser>>().Execute(request, user);

            return ExecutionResult.Create(commandResult.GetContent, commandResult.ActionOutcomeCode);
        }
    }
}