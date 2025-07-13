using System;
using System.Collections.Generic;
using DefaultNamespace.Boosters.Factory;
using DefaultNamespace.Boosters.Interfaces;
using DefaultNamespace.Boosters.ScriptableObjects;
using DefaultNamespace.ScriptableObjects;
using Zenject;
using IInitializable = Unity.VisualScripting.IInitializable;

namespace DefaultNamespace.Boosters.Service
{
    public class PlantModifierService : IPlantModifierService 
    {
        private readonly IFactory<PlantModifierData, IPlantModifier> _factory;
        private readonly IModifierTargetHolder _targetHolder;
        
        private IPlantModifierApplicable _applicable;

        public PlantModifierService(IFactory<PlantModifierData, IPlantModifier> factory, IModifierTargetHolder targetHolder)
        {
            _factory = factory;
            _targetHolder = targetHolder;
        }

        public void Initialize()
        {
            _targetHolder.OnModifierApplied += UpdateApplicable;
        }

        private void UpdateApplicable(IPlantModifierApplicable applicable) => _applicable = applicable;
        
        public void ApplyModifiers(ConsumableData boosterData)
        {
            var modifiersData = boosterData.Modifiers;
            foreach (var modifierData in modifiersData)
            {
                if (!CheckModifierEfficiency(modifierData)) 
                    continue;

                var modifier = _factory.Create(modifierData);
                _applicable.ApplyModifier(modifierData.PlantProperty, modifier);
            }
        }

        private bool CheckModifierEfficiency(PlantModifierData modifierData)
        {
            return !_applicable.TryGetPlantModifier(modifierData.PlantProperty, out var plantModifier) 
                   || plantModifier.IsGreater(modifierData);
        }
        
        public void UpdateModifiers()
        {
            if(_applicable == null)
                return;
            
            _applicable.UpdateModifiers(1);
        }
        
        public void Dispose()
        {
            _targetHolder.OnModifierApplied -=  UpdateApplicable;
        }
    }
}