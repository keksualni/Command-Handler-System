using System;

namespace CommandsAndHandlers.Commands
{
    public class CommandAttribute : Attribute
    {
        public string CommandName { get; set; }
        public string CommandDescription { get; set; }


        public CommandAttribute(string commandName)
        {
            CommandName = commandName;
            CommandDescription = "No Description";
        }

        public CommandAttribute(string commandName, string commandDescription)
        {
            CommandName = commandName;
            CommandDescription = CommandDescription;
        }
    }
}