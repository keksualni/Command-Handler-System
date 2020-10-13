using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CommandsAndHandlers.Commands;
using CommandsAndHandlers.Dispatcher;
using Microsoft.Extensions.Hosting;

namespace CommandsAndHandlers
{
    public class ConsoleUIService : IHostedService
    {
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly Dictionary<string, Type> _commandsDictionary;


        public ConsoleUIService(
            IHostApplicationLifetime applicationLifetime,
            ICommandDispatcher commandDispatcher,
            Dictionary<string, Type> commandsDictionary
        )
        {
            _applicationLifetime = applicationLifetime;
            _commandDispatcher = commandDispatcher;
            _commandsDictionary = commandsDictionary;
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            _applicationLifetime.ApplicationStarted.Register(OnStarted);
            _applicationLifetime.ApplicationStopping.Register(OnStopping);
            _applicationLifetime.ApplicationStopped.Register(OnStopped);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void OnStarted()
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Application stared!");
            Console.WriteLine("Press 'Ctrl + C' to stop application");
            Console.WriteLine("-----------------------------\n\n\n\n");

            StartCommandLineObserving();
        }

        private void OnStopping()
        {
            Console.WriteLine("\n\n\n\nApplication is stopping...\n\n\n\n");
        }

        private void OnStopped()
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Application stopped!");
            Console.WriteLine("-----------------------------");
        }

        private void StartCommandLineObserving()
        {
            string commandExecuteName;
            do
            {
                Console.Write("Enter the command: ");
                commandExecuteName = Console.ReadLine();

                if (_commandsDictionary.TryGetValue(commandExecuteName, out Type command))
                {
                    var commandToExecute = Activator.CreateInstance(command, _commandDispatcher) as Command;

                    commandToExecute?.Execute();
                }
                else
                {
                    Console.WriteLine($"No command with name ${commandExecuteName} was found!");
                }
            } while (commandExecuteName != "exit");
        }
    }
}