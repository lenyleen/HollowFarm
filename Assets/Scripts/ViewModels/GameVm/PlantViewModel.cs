using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Boosters.Interfaces;
using DefaultNamespace.Boosters.ScriptableObjects;
using DefaultNamespace.Models;
using DefaultNamespace.ScriptableObjects;
using DefaultNamespace.Signals;
using DefaultNamespace.Statics;
using DefaultNamespace.ViewModels.Interfaces;
using DefaultNamespace.Views;
using DefaultNamespace.Views.UIVIiews;
using Service;
using UniRx;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.ViewModels
{
    public class PlantViewModel : IInitializable,IDisposable
    {
        private readonly CompositeDisposable _disposable = new();
        private readonly PlantModel _model;
        private readonly SignalBus _signalBus;
        private PlantStatus _currentMainStatus = PlantStatus.None;
        public ReactiveProperty<Sprite> GrowthStage { get;}
        public ReactiveCollection<PlantStatus> StatusIcons { get; } = new (new HashSet<PlantStatus>());
        public ReactiveProperty<Color> BuffColor { get; } = new ReactiveProperty<Color>();
        public ReactiveProperty<bool> IsHovered { get; } = new(false);
        public PlantViewModel(PlantModel model, SignalBus signalBus)
        {
            _model = model;
            _signalBus = signalBus;
            GrowthStage = new ReactiveProperty<Sprite>() { Value = _model.Data.SpritesByPhase[0] };
        }
        public void Initialize()
        {
            _model.CurrentGrowthTime.Subscribe(UpdateSpriteByGrowth).AddTo(_disposable);
            _model.CurrentStatus.Pairwise().Subscribe(status =>
            {
                UpdateMainIcon(status.Current);
                
                if(status.Current == PlantStatus.Died && status.Previous != PlantStatus.Died)
                    HandleDead(_model.Data.SpritesByPhase,_model.Data.DiedSpritesByPhase);
                
                else if (status.Previous == PlantStatus.Died && status.Current != PlantStatus.Died)
                    HandleDead(_model.Data.DiedSpritesByPhase, _model.Data.SpritesByPhase);
                
            }).AddTo(_disposable);
            
             _model.Modifiers.ObserveAdd().Subscribe(OnModifierAdded).AddTo(_disposable);
             _model.Modifiers.ObserveRemove().Subscribe(OnModifierRemoved).AddTo(_disposable);
             
            UpdateMainIcon(PlantStatus.Growing);
        }
        
        private void UpdateSpriteByGrowth(TimeSpan time)
        {
            var totalSec = _model.Data.growthDurationInMinutes * 60;
            var left = time.TotalSeconds;
            var percent = 1 - (left / totalSec);

            var sprite = percent switch
            {
                <= 0.66 =>  GrowthStage.Value = _model.Data.SpritesByPhase[0],
                < 1.0  => GrowthStage.Value = _model.Data.SpritesByPhase[1],
                > 1.0 => GrowthStage.Value = _model.Data.SpritesByPhase[2]
            };
            
            if(GrowthStage.Value != sprite)
                GrowthStage.Value = sprite;
        }

        private void HandleDead(List<Sprite> curSprites, List<Sprite> nexrSprites)
        {
            var curIndex = curSprites.IndexOf(_model.Data.SpritesByPhase[0]);
            GrowthStage.Value = nexrSprites[curIndex];
        }
        
        private void UpdateMainIcon(PlantStatus status)
        {
            if(status == _currentMainStatus)
                return;
            
            StatusIcons.Remove(_currentMainStatus);

            _currentMainStatus = status;
            StatusIcons.Add(status);
        }

        public void Harvest()
        {
            if(!_model.TryHarvest(out var signal))
                return;
            
            _signalBus.Fire(signal);
        }
        public void Hover(Vector3 position, int direction = 1)
        {
            IsHovered.Value = direction > 0;
        }

        private void OnModifierAdded(CollectionAddEvent<Dictionary<PlantProperty, IPlantModifier>> addEvent)
        {
            BuffColor.Value = BuffColorBlender.MixModifierColors(addEvent.Value.Values.ToList());
        }
        
        private void OnModifierRemoved(CollectionRemoveEvent<Dictionary<PlantProperty, IPlantModifier>> removeEvent)
        {
            BuffColor.Value = BuffColorBlender.MixModifierColors(removeEvent.Value.Values.ToList());
        }
        
        
        public void Dispose()
        {
            _disposable?.Dispose();
        }
        
        public class Factory : PlaceholderFactory<PlantModel, PlantViewModel>
        {
            
        }
    }
}