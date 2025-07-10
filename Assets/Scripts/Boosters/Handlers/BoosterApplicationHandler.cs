using DefaultNamespace.Boosters.Commands;
using DefaultNamespace.Boosters.Factory;
using DefaultNamespace.Boosters.Signals;
using DefaultNamespace.ScriptableObjects;
using DefaultNamespace.ViewModels;
using Zenject;

namespace DefaultNamespace.Boosters.Handlers
{
    public class BoosterApplicationHandler
    {
        private readonly PlantModifierFactory _factory;
        private readonly SignalBus _signalBus;

        public BoosterApplicationHandler(PlantModifierFactory factory, SignalBus signalBus)
        {
            _factory = factory;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<ApplyBoosterSignal>(Handle);
        }

        private void Handle(ApplyBoosterSignal signal)
        {
            CreateAndExecuteCommand(signal.SoilViewModel, signal.BoosterData);
        }

        private void CreateAndExecuteCommand(SoilViewModel soilViewModel, ConsumableData boosterData)
        {
            var command = new ApplyBoosterCommand(soilViewModel, boosterData, _factory);
            command.Execute();
        }
        
        public void Dispose()
        {
            _signalBus.Unsubscribe<ApplyBoosterSignal>(Handle);
        }

    }
}

