using System;
using System.Threading.Tasks;
using CommandsAndHandlers.Commands;
using CommandsAndHandlers.Models;
using CommandsAndHandlers.Repositories;

namespace CommandsAndHandlers.Handlers
{
    public class GetUsersHandler : ICommandHandler<GetUsersCommand>
    {
        public void Handle(GetUsersCommand command)
        {
            foreach (User user in UserRepository.Users)
            {
                Console.WriteLine($"{user.FirstName} {user.LastName}");
            }
        }
    }
}