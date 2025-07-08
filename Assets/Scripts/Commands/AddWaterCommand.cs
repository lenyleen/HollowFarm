using System.Collections.Generic;
using DefaultNamespace.ViewModels.Interfaces;
using Service;
using Zenject;

namespace DefaultNamespace.Commands
{
    public class AddWaterCommand : CommandAbstract
    {
        public AddWaterCommand(SoilActionType action, SignalBus signalBus) : base(action, signalBus)
        {
        }

        public override void Execute()
        {
            foreach (var commandPerformer in CommandPerformers)
            {
                commandPerformer.AddWater();
            }
            CommandPerformed();
        }
    }
}