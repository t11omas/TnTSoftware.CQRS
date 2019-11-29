namespace TnTSoftware.Cqrs
{
    public class EmptyResponse
    {
        private static EmptyResponse emptyResponse = new EmptyResponse();

        public static EmptyResponse Create()
        {
            return emptyResponse;
        }
    }
}