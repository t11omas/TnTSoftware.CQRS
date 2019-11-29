namespace TnTSoftware.Cqrs
{
    public interface IExecutionContext
    {
        bool TryGetPayload<T>(out T payload)
            where T : class;

        T GetOrAddPayload<T>(T payload)
            where T : class;

        T AddOrUpdatePayload<T>(T payload)
            where T : class;
    }
}