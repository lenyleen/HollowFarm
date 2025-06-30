using System.Collections.Generic;
using DefaultNamespace.ViewModels.Interfaces;
using Service;

namespace DefaultNamespace.Commands
{
    public class AddWaterCommand : CommandAbstract
    {
        public AddWaterCommand(SoilActionType action) : base(action)
        {
        }

        public override void Execute()
        {
            foreach (var commandPerformer in CommandPerformers)
            {
                commandPerformer.AddWater();
            }
        }
    }
}