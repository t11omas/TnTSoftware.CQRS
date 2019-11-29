namespace TnTSoftware.Cqrs.Command
{
    public static class ICommandHandlerX
    {
        public static ExecutionResponse Ok<TCommand, TResult>(this ICommandHandler<TCommand> handler, TResult content)
            where TCommand : ICommand
        {
            return ExecutionResponse.Create(content, ActionOutcome.Ok, null);
        }

        public static ExecutionResponse Ok<TCommand>(this ICommandHandler<TCommand> handler)
            where TCommand : ICommand
        {
            return ExecutionResponse.Ok;
        }

        public static ExecutionResponse Conflict<TCommand>(this ICommandHandler<TCommand> handler)
           where TCommand : ICommand
        {
            return ExecutionResponse.Conflict;
        }

        public static ExecutionResponse Conflict<TCommand, TResult>(this ICommandHandler<TCommand> handler, TResult content)
            where TCommand : ICommand
        {
            return ExecutionResponse.Create(content, ActionOutcome.Conflict, null);
        }

        public static ExecutionResponse Forbidden<TCommand>(this ICommandHandler<TCommand> handler)
            where TCommand : ICommand
        {
            return ExecutionResponse.Forbidden;
        }

        public static ExecutionResponse Forbidden<TCommand, TResult>(this ICommandHandler<TCommand> handler, TResult content)
            where TCommand : ICommand
        {
            return ExecutionResponse.Create(content, ActionOutcome.Forbidden, null);
        }

        public static ExecutionResponse BadRequest<TCommand>(this ICommandHandler<TCommand> handler)
            where TCommand : ICommand
        {
            return ExecutionResponse.BadRequest;
        }

        public static ExecutionResponse BadRequest<TCommand, TResult>(this ICommandHandler<TCommand> handler, TResult content)
            where TCommand : ICommand
        {
            return ExecutionResponse.Create(content, ActionOutcome.BadRequest, null);
        }

        public static ExecutionResponse UnknownError<TCommand>(this ICommandHandler<TCommand> handler)
            where TCommand : ICommand
        {
            return ExecutionResponse.UnknownError;
        }

        public static ExecutionResponse UnknownError<TCommand, TResult>(this ICommandHandler<TCommand> handler, TResult content)
            where TCommand : ICommand
        {
            return ExecutionResponse.Create(content, ActionOutcome.UnknownError, null);
        }
    }
}