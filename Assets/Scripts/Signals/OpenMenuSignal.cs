using System.Collections.Generic;
using DefaultNamespace.ScriptableObjects;
using DefaultNamespace.Signals.UiSignalsParams;
using DefaultNamespace.ViewModels;
using DefaultNamespace.ViewModels.UiVm;
using UnityEngine;

namespace DefaultNamespace.Signals
{
    public class OpenSoilMenuSignal<T> : IUiSignal<InventoryItemsMenuVm<T>,SoilPopupParams> where T : ICanBeDrawnInInventory
    {
        private readonly SoilPopupParams _soilPopupParams;

        public OpenSoilMenuSignal(SoilPopupParams soilPopupParams)
        {
            _soilPopupParams = soilPopupParams;
        }

        public SoilPopupParams GetParams() => _soilPopupParams;
    }
}