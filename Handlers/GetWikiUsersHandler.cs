using System;
using CommandsAndHandlers.Commands;
using CommandsAndHandlers.Models;
using CommandsAndHandlers.Repositories;

namespace CommandsAndHandlers.Handlers
{
    public class GetWikiUsersHandler : ICommandHandler<GetWikiUsersCommand>
    {
        public void Handle(GetWikiUsersCommand command)
        {
            foreach (User user in WikiUsersRepository.Users)
            {
                Console.WriteLine($"{user.FirstName} {user.LastName}");
            }
        }
    }
}