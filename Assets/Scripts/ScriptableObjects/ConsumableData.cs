using UnityEngine;

namespace DefaultNamespace.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Consumable", menuName = "ScriptableObjects/Consumable")]
    public class ConsumableData : ScriptableObject, ICanBeDrawnInInventory
    {
        public float BoostDurationInPercent;
        public float BoostTimeInSeconds;
    }
}