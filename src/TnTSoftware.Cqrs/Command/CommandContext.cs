namespace TnTSoftware.Cqrs.Command
{
    using MediatR;
    using TnTSoftware.Cqrs.Payloads;

    public class CommandContext<TCommand> : BaseContext, IRequest<ExecutionResponse>, ICommandContext<TCommand>
        where TCommand : ICommand
    {
        public CommandContext(TCommand command, IPayloadCache payloadCache)
            : base(payloadCache)
        {
            this.Command = command;
        }

        public TCommand Command { get; }
    }
}
