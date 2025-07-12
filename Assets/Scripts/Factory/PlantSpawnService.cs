using System.Collections.Generic;
using DefaultNamespace.DataObjects;
using DefaultNamespace.Handlers;
using DefaultNamespace.Models;
using DefaultNamespace.Models.PlantModelStates;
using DefaultNamespace.ScriptableObjects;
using DefaultNamespace.ViewModels;
using DefaultNamespace.Views;
using DefaultNamespace.Views.UIVIiews;
using Handlers.ClickHandler;
using Service;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Views.GameViews.OutlineManager;
using Zenject;

namespace DefaultNamespace.Factory
{
    public class PlantSpawnService : IPlantSpawnService
    {
        private readonly Image _iconPrefab;
        private readonly PlantStatusIconData _plantStatusIconData;
        private readonly DiContainer _container;
        private readonly SignalBus _signalBus;
        private readonly PlantView.Pool _pool;
        private readonly Dictionary<PlantViewModel, PlantView> _views;
        private readonly IOutlineManager _outlineManager;
        

        public PlantSpawnService(DiContainer container,PlantView.Pool pool,  SignalBus signalBus, Image prefab, 
            PlantStatusIconData plantStatusIconData, IOutlineManager outlineManager)
        {
            _container = container;
            _signalBus = signalBus;
            _iconPrefab = prefab;
            _pool = pool;
            _plantStatusIconData = plantStatusIconData;
            _outlineManager = outlineManager;
            _views = new  Dictionary<PlantViewModel, PlantView>();
        }

        public PlantViewModel Spawn(PlantData plantData, Soil soilModel, Vector3 position)
        {
            var model = new PlantModel(plantData);
            soilModel.SetPlantModel(model);
            
            var plantViewModel = new PlantViewModel(model, _signalBus);
            
            
            var images = new Dictionary<PlantStatus, Image>();
            foreach (var statusIcon in _plantStatusIconData.statusIcons)
            {
                var image = _container.InstantiatePrefabForComponent<Image>(_iconPrefab);
                image.sprite = statusIcon.Value;
                image.SetNativeSize();
                var imageRect = image.rectTransform.sizeDelta;
                image.rectTransform.sizeDelta = new Vector2(imageRect.x/26, imageRect.y/26);
                images.Add(statusIcon.Key, image);
            }

            var spriteHeight = plantData.SpritesByPhase[0].bounds.size.y;
            var worldPosition = new Vector3(position.x, position.y + 2.3f, 0);
            var sortingOrder = (int)-worldPosition.y;
            
            var plantView = _pool.Spawn(plantViewModel, worldPosition,
                model.Data.LightColor, model.Data.SpritesByPhase[0], _outlineManager);
            
            plantView.IconView.Initialize(images, spriteHeight, sortingOrder);
            
            _views.Add(plantViewModel, plantView);
            
            var stateMachine = new PlantModelSM<IState<PlantStatus>>();
            var stateModelController =
                _container.Instantiate<PlantStateController>(new object[] { model, stateMachine });
            stateModelController.Initialize();
            
            plantViewModel.Initialize();
            return plantViewModel;
        }

        public void Despawn(PlantViewModel plantViewModel)
        {
            if(!_views.TryGetValue(plantViewModel, out var view))
                return;
            
            _pool.Despawn(view);
            plantViewModel.Dispose();//TODO: возможно нужно сделать отдельный сервис для хендла анимаций
            _views.Remove(plantViewModel);
        }
    }
}

