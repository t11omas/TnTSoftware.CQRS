namespace TnTSoftware.Cqrs.Command
{
    using System.Threading.Tasks;

    using TnTSoftware.Cqrs.Payloads;

    public interface ICommandInvoker
    {
        Task<ExecutionResponse> Invoke<TCommand>(TCommand command, IPayloadCache payloadCache = null)
             where TCommand : ICommand;

        Task<ExecutionResponse<TResult>> Invoke<TCommand, TResult>(TCommand command, IPayloadCache payloadCache = null)
            where TCommand : ICommand;
    }
}