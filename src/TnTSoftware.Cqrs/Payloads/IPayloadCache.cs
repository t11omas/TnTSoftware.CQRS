namespace TnTSoftware.Cqrs.Payloads
{
    public interface IPayloadCache
    {
        T GetOrAddPayload<T>(T payload)
            where T : class;

        T AddOrUpdatePayload<T>(T payload)
            where T : class;

        bool TryGetPayload<TPayload>(out TPayload payload)
            where TPayload : class;
    }
}