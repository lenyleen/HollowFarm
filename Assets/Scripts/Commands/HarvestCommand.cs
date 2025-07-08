using Service;
using Zenject;

namespace DefaultNamespace.Commands
{
    public class HarvestCommand : CommandAbstract
    {
        public HarvestCommand(SoilActionType action, SignalBus signalBus) : base(action, signalBus)
        {
        }

        public override void Execute()
        {
            foreach (var commandPerformer in CommandPerformers)
            {
                commandPerformer.Harvest();
            }
            CommandPerformed();
        }
    }
}