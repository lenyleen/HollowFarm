using DefaultNamespace.ScriptableObjects;

namespace DefaultNamespace.Boosters.Interfaces
{
    public interface IPlantModifierService
    {
        void ApplyModifiers(ConsumableData boosterData);
        void UpdateModifiers();
    }
}