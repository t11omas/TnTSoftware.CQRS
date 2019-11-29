namespace TnTSoftware.Cqrs.AspNetCore.Tests
{
    using System.Threading.Tasks;
    using TnTSoftware.Cqrs.Command;
    using FakeItEasy;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using Xunit;

    public class CommandControllerTests
    {
        [Fact]
        public async Task CommandResult_ReturnsNotFound_IfCommandResultNull()
        {
            // Arrange
            var command = new TestCommand();
            var invoker = A.Fake<ICommandInvoker>();
            A.CallTo(() => invoker.Invoke(command, null))
                .Returns(Task.FromResult((ExecutionResponse) null));

            var controller = new TestCommandController(invoker);
            
            // Act
            var result = await controller.CallCommand(command);
            
            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
        
        [Fact]
        public async Task CommandResult_ReturnsStatusCodeAndContent()
        {
            // Arrange
            var command = new TestCommand();
            var executionResponse = ExecutionResponse.Create("the result", ActionOutcome.Conflict, null);
            var invoker = A.Fake<ICommandInvoker>();
            A.CallTo(() => invoker.Invoke(command, null))
                .Returns(executionResponse);

            var controller = new TestCommandController(invoker);
            
            // Act
            var result = (ObjectResult)await controller.CallCommand(command);
            
            // Assert
            result.StatusCode.Should().Be(409);
            result.Value.Should().Be("the result");
        }

        [Theory]
        [InlineData(100)]
        [InlineData(101)]
        [InlineData(204)]
        [InlineData(205)]
        [InlineData(304)]
        public async Task CommandResult_ReturnsStatusCodeOnly_IfStatusCodeHasNoBody(int statusCode)
        {
            // Arrange
            var command = new TestCommand();
            var actionOutcome = new ActionOutcome("foobar", statusCode, true);
            var executionResponse = ExecutionResponse.Create("the result", actionOutcome, null);
            var invoker = A.Fake<ICommandInvoker>();
            A.CallTo(() => invoker.Invoke(command, null))
                .Returns(executionResponse);

            var controller = new TestCommandController(invoker);
            
            // Act
            var result = await controller.CallCommand(command);
            
            // Assert
            result.Should().BeOfType<StatusCodeResult>().Which.StatusCode.Should().Be(statusCode);
        }
    }
    
    public class TestCommandController : CommandController
    {
        public TestCommandController(ICommandInvoker commandInvoker) : base(commandInvoker)
        {
        }

        public Task<IActionResult> CallCommand(TestCommand command)
            => CommandResult(command);
    }

    public class TestCommand : ICommand
    {
    }
}