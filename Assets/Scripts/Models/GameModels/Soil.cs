using System;
using DefaultNamespace.Models.Interfaces;
using DefaultNamespace.ScriptableObjects;
using DefaultNamespace.ViewModels;
using DefaultNamespace.ViewModels.Interfaces;
using UniRx;
using UnityEngine;

namespace DefaultNamespace.Models
{
    public class Soil : IPlantSoilConext
    {
        public bool IsOccupied => _plant != null;
        private readonly SoilData _soilData; 
        public readonly Vector3Int TilePosition;
        public readonly Vector3 WorldPosition;
        public bool IsWatered { get; private set; }
        public bool IsFertilized { get; private set; }
        private PlantModel _plant;
        private TimeSpan _wateredTime;
        private TimeSpan _fertilizedTime;
        

        public Soil(SoilData soilData,Vector3Int tilePosition, Vector3 worldPosition)
        {
            _wateredTime = new TimeSpan();
            _fertilizedTime = new TimeSpan();

            _soilData = soilData;
            TilePosition = tilePosition;
            WorldPosition = worldPosition;
        }
        
        public void UpdateTime()
        {
            _wateredTime -= TimeSpan.FromSeconds(1);
            if (_wateredTime.TotalSeconds <= 0)
            {
                IsWatered = false;
                _wateredTime = TimeSpan.Zero; 
            }
            
            _fertilizedTime -= TimeSpan.FromSeconds(1);
            if (!(_fertilizedTime.TotalSeconds <= 0)) return;
            IsFertilized = false;
            _fertilizedTime = TimeSpan.Zero;
        }   
        
        public void Fertilize()
        {
            if(!IsOccupied)
                return;
            IsFertilized = true;
            _fertilizedTime = TimeSpan.FromMinutes(_soilData.fertilizedDurationInMinutes);
            // Логика для увеличения питательных веществ в почве
        }
        
        public void Water()
        {
            if(!IsOccupied)
                return;
            
            IsWatered = true;
            _wateredTime = TimeSpan.FromMinutes(_soilData.wateredDurationInMinutes);
            _plant.AddWater(_wateredTime);
        }

        public void RemovePlant()
        {
            _plant.Dispose();
            _plant = null;
        }

        public void SetPlantModel(PlantModel plantModel)
        {
            _plant = plantModel;
            IsWatered = true;
        }
        
        public void Clear()
        {
            IsWatered = false;
            IsFertilized = false;
            _wateredTime = TimeSpan.Zero;
            _fertilizedTime = TimeSpan.Zero;
        }
    }
}