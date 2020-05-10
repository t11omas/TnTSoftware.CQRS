namespace TnT.Cqrs.Core.Command
{
    using System.Threading;
    using System.Threading.Tasks;

    using TnTSoftware.Cqrs;

    public interface ICommandExecutor
    {
        Task<ExecutionResult> Send<TCommand, TUser>(TCommand command, TUser user, CancellationToken cancellationToken = default)
            where TCommand : ICommand;
    }
}