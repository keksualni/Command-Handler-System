using System.Threading.Tasks;
using CommandsAndHandlers.Commands;

namespace CommandsAndHandlers.Handlers
{
    public class RegisterUserOnForum : ICommandHandlerAsync<RegisterUserCommand>
    {
        public Task HandleAsync(RegisterUserCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}