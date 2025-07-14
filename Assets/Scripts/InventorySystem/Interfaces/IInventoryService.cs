using System.Collections.Generic;
using DefaultNamespace.DataObjects;
using DefaultNamespace.ScriptableObjects;
using NUnit.Framework;
using UniRx;

namespace DefaultNamespace.InventorySystem.Interfaces
{
    public interface IInventoryService
    {
        public ReactiveProperty<List<InventoryItem>> Items { get;}
        public List<InventoryItem> GetItemsOfType<T>() where T : ICanBeDrawnInInventory;
    }
}