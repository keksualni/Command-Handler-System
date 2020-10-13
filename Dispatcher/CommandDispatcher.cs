using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommandsAndHandlers.Commands;
using CommandsAndHandlers.Handlers;

namespace CommandsAndHandlers.Dispatcher
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly Dictionary<Type, IEnumerable<object>> _commandHandlerDictionary;


        public CommandDispatcher(Dictionary<Type, IEnumerable<object>> commandHandlerDictionary)
        {
            _commandHandlerDictionary = commandHandlerDictionary;
        }


        public void Dispatch<TCommand>(TCommand command) where TCommand : Command
        {
            command.FillCommandValues();

            Type handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());

            if (_commandHandlerDictionary.TryGetValue(handlerType, out IEnumerable<object> handlers))
            {
                Parallel.ForEach(handlers, handler =>
                {
                    var concreteHandler = handler as ICommandHandler<TCommand>;

                    concreteHandler?.Handle(command);
                });
            }
            else
            {
                throw new AggregateException("No handler registered");
            }
        }
    }
}