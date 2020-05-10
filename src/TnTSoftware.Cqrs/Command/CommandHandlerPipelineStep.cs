namespace TnT.Cqrs.Core.Command
{
    using System.Threading;
    using System.Threading.Tasks;

    using TnT.Cqrs.Core.ServiceFactory;

    using TnTSoftware.Cqrs;

    public class CommandHandlerPipelineStep<TRequest, TUser> : HandlerPipelineStep<TRequest, TUser>
        where TRequest : ICommand
    {
        private readonly ServiceFactory serviceFactory;

        public CommandHandlerPipelineStep(ServiceFactory serviceFactory)
        {
            this.serviceFactory = serviceFactory;
        }

        public override async Task<ExecutionResult> Execute(TRequest request, TUser user, CancellationToken cancellationToken, RequestHandlerDelegate<ExecutionResult> next)
        {
            var commandResult = await this.serviceFactory.GetInstance<ICommandHandler<TRequest, TUser>>().Execute(request, user);

            return ExecutionResult.Create(commandResult.GetContent, commandResult.ActionOutcomeCode);
        }
    }
}