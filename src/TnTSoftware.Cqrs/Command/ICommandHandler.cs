namespace TnT.Cqrs.Core.Command
{
    using System.Threading.Tasks;

    using TnTSoftware.Cqrs;

    public interface ICommandHandler<in TCommand, in TUser>
        where TCommand : ICommand
    {
        Task<ExecutionResult> Execute(TCommand command, TUser user);
    }
}