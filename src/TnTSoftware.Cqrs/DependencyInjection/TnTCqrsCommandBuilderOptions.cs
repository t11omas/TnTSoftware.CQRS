namespace TnT.Cqrs.Core.DependencyInjection
{
    using System;
    using System.Collections.Generic;

    using TnT.Cqrs.Core.Command;
    using TnT.Cqrs.Core.Query;
    using TnT.Cqrs.Core.Validation;

    using TnTSoftware.Cqrs;

    public class TnTCqrsCommandBuilderOptions
    {
        internal List<OrderedItem<Type>> PipelineSteps { get; set; } = new List<OrderedItem<Type>>();

        public void AddPipelineStep(int order, Type type)
        {
            this.PipelineSteps.Add(new OrderedItem<Type> { Item = type, Order = order });
        }

        public void AddCommandHandlerPipeline(int order)
        {
            this.AddPipelineStep(order, typeof(CommandHandlerPipelineStep<,>));
        }

        public void AddQueryHandlerPipeline(int order)
        {
            this.AddPipelineStep(order, typeof(QueryHandlerPipelineStep<,>));
        }

        public void AddDataAnnotationsValidationPipelineStep(int order)
        {
            this.AddPipelineStep(order, typeof(ValidationPipelineStep<,>));
        }

        internal void Clear()
        {
            this.PipelineSteps.Clear();
        }
    }
}
