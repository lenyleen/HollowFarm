using System;
using DefaultNamespace.ViewModels.UiVm;
using Service;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace DefaultNamespace.Views.UIVIiews.Service
{
    [RequireComponent(typeof(Image)), RequireComponent(typeof(Button))] 
    public class SoilActionButton : MonoBehaviour, ISoilActionButton
    {
        [SerializeField] private readonly Image _image;
        [SerializeField] private readonly Button _button;

        private SoilActionType _actionType;
        private readonly Subject<SoilActionType> _onClickSubject = new();

        public IObservable<SoilActionType> OnClickAsObservable => _onClickSubject;

        private void Awake()
        {
            _button.onClick.AddListener(() => _onClickSubject.OnNext(_actionType));
        }

        public void Initialize(SoilActionType actionType)
        {
            _actionType = actionType;
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
            _onClickSubject?.Dispose();
        }
    }
}