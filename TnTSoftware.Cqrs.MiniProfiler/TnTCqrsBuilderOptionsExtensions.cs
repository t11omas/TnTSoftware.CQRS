namespace TnTSoftware.Cqrs.MiniProfiler
{
    using System;

    using Microsoft.Extensions.DependencyInjection;

    using TnT.Cqrs.Core.DependencyInjection;

    public static class TnTCqrsBuilderOptionsExtensions
    {
        public static TnTCqrsBuilderOptions AddMiniProfiler(this TnTCqrsBuilderOptions options)
        {
            options.SetPipelineStepExecutor(typeof(MiniProfilerPipelineStepExecutor<,>));
            return options;
        }

        public static void UseMiniProfilerIf<TUser>(this TnTCqrsBuilderOptions options, Func<TUser, bool> condition)
        {
            MiniProfilerSwitch<TUser> mps = new MiniProfilerSwitch<TUser>();
            mps.Condition = condition;
            options.Services.AddSingleton(mps);
        }
    }
}