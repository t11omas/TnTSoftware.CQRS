namespace TnT.Cqrs.Core.DependencyInjection
{
    using System;

    using Microsoft.Extensions.DependencyInjection;

    using TnTSoftware.Cqrs;

    public class TnTCqrsBuilderOptions
    {
        public TnTCqrsBuilderOptions(IServiceCollection services)
        {
            this.CommandOptions.AddDataAnnotationsValidationPipelineStep(0);
            this.CommandOptions.AddCommandHandlerPipeline(1);
            this.QueryOptions.AddDataAnnotationsValidationPipelineStep(0);
            this.QueryOptions.AddQueryHandlerPipeline(1);
            this.Services = services;
        }

        public IServiceCollection Services { get; }

        internal Type PipelineStepExecutor { get; private set; } = typeof(PipelineStepExecutor<,>);

        internal TnTCqrsCommandBuilderOptions CommandOptions { get; private set; } = new TnTCqrsCommandBuilderOptions();

        internal TnTCqrsCommandBuilderOptions QueryOptions { get; private set; } = new TnTCqrsCommandBuilderOptions();

        public TnTCqrsBuilderOptions ConfigureCommandPipeline(Action<TnTCqrsCommandBuilderOptions> configure)
        {
            this.CommandOptions.Clear();
            configure(this.CommandOptions);
            return this;
        }

        public TnTCqrsBuilderOptions ConfigureQueryPipeline(Action<TnTCqrsCommandBuilderOptions> configure)
        {
            this.QueryOptions.Clear();
            configure(this.CommandOptions);
            return this;
        }

        public TnTCqrsBuilderOptions SetPipelineStepExecutor(Type pipelineStepExecutorType)
        {
            this.PipelineStepExecutor = pipelineStepExecutorType;
            return this;
        }
    }
}