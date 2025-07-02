using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.DataObjects;
using DefaultNamespace.Factory;
using DefaultNamespace.Models;
using DefaultNamespace.Models.Interfaces;
using DefaultNamespace.Models.UiModels;
using DefaultNamespace.ScriptableObjects;
using DefaultNamespace.Signals;
using DefaultNamespace.Signals.UiSignalsParams;
using DefaultNamespace.ViewModels.Interfaces;
using DefaultNamespace.Views.UIVIiews;
using DefaultNamespace.Views.UIVIiews.Interfaces;
using Service;
using Service.UI;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace DefaultNamespace.ViewModels.UiVm
{
    public class InventoryItemsMenuVm<T> : IInitializable, IUiElement<SoilPopupParams>, IDisposable where T : ICanBeDrawnInInventory
    {
        public UiType UiType => _view.UiType;
        private readonly IInventoryItemSelectionMenu _view;
        private readonly InventoyItemMenuModel<T> _model;
        private readonly CompositeDisposable _disposables = new CompositeDisposable();
        private readonly Dictionary<InventoryItem,InventoryItemButton> _activeButtons = new();
        private readonly IFactory<InventoryItem, UnityAction, InventoryItemButton> _inventoryItemButtonFactory;
        private readonly ITileClickedPublisher _tileClickedPublisher;
        private readonly SignalBus _signalBus;
        private List<Vector3Int> _positions;
        
        protected InventoryItemsMenuVm(IInventoryItemSelectionMenu view, InventoyItemMenuModel<T> model, 
            InventoryItemButtonFactory<T> inventoryItemButtonFactory, SignalBus signalBus)
        {
            _view = view;
            _model = model;
            _inventoryItemButtonFactory = inventoryItemButtonFactory;
            _signalBus = signalBus;
        }
        
        public void Initialize()
        {
            _view.Hide();
            _model.Items
                .Subscribe(UpdateItemsButtons)
                .AddTo(_disposables);
            
            _model.GetAvialableItems();
        }
        public void Show(){}

        public void Show(SoilPopupParams @params)
        {
            _positions = @params.Positions;
            _view.Show();
        }
        
        private void UpdateItemsButtons(List<InventoryItem> items)
        {
            if(items == null)
                return;
            var toRemove = _activeButtons.Keys.Except(items).ToList();
            foreach (var item in toRemove)
            {
                _activeButtons[item].Dispose();
                _activeButtons.Remove(item);
            }
            
            foreach (var item in items)
            {
                if (_activeButtons.TryGetValue(item, out var button))
                {
                    button.UpdateQuantity(item.Quantity);
                }
                else
                {
                    var onClick = new UnityAction(() => {OnItemButtonClicked(item);});
                    var newButton = _inventoryItemButtonFactory.Create(item, onClick);
                    _view.ShowButton(newButton);
                    _activeButtons.Add(item, newButton);
                }
            }
        }

        public void Hide()
        {
            _view.Hide();
            _signalBus.Fire<SoilMenuClosed>();
        }
        
        private void OnItemButtonClicked(InventoryItem item)
        {
            if (_positions == null) return;
            var toUseItems = item.Quantity;

            if (toUseItems > _positions.Count)
                toUseItems = _positions.Count; 
                
            _model.ItemUsed(item, toUseItems);
            
            if (_activeButtons.TryGetValue(item, out var button))
                button.UpdateQuantity(-toUseItems);
            
            _signalBus.Fire(new ItemUsedSignal<T>{Seed = item, Positions =  _positions.GetRange(0, toUseItems)});
            _view.Hide();
            _positions = null;
        }
        
        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}