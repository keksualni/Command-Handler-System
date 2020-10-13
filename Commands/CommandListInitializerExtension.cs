using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CommandsAndHandlers.Models;
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

            var commandConcurrentDictionary = new ConcurrentDictionary<string, Type>();
            var commandInfoConcurrentList = new ConcurrentBag<CommandInfo>();

            Parallel.ForEach(commands, command =>
            {
                var commandAttribute = (CommandAttribute) command.GetCustomAttributes()
                    .First(a => a is CommandAttribute);

                if (!commandConcurrentDictionary.TryAdd(commandAttribute.CommandName, command))
                {
                    throw new AggregateException($"Adding {commandAttribute.CommandName} command failed!");
                }

                commandInfoConcurrentList.Add(
                    new CommandInfo(commandAttribute.CommandName, commandAttribute.CommandDescription)
                );
            });

            var commandsDictionary = new Dictionary<string, Type>(commandConcurrentDictionary);
            IReadOnlyCollection<CommandInfo> commandInfoList = new List<CommandInfo>(commandInfoConcurrentList);

            services.AddSingleton(commandsDictionary);
            services.AddSingleton(commandInfoList);
        }
    }
}