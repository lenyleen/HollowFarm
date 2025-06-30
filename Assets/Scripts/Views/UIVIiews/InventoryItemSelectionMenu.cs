using System;
using Cysharp.Threading.Tasks;
using DefaultNamespace.DataObjects;
using DefaultNamespace.ScriptableObjects;
using DG.Tweening;
using Service.UI;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using DefaultNamespace.Views.UIVIiews.Interfaces;

namespace DefaultNamespace.Views.UIVIiews
{
    
    public class InventoryItemSelectionMenu : MonoBehaviour, IInventoryItemSelectionMenu
    {
        [SerializeField] private RectTransform _contentPanel;
        [SerializeField] private RectTransform _seedButtonContainer;
        [SerializeField] private float _topPosition;
        [SerializeField] private float _bottomPosition;

        private CancellationTokenSource _cts;
        private bool _isShowing;

        public async void Show()
        {
            CancelMove();
            _isShowing = true;
            gameObject.SetActive(true);
            try
            {
                await MoveTo(_topPosition, _cts.Token);
            }
            catch (OperationCanceledException) { }
        }
        
        public async void Hide()
        {
            CancelMove();
            _isShowing = false;
            try
            {
                await MoveTo(_bottomPosition, _cts.Token);
            }
            catch (OperationCanceledException) { }
            if (!_isShowing) 
                gameObject.SetActive(false);
        }

        private async UniTask MoveTo(float position, CancellationToken token)
        {
            var tcs = new UniTaskCompletionSource();
            var tween = _contentPanel.DOAnchorPosY(position, 0.5f)
                .SetEase(Ease.OutBack)
                .OnComplete(() => tcs.TrySetResult());
            
            await using (token.Register(() => {
                             tween.Kill();
                             tcs.TrySetCanceled(token);
                         }))
            {
                await tcs.Task;
            }
        }

        private void CancelMove()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = new CancellationTokenSource();
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

