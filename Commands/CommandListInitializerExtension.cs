using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace CommandsAndHandlers.Commands
{
    public static class CommandListInitializerExtension
    {
        public static void AddCommandList(this IServiceCollection services)
        {
            IEnumerable<Type> commands = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.GetCustomAttributes()
                    .Any(a => a is CommandAttribute)
                )
                .AsEnumerable();

            var commandsConcurrentDictionary = new ConcurrentDictionary<string, Type>();

            Parallel.ForEach(commands, command =>
            {
                var commandAttribute = (CommandAttribute) command.GetCustomAttributes()
                    .First(a => a is CommandAttribute);

                if (!commandsConcurrentDictionary.TryAdd(commandAttribute.CommandName, command))
                {
                    throw new AggregateException($"Adding {commandAttribute.CommandName} command failed!");
                }
            });

            var commandsDictionary = new Dictionary<string, Type>(commandsConcurrentDictionary);

            services.AddSingleton(commandsDictionary);
        }
    }
}