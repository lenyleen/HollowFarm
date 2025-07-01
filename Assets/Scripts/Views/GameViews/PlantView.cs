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
using Views.GameViews;
using Zenject;

namespace DefaultNamespace.Views
{
    public class PlantView : MonoBehaviour
    {
        [SerializeField] private PlantUiView _iconView;
        private SpriteRenderer _spriteRenderer;
        private Light2D _light2D;
        private CompositeDisposable _disposable;
        
        public void Initialize(PlantViewModel viewModel,Vector3 position, 
            Color lightColor, Sprite sprite, Dictionary<PlantStatus, Image> imageDict)
        {
            _disposable =  new CompositeDisposable();
            
            transform.position = position;
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

        private void Despawn()
        {
            transform.position = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            gameObject.SetActive(false);
            _spriteRenderer.sprite = null;
            _disposable.Dispose();
        }
        
        private void OnDestroy()
        {
            _disposable.Dispose();
        }
        
        public class Pool : MonoMemoryPool<PlantViewModel,Vector3, Color, Sprite, Dictionary<PlantStatus,Image>,PlantView>
        {
            protected override void Reinitialize(PlantViewModel p1,Vector3 postion, Color p2, Sprite p3, 
                Dictionary<PlantStatus, Image> p4, PlantView item)
            {
                item.Initialize(p1,postion,p2,p3,p4);
            }

            protected override void OnDespawned(PlantView item)
            {
                item.Despawn();
            }
        }
        
    }
}