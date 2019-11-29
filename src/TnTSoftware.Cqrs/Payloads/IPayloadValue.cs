namespace TnTSoftware.Cqrs.Payloads
{
    using System;

    public interface IPayloadValue
    {
        Type ValueType { get; }

        bool TryGetValue<T>(out T value)
            where T : class;
    }

    public interface IPayloadValue<out TPayload> :
        IPayloadValue
        where TPayload : class
    {
        TPayload Value { get; }
    }
}