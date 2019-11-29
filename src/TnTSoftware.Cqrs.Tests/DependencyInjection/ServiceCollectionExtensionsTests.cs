namespace TnTSoftware.Cqrs.Tests.DependencyInjection
{
    using System;
    using Command;
    using Cqrs.DependencyInjection;
    using FluentAssertions;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using Query;
    using Xunit;

    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddCommandHandler_AddsProvidedCommandHandler()
        {
            // Arrange
            var services = new ServiceCollection();
            
            // Act
            services.AddCommandHandler<TestCommand, TestCommandHandler>();
            
            // Assert
            GetCommandHandler<TestCommand, TestCommandHandler>(services.BuildServiceProvider()).Should().NotBeNull();
        }
        
        [Fact]
        public void AddCommandHandler_Factory_AddsProvidedCommandHandler()
        {
            // Arrange
            var services = new ServiceCollection();
            const string testValue = "foo bar";
            
            // Act
            services.AddCommandHandler<TestCommand, TestCommandHandler>(svc =>
                new TestCommandHandler(testValue));
            
            // Assert
            var service = GetCommandHandler<TestCommand, TestCommandHandler>(services.BuildServiceProvider());
            service.SettableValue.Should().Be(testValue);
        }
        
        [Fact]
        public void AddQueryHandler_AddsProvidedQueryHandler()
        {
            // Arrange
            var services = new ServiceCollection();
            
            // Act
            services.AddQueryHandler<TestQuery, TestQueryHandler>();
            
            // Assert
            GetQueryHandler<TestQuery, TestQueryHandler>(services.BuildServiceProvider()).Should().NotBeNull();
        }
        
        [Fact]
        public void AddQueryHandler_Factory_AddsProvidedQueryHandler()
        {
            // Arrange
            var services = new ServiceCollection();
            const string testValue = "foo bar";
            
            // Act
            services.AddQueryHandler<TestQuery, TestQueryHandler>(svc =>
                new TestQueryHandler(testValue));
            
            // Assert
            var service = GetQueryHandler<TestQuery, TestQueryHandler>(services.BuildServiceProvider());
            service.SettableValue.Should().Be(testValue);
        }

        private THandler GetCommandHandler<TCommand, THandler>(IServiceProvider serviceProvider)
            where TCommand : ICommand
            where THandler : class, IPipelineBehavior<CommandContext<TCommand>, ExecutionResponse>
            => serviceProvider
                .GetService<IPipelineBehavior<CommandContext<TCommand>, ExecutionResponse>>() as THandler;
        
        private THandler GetQueryHandler<TQuery, THandler>(IServiceProvider serviceProvider)
            where TQuery : IQuery
            where THandler : class, IPipelineBehavior<QueryContext<TQuery>, ExecutionResponse>
            => serviceProvider
                .GetService<IPipelineBehavior<QueryContext<TQuery>, ExecutionResponse>>() as THandler;
    }
}