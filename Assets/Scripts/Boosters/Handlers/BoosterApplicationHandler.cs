using DefaultNamespace.Boosters.Factory;
using DefaultNamespace.Boosters.Signals;
using DefaultNamespace.ScriptableObjects;
using DefaultNamespace.ViewModels;
using Zenject;

namespace DefaultNamespace.Boosters.Handlers
{
    public class BoosterApplicationHandler
    {
        private readonly SignalBus _signalBus;
        public BoosterApplicationHandler(SignalBus signalBus)
        {
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
            var boosterService = soilViewModel.BoosterService;
            boosterService.ApplyModifiers(boosterData);
        }
        
        public void Dispose()
        {
            _signalBus.Unsubscribe<ApplyBoosterSignal>(Handle);
        }

    }
}

