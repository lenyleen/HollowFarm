using System.Collections.Generic;
using DefaultNamespace.DataObjects;
using DefaultNamespace.ScriptableObjects;
using UniRx;
using UnityEngine;

namespace DefaultNamespace.Models.UiModels
{
    public class InventoyItemMenuModel<T> where T : ICanBeDrawnInInventory
    {
        private readonly PlayerInventory _playerInventory;

        public ReactiveProperty<List<InventoryItem>> Items { get; }


        public InventoyItemMenuModel(PlayerInventory playerInventory)
        {
            Items = new ReactiveProperty<List<InventoryItem>>();
            _playerInventory = playerInventory;
        }

        public void GetAvialableItems()
        {
            var itemsData = _playerInventory.GetItemWithDataOfType<T>();
            if (itemsData != null)
                Items.Value = itemsData;
            else
                Items.Value = new List<InventoryItem>();
        }

        public void ItemUsed(InventoryItem item, int amount)
        {
            if (item.Quantity < amount)
                return;

            item.RemoveQuantity(amount);
            _playerInventory.RemoveItem(item, amount);
        }
    }
}


