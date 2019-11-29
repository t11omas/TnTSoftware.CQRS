namespace TnTSoftware.Cqrs.Payloads
{
    using System;

    using GreenPipes.Payloads;

    [Serializable]
    public class PayloadNotFoundException :
        PayloadException
    {
        public PayloadNotFoundException()
        {
        }

        public PayloadNotFoundException(string message)
            : base(message)
        {
        }

        public PayloadNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}