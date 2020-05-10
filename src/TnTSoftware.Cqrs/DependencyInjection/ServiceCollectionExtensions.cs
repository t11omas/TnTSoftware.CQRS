namespace TnTSoftware.Cqrs.DependencyInjection
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    using TnT.Cqrs.Core;
    using TnT.Cqrs.Core.Command;
    using TnT.Cqrs.Core.DependencyInjection;
    using TnT.Cqrs.Core.ServiceFactory;
    using TnTSoftware.Cqrs.Query;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTnTSoftwareCqrs(this IServiceCollection services, Action<TnTCqrsBuilderOptions> configure = default)
        {
            services.AddSingleton<ServiceFactory>(p => p.GetService);
            services.AddSingleton<ICommandExecutor, CommandExecutor>();
            services.AddSingleton<IQueryExecutor, QueryExecutor>();
            services.Scan(scan => scan
                .FromApplicationDependencies()
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
                .AsImplementedInterfaces()
                .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces());
            TnTCqrsBuilderOptions options = new TnTCqrsBuilderOptions(services);

            configure?.Invoke(options);

            services.AddScoped(typeof(PipelineStepExecutor<,>), options.PipelineStepExecutor);

            foreach (var optionsPipelineStep in options.CommandOptions.PipelineSteps)
            {
                services.AddScoped(typeof(IPipelineStep<,>), optionsPipelineStep.Item);
            }

            foreach (var optionsPipelineStep in options.QueryOptions.PipelineSteps)
            {
                services.AddScoped(typeof(IPipelineStep<,>), optionsPipelineStep.Item);
            }

            return services;
        }
    }
}
