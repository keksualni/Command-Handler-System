using System;
using CommandsAndHandlers.Commands;
using CommandsAndHandlers.Dispatcher;
using CommandsAndHandlers.Models;
using CommandsAndHandlers.Repositories;

namespace CommandsAndHandlers.Handlers
{
    public class GetAllUsersHandler : ICommandHandler<GetAllUsersCommand>
    {
        public void Handle(GetAllUsersCommand command)
        {
            Console.WriteLine("***USERS***");
            foreach (User user in UserRepository.Users)
            {
                Console.WriteLine($"{user.FirstName} {user.LastName}");
            }

            Console.WriteLine("***FORUM USERS***");
            foreach (User user in ForumUsersRepository.Users)
            {
                Console.WriteLine($"{user.FirstName} {user.LastName}");
            }

            Console.WriteLine("***WIKI USERS***");
            foreach (User user in WikiUsersRepository.Users)
            {
                Console.WriteLine($"{user.FirstName} {user.LastName}");
            }
        }
    }
}