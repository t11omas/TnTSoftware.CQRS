namespace TnTSoftware.Cqrs.AspNetCore.Tests
{
    using System.Threading.Tasks;
    using FakeItEasy;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using Query;
    using Xunit;

    public class QueryControllerTests
    {
        [Fact]
        public async Task QueryResult_ReturnsNotFound_IfCommandResultNull()
        {
            // Arrange
            var command = new TestQuery();
            var invoker = A.Fake<IQueryInvoker>();
            A.CallTo(() => invoker.Invoke(command, null))
                .Returns(Task.FromResult((ExecutionResponse) null));

            var controller = new TestQueryController(invoker);

            // Act
            var result = await controller.CallQuery(command);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task QueryResult_ReturnsStatusCodeAndContent()
        {
            // Arrange
            var command = new TestQuery();
            var executionResponse = ExecutionResponse.Create("the result", ActionOutcome.Conflict, null);
            var invoker = A.Fake<IQueryInvoker>();
            A.CallTo(() => invoker.Invoke(command, null))
                .Returns(executionResponse);

            var controller = new TestQueryController(invoker);

            // Act
            var result = (ObjectResult) await controller.CallQuery(command);

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
        public async Task QueryResult_ReturnsStatusCodeOnly_IfStatusCodeHasNoBody(int statusCode)
        {
            // Arrange
            var command = new TestQuery();
            var actionOutcome = new ActionOutcome("foobar", statusCode, true);
            var executionResponse = ExecutionResponse.Create("the result", actionOutcome, null);
            var invoker = A.Fake<IQueryInvoker>();
            A.CallTo(() => invoker.Invoke(command, null))
                .Returns(executionResponse);

            var controller = new TestQueryController(invoker);
            
            // Act
            var result = await controller.CallQuery(command);
            
            // Assert
            result.Should().BeOfType<StatusCodeResult>().Which.StatusCode.Should().Be(statusCode);
        }
    }

    public class TestQueryController : QueryController
    {
        public TestQueryController(IQueryInvoker commandInvoker) : base(commandInvoker)
        {
        }

        public Task<IActionResult> CallQuery(TestQuery command)
            => QueryResult(command);
    }

    public class TestQuery : IQuery
    {
    }
}