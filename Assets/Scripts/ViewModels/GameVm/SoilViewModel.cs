using System;
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
        
        private PlantViewModel _plantViewModel; 
        public bool IsOccupied => _model.IsOccupied;

        public SoilViewModel(Soil soil, HoverVisualizerService hoverVisualizer, IPlantSpawnService plantSpawnService,
            SignalBus signalBus)
        {
            _model = soil;
            _hoverVisualizer = hoverVisualizer;
            _plantSpawnService = plantSpawnService;
            _signalBus = signalBus;
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
        public class Factory : PlaceholderFactory<Soil,SoilViewModel> { }

        public void AddWater() => _model.Water();

        public void Remove()
        {
            throw new NotImplementedException();  //TODO дописать логику для команд действий (инит в контейнер, методы дестроя и харвеста для вью модели планта)
        }

        public void Harvest()
        {
            throw new NotImplementedException();
        }
    }
}