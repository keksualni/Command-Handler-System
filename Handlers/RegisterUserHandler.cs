using System;
using System.Threading.Tasks;
using CommandsAndHandlers.Commands;

namespace CommandsAndHandlers.Handlers
{
    public class RegisterUserHandler : ICommandHandlerAsync<RegisterUserCommand>
    {
        public Task HandleAsync(RegisterUserCommand command)
        {
            Console.WriteLine($"{command.UserFirstName} registered!");

            return Task.CompletedTask;
        }
    }
}