namespace TnTSoftware.Cqrs.Tests
{
    using System.Threading;
    using System.Threading.Tasks;
    using Command;
    using MediatR;

    public class TestCommandHandler : ICommandHandler<TestCommand>
    {
        public string SettableValue { get; }

        public TestCommandHandler(string settableValue = null)
        {
            SettableValue = settableValue;
        }
        
        public Task<ExecutionResponse> Handle(CommandContext<TestCommand> request, CancellationToken cancellationToken, RequestHandlerDelegate<ExecutionResponse> next)
        {
            throw new System.NotImplementedException();
        }
    }
    
    public class TestCommand : ICommand
    {
        public string TestProp { get; set; }
    }
}