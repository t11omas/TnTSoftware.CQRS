using System.Threading.Tasks;
using TnTSoftware.Cqrs.Command;
using Microsoft.AspNetCore.Mvc;

namespace TnTSoftware.Cqrs.AspNetCore
{
    public abstract class CommandController : Controller
    {
        private readonly ICommandInvoker commandInvoker;

        protected CommandController(ICommandInvoker commandInvoker)
        {
            this.commandInvoker = commandInvoker;
        }

        [NonAction]
        protected virtual async Task<IActionResult> CommandResult<T>(T command)
            where T : ICommand
        {
            ExecutionResponse commandResponse = await this.commandInvoker.Invoke(command);
            return this.HandleCommandResponse(commandResponse);
        }

        [NonAction]
        protected virtual IActionResult HandleCommandResponse(ExecutionResponse commandResponse)
            => commandResponse.ConvertToActionResult();
    }
}