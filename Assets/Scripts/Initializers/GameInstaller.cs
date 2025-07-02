using System;
using DefaultNamespace.Factory;
using DefaultNamespace.Handlers;
using DefaultNamespace.Handlers.ClickHandler;
using DefaultNamespace.Models;
using DefaultNamespace.ScriptableObjects;
using DefaultNamespace.View;
using DefaultNamespace.ViewModels;
using DefaultNamespace.Views;
using Handlers.ClickHandler;
using Handlers.ClickHandler.States;
using Service;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Zenject;

namespace DefaultNamespace.Initializers
{
    public class GameInstaller : Installer<GameInstallerData,GameInstaller>
    {
        private readonly GameInstallerData _gameInstallerData;
        public GameInstaller(GameInstallerData gameInstallerData)
        {
            _gameInstallerData = gameInstallerData;
        }
        
        public override void InstallBindings()
        {
            Container.Bind<Tilemap>()
                .FromInstance(_gameInstallerData.Tilemap)
                .AsSingle();
            
            Container.BindMemoryPool<SoilTileHoverVisualizer, SoilTileHoverVisualizer.Pool>()
                .WithInitialSize(10)
                .FromComponentInNewPrefab(_gameInstallerData.SoilTileHoverVisualizer)
                .UnderTransformGroup("Hovers");
            
            Container.BindInterfacesAndSelfTo<HoverVisualizerService>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<SoilGrid>()
                .AsSingle()
                .WithArguments(_gameInstallerData.SoilData)
                .NonLazy();

            Container.BindInterfacesAndSelfTo<MenuSelector>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<FieldInteractionController>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<FieldModel>()
                .AsSingle()
                .WithArguments(_gameInstallerData.SoilData);
            
            Container.BindInterfacesAndSelfTo<FieldViewModel>()
                .AsSingle();
            
            Container.BindFactory<Soil, SoilViewModel, SoilViewModel.Factory>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<TimeHandler>()
                .AsSingle();
            
            InitializeInput();
            InitializePlant();
        }

        private void InitializePlant()
        {
            Container.BindMemoryPool<PlantView,PlantView.Pool>().WithInitialSize(10).
            FromComponentInNewPrefab(_gameInstallerData.Data._plantViewPrefab).UnderTransformGroup("Plants");
            
            Container.BindInterfacesAndSelfTo<PlantSpawnService>()
                .AsSingle()
                .WithArguments( _gameInstallerData.PlantStatusIconData,
                    _gameInstallerData.PlantIconPrefab);
            
            Container.BindInterfacesAndSelfTo<PlantStateFactory>().AsSingle();
            
        }
        
        private void InitializeInput()
        {
            Container.Bind<ClickContext>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<ClickStateFactory>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<ClickSM>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<InputHandler>()
                .AsSingle();
        }
    }
    [Serializable]
    public class GameInstallerData
    {
        [field:SerializeField] public Tilemap Tilemap { get; private set; }
        [field:SerializeField]public PlantToFactoryData Data{ get; private set; }
        [field:SerializeField]public SoilData SoilData { get; private set; }
        [field:SerializeField]public SoilTileHoverVisualizer SoilTileHoverVisualizer { get; private set; }
        [field:SerializeField]public PlantStatusIconData PlantStatusIconData { get; private set; }
        [field:SerializeField]public Image PlantIconPrefab { get; private set; }
    }
}