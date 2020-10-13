using System;
using CommandsAndHandlers.Dispatcher;

namespace CommandsAndHandlers.Commands
{
    public abstract class Command
    {
        protected readonly ICommandDispatcher _commandDispatcher;


        protected Command(ICommandDispatcher commandDispatcher)
        {
            this._commandDispatcher = commandDispatcher;
        }


        public virtual void FillCommandValues()
        {
        }

        public virtual void Execute()
        {
            Console.WriteLine("Executed!");
        }
    }
}