namespace TnTSoftware.Cqrs.Query
{
    public interface IQueryContext<out TQuery> : IExecutionContext
        where TQuery : IQuery
    {
        TQuery Query { get; }
    }
}