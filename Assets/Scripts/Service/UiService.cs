using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.ScriptableObjects;
using DefaultNamespace.Signals;
using DefaultNamespace.Signals.UiSignalsParams;
using DefaultNamespace.ViewModels.UiVm;
using UniRx;
using Zenject;

namespace Service
{
    public class UiService : IUiService, IClickBlockerService, IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly Dictionary<Type, IUiElement> _uiElements;
        private readonly Stack<IUiElement> _showedUiElements;
        
        public ReactiveProperty<bool> IsUiClosingClicksEnabled { get; } = new ReactiveProperty<bool>(false);

        public UiService(SignalBus signalBus, List<IUiElement> uiElements)
        {
            _signalBus = signalBus;
            _uiElements = uiElements.ToDictionary(elem => elem.GetType(), elem => elem);
            _showedUiElements = new Stack<IUiElement>();
        }

        public void Initialize()
        {
            _signalBus.Subscribe<ClosePopUpRequestSignal>(CloseOne);
        }

        private void EnableClosingUiClicks(bool enable)
        {
            if(IsUiClosingClicksEnabled.Value == enable)
                return;
            
            IsUiClosingClicksEnabled.Value = enable;
        }
        
        public void Show<TUi, TParams>(TParams args) where TUi : IUiElement<TParams>
        {
            if (!_uiElements.TryGetValue(typeof(TUi), out var ui)) return;

            if(_showedUiElements.Contains(ui)) return;
            
            if (args == null)
                ((TUi)ui).Show();
            else 
                ((TUi)ui).Show(args);
            
            _showedUiElements.Push(ui);
            
            EnableClosingUiClicks(true);
        }
        
        public TUi ShowDialogMenu<TUi, TParams, TResult>(TParams args) where TUi : IDialogMenu<TParams, TResult>
        {
            if (!_uiElements.TryGetValue(typeof(TUi), out var ui)) return default;

            if(_showedUiElements.Contains(ui)) return (TUi)ui;
            
            ((TUi)ui).Show(CloseOne,args);
            
            _showedUiElements.Push(ui);
            
            EnableClosingUiClicks(true);
            return (TUi)ui;
        }
        
        public void CloseOne()
        {
            if(!_showedUiElements.Any()) return;
            
            var popUp = _showedUiElements.Pop();
            popUp.Hide();

            if (_showedUiElements.Any())
                return;
            
            EnableClosingUiClicks(false);
        }

        public void CloseAll()
        {
            foreach (var showedUiElement in _showedUiElements)
            {
                showedUiElement.Hide();
            }
            _showedUiElements.Clear();
            
            EnableClosingUiClicks(false);
        }
        

        public void Dispose()
        {
            _signalBus.Unsubscribe<ClosePopUpRequestSignal>(CloseOne);
        }
    }
}