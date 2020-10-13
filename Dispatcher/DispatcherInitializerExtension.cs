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

            foreach (Type handler in handlers)
            {
                Type handlerCommandInterface = handler
                    .GetInterfaces()
                    .FirstOrDefault(i => i.GetGenericTypeDefinition() == typeof(ICommandHandlerAsync<>)
                        && CheckCommandType(i.GetGenericArguments()[0])
                    );

                if (handlerCommandInterface == null)
                {
                    continue;
                }

                services.AddScoped(handlerCommandInterface, handler);
            }

            Type[] descriptors = services
                .Where(t => t.ServiceType.IsGenericType &&
                            t.ServiceType.GetGenericTypeDefinition() == typeof(ICommandHandlerAsync<>))
                .Select(x => x.ServiceType)
                .Distinct()
                .ToArray();

            ServiceProvider provider = services.BuildServiceProvider();

            Parallel.ForEach(descriptors, descriptor =>
            {
                commandHandlerConcurrentStore.AddOrUpdate(
                    descriptor,
                    provider.GetServices(descriptor),
                    (key, oldValue) => provider.GetServices(descriptor)
                );
            });

            var commandHandlerStore = new Dictionary<Type, IEnumerable<object>>(commandHandlerConcurrentStore);
            services.AddSingleton<ICommandDispatcher>(new CommandDispatcher(commandHandlerStore));
        }
    }
}