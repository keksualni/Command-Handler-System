using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CommandsAndHandlers.Commands;
using CommandsAndHandlers.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace CommandsAndHandlers.Dispatcher
{
    public static class DispatcherInitializerExtension
    {
        public static void AddCommandDispatcher(this IServiceCollection services)
        {
            bool CheckCommandType(Type type)
            {
                return type.IsSubclassOf(typeof(Command))
                       && type.GetCustomAttributes().Any(a => a is CommandAttribute);
            }


            var commandHandlerConcurrentStore = new ConcurrentDictionary<Type, IEnumerable<object>>();

            Type[] handlers = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => x.IsClass
                    && x.GetInterfaces().Any(i => i.IsGenericType
                        && i.GetGenericTypeDefinition() == typeof(ICommandHandlerAsync<>)))
                .ToArray();

            Parallel.ForEach(handlers, handler =>
            {
                Type handlerCommandInterface = handler
                    .GetInterfaces()
                    .FirstOrDefault(i => i.GetGenericTypeDefinition() == typeof(ICommandHandlerAsync<>)
                        && CheckCommandType(i.GetGenericArguments()[0])
                    );

                if (handlerCommandInterface == null)
                {
                    return;
                }

                object[] arrayWithNewHandler = new [] { Activator.CreateInstance(handler) };

                commandHandlerConcurrentStore.AddOrUpdate(
                    handlerCommandInterface,
                    arrayWithNewHandler.AsEnumerable(),
                    (key, oldValue) => oldValue.Concat(arrayWithNewHandler)
                );
            });

            var commandHandlerStore = new Dictionary<Type, IEnumerable<object>>(commandHandlerConcurrentStore);
            services.AddSingleton(new CommandDispatcher(commandHandlerStore));
        }
    }
}