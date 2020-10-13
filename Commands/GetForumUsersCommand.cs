using CommandsAndHandlers.Dispatcher;

namespace CommandsAndHandlers.Commands
{
    [Command("get-forum-users")]
    public class GetForumUsersCommand : Command
    {
        public GetForumUsersCommand(ICommandDispatcher commandDispatcher) : base(commandDispatcher) { }


        public override void Execute()
        {
            CommandDispatcher.Dispatch(this);
        }
    }
}