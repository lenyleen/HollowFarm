using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.DataObjects;
using DefaultNamespace.InventorySystem.Interfaces;
using DefaultNamespace.ScriptableObjects;
using DefaultNamespace.Signals;
using UniRx;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.InventorySystem
{
    public class InventoryService : IInventoryService, IInitializable, IDisposable
    {
        private readonly PlayerInventory _playerInventory;
        private readonly SignalBus _signalBus;
        
        public ReactiveProperty<List<InventoryItem>> Items { get; private set; }

        public InventoryService(PlayerInventory playerInventory, SignalBus signalBus)
        {
            _playerInventory = playerInventory;
            _signalBus = signalBus;
            Items = new ReactiveProperty<List<InventoryItem>>();
            RefreshItems();
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<ItemAddSignal<PlantData>>(OnItemAdded);
            _signalBus.Subscribe<ItemAddSignal<ConsumableData>>(OnItemAdded);
            
            _signalBus.Subscribe<ItemUsedSignal<PlantData>>(OnItemUsed);
            _signalBus.Subscribe<ItemUsedSignal<ConsumableData>>(OnItemUsed);
        }
        
        private void OnItemAdded<T>(ItemAddSignal<T> signal) where T : ICanBeDrawnInInventory
        {
            var itemsOfType = Items.Value.Where(item => item.Data.GetType() ==  typeof(T));

            if (!itemsOfType.Any())
            {
                AddNewItem(signal.ItemData,signal.Amount);
                RefreshItems();
                return;
            }
            
            var itemToAdd = itemsOfType.First(item => item.Data ==  signal.ItemData as ICanBeDrawnInInventory);
            itemToAdd.AddQuantity(signal.Amount);
        }

        private void AddNewItem<T>(T data, int amount) where T : ICanBeDrawnInInventory
        {
            var newItem = new InventoryItem(data.Icon,data.Name,data.Description,
                amount,data as ScriptableObject);
            
            _playerInventory.AddItem(newItem);
        }

        private void OnItemUsed<T>(ItemUsedSignal<T> signal) where T : ICanBeDrawnInInventory
        {
            signal.Seed.RemoveQuantity(signal.GetItemAmount());
            if(signal.Seed.Quantity >= 0)
                return;
            
            _playerInventory.RemoveItem(signal.Seed);
            RefreshItems();
        }
        

        public List<InventoryItem> GetItemsOfType<T>() where T : ICanBeDrawnInInventory  => _playerInventory.GetItemWithDataOfType<T>();
        
        private void RefreshItems()
        {
            var allItems = _playerInventory.GetAllItems;

            Items.Value = allItems;
        }
        
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}