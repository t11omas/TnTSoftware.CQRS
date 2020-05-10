namespace TnT.Cqrs.Core.Command
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using TnT.Cqrs.Core.ServiceFactory;

    using TnTSoftware.Cqrs;

    public class CommandExecutor : ICommandExecutor
    {
        private readonly Core.ServiceFactory.ServiceFactory serviceFactory;

        public CommandExecutor(Core.ServiceFactory.ServiceFactory serviceFactory)
        {
            this.serviceFactory = serviceFactory;
        }

        public Task<ExecutionResult> Send<TCommand, TUser>(TCommand command, TUser user, CancellationToken cancellationToken = default)
            where TCommand : ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            return this.serviceFactory
                .GetInstance<PipelineStepExecutor<TCommand, TUser>>().Execute(command, user, cancellationToken);
        }
    }
}