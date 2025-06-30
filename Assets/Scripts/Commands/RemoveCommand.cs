using System.Collections.Generic;
using DefaultNamespace.ViewModels.Interfaces;
using Service;
using Unity.VisualScripting;

namespace DefaultNamespace.Commands
{
    public class RemoveCommand : CommandAbstract
    {
        public RemoveCommand(SoilActionType action) : base(action)
        {
        }

        public override void Execute()
        {
            foreach (var commandPerformer in CommandPerformers)
            {
                commandPerformer.Remove();
            }
        }
    }
}