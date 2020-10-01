using System.Threading.Tasks;

namespace CommandsAndHandlers.Handlers
{
    public class RegisterUserHandler : ICommandHandlerAsync<RegisterUserHandler>
    {
        public Task HandleAsync(RegisterUserHandler command)
        {
            throw new System.NotImplementedException();
        }
    }
}