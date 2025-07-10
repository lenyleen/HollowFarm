namespace DefaultNamespace.Boosters.Interfaces
{
    public interface IPlantModifierApplicable
    {
        public void ApplyModifier(IPlantModifier modifier);
        public void RemoveModifier(IPlantModifier modifier);
        public void UpdateModifiers(float deltaTime);
    }
}