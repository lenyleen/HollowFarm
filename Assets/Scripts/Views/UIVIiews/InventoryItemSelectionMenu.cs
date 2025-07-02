using System;
using Cysharp.Threading.Tasks;
using DefaultNamespace.DataObjects;
using DefaultNamespace.ScriptableObjects;
using DG.Tweening;
using Service.UI;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using DefaultNamespace.Views.UIVIiews.Abstractions;
using DefaultNamespace.Views.UIVIiews.Interfaces;
using Service;

namespace DefaultNamespace.Views.UIVIiews
{
    
    public class InventoryItemSelectionMenu : MovableUiElement, IInventoryItemSelectionMenu
    {
        [filed:SerializeField] public UiType UiType { get; private set; }
        [SerializeField] private RectTransform _seedButtonContainer;

        private bool _isShowing;
        
        public async void Show()
        {
            CancelMove();
            _isShowing = true;
            gameObject.SetActive(true);
            try
            {
                await MoveTo(_startPosition, _cts.Token);
            }
            catch (OperationCanceledException) { }
        }
        
        public async void Hide()
        {
            CancelMove();
            _isShowing = false;
            try
            {
                await MoveTo(_endPosition, _cts.Token);
            }
            catch (OperationCanceledException) { }
            if (!_isShowing) 
                gameObject.SetActive(false);
        }
        public void ShowButton(InventoryItemButton _button) 
        {
            _button.transform.SetParent(_seedButtonContainer, false);
            _button.transform.localScale = Vector3.one;
            _button.transform.localPosition = Vector3.zero;

            _button.gameObject.SetActive(true);
        }
    }
}

