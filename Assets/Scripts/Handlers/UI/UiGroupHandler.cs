using System;
using System.Collections.Generic;
using DefaultNamespace.ViewModels.UiVm;
using Service;

namespace DefaultNamespace.Handlers
{
    public abstract class UiGroupHandler : IUiGroupHandler
    {
        public UiType GroupType { get; }
        public bool IsFilled { get; } // TODO: занимаюсь пересборкой юи, группы харнятся в стеке на ию сревисе и работают дальше саами, сечас нужно добавить логику добавления в юи сервис
        protected readonly Dictionary<Type, IUiElement> UIElements;
        protected readonly Stack<IUiElement> ShowedUiElements;

        protected UiGroupHandler(Dictionary<Type, IUiElement> uiElements, Stack<IUiElement> showedUiElements, UiType type)
        {
            UIElements = uiElements;
            ShowedUiElements = showedUiElements;
            GroupType = type;
        }
        public void Show<TUi, TParams>(TParams args) where TUi : IUiElement<TParams>
        {
            if (!UIElements.TryGetValue(typeof(TUi), out var ui)) return;

            if(ShowedUiElements.Contains(ui)) return;
            
            if (args == null)
                ((TUi)ui).Show();
            else 
                ((TUi)ui).Show(args);
            
            ShowedUiElements.Push(ui);
        }
        
        public TUi ShowDialogMenu<TUi, TParams, TResult>(TParams args) where TUi : IDialogMenu<TParams, TResult>
        {
            if (!UIElements.TryGetValue(typeof(TUi), out var ui)) return default;

            if(ShowedUiElements.Contains(ui)) return (TUi)ui;
            
            ((TUi)ui).Show(Close,args);
            
            ShowedUiElements.Push(ui);
            
            return (TUi)ui;
        }

        public abstract void Close();
    }
}