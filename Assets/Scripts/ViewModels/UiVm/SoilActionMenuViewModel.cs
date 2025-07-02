using System;
using System.Collections.Generic;
using DefaultNamespace.Commands;
using DefaultNamespace.Factory;
using DefaultNamespace.ViewModels.Interfaces;
using DefaultNamespace.ViewModels.UiVm.Params;
using DefaultNamespace.Views.UIVIiews;
using Service;
using UniRx;
using Zenject;

namespace DefaultNamespace.ViewModels.UiVm
{
    public class SoilActionMenuViewModel :IDisposable, IUiElement<SoilActionMenuParams>
    {
        public UiType UiType => _menu.UiType;
        private readonly SoilActionsMenu _menu;
        private readonly SignalBus _signalBus;
        private readonly IFactory<IEnumerable<ICommandPerformer>, SoilActionType, ICommand<ICommandPerformer>> _commandFactory;
        private readonly CompositeDisposable _disposable = new();
        

        public SoilActionMenuViewModel(SoilActionsMenu menu, SignalBus signalBus,
            IFactory<IEnumerable<ICommandPerformer>, SoilActionType, ICommand<ICommandPerformer>>  commandFactory)
        {
            _menu = menu;
            _signalBus = signalBus;
            _commandFactory = commandFactory;
        }

        public void Show(SoilActionMenuParams param)
        {
            _menu.OnActionClicked
                .Subscribe(actionType =>
                {
                    var command = _commandFactory.Create(param.CommandPerformers, actionType);
                    command.Execute();
                })
                .AddTo(_disposable);
               
            _menu.Show();
        }

        public void Show()
        { }

        public void Hide()
        {
            _menu.Hide();  
            _disposable.Dispose();
        }
        
        public void Dispose()
        {
            _disposable.Dispose();
        }

    }
}