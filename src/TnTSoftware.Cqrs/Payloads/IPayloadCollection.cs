namespace TnTSoftware.Cqrs.Payloads
{
    using GreenPipes.Payloads;

    public interface IPayloadCollection
    {
        IPayloadCollection Add(IPayloadValue payload);

        bool TryGetPayload<TPayload>(out TPayload payload)
            where TPayload : class;
    }
}