namespace TnTSoftware.Cqrs
{
    using TnTSoftware.Cqrs.Payloads;

    public class ExecutionResponse : BaseContext
    {
        private ExecutionResponse(ActionOutcome actionOutcomeCode, object content, IPayloadCache payloadCache)
            : base(payloadCache)
        {
            ActionOutcomeCode = actionOutcomeCode;
            GetContent = content;
        }

        public static ExecutionResponse Conflict => Create<object>(null, ActionOutcome.Conflict, null);

        public static ExecutionResponse BadRequest => Create<object>(null, ActionOutcome.BadRequest, null);

        public static ExecutionResponse Forbidden => Create<object>(null, ActionOutcome.Forbidden, null);

        public static ExecutionResponse NotFound => Create<object>(null, ActionOutcome.NotFound, null);

        public static ExecutionResponse UnknownError => Create<object>(null, ActionOutcome.UnknownError, null);

        public static ExecutionResponse Ok => Create<object>(null, ActionOutcome.Ok, null);

        public object GetContent { get; }

        public ActionOutcome ActionOutcomeCode { get; }

        public static ExecutionResponse Create<T>(T content, ActionOutcome actionOutcome, IPayloadCache payloadCache)
        {
            return new ExecutionResponse(actionOutcome, content, payloadCache);
        }

        public TResult Result<TResult>()
        {
            return (TResult)this.GetContent;
        }
    }

#pragma warning disable SA1402 // File may only contain a single type
    public class ExecutionResponse<TResult> : BaseContext
#pragma warning restore SA1402 // File may only contain a single type
    {
        private ExecutionResponse(ActionOutcome actionOutcomeCode, TResult content, IPayloadCache payloadCache)
            : base(payloadCache)
        {
            ActionOutcomeCode = actionOutcomeCode;
            GetContent = content;
        }

        public TResult GetContent { get; }

        public ActionOutcome ActionOutcomeCode { get; }

        public static ExecutionResponse<TResult> Create(TResult content, ActionOutcome actionOutcome, IPayloadCache payloadCache)
        {
            return new ExecutionResponse<TResult>(actionOutcome, content, payloadCache);
        }
    }
}