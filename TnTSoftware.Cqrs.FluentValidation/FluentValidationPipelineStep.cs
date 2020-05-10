namespace TnTSoftware.Cqrs.FluentValidation
{
    using System.Threading;
    using System.Threading.Tasks;

    using global::FluentValidation;
    using global::FluentValidation.Internal;

    using TnT.Cqrs.Core.ServiceFactory;

    public class FluentValidationPipelineStep<TRequest, TUser> : IPipelineStep<TRequest, TUser> where TRequest : IMessage
    {
        private readonly ServiceFactory serviceFactory;

        public FluentValidationPipelineStep(ServiceFactory serviceFactory)
        {
            this.serviceFactory = serviceFactory;
        }

        public async Task<ExecutionResult> Execute(TRequest request, TUser user, CancellationToken cancellationToken, RequestHandlerDelegate<ExecutionResult> next)
        {
            var validator = this.serviceFactory.GetInstance<IValidator<TRequest>>();

            if (validator != null)
            {
                var context = new ValidationContext<TRequest>(
                    request,
                    new PropertyChain(),
                    ValidatorOptions.ValidatorSelectors.DefaultValidatorSelectorFactory());
                context.SetContextData(user);
                var validationResult = await validator.ValidateAsync(context, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return ExecutionResult.Create(validationResult, ActionOutcome.BadRequest);
                }
            }

            return await next();
        }
    }
}