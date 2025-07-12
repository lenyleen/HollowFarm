using System;
using System.Collections.Generic;
using DefaultNamespace.Boosters.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Consumable", menuName = "ScriptableObjects/Consumable")]
    public class ConsumableData : ScriptableObject, ICanBeDrawnInInventory
    {
        [Header("Основные параметры")]
        [SerializeField] private string consumableName;
        [SerializeField] private string description;
        [SerializeField] private Sprite icon;
        [SerializeField] private float duration = 60f;
        [SerializeField] [Range(1, 3)] private int _rating;
        [SerializeField] private bool _isRatingShown;
        
        [Header("Модификаторы")]
        [SerializeField] private List<PlantModifierData> modifiers = new();

        private void Awake()
        {
            foreach (var modifier in modifiers)
                modifier.Duration = duration;
        }

        public string ConsumableName => consumableName;
        public string Description => description;
        public Sprite Icon => icon;
        public float Duration => duration;
        public int Rating => _isRatingShown ? _rating : 0;
        public IReadOnlyList<PlantModifierData> Modifiers => modifiers.AsReadOnly();
    }

    
    
    public enum ModifierType
    {
        GrowthSpeed,
        WaterRetention,
        HarvestMultiplier
    }
    
}