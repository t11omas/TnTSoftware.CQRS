namespace TnT.Cqrs.Core.Command
{
    using TnTSoftware.Cqrs;

    public static class CommandHandlerX
    {
        public static ExecutionResult Ok<TCommand, TUser, TResult>(
            this ICommandHandler<TCommand, TUser> handler,
            TResult content)
            where TCommand : ICommand
        {
            return ExecutionResult.Create(content, ActionOutcome.Ok);
        }

        public static ExecutionResult BadRequest<TCommand, TUser, TResult>(
            this ICommandHandler<TCommand, TUser> handler,
            TResult content)
            where TCommand : ICommand
        {
            return ExecutionResult.Create(content, ActionOutcome.BadRequest);
        }

        public static ExecutionResult Conflict<TCommand, TUser>(this ICommandHandler<TCommand, TUser> handler)
            where TCommand : ICommand
        {
            return ExecutionResult.Conflict;
        }

        public static ExecutionResult Forbidden<TCommand, TUser>(this ICommandHandler<TCommand, TUser> handler)
            where TCommand : ICommand
        {
            return ExecutionResult.Forbidden;
        }

        public static ExecutionResult Forbidden<TCommand, TUser, TResult>(
            this ICommandHandler<TCommand, TUser> handler,
            TResult content)
            where TCommand : ICommand
        {
            return ExecutionResult.Create(content, ActionOutcome.Forbidden);
        }

        public static ExecutionResult Result<TCommand, TUser, TResult>(
            this ICommandHandler<TCommand, TUser> handler,
            TResult content,
            ActionOutcome actionOutcome)
            where TCommand : ICommand
        {
            return ExecutionResult.Create(content, actionOutcome);
        }
    }
}
