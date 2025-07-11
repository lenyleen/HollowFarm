using System;
using DefaultNamespace.Boosters.Interfaces;
using DefaultNamespace.Boosters.Service;
using DefaultNamespace.Factory;
using DefaultNamespace.Models;
using DefaultNamespace.ScriptableObjects;
using DefaultNamespace.Signals;
using DefaultNamespace.View;
using DefaultNamespace.ViewModels.Interfaces;
using JetBrains.Annotations;
using Service;
using UniRx;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.ViewModels
{
    public class SoilViewModel : IInitializable, IDisposable, ICommandPerformer
    {
        private readonly Soil _model;
        private readonly SignalBus _signalBus;
        private readonly HoverVisualizerService _hoverVisualizer;
        private readonly IPlantSpawnService _plantSpawnService;
        private readonly IPlantModifierService _plantModifierService;
        
        private PlantViewModel _plantViewModel; 
        public bool IsOccupied => _model.IsOccupied;
        public IPlantModifierService BoosterService => _plantModifierService;

        public SoilViewModel(Soil soil, HoverVisualizerService hoverVisualizer, IPlantSpawnService plantSpawnService,
            SignalBus signalBus, IPlantModifierService plantModifierService)
        {
            _model = soil;
            _hoverVisualizer = hoverVisualizer;
            _plantSpawnService = plantSpawnService;
            _signalBus = signalBus;
            _plantModifierService = plantModifierService;
        }

        public void Initialize()
        {
            
        }

        public void Hover(int direction)
        {
            _hoverVisualizer.Hover(_model.TilePosition,direction);
            _plantViewModel?.Hover(_model.WorldPosition,direction);
        }

        public void Plant(PlantData plantData)
        {
            _plantViewModel = _plantSpawnService.Spawn(plantData,_model,_model.WorldPosition);
            _hoverVisualizer.Hover(_model.TilePosition,-1);
        }
        
        public void Dispose()
        {
            // TODO release managed resources here
        }
        
        public void AddWater() => _model.Water();

        public void ApplyBooster(ConsumableData  consumableData)
        {
            
        }

        public void Remove()
        {
            _plantSpawnService.Despawn(_plantViewModel);
            _plantViewModel = null;
            _model.RemovePlant();
        }
        //TODO: Возможно нужно вынести логику с растение в отдельный сервис, вм должна заниматься только юи?
        public void Harvest()
        {
            _plantViewModel.Harvest();
            _plantSpawnService.Despawn(_plantViewModel);
            _plantViewModel = null;
            _model.RemovePlant(); ;
        }
        public class Factory : PlaceholderFactory<Soil,SoilViewModel> { }
    }
}