using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Handlers;
using DefaultNamespace.ScriptableObjects;
using DefaultNamespace.Signals;
using DefaultNamespace.Signals.UiSignalsParams;
using DefaultNamespace.ViewModels.UiVm;
using UniRx;
using Zenject;

namespace Service
{
    public class UiService : IUiGroupService, IClickBlockerService, IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly Dictionary<UiType, IUiGroupHandler> _uiGroups;
        private readonly Stack<IUiGroupHandler> _showedGroups;
        private IUiGroupHandler _currentGroup;
        
        public ReactiveProperty<bool> IsUiClosingClicksEnabled { get; } = new ReactiveProperty<bool>(false);

        public UiService(SignalBus signalBus, List<IUiGroupHandler> uiGroups)
        {
            _signalBus = signalBus;
            _uiGroups = uiGroups.ToDictionary(elem => elem.GroupType, elem => elem);
            _showedGroups = new Stack<IUiGroupHandler>();
        }

        public void Initialize()
        {
            _signalBus.Subscribe<ClosePopUpRequestSignal>(Close);
        }

        private void EnableClosingUiClicks(bool enable)
        {
            if(IsUiClosingClicksEnabled.Value == enable)
                return;
            
            IsUiClosingClicksEnabled.Value = enable;
        }
        
        public void Show<TUi, TParams>(TParams args, UiType type) where TUi : IUiElement<TParams>
        {
           if(!_uiGroups.TryGetValue(type, out var group))
               throw new Exception("Group not found");
           
           group.Show<TUi, TParams>(args);
           
           EnableClosingUiClicks(true);
           
           if(!_showedGroups.Contains(group))
            _showedGroups.Push(group);
        }
        
        public TUi ShowDialogMenu<TUi, TParams, TResult>(TParams args, UiType type) where TUi : IDialogMenu<TParams, TResult>
        {
            if(!_uiGroups.TryGetValue(type, out var group))
                throw new Exception("Group not found");
            
            var dialog = group.ShowDialogMenu<TUi, TParams,TResult>(args);
            
            if(!_showedGroups.Contains(group))
                _showedGroups.Push(group);
            EnableClosingUiClicks(true); 
            
            return dialog;
        }
        
        public void Close()
        {
            if (_currentGroup == null)
            {
                if (!_showedGroups.TryPop(out var result))
                    return;
                _currentGroup = result;
            }
            
            _currentGroup.Close();
            
            if(_currentGroup.IsFilled) return;

            EnableClosingUiClicks(false);
            _currentGroup = null;   
        }
        
        public void Dispose()
        {
            _signalBus.Unsubscribe<ClosePopUpRequestSignal>(Close);
        }

        public void RegisterGroup(IUiService group, UiType type)
        {
            throw new NotImplementedException();
        }

        public void UnregisterGroup(IUiService group, UiType type)
        {
            throw new NotImplementedException();
        }
    }
}