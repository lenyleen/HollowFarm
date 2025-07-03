using System;
using System.Collections.Generic;
using System.ComponentModel;
using DefaultNamespace.Commands;
using DefaultNamespace.DataObjects;
using DefaultNamespace.Factory;
using DefaultNamespace.Handlers;
using DefaultNamespace.Models.UiModels;
using DefaultNamespace.ScriptableObjects;
using DefaultNamespace.ViewModels.UiVm;
using DefaultNamespace.Views.UIVIiews;
using DefaultNamespace.Views.UIVIiews.Service;
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
            Container.BindInterfacesAndSelfTo<InGameOverlayUiHandler>().AsSingle().WithArguments(UiType.InGameOverlay);
            
            InitializeCommands();
            InitializeActionMenu();
        }

        private void InitializeCommands()
        {
            Container.BindInterfacesAndSelfTo<AddWaterCommand>().AsSingle()
                .WithArguments(SoilActionType.Water);
            
            Container.BindInterfacesAndSelfTo<RemoveCommand>().AsSingle()
                .WithArguments(SoilActionType.Remove);
            
            Container.BindInterfacesAndSelfTo<HarvestCommand>().AsSingle()
                .WithArguments(SoilActionType.Harvest);
        }

        private void InitializeActionMenu()
        {
            var fatory =
                new SoilActionButtonFactory(_uiData.SoilActionButton, _uiData.SoilActionsButtonsData, Container);

            var buttons = new List<ISoilActionButton>();
            var buttonActionsData = _uiData.SoilActionsButtonsData.Actions;
            foreach (var buttonType in buttonActionsData.Keys)
            {
                buttons.Add(fatory.Create(_uiData.SoilActionsMenu.GridRect, buttonType));
            }
            
            _uiData.SoilActionsMenu.Initialize(buttons);

            Container.BindInterfacesAndSelfTo<ActionCommandFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<SoilActionMenuViewModel>()
                .AsSingle()
                .WithArguments(_uiData.SoilActionsMenu);
        }

        [Serializable]
        public class UiData
        {
            [field: SerializeField] public InventoryItemSelectionMenu SeedSelectionMenu { get; private set; }
            [field: SerializeField] public SoilActionsMenu SoilActionsMenu { get; private set; }
            [field: SerializeField] public SoilActionsButtonsData SoilActionsButtonsData { get; private set; }
            [field:SerializeField] public SoilActionButton SoilActionButton { get; private set; }
            [field: SerializeField] public InventoryItemButton SeedButtonPrefab { get; private set; }

            [field: SerializeField]
            public InventoryItemSelectionMenu ConsumableSelectionMenu { get; private set; }

            [field: SerializeField] public InventoryItemButton ConsumableButtonPrefab { get; private set; }
            [field: SerializeField] public SoilFilterDialogMenu FilterDialogMenu{ get; private set; }
        }
    }
}