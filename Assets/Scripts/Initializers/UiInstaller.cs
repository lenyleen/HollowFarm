using System;
using System.ComponentModel;
using DefaultNamespace.DataObjects;
using DefaultNamespace.Factory;
using DefaultNamespace.Models.UiModels;
using DefaultNamespace.ScriptableObjects;
using DefaultNamespace.ViewModels.UiVm;
using DefaultNamespace.Views.UIVIiews;
using Service;
using Service.UI;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace DefaultNamespace.Initializers
{
    public class UiInstaller : Installer<UiInstaller.UiData, UiInstaller>
    {
        private readonly UiData _uiData;

        public UiInstaller(UiData uiData)
        {
            _uiData = uiData;
        }

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InventoryItemButtonFactory<PlantData>>()
                .AsSingle()
                .WithArguments(_uiData.SeedButtonPrefab);
           
            Container.BindInterfacesAndSelfTo<InventoyItemMenuModel<PlantData>>()
                .AsSingle();
           
            Container.BindInterfacesAndSelfTo<InventoryItemsMenuVm<PlantData>>()
                .AsSingle()
                .WithArguments(_uiData.SeedSelectionMenu);
            
            Container.BindInterfacesAndSelfTo<InventoryItemButtonFactory<ConsumableData>>()
                .AsSingle()
                .WithArguments(_uiData.ConsumableButtonPrefab);
           
            Container.BindInterfacesAndSelfTo<InventoyItemMenuModel<ConsumableData>>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<InventoryItemsMenuVm<ConsumableData>>()
                .AsSingle()
                .WithArguments(_uiData.ConsumableSelectionMenu);

            Container.BindInterfacesAndSelfTo<SoilFilterDialogMenu>()
                .FromInstance(_uiData.FilterDialogMenu)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<UiService>().AsSingle();
        }

        [Serializable]
        public class UiData
        {
            [field: SerializeField] public InventoryItemSelectionMenu SeedSelectionMenu { get; private set; }
            [field: SerializeField] public InventoryItemButton SeedButtonPrefab { get; private set; }

            [field: SerializeField]
            public InventoryItemSelectionMenu ConsumableSelectionMenu { get; private set; }

            [field: SerializeField] public InventoryItemButton ConsumableButtonPrefab { get; private set; }
            [field: SerializeField] public SoilFilterDialogMenu FilterDialogMenu{ get; private set; }
        }
    }
}