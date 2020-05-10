namespace TnTSoftware.Cqrs.Tests
{
    using System.Threading;
    using System.Threading.Tasks;

    using TnT.Cqrs.Core;
    using TnT.Cqrs.Core.Command;

    public class TestCommandHandler : ICommandHandler<TestCommand, TestUser>
    {
        public string SettableValue { get; }

        public TestCommandHandler(string settableValue = null)
        {
            SettableValue = settableValue;
        }

        public Task<ExecutionResult> Execute(TestCommand command, TestUser user)
        {
            throw new System.NotImplementedException();
        }
    }
    
    public class TestCommand : ICommand
    {
        public string TestProp { get; set; }
    }

    public class TestUser
    {
        public string Name { get; set; }
    }
}