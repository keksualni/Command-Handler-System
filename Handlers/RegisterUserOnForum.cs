using System;
using System.Threading.Tasks;
using CommandsAndHandlers.Commands;

namespace CommandsAndHandlers.Handlers
{
    public class RegisterUserOnForum : ICommandHandlerAsync<RegisterUserCommand>
    {
        public void Handle(RegisterUserCommand command)
        {
            Console.WriteLine($"{command.UserFirstName} {command.UserLastName} registered on the forum!");
        }
    }
}