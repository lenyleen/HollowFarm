using DefaultNamespace.Boosters.Interfaces;
using DefaultNamespace.Models;
using DefaultNamespace.ScriptableObjects;
using UnityEngine;

namespace DefaultNamespace.Boosters
{
    public abstract class PlantModifier : IPlantModifier
    {
        protected readonly string _name;
        protected readonly float _duration;
        protected readonly float _value;
        protected readonly Color _color;
        
        protected bool _isActive;

        public string Name => _name;
        public float Duration => _duration;
        public bool IsActive => _isActive;
        
        protected PlantModifier(ModifierData data)
        {
            _name = data.Type.ToString();
            _duration = data.Value;
            _value = data.Value;
            _color = data.Color;
            _remainingTime = _duration;
            _isActive = false;
        }
        
        public void Apply(PlantModel plant)
        {
            
        }

        public void Update(float deltaTime)
        {
            
        }

        public void Remove(PlantModel plant)
        {
            
        }

        public abstract float GetModifierValue();
        
        protected abstract void OnApply(PlantModel plant);
        protected abstract void OnRemove(PlantModel plant);
    }
}