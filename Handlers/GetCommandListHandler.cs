using System;
using System.Collections.Generic;
using CommandsAndHandlers.Commands;
using CommandsAndHandlers.Models;

namespace CommandsAndHandlers.Handlers
{
    public class GetCommandListHandler : ICommandHandlerAsync<GetCommandListCommand>
    {
        private readonly IReadOnlyCollection<CommandInfo> _commandInfos;


        public GetCommandListHandler(IReadOnlyCollection<CommandInfo> commandInfos)
        {
            _commandInfos = commandInfos;
        }


        public void Handle(GetCommandListCommand command)
        {
            foreach (CommandInfo commandInfo in _commandInfos)
            {
                Console.WriteLine($"{commandInfo.Name} - {commandInfo.Description}");
            }
        }
    }
}