namespace TnTSoftware.Cqrs.FluentValidation
{
    using TnT.Cqrs.Core.DependencyInjection;

    public static class TnTCqrsBuilderOptionsExtensions
    {
        public static void AddFluentValidationPipelineStep(this TnTCqrsCommandBuilderOptions options, int order)
        {
            options.AddPipelineStep(order, typeof(FluentValidationPipelineStep<,>));
        }
    }
}