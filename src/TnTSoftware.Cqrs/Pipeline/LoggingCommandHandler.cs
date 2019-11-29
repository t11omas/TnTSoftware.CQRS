namespace TnTSoftware.Cqrs.Pipeline
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using TnTSoftware.Cqrs.Command;
    using TnTSoftware.Cqrs.Query;

    internal class LoggingCommandHandler<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (request is IExecutionContext executionContext)
            {
                ILogger logger;
                executionContext.TryGetPayload<ILogger>(out logger);

                string name = typeof(TRequest).Name;
                if (request is IQueryContext<IQuery> queryContext)
                {
                    name = queryContext.Query.GetType().Name;
                }
                else

                if (request is ICommandContext<ICommand> commandContext)
                {
                    name = commandContext.Command.GetType().Name;
                }

                Stopwatch stopwatch = Stopwatch.StartNew();

                logger.LogTrace($"Handling request for {name}");

                TResponse response = default(TResponse);
                try
                {
                    response = await next().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"Error handling request for {name}");
                    throw;
                }
                finally
                {
                    stopwatch.Stop();
                    logger.LogTrace($"Handled request for {name} completed in {stopwatch.Elapsed}");
                }

                return response;
            }

            return await next().ConfigureAwait(false);
        }
    }
}