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
            var commandHandlerConcurrentStore = new Dictionary<Type, IEnumerable<object>>();

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
                        && i.GetGenericArguments()[0].IsAssignableFrom(typeof(Command))
                    );

                if (handlerCommandInterface == null)
                {
                    return;;
                }

                if (commandHandlerConcurrentStore.TryGetValue(handlerCommandInterface, out IEnumerable<object> list))
                {
                    commandHandlerConcurrentStore[handlerCommandInterface] = list.Concat(new [] { Activator.CreateInstance(handler) });
                }
                else
                {
                    object[] newHandlersList = new [] { Activator.CreateInstance(handler) };

                    commandHandlerConcurrentStore[handlerCommandInterface] = newHandlersList.AsEnumerable();
                }
            });

            var commandHandlerStore = new Dictionary<Type, IEnumerable<object>>(commandHandlerConcurrentStore);
            services.AddSingleton(new CommandDispatcher(commandHandlerStore));
        }
    }
}