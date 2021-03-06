﻿using System;
using CommandsAndHandlers.Dispatcher;

namespace CommandsAndHandlers.Commands
{
    public abstract class Command
    {
        protected readonly ICommandDispatcher CommandDispatcher;


        protected Command(ICommandDispatcher commandDispatcher)
        {
            this.CommandDispatcher = commandDispatcher;
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