namespace TnTSoftware.Cqrs
{
    public class ActionOutcome
    {
        public static ActionOutcome Ok => new ActionOutcome("Ok", 200, true);

        public static ActionOutcome BadRequest => new ActionOutcome("BadRequest", 400, false);

        public static ActionOutcome Conflict => new ActionOutcome("Conflict", 409, false);

        public static ActionOutcome Forbidden => new ActionOutcome("Forbidden", 403, false);

        public static ActionOutcome NotFound => new ActionOutcome("NotFound", 404, false);

        public static ActionOutcome UnknownError => new ActionOutcome("UnknownError", 500, false);

        public ActionOutcome(string outcome, int statusCode, bool isSuccessOutcome)
        {
            this.Outcome = outcome;
            this.StatusCode = statusCode;
            this.IsSuccessOutcome = isSuccessOutcome;
        }

        public string Outcome { get; }

        public int StatusCode { get; }

        public bool IsSuccessOutcome { get; }
    }
}