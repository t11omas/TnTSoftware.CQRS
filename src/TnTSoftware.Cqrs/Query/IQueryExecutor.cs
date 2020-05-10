namespace TnTSoftware.Cqrs.Query
{
    using System.Threading;
    using System.Threading.Tasks;

    using TnT.Cqrs.Core;
    using TnT.Cqrs.Core.Command;

    public interface IQueryExecutor
    {
        Task<ExecutionResult> Execute<TQuery, TUser>(TQuery command, TUser user, CancellationToken cancellationToken = default)
            where TQuery : IQuery;
    }
}