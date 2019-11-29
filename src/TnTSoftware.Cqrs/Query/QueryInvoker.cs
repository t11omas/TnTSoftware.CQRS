namespace TnTSoftware.Cqrs.Query
{
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using TnTSoftware.Cqrs.Payloads;

    internal class QueryInvoker : IQueryInvoker
    {
        private readonly IMediator mediator;

        private readonly ILoggerFactory loggerFactory;

        public QueryInvoker(IMediator mediator, ILoggerFactory loggerFactory)
        {
            this.mediator = mediator;
            this.loggerFactory = loggerFactory;
        }

        public async Task<ExecutionResponse> Invoke<TQuery>(TQuery command, IPayloadCache payloadCache = null)
            where TQuery : IQuery
        {
            if (payloadCache == null)
            {
                payloadCache = new PayloadCache();
            }

            ILogger logger = this.loggerFactory.CreateLogger<TQuery>();
            payloadCache.GetOrAddPayload<ILogger>(logger);
            QueryContext<TQuery> context = new QueryContext<TQuery>(command, payloadCache);
            ExecutionResponse executionResponse = await this.mediator.Send(context).ConfigureAwait(false);
            executionResponse.SetPayloadCache(payloadCache);
            return executionResponse;
        }

        public async Task<ExecutionResponse<TResult>> Invoke<TQuery, TResult>(TQuery command, IPayloadCache payloadCache = null)
            where TQuery : IQuery
        {
            ExecutionResponse executionResponse = await this.Invoke(command, payloadCache).ConfigureAwait(false);
            return ExecutionResponse<TResult>.Create(
                executionResponse.Result<TResult>(),
                executionResponse.ActionOutcomeCode,
                executionResponse.PayloadCache);
        }
    }
}