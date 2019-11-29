namespace TnTSoftware.Cqrs.Payloads
{
    using System;
    using System.Collections.Generic;
    using GreenPipes.Payloads;

    public delegate TPayload PayloadFactory<out TPayload>()
        where TPayload : class;

    public delegate TPayload UpdatePayloadFactory<TPayload>(TPayload existing)
        where TPayload : class;

    public class PayloadCache : IPayloadCache
  {
      private IDictionary<Type, IPayloadValue> payloadValues;

      public PayloadCache()
      {
          this.payloadValues = new Dictionary<Type, IPayloadValue>();
      }

      T IPayloadCache.GetOrAddPayload<T>(T payload)
      {
          try
          {
              IPayloadValue<T> payloadValue = null;
              if (this.payloadValues.TryGetValue(typeof(T), out var existingPayloadValue))
              {
                  existingPayloadValue.TryGetValue(out T existingValue);
                  return existingValue;
              }

              payloadValue = new PayloadValue<T>(payload);

              this.payloadValues.Add(typeof(T), payloadValue);

              return payloadValue.Value;
          }
          catch (Exception exception)
          {
              throw new PayloadFactoryException($"The payload factory faulted: {typeof(T).Name}", exception);
          }
      }

      T IPayloadCache.AddOrUpdatePayload<T>(T payload)
      {
          try
          {
              IPayloadValue<T> payloadValue = null;
              if (this.payloadValues.TryGetValue(typeof(T), out var existingPayloadValue))
              {
                  payloadValue = new PayloadValue<T>(payload);
              }
              else
              {
                  payloadValue = new PayloadValue<T>(payload);

                  this.payloadValues.Add(payloadValue.ValueType, payloadValue);
              }

              return payloadValue.Value;
          }
          catch (Exception exception)
          {
              throw new PayloadFactoryException($"The payload factory faulted: {typeof(T).Name}", exception);
          }
      }

      public bool TryGetPayload<TPayload>(out TPayload payload)
          where TPayload : class
      {
          if (this.payloadValues.TryGetValue(typeof(TPayload), out var existingPayloadValue))
          {
              return existingPayloadValue.TryGetValue(out payload);
          }

          payload = default(TPayload);
          return false;
      }
  }
}