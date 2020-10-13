using System.Threading.Tasks;
using CommandsAndHandlers.Commands;

namespace CommandsAndHandlers.Dispatcher
{
    public interface ICommandDispatcher
    {
        public void Dispatch<TCommand>(TCommand command) where TCommand : Command;
    }
}