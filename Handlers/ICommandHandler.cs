using System.Threading.Tasks;

namespace CommandsAndHandlers.Handlers
{
    public interface ICommandHandler<in TCommand> where TCommand : class
    {
        void Handle(TCommand command);
    }
}