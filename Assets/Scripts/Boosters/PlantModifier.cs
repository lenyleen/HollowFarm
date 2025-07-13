using System;
using DefaultNamespace.Boosters.Interfaces;
using DefaultNamespace.Boosters.ScriptableObjects;
using DefaultNamespace.Models;
using DefaultNamespace.ScriptableObjects;
using UnityEngine;

namespace DefaultNamespace.Boosters
{
    public class PlantModifier : IPlantModifier
    {
        private readonly string _name;
        private readonly float _duration;
        private readonly Color _color;
        private readonly float _value;
        
        private bool _isActive;
        private TimeSpan _remainingTime;
        
        public string Name => _name;
        public bool IsActive => _isActive;
        public Color Color => _color;
        public float Value => _value;
        
        public TimeSpan RemainingTime => _remainingTime;

        public PlantModifier(PlantModifierData data)
        {
            _name = data.Name;
            _duration = data.Duration;
            _value = data.Value;
            _color = data.Color;
            _isActive = false;
            _remainingTime = TimeSpan.FromSeconds(_duration);
        } 
        public void Update(float deltaTime)
        {
            _remainingTime -= TimeSpan.FromSeconds(deltaTime);
        }

        public bool IsGreater(PlantModifierData modifierData)
        {
            var utility = CalculateUtility();
            var newUtility = modifierData.Duration / modifierData.Value;

            return newUtility >= utility;
        }
        
        public float CalculateUtility()
        {
            var timeFactor = (float)(RemainingTime.TotalSeconds / _duration);
            
            return Value * timeFactor;
        }
    }
}