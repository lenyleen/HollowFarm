using System.Collections.Generic;
using DefaultNamespace.Boosters.Interfaces;
using DefaultNamespace.ScriptableObjects;

namespace DefaultNamespace.Boosters.Factory
{
    public class PlantModifierFactory
    {
        public List<IPlantModifier> CreateModifiers(IReadOnlyList<ModifierData> modifierDataList)
        {
            var modifiers = new List<IPlantModifier>();
            
            foreach (var data in modifierDataList)
            {
                var modifier = CreateModifier(data);
                if (modifier != null)
                {
                    modifiers.Add(modifier);
                }
            }
            
            return modifiers;
        }

        private IPlantModifier CreateModifier(ModifierData data)
        {
            return data.Type switch
            {
                ModifierType.GrowthSpeed => new GrowthSpeedModifier(data),
                //ModifierType.WaterRetention => new WaterRetentionModifier(data),
                ModifierType.HarvestMultiplier => new HarvestMultiplierModifier(data),
                _ => null
            };
        }
    }
}