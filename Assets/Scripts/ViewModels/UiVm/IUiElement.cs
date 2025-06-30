using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Service;
using UniRx;
using UnityEngine;

namespace DefaultNamespace.ViewModels.UiVm
{
    public interface IUiElement
    {
        public void Show();
        public void Hide();
    }

    public interface IUiElement<TParams> : IUiElement
    {
        public void Show(TParams param);
    }
    
    public interface IDialogMenu<TParams, TResult> : IUiElement
    {
        public void Complete(TResult result);
        public UniTask<TResult> WaitForResultAsync();
        public void Show(Action onSelected, TParams param);
    }
}