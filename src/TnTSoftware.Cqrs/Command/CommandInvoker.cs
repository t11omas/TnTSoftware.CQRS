namespace TnTSoftware.Cqrs.Command
{
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using TnTSoftware.Cqrs.Payloads;

    internal class CommandInvoker : ICommandInvoker
    {
        private readonly IMediator mediator;

        private readonly ILoggerFactory loggerFactory;

        public CommandInvoker(IMediator mediator, ILoggerFactory loggerFactory)
        {
            this.mediator = mediator;
            this.loggerFactory = loggerFactory;
        }

        public async Task<ExecutionResponse> Invoke<TCommand>(TCommand command, IPayloadCache payloadCache = null)
            where TCommand : ICommand
        {
            if (payloadCache == null)
            {
                payloadCache = new PayloadCache();
            }

            ILogger logger = this.loggerFactory.CreateLogger<TCommand>();
            payloadCache.GetOrAddPayload<ILogger>(logger);
            CommandContext<TCommand> context = new CommandContext<TCommand>(command, payloadCache);
            ExecutionResponse executionResponse = await this.mediator.Send(context).ConfigureAwait(false);
            executionResponse.SetPayloadCache(payloadCache);
            return executionResponse;
        }

        public async Task<ExecutionResponse<TResult>> Invoke<TCommand, TResult>(TCommand command, IPayloadCache payloadCache = null)
            where TCommand : ICommand
        {
            ExecutionResponse executionResponse = await this.Invoke(command, payloadCache).ConfigureAwait(false);
            return ExecutionResponse<TResult>.Create(
                executionResponse.Result<TResult>(),
                executionResponse.ActionOutcomeCode,
                executionResponse.PayloadCache);
        }
    }
}