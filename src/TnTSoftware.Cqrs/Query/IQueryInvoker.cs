namespace TnTSoftware.Cqrs.Query
{
    using System.Threading.Tasks;

    using TnTSoftware.Cqrs.Payloads;

    public interface IQueryInvoker
    {
        Task<ExecutionResponse> Invoke<TQuery>(TQuery command, IPayloadCache payloadCache = null)
             where TQuery : IQuery;

        Task<ExecutionResponse<TResult>> Invoke<TQuery, TResult>(TQuery command, IPayloadCache payloadCache = null)
            where TQuery : IQuery;
    }
}