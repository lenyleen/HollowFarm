using DefaultNamespace.Models;
using DefaultNamespace.ScriptableObjects;
using UnityEngine;

namespace DefaultNamespace.Signals
{
    public class PlantHarvestedSignal
    {
        public PlantData PlantData { get; }
        public int Amount { get; }

        public PlantHarvestedSignal(PlantData plantData, int amount)
        {
            PlantData = plantData;
            Amount = amount;
            Debug.Log($"Собрано {Amount} | {plantData.name}");
        }
    }
}