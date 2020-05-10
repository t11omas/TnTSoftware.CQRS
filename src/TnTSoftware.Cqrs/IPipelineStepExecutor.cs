namespace TnTSoftware.Cqrs
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IPipelineStepExecutor<in TCommand, in TUser>
    {
        Task<ExecutionResult> Execute(TCommand command, TUser user, CancellationToken cancellationToken);
    }
}