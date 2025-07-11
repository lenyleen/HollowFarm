using System.Collections.Generic;
using DefaultNamespace.Boosters.ScriptableObjects;

namespace DefaultNamespace.Boosters.Interfaces
{
    public interface IPlantModifierApplicable
    {
       public bool TryGetPlantModifier(PlantProperty plantProperty, out IPlantModifier plantModifier); 
       public void ApplyModifier(PlantProperty plantProperty, IPlantModifier modifier);
       public void UpdateModifiers(float deltaTime);
    }
}