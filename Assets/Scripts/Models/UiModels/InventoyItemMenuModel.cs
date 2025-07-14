using System;
using System.Collections.Generic;
using DefaultNamespace.DataObjects;
using DefaultNamespace.InventorySystem.Interfaces;
using DefaultNamespace.ScriptableObjects;
using UniRx;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.Models.UiModels
{
    public class InventoyItemMenuModel<T> : IInitializable, IDisposable where T : ICanBeDrawnInInventory
    {
        private readonly IInventoryService _playerInventory;
        private readonly CompositeDisposable _disposables = new ();
        public ReactiveProperty<List<InventoryItem>> Items { get; }
        
        
        public InventoyItemMenuModel(IInventoryService playerInventory)
        {
            Items = new ReactiveProperty<List<InventoryItem>>();
            _playerInventory = playerInventory;
        }
        
        public void Initialize()
        {
            _playerInventory.Items.Subscribe(items =>  Items.Value = _playerInventory.GetItemsOfType<T>())
                .AddTo(_disposables);
        }

        public void GetAvialableItems()
        {
            var itemsData = _playerInventory.GetItemsOfType<T>();
            
            Items.Value = itemsData ?? new List<InventoryItem>();
        }
        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}


