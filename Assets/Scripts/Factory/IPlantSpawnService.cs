using DefaultNamespace.Models;
using DefaultNamespace.ScriptableObjects;
using DefaultNamespace.ViewModels;
using UnityEngine;

namespace DefaultNamespace.Factory
{
    public interface IPlantSpawnService
    {
        public PlantViewModel Spawn(PlantData plantData, Soil soilModel, Vector3 position);
        public void Despawn(PlantViewModel plantViewModel);
    }
}