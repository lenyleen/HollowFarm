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
using Zenject;

namespace DefaultNamespace.Factory
{
    public class PlantSpawnService : IPlantSpawnService
    {
        private readonly Image _iconPrefab;
        private readonly PlantStatusIconData _plantStatusIconData;
        private readonly DiContainer _container;
        private readonly PlantView _plantViewPrefab;
        private readonly SignalBus _signalBus;
        private readonly ITimeHandler _timeHandler;

        public PlantSpawnService(DiContainer container, PlantView view, SignalBus signalBus, Image prefab, 
            PlantStatusIconData plantStatusIconData, ITimeHandler timeHandler)
        {
            _plantViewPrefab = view;
            _container = container;
            _signalBus = signalBus;
            _iconPrefab = prefab;
            _plantStatusIconData = plantStatusIconData;
        }

        public PlantViewModel Spawn(PlantData plantData, Soil soilModel, Vector3 position)
        {
            var model = new PlantModel(plantData);
            soilModel.SetPlantModel(model);

            var stateMachine = new PlantModelSM<IState<PlantStatus>>();
            var stateModelController = _container.Instantiate<PlantStateController>(new object[] { model, stateMachine });
            stateModelController.Initialize();
            
            var plantViewModel = new PlantViewModel(model, _signalBus);
            plantViewModel.Initialize();

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
            
            var plantView = _container.InstantiatePrefabForComponent<PlantView>(_plantViewPrefab);
            plantView.transform.position = new Vector3(position.x,position.y + 2.3f,0);
            plantView.Initialize(plantViewModel, model.Data.LightColor,model.Data.SpritesByPhase[0],images);
            
            return plantViewModel;
        }
    }
}

