namespace TnTSoftware.Cqrs.Query
{
    using MediatR;

    public interface IQueryHandler<TQuery> : IPipelineBehavior<QueryContext<TQuery>, ExecutionResponse>
        where TQuery : IQuery
    {
    }
}