using System;
using System.Threading.Tasks;
using CommandsAndHandlers.Commands;
using CommandsAndHandlers.Models;
using CommandsAndHandlers.Repositories;

namespace CommandsAndHandlers.Handlers
{
    public class RegisterUserHandler : ICommandHandler<RegisterUserCommand>
    {
        public void Handle(RegisterUserCommand command)
        {
            var userToAdd = new User()
            {
                FirstName = command.UserFirstName,
                LastName = command.UserLastName
            };

            UserRepository.Users.Add(userToAdd);

            Console.WriteLine($"{command.UserFirstName} {command.UserLastName} registered!");
        }
    }
}