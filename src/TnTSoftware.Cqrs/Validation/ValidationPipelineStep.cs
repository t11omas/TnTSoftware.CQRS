namespace TnT.Cqrs.Core.Validation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Threading;
    using System.Threading.Tasks;

    using TnTSoftware.Cqrs;

    public class ValidationPipelineStep<TRequest, TUser> : IPipelineStep<TRequest, TUser>
        where TRequest : IMessage
    {
        private IServiceProvider serviceProvider;

        public ValidationPipelineStep(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Task<ExecutionResult> Execute(TRequest request, TUser user, CancellationToken cancellationToken, RequestHandlerDelegate<ExecutionResult> next)
        {
            var context = new ValidationContext(request, serviceProvider: this.serviceProvider, items: null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(request, context, results);

            if (!isValid)
            {
                return Task.FromResult(ExecutionResult.Create(results, ActionOutcome.BadRequest));
            }

            return next();
        }
    }
}
