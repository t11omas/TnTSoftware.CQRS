namespace TnTSoftware.Cqrs.Query
{
    using TnT.Cqrs.Core;

    public static class IQueryHandlerX
    {
        public static ExecutionResult Ok<TQuery, TUser, TResult>(this IQueryHandler<TQuery, TUser> handler, TResult content)
           where TQuery : IQuery
        {
            return ExecutionResult.Create(content, ActionOutcome.Ok);
        }

        public static ExecutionResult NotFound<TQuery, TUser>(this IQueryHandler<TQuery, TUser> handler)
            where TQuery : IQuery
        {
            return ExecutionResult.NotFound;
        }
    }
}