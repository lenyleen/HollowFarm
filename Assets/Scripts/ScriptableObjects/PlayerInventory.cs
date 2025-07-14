using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.DataObjects;
using Sirenix.Serialization;
using UnityEngine;

namespace DefaultNamespace.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerInventory", menuName = "ScriptableObjects/PlayerInventory", order = 1)]
    public class PlayerInventory : ScriptableObject
    {
        [SerializeField]private List<InventoryItem> _items;

        public List<InventoryItem> GetItemWithDataOfType<T>() where T : ICanBeDrawnInInventory
        {
            if (_items == null)
            {
                Debug.LogWarning("Inventory items are not initialized.");
                return new List<InventoryItem>();
            }
            return _items.Where(item => item.Data is T).ToList();
        }

        public List<InventoryItem> GetAllItems => _items ?? new List<InventoryItem>();
        
        public void RemoveItem(InventoryItem item)
        {
            if(!_items.Any())
                return;
            
            if(!_items.Contains(item))
                return;
            
            _items.Remove(item);
        }

        public void AddItem(InventoryItem item) =>_items.Add(item);
    }
}