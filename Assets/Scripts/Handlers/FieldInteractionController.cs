using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DefaultNamespace.Extensions;
using DefaultNamespace.Models;
using DefaultNamespace.Models.Interfaces;
using DefaultNamespace.Signals;
using DefaultNamespace.Signals.UiSignalsParams;
using DefaultNamespace.View;
using DefaultNamespace.ViewModels;
using DefaultNamespace.ViewModels.Interfaces;
using DefaultNamespace.Views.UIVIiews;
using Service;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using Zenject;

namespace DefaultNamespace.Handlers
{
    public class FieldInteractionController : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly FieldViewModel _fieldViewModel;
        private readonly Tilemap _tilemap;
        private readonly ISoilMenuSelector _soilMenuSelector;
        
        private List<SoilViewModel> _soilViewModels;

        public FieldInteractionController(SignalBus signalBus, FieldViewModel fieldViewModel, ISoilMenuSelector soilMenuSelector,
            Tilemap tilemap)
        {
            _signalBus = signalBus;
            _fieldViewModel = fieldViewModel;
            _soilMenuSelector = soilMenuSelector;
            _tilemap = tilemap;
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<SoilSelectedSignal>(SoilSelected);
            _signalBus.Subscribe<SoilMenuClosed>(SoilDeselected);
        }

        private void SoilDeselected()
        {
            foreach (var soilViewModel in _soilViewModels)
            {
                soilViewModel.Hover(-1);
            }
            _soilViewModels = null;
        }

        private async void SoilSelected(SoilSelectedSignal s)
        {
            var soilPairs = GetSoilViewModels(s.positions);
            _soilViewModels = soilPairs.Values.ToList();
            
            switch (soilPairs.Count)
            {
                case 0:
                    return;
                case 1:
                    ShowSingleSoilMenu(soilPairs);
                    return;
            }

            if (CheckSameOccupation(_soilViewModels))
            {
                HoverSoils(_soilViewModels);
                _soilMenuSelector.SelectSoilMenu(soilPairs);
                return;
            }
            
            var center = TilemapUtils.GetWorldCenter(soilPairs.Keys.ToList(), _tilemap);
            var filter = await _soilMenuSelector.OpenFilterDialogMenu(center);
            
            if(filter == SoilFilter.None) return;

            await UniTask.Yield();
            
            var filteredSoilVms = ApplyFilter(soilPairs, filter);

            HoverSoils(filteredSoilVms.Values);
            
            _soilMenuSelector.SelectSoilMenu(filteredSoilVms);
        }

        private Dictionary<Vector3Int, SoilViewModel> GetSoilViewModels(IEnumerable<Vector3Int> positions)
        {
            var result = new Dictionary<Vector3Int, SoilViewModel>();
            foreach (var pos in positions)
            {
                var vm = _fieldViewModel.GetVmAt(pos);
                if (vm != null)
                    result[pos] = vm;
            }
            return result;
        }
        
        private void ShowSingleSoilMenu(Dictionary<Vector3Int, SoilViewModel> soils)
        {
            var vm = soils.First().Value;
            vm.Hover(1);
            _soilMenuSelector.SelectSoilMenu(soils);
        }

        private bool CheckSameOccupation(List<SoilViewModel> soils)
        {
            var occupied = soils.First().IsOccupied;
            
            return soils.All(s => s.IsOccupied == occupied);
        }
        
        private void HoverSoils(IEnumerable<SoilViewModel> soils)
        {
            foreach (var vm in soils)
                vm.Hover(1);
        }
        
        private Dictionary<Vector3Int, SoilViewModel> ApplyFilter(
            Dictionary<Vector3Int, SoilViewModel> soils, SoilFilter filter)
        {
            return filter switch
            {
                SoilFilter.Empty => soils.Where(s => !s.Value.IsOccupied).ToDictionary(s => s.Key, s => s.Value),
                SoilFilter.Occupied => soils.Where(s => s.Value.IsOccupied).ToDictionary(s => s.Key, s => s.Value),
                _ => new Dictionary<Vector3Int, SoilViewModel>()
            };
        }
        
        public void Dispose()
        {
            _signalBus.Unsubscribe<SoilSelectedSignal>(SoilSelected);
        }
    }
}
