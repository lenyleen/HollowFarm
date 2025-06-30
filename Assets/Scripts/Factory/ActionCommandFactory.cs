using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Commands;
using DefaultNamespace.ViewModels.Interfaces;
using Service;
using Zenject;

namespace DefaultNamespace.Factory
{
    public class ActionCommandFactory : IFactory<IEnumerable<ICommandPerformer>, SoilActionType, ICommand<ICommandPerformer>>
    {
        private readonly Dictionary<SoilActionType, IInitializableCommand> _commands;

        public ActionCommandFactory(List<IInitializableCommand>  commands)
        {
            _commands = commands.ToDictionary(command => command.Action);
        }
        
        public ICommand<ICommandPerformer> Create(IEnumerable<ICommandPerformer> targets, SoilActionType type)
        {
            if (!_commands.TryGetValue(type, out var command))
                throw new Exception("Command not found");
            
            command.Initialize(targets);
            return command;
        }
    }
}