using System;
using UnityEngine;
using Zenject;
using DefaultNamespace.Handlers;
using DefaultNamespace.ScriptableObjects;
using DefaultNamespace.Signals;
using DefaultNamespace.View;
using DefaultNamespace.Views;
using DefaultNamespace.Views.UIVIiews;
using Service;
using Service.UI;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace DefaultNamespace.Initializers
{
    public class FarmInstaller : MonoInstaller
    {
        [SerializeField] private UiInstaller.UiData _uiData;
        [SerializeField] private PlayerInventory _playerInventory; //TODO должен находиться в общем контексте всего проекта, а не в ферме
        [SerializeField] private GameInstallerData _gameInstallerData;
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            InstallSignals();
            Container.Bind<PlayerInventory>().FromInstance(_playerInventory).AsSingle();
            DG.Tweening.DOTween.Init();
            UiInstaller.Install(Container, _uiData);
            GameInstaller.Install(Container,_gameInstallerData);
        }

        private void InstallSignals()
        {
            Container.DeclareSignal<ItemUsedSignal<PlantData>>();
            Container.DeclareSignal<ItemUsedSignal<ConsumableData>>();
            Container.DeclareSignal<OpenSoilMenuSignal<PlantData>>();
            Container.DeclareSignal<OpenSoilMenuSignal<ConsumableData>>();
            Container.DeclareSignal<SoilSelectedSignal>();
            Container.DeclareSignal<SpawnTimeRequestSignal>();
            Container.DeclareSignal<SoilMenuClosed>();
            Container.DeclareSignal<ClosePopUpRequestSignal>();
        }
    }
    [Serializable]
    public class PlantToFactoryData
    {
        [field:SerializeField] public PlantView _plantViewPrefab { get;private set; }
        [field:SerializeField] public PlantUiView _plantUiPrefab { get; private set; }
    }
}