using System;
using CommandsAndHandlers.Commands;
using CommandsAndHandlers.Dispatcher;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CommandsAndHandlers
{
    class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder().Build().Run();
        }

        public static IHostBuilder CreateHostBuilder() => new HostBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddCommandList();
                services.AddCommandDispatcher();

                services.AddHostedService<ConsoleUIService>();
            })
            .UseConsoleLifetime();
    }
}