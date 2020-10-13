using System.Threading.Tasks;

namespace CommandsAndHandlers.Handlers
{
    public interface ICommandHandlerAsync<in TCommand> where TCommand : class
    {
        void Handle(TCommand command);
    }
}