using CommandsAndHandlers.Dispatcher;

namespace CommandsAndHandlers.Commands
{
    [Command("help")]
    public class GetCommandListCommand : Command
    {
        public GetCommandListCommand(ICommandDispatcher commandDispatcher) : base(commandDispatcher) { }


        public override void Execute()
        {
            CommandDispatcher.Dispatch(this);
        }
    }
}