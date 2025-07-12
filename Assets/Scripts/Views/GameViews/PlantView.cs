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
using Views.GameViews.OutlineManager;
using Zenject;

namespace DefaultNamespace.Views
{
    public class PlantView : MonoBehaviour
    {
        [field: SerializeField] public PlantUiView IconView { get; private set; }
        private SpriteRenderer _spriteRenderer;
        private Light2D _light2D;
        private CompositeDisposable _disposable;
        private IOutlineManager _outlineManager;
        
        public void Initialize(PlantViewModel viewModel,Vector3 position, 
            Color lightColor, Sprite sprite, IOutlineManager outlineManager)
        {
            _disposable =  new CompositeDisposable();
            
            transform.position = position;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _light2D = GetComponent<Light2D>();
            
            ChangeSprite(sprite);
            _light2D.color = lightColor;

            viewModel.IsHovered.Subscribe(Hover).AddTo(_disposable);
            viewModel.GrowthStage.Subscribe(ChangeSprite).AddTo(_disposable);

            viewModel.StatusIcons.ObserveAdd().Subscribe(status => 
                IconView.ShowIcon(status.Value))
                .AddTo(_disposable);
            
            viewModel.StatusIcons.ObserveRemove().Subscribe(status => 
                    IconView.HideIcon(status.Value))
                .AddTo(_disposable);

            viewModel.BuffColor.Subscribe(ChangeOutline)
                .AddTo(_disposable);
            
            _outlineManager =  outlineManager;
            _outlineManager.RegisterRenderer(_spriteRenderer);
            _outlineManager.SetObjectOutline(_spriteRenderer,Color.clear);
        }

        private void ChangeSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
            var height = _spriteRenderer.sprite.bounds.size.y;
            IconView.SpriteChanged(height);
        }

        private void Hover(bool hover)
        {
            var direction = hover ? 1 : -1;
            transform.DOMoveY(transform.position.y + 0.3f * direction, .01f);
        }

        private void ChangeOutline(Color color)
        {
            _outlineManager.SetObjectOutline(_spriteRenderer,color);
        }

        private void Despawn()
        {
            transform.position = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            gameObject.SetActive(false);
            _spriteRenderer.sprite = null;
            _outlineManager.SetObjectOutline(_spriteRenderer, Color.clear);
            _disposable.Dispose();
        }
        
        private void OnDestroy()
        {
            _outlineManager.UnregisterRenderer(_spriteRenderer);
            if(_disposable == null) return;
            _disposable.Dispose();
        }
        
        public class Pool : MonoMemoryPool<PlantViewModel,Vector3, Color, Sprite, IOutlineManager,PlantView>
        {
            protected override void Reinitialize(PlantViewModel p1,Vector3 postion, Color p2, Sprite p3, 
                IOutlineManager outlineManager, PlantView item)
            {
                item.Initialize(p1,postion,p2,p3,outlineManager);
            }

            protected override void OnDespawned(PlantView item)
            {
                item.Despawn();
            }
        }
        
    }
}