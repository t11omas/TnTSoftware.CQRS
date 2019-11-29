namespace TnTSoftware.Cqrs.Query
{
    using MediatR;
    using TnTSoftware.Cqrs.Payloads;

    public class QueryContext<TQuery> : BaseContext, IRequest<ExecutionResponse>, IQueryContext<TQuery>
        where TQuery : IQuery
    {
        public QueryContext(TQuery query, IPayloadCache payloadCache)
            : base(payloadCache)
        {
            this.Query = query;
        }

        public TQuery Query { get; }
    }
}
