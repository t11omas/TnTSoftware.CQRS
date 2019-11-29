#  CQRS  

This package contains a lightweight framework to facilitate separate stacks command and queries as described by Martin Fowler ([https://martinfowler.com/bliki/CQRS.html](https://martinfowler.com/bliki/CQRS.html)).

The framework is underpinned by Mediatr [https://github.com/jbogard/MediatR](https://github.com/jbogard/MediatR) and makes use of Behaviours [https://github.com/jbogard/MediatR/wiki/Behaviors](https://github.com/jbogard/MediatR/wiki/Behaviors)
therefore these can be added to the pipeline to address any cross cutting concerns or split the business logic up into multiple handlers where appropriate to-do so.

## Configuring in startup 

```cs
services.AddArnoldClarkCqrs();
```

## Simple Command

### Command

```cs
public class TestCommand : ICommand
{
}
```

### Response

```cs
public class TestResponse
{
}
```

### CommandHandler

```cs
public class TestCommandHandler : ICommandHandler<TestCommand>
{
	public override async Task<ExecutionResponse> Handle(CommandContext<TestCommand> commandContext, CancellationToken cancellationToken, RequestHandlerDelegate<ExecutionResponse> next)
	{
		//await next() -- this should be call if part of a pipeline 
		return this.Ok();
	}
}
```

### Register CommandHandler

```cs
// without factory
services.AddCommandHandler<TestCommand, TestCommandHandler>();

// with factory
services.AddCommandHandler<TestCommand, TestCommandHandler>(svc => new TestCommandHandler());
```

## Simple Query

### Query

```cs
public class TestQuery : IQuery
{
}
```

### Response

```cs
public class TestResponse
{
}
```

### QueryHandler

```cs
public class TestQueryHandler : IQueryHandler<TestQuery>
{
	public override async Task<ExecutionResponse> Handle(QueryContext<TestCommand> commandContext, CancellationToken cancellationToken, RequestHandlerDelegate<ExecutionResponse> next)
	{
		//await next() -- this should be call if part of a pipeline 
		return this.Ok();
	}
}
```

### Register QueryHandler

```cs
// without factory
services.AddQueryHandler<TestQuery, TestQueryHandler>();

// with factory
services.AddQueryHandler<TestQuery, TestQueryHandler>(svc => new TestQueryHandler());
```

## Pass data between steps

## insert data at start of request
