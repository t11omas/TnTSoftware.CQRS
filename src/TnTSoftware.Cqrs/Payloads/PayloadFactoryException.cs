namespace GreenPipes.Payloads
{
    using System;

    [Serializable]
    public class PayloadFactoryException :
        PayloadException
    {
        public PayloadFactoryException()
        {
        }

        public PayloadFactoryException(string message)
            : base(message)
        {
        }

        public PayloadFactoryException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}