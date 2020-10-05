using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommandsAndHandlers.Handlers;

namespace CommandsAndHandlers.Dispatcher
{
    public class CommandDispatcher
    {
        private readonly Dictionary<Type, IEnumerable<object>> _commandHandlerDictionary;


        public CommandDispatcher(Dictionary<Type, IEnumerable<object>> commandHandlerDictionary)
        {
            _commandHandlerDictionary = commandHandlerDictionary;
        }


        public Task DispatchAsync<TCommand>(TCommand command) where TCommand: class
        {
            Type handlerType = typeof(ICommandHandlerAsync<>).MakeGenericType(command.GetType());

            if (_commandHandlerDictionary.TryGetValue(handlerType, out IEnumerable<object> handlers))
            {
                Parallel.ForEach(handlers, handler =>
                {
                    var concreteHandler = handler as ICommandHandlerAsync<TCommand>;

                    concreteHandler?.HandleAsync(command).Start();
                });
            }
            else
            {
                throw new AggregateException("No handler registered");
            }

            return Task.CompletedTask;
        }
    }
}