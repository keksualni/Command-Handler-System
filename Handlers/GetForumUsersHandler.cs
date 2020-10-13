using System;
using System.Threading.Tasks;
using CommandsAndHandlers.Commands;
using CommandsAndHandlers.Models;
using CommandsAndHandlers.Repositories;

namespace CommandsAndHandlers.Handlers
{
    public class GetForumUsersHandler : ICommandHandler<GetForumUsersCommand>
    {
        public void Handle(GetForumUsersCommand command)
        {
            foreach (User user in ForumUsersRepository.Users)
            {
                Console.WriteLine($"{user.FirstName} {user.LastName}");
            }
        }
    }
}