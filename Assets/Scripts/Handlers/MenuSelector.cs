using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DefaultNamespace.Models;
using DefaultNamespace.ScriptableObjects;
using DefaultNamespace.Signals;
using DefaultNamespace.Signals.UiSignalsParams;
using DefaultNamespace.ViewModels;
using DefaultNamespace.ViewModels.Interfaces;
using DefaultNamespace.ViewModels.UiVm;
using DefaultNamespace.ViewModels.UiVm.Params;
using DefaultNamespace.Views.UIVIiews;
using Service;
using UniRx;
using UnityEngine;
using Zenject;
using Vector3Int = UnityEngine.Vector3Int;

namespace DefaultNamespace.Handlers 
{
    public class MenuSelector : ISoilMenuSelector
    {
        private SignalBus _signalBus;
        private readonly IUiService _uiService;

        public MenuSelector(SignalBus signalBus, IUiService uiService)
        {
            _signalBus = signalBus;
            _uiService = uiService;
        }
        
        public void SelectSoilMenu(Dictionary<Vector3Int, SoilViewModel> soilVmData)
        {
           if(soilVmData.First().Value.IsOccupied)
               HandleOccupied(soilVmData);
           else
               HandleEmpty(soilVmData);
               
        }

        private void HandleOccupied(Dictionary<Vector3Int, SoilViewModel> soilVmData)
        {
            _uiService.Show<InventoryItemsMenuVm<ConsumableData>,SoilPopupParams>(new SoilPopupParams( soilVmData.Keys.ToList()));
            _uiService.Show<SoilActionMenuViewModel,SoilActionMenuParams>(new SoilActionMenuParams(soilVmData.Values));
        }

        private void HandleEmpty(Dictionary<Vector3Int, SoilViewModel> soilVmData)
        {
            _uiService.Show<InventoryItemsMenuVm<PlantData>,SoilPopupParams>(new SoilPopupParams( soilVmData.Keys.ToList()));
        }

        public async UniTask<SoilFilter> OpenFilterDialogMenu(Vector3 position)
        {
            var dialog = _uiService.ShowDialogMenu<SoilFilterDialogMenu, SoilFilterParams,SoilFilter>(new SoilFilterParams(position));
            var result = await dialog.WaitForResultAsync();
            return result;
        }
    }
}