using CommandsAndHandlers.Dispatcher;

namespace CommandsAndHandlers.Commands
{
    [Command("get-wiki-users")]
    public class GetWikiUsersCommand : Command
    {
        public GetWikiUsersCommand(ICommandDispatcher commandDispatcher) : base(commandDispatcher) { }


        public override void Execute()
        {
            CommandDispatcher.Dispatch(this);
        }
    }
}