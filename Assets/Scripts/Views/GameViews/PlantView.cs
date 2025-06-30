using System;
using System.Collections.Generic;
using DefaultNamespace.ScriptableObjects;
using DefaultNamespace.ViewModels;
using DefaultNamespace.Views.UIVIiews;
using DG.Tweening;
using Service;
using UnityEngine;
using UniRx;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace DefaultNamespace.Views
{
    public class PlantView : MonoBehaviour
    {
        [SerializeField] private PlantUiView _iconView;
        private SpriteRenderer _spriteRenderer;
        private Light2D _light2D;
        private readonly CompositeDisposable _disposable = new();

        public void Initialize(PlantViewModel viewModel, Color lightColor, Sprite sprite, Dictionary<PlantStatus, Image> imageDict)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _light2D = GetComponent<Light2D>();
            
            ChangeSprite(sprite);
            _light2D.color = lightColor;

            viewModel.IsHovered.Subscribe(Hover).AddTo(_disposable);
            viewModel.GrowthStage.Subscribe(ChangeSprite).AddTo(_disposable);
            
            _spriteRenderer.sortingOrder = (int)-transform.position.y;
            var height = _spriteRenderer.sprite.bounds.size.y;
            _iconView.Initialize(imageDict,height,_spriteRenderer.sortingOrder);
            
            viewModel.StatusIcons.ObserveAdd().Subscribe(status => 
                _iconView.ShowIcon(status.Value))
                .AddTo(_disposable);
            
            viewModel.StatusIcons.ObserveRemove().Subscribe(status => 
                    _iconView.HideIcon(status.Value))
                .AddTo(_disposable);
            
            
        }

        private void ChangeSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
            var height = _spriteRenderer.sprite.bounds.size.y;
            _iconView.SpriteChanged(height);
        }

        private void Hover(bool hover)
        {
            var direction = hover ? 1 : -1;
            transform.DOMoveY(transform.position.y + 0.3f * direction, .01f);
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}