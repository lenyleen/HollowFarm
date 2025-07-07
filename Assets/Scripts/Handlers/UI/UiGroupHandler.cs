using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.ViewModels.UiVm;
using Service;

namespace DefaultNamespace.Handlers
{
    public abstract class UiGroupHandler : IUiGroupHandler
    {
        public UiType GroupType { get; }
        public bool IsFilled { get; protected set; } // TODO: занимаюсь пересборкой юи, группы харнятся в стеке на ию сревисе и работают дальше саами, сечас нужно добавить логику добавления в юи сервис
        private readonly Dictionary<Type, IUiElement> _uiElements;
        protected readonly Stack<IUiElement> ShowedUiElements;

        protected UiGroupHandler(List<IUiElement> uiElements, UiType type)
        {
            _uiElements = uiElements.ToDictionary(element => element.GetType());
            ShowedUiElements = new Stack<IUiElement>();
            GroupType = type;
        }
        public void Show<TUi, TParams>(TParams args) where TUi : IUiElement<TParams>
        {
            if (!_uiElements.TryGetValue(typeof(TUi), out var ui)) return;

            if(ShowedUiElements.Contains(ui)) return;
            
            if (args == null)
                ((TUi)ui).Show();
            else 
                ((TUi)ui).Show(args);
            
            IsFilled = true;
            
            ShowedUiElements.Push(ui);
        }
        
        public TUi ShowDialogMenu<TUi, TParams, TResult>(TParams args) where TUi : IDialogMenu<TParams, TResult>
        {
            if (!_uiElements.TryGetValue(typeof(TUi), out var ui)) return default;

            if(ShowedUiElements.Contains(ui)) return (TUi)ui;
            
            ((TUi)ui).Show(Close,args);
            
            ShowedUiElements.Push(ui);
            
            return (TUi)ui;
        }

        public abstract void Close();
    }
}