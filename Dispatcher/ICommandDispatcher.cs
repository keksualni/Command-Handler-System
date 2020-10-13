using System.Threading.Tasks;
using CommandsAndHandlers.Commands;

namespace CommandsAndHandlers.Dispatcher
{
    public interface ICommandDispatcher
    {
        public Task DispatchAsync<TCommand>(TCommand command) where TCommand : Command;
    }
}