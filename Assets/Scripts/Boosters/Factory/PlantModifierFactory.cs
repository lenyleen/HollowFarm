using System.Collections.Generic;
using DefaultNamespace.Boosters.Interfaces;
using DefaultNamespace.Boosters.ScriptableObjects;
using DefaultNamespace.ScriptableObjects;
using Zenject;

namespace DefaultNamespace.Boosters.Factory
{
    public class PlantModifierFactory : IFactory<PlantModifierData, IPlantModifier>
    {
        public IPlantModifier Create(PlantModifierData data)
        {
            return new PlantModifier(data);
        }
    }
}