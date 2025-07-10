using DefaultNamespace.Boosters.Factory;
using DefaultNamespace.Boosters.Interfaces;
using DefaultNamespace.ScriptableObjects;

namespace DefaultNamespace.Boosters.Service
{
    public class PlantModifierService : IPlantModifierService
    {
        private readonly PlantModifierFactory _factory;
        private readonly IModifierTargetHolder _targetHolder;

        public PlantModifierService(PlantModifierFactory factory, IModifierTargetHolder targetHolder)
        {
            _factory = factory;
            _targetHolder = targetHolder;
        }

        public void ApplyBooster(ConsumableData boosterData)
        {
            
        }

        public void RemoveBooster(IPlantModifier boosterName)
        {
            
        }

        public void UpdateModifiers()
        {
            
        }
    }
}