
using DefaultNamespace.Models;
using UnityEngine;

namespace DefaultNamespace.Boosters.Interfaces
{
    public interface IPlantModifierWithValue<T> : IPlantModifier where T : struct
    {
        public T Value { get; }
    }
}