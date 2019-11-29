namespace TnTSoftware.Cqrs.Command
{
    public interface ICommandContext<out TCommand> : IExecutionContext
        where TCommand : ICommand
    {
        TCommand Command { get; }
    }
}