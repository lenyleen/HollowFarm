using System.Collections.Generic;
using DefaultNamespace.Boosters.Factory;
using DefaultNamespace.Boosters.Interfaces;
using DefaultNamespace.ScriptableObjects;
using DefaultNamespace.ViewModels;

namespace DefaultNamespace.Boosters.Commands
{
    public class ApplyBoosterCommand
    {
        private readonly IPlantModifierService _service;
        private readonly ConsumableData _boosterData;
        private readonly PlantModifierFactory _factory;

        public ApplyBoosterCommand(IPlantModifierService service, ConsumableData boosterData)// вынести в фабрику соилов PlantModifierFactory factory
        {
            _service = service;
            _boosterData = boosterData;
        }

        public void Execute()
        {
            var modifiers = _factory.CreateModifiers(_boosterData.Modifiers);
            ApplyModifiersToPlant(modifiers);
        }

        private void ApplyModifiersToPlant(List<IPlantModifier> modifiers)
        {
            _soilViewModel.ApplyBooster(_boosterData);
        }
    }
}