using System;
using Microsoft.Extensions.Hosting;

namespace CommandsAndHandlers
{
    class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder().Build();
        }

        public static IHostBuilder CreateHostBuilder() => new HostBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                DispatcherInitializer.Initialize(services);
            })
            .UseConsoleLifetime();
    }
}