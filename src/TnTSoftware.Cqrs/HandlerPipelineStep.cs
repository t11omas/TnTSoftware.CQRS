namespace TnTSoftware.Cqrs
{
    using System.Threading;
    using System.Threading.Tasks;

    using TnT.Cqrs.Core;

    public abstract class HandlerPipelineStep<TRequest, TUser> : IPipelineStep<TRequest, TUser>
        where TRequest : IMessage
    {
        public abstract Task<ExecutionResult> Execute(
            TRequest request,
            TUser user,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<ExecutionResult> next);
    }
}