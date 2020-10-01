using System.Threading.Tasks;

namespace CommandsAndHandlers.Handlers
{
    public interface ICommandHandlerAsync<in TCommand> where TCommand : class
    {
        Task HandleAsync(TCommand command);
    }
}