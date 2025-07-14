using DefaultNamespace.Models;
using DefaultNamespace.ScriptableObjects;
using UnityEngine;

namespace DefaultNamespace.Signals
{
    public class ItemAddSignal<T> where T : ICanBeDrawnInInventory
    {
        public T ItemData { get; }
        public int Amount { get; }

        public ItemAddSignal(T itemData, int amount)
        {
            ItemData= itemData;
            Amount = amount;
        }
    }
}