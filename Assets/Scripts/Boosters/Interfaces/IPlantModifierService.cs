using DefaultNamespace.ScriptableObjects;

namespace DefaultNamespace.Boosters.Interfaces
{
    public interface IPlantModifierService
    {
        void ApplyBooster(ConsumableData boosterData);
        void RemoveBooster(IPlantModifier boosterName);
        void UpdateModifiers();
    }
}