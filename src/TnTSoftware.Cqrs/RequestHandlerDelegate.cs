namespace TnTSoftware.Cqrs
{
    using System.Threading.Tasks;

    public delegate Task<ExecutionResult> RequestHandlerDelegate<ExecutionResult>();
}