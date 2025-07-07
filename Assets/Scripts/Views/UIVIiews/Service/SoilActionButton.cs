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
        [SerializeField] private Image _image;
        [SerializeField] private Button _button;

        private SoilActionType _actionType;
        private readonly Subject<SoilActionType> _onClickSubject = new();

        public IObservable<SoilActionType> OnClickAsObservable => _onClickSubject;

        public void Initialize(Sprite sprite, RectTransform parentTransform, SoilActionType actionType) 
        {
            _button.onClick.AddListener(() => _onClickSubject.OnNext(_actionType));
            
            transform.SetParent(parentTransform);
            transform.localScale = Vector3.one;
            _actionType = actionType;
            _image.sprite = sprite;
            _image.SetNativeSize();
        }
        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
            _onClickSubject?.Dispose();
        }
    }
}