using System;
using System.Collections.Generic;
using DefaultNamespace.DataObjects;
using DefaultNamespace.Models;
using DefaultNamespace.Models.Interfaces;
using DefaultNamespace.ScriptableObjects;
using DefaultNamespace.Signals;
using DefaultNamespace.View;
using DefaultNamespace.ViewModels.Interfaces;
using NUnit.Framework;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace DefaultNamespace.ViewModels
{
    public class FieldViewModel : IInitializable, IDisposable
    {
        private readonly SoilViewModel.Factory _soilFactory;
        private readonly FieldModel _fieldModel;
        private readonly SignalBus _signalBus;
        
        private Dictionary<Vector3Int, SoilViewModel> _soilVms = new Dictionary<Vector3Int, SoilViewModel>();

        public FieldViewModel(SoilViewModel.Factory soilFactory, FieldModel fieldModel, SignalBus signalBus)
        {
            _soilFactory = soilFactory;
            _fieldModel = fieldModel;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            var soilModels = _fieldModel.All;
            _signalBus.Subscribe<ItemUsedSignal<PlantData>>(PlantPlant);
            foreach (var soilModel in soilModels)
            {
                var vm = _soilFactory.Create(soilModel);
                _soilVms.Add(soilModel.TilePosition, vm);
            }
        }
        
        

        public SoilViewModel GetVmAt(Vector3Int position) => _soilVms.GetValueOrDefault(position);

        private void PlantPlant(ItemUsedSignal<PlantData> s)
        {
            foreach (var position in s.Positions)
            {
                var soilVm = GetVmAt(position);
                if (!s.Seed.TryGetData<PlantData>(out var data))
                    return;
                soilVm.Plant(data);
            }

            _signalBus.Fire<ClosePopUpRequestSignal>();
        }

        public void ConsumableSelected(ItemUsedSignal<ConsumableData> s)
        {
            
        }
        
        public void Dispose()
        {
            _soilVms.ForEach(s => s.Value.Dispose());
            _soilVms.Clear();
        }
       
    }
}