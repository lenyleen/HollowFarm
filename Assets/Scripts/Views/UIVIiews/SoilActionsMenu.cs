using System;
using System.Collections.Generic;
using DefaultNamespace.ViewModels.UiVm;
using DefaultNamespace.Views.UIVIiews.Abstractions;
using DefaultNamespace.Views.UIVIiews.Service;
using Service;
using UniRx;
using UnityEngine;

namespace DefaultNamespace.Views.UIVIiews
{
    public class SoilActionsMenu : MovableUiElement, IUiElement, IDisposable
    {
        public UiType UiType => _type;
        [SerializeField] private UiType _type;
        [field: SerializeField] public RectTransform GridRect { get; private set; }
        private readonly CompositeDisposable  _disposable = new();
        private List<ISoilActionButton> _buttons;
        private bool _isShowing;
        
        private readonly Subject<SoilActionType> _actionClickedSubject = new();
        public IObservable<SoilActionType> OnActionClicked => _actionClickedSubject;

        public void Initialize(List<ISoilActionButton> buttons)
        {
            _buttons = buttons;
            
            

            foreach (var btn in _buttons)
            {
                btn.OnClickAsObservable
                    .Subscribe(actionType => _actionClickedSubject.OnNext(actionType))
                    .AddTo(_disposable);
            }
        }

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

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}