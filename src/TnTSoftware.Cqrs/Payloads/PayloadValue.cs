namespace TnTSoftware.Cqrs.Payloads
{
    using System;

    public class PayloadValue<TPayload> :
        IPayloadValue<TPayload>
        where TPayload : class
    {
        private readonly TPayload value;

        public PayloadValue(TPayload value)
        {
            if (value == default(TPayload))
            {
                throw new PayloadNotFoundException($"The payload was not found: {typeof(TPayload).Name}");
            }

            this.value = value;
        }

        Type IPayloadValue.ValueType => this.value?.GetType();

        TPayload IPayloadValue<TPayload>.Value => this.value;

        bool IPayloadValue.TryGetValue<T>(out T value)
        {
            value = this.value as T;

            return value != null;
        }
    }
}