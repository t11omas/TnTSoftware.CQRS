namespace TnTSoftware.Cqrs.DependencyInjection
{
    using System;
    using System.Reflection;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using TnTSoftware.Cqrs.Command;
    using TnTSoftware.Cqrs.Pipeline;
    using TnTSoftware.Cqrs.Query;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTnTSoftwareCqrs(this IServiceCollection services)
        {
            services.AddTnTSoftwareCqrs(Assembly.GetCallingAssembly());

            // This override the default Mediar behaviour which catches argument exceptions and tries to create the pipeline via reflections.  This
            // doesn't work for us, and just results in exceptions being swallowed, which takes too much effort to track down
            services.AddScoped<ServiceFactory>(p => p.GetService);
            services.AddScoped<ICommandInvoker, CommandInvoker>();
            services.AddScoped<IQueryInvoker, QueryInvoker>();
            return services;
        }

        public static IServiceCollection AddTnTSoftwareCqrs(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddMediatR(c => c.AsScoped(), assemblies);

            // This override the default Mediar behaviour which catches argument exceptions and tries to create the pipeline via reflections.  This
            // doesn't work for us, and just results in exceptions being swallowed, which takes too much effort to track down
            services.AddScoped<ServiceFactory>(p => p.GetService);
            services.AddScoped<ICommandInvoker, CommandInvoker>();
            services.AddScoped<IQueryInvoker, QueryInvoker>();
            return services;
        }

        public static IServiceCollection AddLoggingPipeline(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingCommandHandler<,>));
            return services;
        }

        public static IServiceCollection AddCommandHandler<TCommand, THandler>(this IServiceCollection services)
            where TCommand : ICommand
            where THandler : class, IPipelineBehavior<CommandContext<TCommand>, ExecutionResponse>
        {
            services.AddScoped<IPipelineBehavior<CommandContext<TCommand>, ExecutionResponse>, THandler>();
            return services;
        }

        public static IServiceCollection AddCommandHandler<TCommand, THandler>(
            this IServiceCollection services,
            Func<IServiceProvider, THandler> implementationFactory)
            where TCommand : ICommand
            where THandler : class, IPipelineBehavior<CommandContext<TCommand>, ExecutionResponse>
        {
            services.AddScoped<IPipelineBehavior<CommandContext<TCommand>, ExecutionResponse>>(implementationFactory);
            return services;
        }

        public static IServiceCollection AddQueryHandler<TQuery, THandler>(this IServiceCollection services)
            where TQuery : IQuery
            where THandler : class, IPipelineBehavior<QueryContext<TQuery>, ExecutionResponse>
        {
            services.AddScoped<IPipelineBehavior<QueryContext<TQuery>, ExecutionResponse>, THandler>();
            return services;
        }

        public static IServiceCollection AddQueryHandler<TQuery, THandler>(
            this IServiceCollection services,
            Func<IServiceProvider, THandler> implementationFactory)
            where TQuery : IQuery
            where THandler : class, IPipelineBehavior<QueryContext<TQuery>, ExecutionResponse>
        {
            services.AddScoped<IPipelineBehavior<QueryContext<TQuery>, ExecutionResponse>, THandler>(implementationFactory);
            return services;
        }
    }
}
