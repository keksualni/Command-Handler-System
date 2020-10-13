using CommandsAndHandlers.Dispatcher;

namespace CommandsAndHandlers.Commands
{
    [Command("help")]
    public class GetCommandsListCommand : Command
    {
        public GetCommandsListCommand(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
        }


        public override void Execute()
        {
            _commandDispatcher.DispatchAsync(this);
        }
    }
}