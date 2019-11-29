namespace TnTSoftware.Cqrs
{
    using System.Threading;

    using TnTSoftware.Cqrs.Payloads;

    public abstract class BaseContext
    {
        protected BaseContext(IPayloadCache payloadCache)
        {
            this.CancellationToken = CancellationToken.None;

            this.PayloadCache = payloadCache;
        }

        public CancellationToken CancellationToken { get; }

        public IPayloadCache PayloadCache { get; private set; }

        public bool TryGetPayload<T>(out T payload)
            where T : class
        {
            return this.PayloadCache.TryGetPayload(out payload);
        }

        public T GetOrAddPayload<T>(T payload)
            where T : class
        {
            return this.PayloadCache.GetOrAddPayload(payload);
        }

        public T AddOrUpdatePayload<T>(T payload)
            where T : class
        {
            return this.PayloadCache.AddOrUpdatePayload(payload);
        }

        internal void SetPayloadCache(IPayloadCache payloadCache)
        {
            this.PayloadCache = payloadCache;
        }
    }
}