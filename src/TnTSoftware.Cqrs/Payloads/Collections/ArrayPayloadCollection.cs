namespace TnTSoftware.Cqrs.Payloads.Collections
{
    using System;

    using GreenPipes.Payloads;

    public class ArrayPayloadCollection : IPayloadCollection
    {
        private readonly IPayloadValue[] payloads;

        public ArrayPayloadCollection(params IPayloadValue[] payloads)
        {
            this.payloads = payloads;
        }

        private ArrayPayloadCollection(IPayloadValue payload, IPayloadValue[] payloads)
        {
            this.payloads = new IPayloadValue[payloads.Length + 1];
            this.payloads[0] = payload;
            Array.Copy(payloads, 0, this.payloads, 1, payloads.Length);
        }

        public bool TryGetPayload<TPayload>(out TPayload payload)
            where TPayload : class
        {
            for (var i = 0; i < this.payloads.Length; i++)
            {
                if (this.payloads[i].TryGetValue(out payload))
                {
                    return true;
                }
            }

            payload = default(TPayload);
            return false;
        }

        public IPayloadCollection Add(IPayloadValue payload)
        {
            return new ArrayPayloadCollection(payload, this.payloads);
        }

        internal static class Shared
        {
            public static readonly ArrayPayloadCollection Empty = new ArrayPayloadCollection();
        }
    }
}