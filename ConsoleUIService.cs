using System;
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
        private readonly CommandDispatcher _commandDispatcher;


        public ConsoleUIService(IHostApplicationLifetime applicationLifetime, CommandDispatcher commandDispatcher)
        {
            _applicationLifetime = applicationLifetime;
            _commandDispatcher = commandDispatcher;
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
            Console.WriteLine("Press 'C' to stop application");
            Console.WriteLine("-----------------------------\n\n\n\n");

            StartCommandLineObserving();
        }

        private void OnStopping()
        {
            Console.WriteLine("Application is stopping...\n\n\n\n");
        }

        private void OnStopped()
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Application stopped!");
            Console.WriteLine("-----------------------------");
        }

        private void StartCommandLineObserving()
        {
            Console.Write("Enter the command: ");
            string command = Console.ReadLine();

            if (command == "register")
            {
                _commandDispatcher.DispatchAsync(new RegisterUserCommand() { UserFirstName  = "Nikita" });
            }
        }
    }
}