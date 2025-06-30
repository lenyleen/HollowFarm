using DefaultNamespace.ScriptableObjects;
using DefaultNamespace.Views.UIVIiews.Service;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.Factory
{
    public class SoilActionButtonFactory : IFactory<RectTransform,Sprite,ISoilActionButton>
    {
        private readonly SoilActionButton _prefab;
        private readonly SoilActionsButtonsData _data;
        private readonly DiContainer _container;

        public SoilActionButtonFactory(SoilActionButton prefab, SoilActionsButtonsData data, DiContainer container)
        {
            _prefab = prefab;
            _data = data;
            _container = container;
        }
        public ISoilActionButton Create(RectTransform parentTransform, Sprite sprite)
        {
            return _container.InstantiatePrefabForComponent<ISoilActionButton>(_prefab);
        }
    }
}