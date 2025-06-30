using System;
using DefaultNamespace.DataObjects;
using DefaultNamespace.ScriptableObjects;
using Service.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace DefaultNamespace.Factory
{
    public class InventoryItemButtonFactory<T> : IFactory<InventoryItem, UnityAction,InventoryItemButton> where T : ICanBeDrawnInInventory
    {
        private readonly DiContainer _container;
        private readonly InventoryItemButton _inventoryItemButtonPrefab;
        
        public InventoryItemButtonFactory(DiContainer container, InventoryItemButton inventoryItemButtonPrefab)
        {
            _container = container;
            _inventoryItemButtonPrefab = inventoryItemButtonPrefab;
        }
        
        public InventoryItemButton Create(InventoryItem state, UnityAction context)
        {
            if (!state.TryGetData<T>(out var itemData))
                return null;
            
            var seedButton =  _container.InstantiatePrefabForComponent<InventoryItemButton>(_inventoryItemButtonPrefab);
            seedButton.Initialize(itemData, state.Icon,state.Quantity, context);
            return seedButton;
        }
    }
}