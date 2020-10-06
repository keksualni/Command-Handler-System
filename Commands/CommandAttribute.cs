using System;

namespace CommandsAndHandlers.Commands
{
    public class CommandAttribute : Attribute
    {
        public string CommandName { get; set; }


        public CommandAttribute(string commandName)
        {
            CommandName = commandName;
        }
    }
}