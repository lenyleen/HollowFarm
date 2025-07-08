using System.Collections.Generic;
using DefaultNamespace.Signals;
using DefaultNamespace.ViewModels.Interfaces;
using Service;
using Zenject;

namespace DefaultNamespace.Commands
{
    public abstract class CommandAbstract : IInitializableCommand
    {
        public SoilActionType Action { get; }
        protected SignalBus  _signalBus;

        protected CommandAbstract(SoilActionType action, SignalBus signalBus)
        {
            Action = action;
            _signalBus = signalBus;
        }

        protected IEnumerable<ICommandPerformer>  CommandPerformers;
        
        public void Initialize(IEnumerable<ICommandPerformer> targets)
        {
            CommandPerformers = targets;
        }

        
        public abstract void Execute();

        protected void CommandPerformed()
        {
            _signalBus.Fire<ClosePopUpRequestSignal>();
        }
    }
}