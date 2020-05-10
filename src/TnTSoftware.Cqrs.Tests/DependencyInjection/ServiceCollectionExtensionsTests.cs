namespace TnTSoftware.Cqrs.Tests.DependencyInjection
{
    using System.Threading.Tasks;

    using Cqrs.DependencyInjection;

    using FluentAssertions;

    using Microsoft.Extensions.DependencyInjection;
    using Query;

    using TnT.Cqrs.Core.Command;

    using Xunit;

    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public async Task AddCommandHandler_AddsProvidedCommandHandler()
        {
            // Arrange
            var services = new ServiceCollection();
            
            // Act
            services.AddTnTSoftwareCqrs(
                    options =>
                        {
                            options.ConfigureCommandPipeline(
                                p =>
                                    {
                                        p.AddCommandHandlerPipeline(0);
                                    });

                            options.ConfigureQueryPipeline(
                                p =>
                                    {
                                        p.AddQueryHandlerPipeline(0);
                                    });
                        });
            
            // Assert
            var sp = services.BuildServiceProvider();
            sp.GetService<ICommandHandler<TestCommand, TestUser>>().Should().NotBeNull();

            var r = await sp.GetService<ICommandExecutor>().Send(new TestCommand(), new TestUser());
        }
        
        ////[Fact]
        ////public void AddCommandHandler_Factory_AddsProvidedCommandHandler()
        ////{
        ////    // Arrange
        ////    var services = new ServiceCollection();
        ////    const string testValue = "foo bar";
            
        ////    // Act
        ////    services.AddCommandHandler<TestCommand, TestCommandHandler>(svc =>
        ////        new TestCommandHandler(testValue));
            
        ////    // Assert
        ////    var service = GetCommandHandler<TestCommand, TestCommandHandler>(services.BuildServiceProvider());
        ////    service.SettableValue.Should().Be(testValue);
        ////}
        
        ////[Fact]
        ////public void AddQueryHandler_AddsProvidedQueryHandler()
        ////{
        ////    // Arrange
        ////    var services = new ServiceCollection();
            
        ////    // Act
        ////    services.AddQueryHandler<TestQuery, TestQueryHandler>();
            
        ////    // Assert
        ////    GetQueryHandler<TestQuery, TestQueryHandler>(services.BuildServiceProvider()).Should().NotBeNull();
        ////}
        
        ////[Fact]
        ////public void AddQueryHandler_Factory_AddsProvidedQueryHandler()
        ////{
        ////    // Arrange
        ////    var services = new ServiceCollection();
        ////    const string testValue = "foo bar";
            
        ////    // Act
        ////    services.AddQueryHandler<TestQuery, TestQueryHandler>(svc =>
        ////        new TestQueryHandler(testValue));
            
        ////    // Assert
        ////    var service = GetQueryHandler<TestQuery, TestQueryHandler>(services.BuildServiceProvider());
        ////    service.SettableValue.Should().Be(testValue);
        ////}

        ////private THandler GetCommandHandler<TCommand, THandler>(IServiceProvider serviceProvider)
        ////    where TCommand : ICommand
        ////    where THandler : class, IPipelineBehavior<CommandContext<TCommand>, ExecutionResponse>
        ////    => serviceProvider
        ////        .GetService<IPipelineBehavior<CommandContext<TCommand>, ExecutionResponse>>() as THandler;
        
        ////private THandler GetQueryHandler<TQuery, THandler>(IServiceProvider serviceProvider)
        ////    where TQuery : IQuery
        ////    where THandler : class, IPipelineBehavior<QueryContext<TQuery>, ExecutionResponse>
        ////    => serviceProvider
        ////        .GetService<IPipelineBehavior<QueryContext<TQuery>, ExecutionResponse>>() as THandler;
    }
}