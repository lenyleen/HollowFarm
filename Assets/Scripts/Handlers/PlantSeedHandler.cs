using DefaultNamespace.DataObjects;
using DefaultNamespace.Models;
using DefaultNamespace.ScriptableObjects;
using DefaultNamespace.Signals;
using DefaultNamespace.ViewModels;
using DefaultNamespace.Views;
using DefaultNamespace.Views.UIVIiews;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.Handlers
{
    public class PlantSeedHandler 
    {
        /*private readonly SignalBus _signalBus;
        private readonly SoilGrid _soilGrid;
        private readonly IFactory<InventoryItem,Soil,PlantViewModel> _plantFactory;

        public PlantSeedHandler(SignalBus signalBus, SoilGrid soilGrid, IFactory<InventoryItem,Soil,PlantViewModel> plantFactory)
        {
            _signalBus = signalBus;
            _soilGrid = soilGrid;
            _plantFactory = plantFactory;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<ItemUsedSignal<PlantData>>(OnPlantSeed);
        }

        private void OnPlantSeed(ItemUsedSignal<PlantData> signal)
        {
            if (signal.Seed == null || signal.TargetSoil == null)
                return;
            
            var plantViewModel = _plantFactory.Create(signal.Seed, signal.TargetSoil);
            signal.TargetSoil.SetOccupied(plantViewModel);
            
        }
        
        public void Dispose()
        {
            _signalBus.Unsubscribe<ItemUsedSignal<PlantData>>(OnPlantSeed);
        }*/
    }
}

