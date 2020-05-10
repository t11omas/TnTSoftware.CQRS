namespace TnTSoftware.Cqrs.MiniProfiler
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    using StackExchange.Profiling;

    public class MiniProfilerPipelineStepExecutor<TCommand, TUser> : PipelineStepExecutor<TCommand, TUser> where TCommand : IMessage
    {
        readonly List<IPipelineStep<TCommand, TUser>> list = new List<IPipelineStep<TCommand, TUser>>();
        private MiniProfilerSwitch<TUser> miniProfilerSwitch;

        public MiniProfilerPipelineStepExecutor(IEnumerable<IPipelineStep<TCommand, TUser>> orderedPipelineSteps, MiniProfilerSwitch<TUser> miniProfilerSwitch) : base(orderedPipelineSteps)
        {
            this.miniProfilerSwitch = miniProfilerSwitch;
            foreach (var orderedPipelineStep in orderedPipelineSteps.Reverse())
            {
                list.Add(orderedPipelineStep);
            }
        }

        public override Task<ExecutionResult> Execute(TCommand command, TUser user,
                                                            CancellationToken cancellationToken)
        {
            var profiler = StackExchange.Profiling.MiniProfiler.Current;

            if (profiler == null || !this.miniProfilerSwitch.Condition(user))
            {
                return base.Execute(command, user, cancellationToken);
            }

            RequestHandlerDelegate<ExecutionResult> next = null;


            using (profiler.CustomTiming("Message", JsonSerializer.Serialize(command)))

                foreach (var keyValuePair in list)
                {
                    var next1 = next;
                    next = () =>
                        {
                            using (profiler.Step(keyValuePair.GetType().Name))
                            {
                                return keyValuePair.Execute(command, user, cancellationToken, next1);
                            }

                        };
                }

            return next?.Invoke();
        }
    }
}

