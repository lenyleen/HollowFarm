using DefaultNamespace.ScriptableObjects;
using DefaultNamespace.Signals;
using DefaultNamespace.ViewModels;

namespace DefaultNamespace.Boosters.Signals
{
    public class ApplyBoosterSignal
    {
        public SoilViewModel SoilViewModel { get; }
        public ConsumableData BoosterData { get; }

        public ApplyBoosterSignal(SoilViewModel soilViewModel, ConsumableData boosterData)
        {
            SoilViewModel = soilViewModel;
            BoosterData = boosterData;
        }
    }
}