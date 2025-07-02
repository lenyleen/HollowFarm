using System;
using DefaultNamespace.ViewModels.UiVm;
using Zenject;

namespace Service
{
    public interface IUiService
    {
        public void Show<TUi, TParams>(TParams args) where TUi : IUiElement<TParams>;
        public TUi ShowDialogMenu<TUi, TParams, TResult>(TParams args) where TUi : IDialogMenu<TParams, TResult>;
        public void Close();
        
    }
}