using System;
using System.Collections.Generic;
using System.Linq;
using CommandsAndHandlers.Commands;
using CommandsAndHandlers.Models;
using CommandsAndHandlers.Repositories;

namespace CommandsAndHandlers.Handlers
{
    public class SearchUsersHandler : ICommandHandler<SearchUsersCommand>
    {
        public void Handle(SearchUsersCommand command)
        {
            IEnumerable<User> foundUsers = UserRepository
                .Users
                .Where(x => x.FirstName.ToLower().Contains(command.SearchText.ToLower())
                        || x.LastName.ToLower().Contains(command.SearchText.ToLower()))
                .AsEnumerable();

            foreach (User user in foundUsers)
            {
                Console.WriteLine($"{user.FirstName} {user.LastName}");
            }
        }
    }
}