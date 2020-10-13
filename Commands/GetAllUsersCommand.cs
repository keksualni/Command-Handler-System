using CommandsAndHandlers.Dispatcher;

namespace CommandsAndHandlers.Commands
{
    [Command("get-all-users")]
    public class GetAllUsersCommand : Command
    {
        public GetAllUsersCommand(ICommandDispatcher commandDispatcher) : base(commandDispatcher) { }


        public override void Execute()
        {
            CommandDispatcher.Dispatch(this);
        }
    }
}