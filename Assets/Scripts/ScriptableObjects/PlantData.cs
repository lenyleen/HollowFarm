using System;
using System.Collections.Generic;
using DefaultNamespace.ScriptableObjects.Service;
using DefaultNamespace.Service;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

namespace DefaultNamespace.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Plant", menuName = "ScriptableObjects/Plant")]
    [System.Serializable]
    public class PlantData : ScriptableObject, ICanBeDrawnInInventory
    {
        public string Name => name.ToString();
        public string Description => _desctiption;
        public Sprite Icon => _inventoryIcon;

        
        [Header("Параметры")]
        public Plants name;
        public string _desctiption;
        public GrowthStates startGrowthState;
        public float growthDurationInMinutes;
        public DateTime belongsToMoonPhase;
        public float byMoonGrowthMultiplier;
        public float byWaterGrowthMultiplier;
        public List<Sprite> SpritesByPhase;
        public List<Sprite> DiedSpritesByPhase;
        public Sprite _inventoryIcon;
        public Color LightColor;
        public int Amount;
    }
}

//name
//Desctiption
//Icon
