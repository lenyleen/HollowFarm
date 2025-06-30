using System;
using Cysharp.Threading.Tasks;
using DefaultNamespace.Signals.UiSignalsParams;
using DefaultNamespace.ViewModels.UiVm;
using Service;
using UniRx;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;
using Vector3 = System.Numerics.Vector3;

namespace DefaultNamespace.Views.UIVIiews
{
    public class SoilFilterDialogMenu : MonoBehaviour, IDialogMenu<SoilFilterParams,SoilFilter>
    {
        [SerializeField] private UnityEngine.UI.Button _emptyButton;
        [SerializeField] private UnityEngine.UI.Button _occupiedButton;
        private Action _onSelected;
        private UniTaskCompletionSource<SoilFilter> _tcs;
        

        private void Awake()
        {
            _emptyButton.onClick.AddListener(() => Complete(SoilFilter.Empty));
            _occupiedButton.onClick.AddListener(() => Complete(SoilFilter.Occupied));
            gameObject.SetActive(false); 
        }
        
        public void Show(Action onSelected, SoilFilterParams param)
        {
            transform.position = param.Position;
            gameObject.SetActive(true);
            _tcs = new UniTaskCompletionSource<SoilFilter>();
            _onSelected = onSelected;
        }

        public void Complete(SoilFilter result)
        {
            _tcs.TrySetResult(result);
            _onSelected?.Invoke();
        }

        public UniTask<SoilFilter> WaitForResultAsync() => _tcs.Task;

       
        public void Show() {}

        public void Hide()
        {
            _onSelected = null;
            _tcs = null;
            transform.position = new UnityEngine.Vector3(-222, -222, -222);
            gameObject.SetActive(false); 
        }
    }
}