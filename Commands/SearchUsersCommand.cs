using System;
using System.Collections;
using CommandsAndHandlers.Dispatcher;

namespace CommandsAndHandlers.Commands
{
    [Command("search-users")]
    public class SearchUsersCommand : Command
    {
        public SearchUsersCommand(ICommandDispatcher commandDispatcher) : base(commandDispatcher) { }


        public string SearchText { get; set; }


        public override void FillCommandValues()
        {
            Console.Write("Enter search text: ");
            SearchText = Console.ReadLine();
        }

        public override void Execute()
        {
            CommandDispatcher.Dispatch(this);
        }
    }
}