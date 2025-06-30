using System;
using DefaultNamespace.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace.DataObjects
{
    [System.Serializable]
    public class InventoryItem
    {
        [SerializeField] private Sprite icon;
        [SerializeField] private string name;
        [SerializeField] private string description;
        [SerializeField] private int quantity;
        [FormerlySerializedAs("_data")] [SerializeField] private ScriptableObject data;

        public Sprite Icon => icon;
        public string Name => name;
        public string Description => description;
        public int Quantity => quantity;
        public ICanBeDrawnInInventory Data => data as ICanBeDrawnInInventory;
        
        public bool TryGetData<T>(out T data) where T : ICanBeDrawnInInventory
        {
            data = default(T);
        
            if (this.data is not T tData) return false;
            
            data = tData;
            return true;
        }
        
        public void AddQuantity(int quantity)
        {
            if (this.quantity >= 99 || quantity <= 0) 
                return;
            this.quantity += quantity;
        }

        public void RemoveQuantity(int quantity)
        {
            if(this.quantity <= 0 || quantity <= 0) 
                return;
            this.quantity -= quantity;
        }
    }
}

