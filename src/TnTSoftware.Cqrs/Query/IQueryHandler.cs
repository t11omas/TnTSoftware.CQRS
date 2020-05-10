namespace TnTSoftware.Cqrs.Query
{
    using System.Threading.Tasks;

    using TnT.Cqrs.Core;

    public interface IQueryHandler<in TQuery, in TUser>
        where TQuery : IQuery
    {
        Task<ExecutionResult> Execute(TQuery query, TUser user);
    }
}