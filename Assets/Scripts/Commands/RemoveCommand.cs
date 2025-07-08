using System.Collections.Generic;
using DefaultNamespace.ViewModels.Interfaces;
using Service;
using Unity.VisualScripting;
using Zenject;

namespace DefaultNamespace.Commands
{
    public class RemoveCommand : CommandAbstract
    {
        public RemoveCommand(SoilActionType action, SignalBus signalBus) : base(action, signalBus)
        {
        }

        public override void Execute()
        {
            foreach (var commandPerformer in CommandPerformers)
            {
                commandPerformer.Remove();
            }
            CommandPerformed();
        }
    }
}