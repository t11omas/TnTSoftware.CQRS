using System;
using System.Threading.Tasks;
using TnTSoftware.Cqrs.Query;
using Microsoft.AspNetCore.Mvc;

namespace TnTSoftware.Cqrs.AspNetCore
{
    public abstract class QueryController : Controller
    {
        private readonly IQueryInvoker queryInvoker;

        protected QueryController(IQueryInvoker queryInvoker)
        {
            this.queryInvoker = queryInvoker;
        }

        [NonAction]
        protected virtual async Task<IActionResult> QueryResult<T>(T query)
            where T : IQuery
        {
            ExecutionResponse queryResponse = await this.queryInvoker.Invoke(query);
            return this.HandleQueryResponse(queryResponse);
        }

        [NonAction]
        protected virtual IActionResult HandleQueryResponse(ExecutionResponse queryResponse)
            => queryResponse.ConvertToActionResult();
    }
}
