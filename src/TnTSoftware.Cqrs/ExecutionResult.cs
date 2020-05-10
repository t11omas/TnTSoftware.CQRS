namespace TnTSoftware.Cqrs
{
    public class ExecutionResult
    {
        private ExecutionResult(ActionOutcome actionOutcomeCode, object content)
        {
            this.ActionOutcomeCode = actionOutcomeCode;
            this.GetContent = content;
        }

        public static ExecutionResult Conflict => Create<object>(null, ActionOutcome.Conflict);

        public static ExecutionResult BadRequest => Create<object>(null, ActionOutcome.BadRequest);

        public static ExecutionResult Forbidden => Create<object>(null, ActionOutcome.Forbidden);

        public static ExecutionResult NotFound => Create<object>(null, ActionOutcome.NotFound);

        public static ExecutionResult UnknownError => Create<object>(null, ActionOutcome.UnknownError);

        public static ExecutionResult Ok => Create<object>(null, ActionOutcome.Ok);

        public object GetContent { get; }

        public ActionOutcome ActionOutcomeCode { get; }

        public static ExecutionResult Create<T>(T content, ActionOutcome actionOutcome)
        {
            return new ExecutionResult(actionOutcome, content);
        }

        public TResult Result<TResult>()
        {
            return (TResult)this.GetContent;
        }
    }
}