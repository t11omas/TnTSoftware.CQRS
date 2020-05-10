namespace TnTSoftware.Cqrs
{
    using System.Threading;
    using System.Threading.Tasks;

    using TnT.Cqrs.Core;

    public interface IPipelineStep<in TRequest, in TUser>
        where TRequest : IMessage
    {
        Task<ExecutionResult> Execute(TRequest request, TUser user, CancellationToken cancellationToken, RequestHandlerDelegate<ExecutionResult> next);
    }
}