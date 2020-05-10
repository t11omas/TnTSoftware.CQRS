namespace TnTSoftware.Cqrs
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using TnT.Cqrs.Core;

    public class PipelineStepExecutor<TCommand, TUser> : IPipelineStepExecutor<TCommand, TUser>
        where TCommand : IMessage
    {
        private readonly List<IPipelineStep<TCommand, TUser>> list = new List<IPipelineStep<TCommand, TUser>>();

        public PipelineStepExecutor(IEnumerable<IPipelineStep<TCommand, TUser>> orderedPipelineSteps)
        {
            this.list = orderedPipelineSteps.Reverse().ToList();
        }

        public virtual Task<ExecutionResult> Execute(TCommand command, TUser user, CancellationToken cancellationToken)
        {
            RequestHandlerDelegate<ExecutionResult> next = null;

            foreach (var keyValuePair in this.list)
            {
                var innerNext = next;
                next = () => keyValuePair.Execute(command, user, cancellationToken, innerNext);
            }

            return next?.Invoke();
        }
    }
}