using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Consumable", menuName = "ScriptableObjects/Consumable")]
    public class ConsumableData : ScriptableObject, ICanBeDrawnInInventory
    {
        [Header("Основные параметры")]
        [SerializeField] private string boosterName;
        [SerializeField] private string description;
        [SerializeField] private Sprite icon;
        [SerializeField] private float duration = 60f;
        
        [Header("Модификаторы")]
        [SerializeField] private List<ModifierData> modifiers = new List<ModifierData>();

        public string BoosterName => boosterName;
        public string Description => description;
        public Sprite Icon => icon;
        public float Duration => duration;
        public IReadOnlyList<ModifierData> Modifiers => modifiers.AsReadOnly();
    }

    
    
    public enum ModifierType
    {
        GrowthSpeed,
        WaterRetention,
        HarvestMultiplier
    }
    
}