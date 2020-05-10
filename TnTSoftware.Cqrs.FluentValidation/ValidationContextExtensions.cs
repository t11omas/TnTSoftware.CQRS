using System;

namespace TnTSoftware.Cqrs.FluentValidation
{
    using global::FluentValidation;

    public static class ValidationContextExtensions
    {
        public static void SetContextData<TInstance, TData>(this ValidationContext<TInstance> context, TData data)
        {
            context.RootContextData.Add(data.GetType().Name, data);
        }

        public static TData GetContextData<TInstance, TData>(this ValidationContext<TInstance> context)
        {
            if (context.RootContextData.TryGetValue(typeof(TData).Name, out object data))
            {
                return (TData)data;
            }

            return default;
        }
    }
}
