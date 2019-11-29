namespace TnTSoftware.Cqrs.Query
{
    public static class IQueryHandlerX
    {
        public static ExecutionResponse Ok<TQuery, TResult>(this IQueryHandler<TQuery> handler, TResult content)
           where TQuery : IQuery
        {
            return ExecutionResponse.Create(content, ActionOutcome.Ok, null);
        }

        public static ExecutionResponse Ok<TQuery>(this IQueryHandler<TQuery> handler)
            where TQuery : IQuery
        {
            return ExecutionResponse.Ok;
        }

        public static ExecutionResponse Conflict<TQuery>(this IQueryHandler<TQuery> handler)
           where TQuery : IQuery
        {
            return ExecutionResponse.Conflict;
        }

        public static ExecutionResponse Conflict<TQuery, TResult>(this IQueryHandler<TQuery> handler, TResult content)
            where TQuery : IQuery
        {
            return ExecutionResponse.Create(content, ActionOutcome.Conflict, null);
        }

        public static ExecutionResponse Forbidden<TQuery>(this IQueryHandler<TQuery> handler)
            where TQuery : IQuery
        {
            return ExecutionResponse.Forbidden;
        }

        public static ExecutionResponse Forbidden<TQuery, TResult>(this IQueryHandler<TQuery> handler, TResult content)
            where TQuery : IQuery
        {
            return ExecutionResponse.Create(content, ActionOutcome.Forbidden, null);
        }

        public static ExecutionResponse BadRequest<TQuery>(this IQueryHandler<TQuery> handler)
            where TQuery : IQuery
        {
            return ExecutionResponse.BadRequest;
        }

        public static ExecutionResponse BadRequest<TQuery, TResult>(this IQueryHandler<TQuery> handler, TResult content)
            where TQuery : IQuery
        {
            return ExecutionResponse.Create(content, ActionOutcome.BadRequest, null);
        }

        public static ExecutionResponse UnknownError<TQuery>(this IQueryHandler<TQuery> handler)
            where TQuery : IQuery
        {
            return ExecutionResponse.UnknownError;
        }

        public static ExecutionResponse UnknownError<TQuery, TResult>(this IQueryHandler<TQuery> handler, TResult content)
            where TQuery : IQuery
        {
            return ExecutionResponse.Create(content, ActionOutcome.UnknownError, null);
        }
    }
}