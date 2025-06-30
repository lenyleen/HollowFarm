using System.Collections.Generic;
using DefaultNamespace.ViewModels.Interfaces;
using Service;

namespace DefaultNamespace.Commands
{
    public abstract class CommandAbstract : IInitializableCommand
    {
        public SoilActionType Action { get; }

        protected CommandAbstract(SoilActionType action)
        {
            Action = action;
        }

        protected IEnumerable<ICommandPerformer>  CommandPerformers;
        
        public void Initialize(IEnumerable<ICommandPerformer> targets)
        {
            CommandPerformers = targets;
        }

        
        public abstract void Execute();
    }
}