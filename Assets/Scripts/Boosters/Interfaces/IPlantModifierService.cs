using System;
using DefaultNamespace.ScriptableObjects;
using Zenject;

namespace DefaultNamespace.Boosters.Interfaces
{
    public interface IPlantModifierService :  IInitializable, IDisposable
    {
        void ApplyModifiers(ConsumableData boosterData);
        void UpdateModifiers();
    }
}