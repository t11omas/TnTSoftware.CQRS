namespace TnTSoftware.Cqrs.AspNetCore
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Extension methods for <see cref="ExecutionResponse"/> objects 
    /// </summary>
    public static class ExecutionResponseExtensions
    {
        // NOTE(DG) This maps to internal array aspnetcore uses for checking if body supported
        private static readonly int[] StatusWithNoResponseBody = {100, 101, 204, 205, 304};
        
        /// <summary>
        /// Convert <see cref="ExecutionResponse"/> object to <see cref="IActionResult"/>.
        /// </summary>
        /// <param name="executionResponse">ExecutionResponse object to convert to IActionResult.</param>
        /// <returns><see cref="NotFoundResult"/> if object is null or converts to <see cref="ObjectResult"/> using
        /// .GetContent and ActionOutcomeCode.StatusCode</returns>
        public static IActionResult ConvertToActionResult(this ExecutionResponse executionResponse)
        {
            if (executionResponse == null)
            {
                return new NotFoundResult();
            }

            var statusCode = executionResponse.ActionOutcomeCode.StatusCode;
            
            if (StatusWithNoResponseBody.Contains(statusCode))
            {
                return new StatusCodeResult(statusCode);
            }
            
            return new ObjectResult(executionResponse.GetContent)
            {
                StatusCode = statusCode
            };
        }
    }
}