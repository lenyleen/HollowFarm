using System;
using DefaultNamespace.Boosters.ScriptableObjects;
using UnityEngine;

namespace DefaultNamespace.Boosters.Interfaces
{
    public interface IPlantModifier
    {
        public string Name { get; }
        public bool IsActive { get; }
        public Color Color { get; }
        public float Value { get; }
        public TimeSpan RemainingTime { get; }

        public void Update(float deltaTime);
        public bool IsGreater(PlantModifierData modifierData);
        public float CalculateUtility();
    }
}