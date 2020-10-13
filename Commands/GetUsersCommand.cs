using CommandsAndHandlers.Dispatcher;

namespace CommandsAndHandlers.Commands
{
    [Command("get-users")]
    public class GetUsersCommand : Command
    {
        public GetUsersCommand(ICommandDispatcher commandDispatcher) : base(commandDispatcher) { }


        public override void Execute()
        {
            CommandDispatcher.Dispatch(this);
        }
    }
}