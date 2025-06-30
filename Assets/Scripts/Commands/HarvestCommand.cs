using Service;

namespace DefaultNamespace.Commands
{
    public class HarvestCommand : CommandAbstract
    {
        public HarvestCommand(SoilActionType action) : base(action)
        {
        }

        public override void Execute()
        {
            foreach (var commandPerformer in CommandPerformers)
            {
                commandPerformer.Harvest();
            }
        }
    }
}