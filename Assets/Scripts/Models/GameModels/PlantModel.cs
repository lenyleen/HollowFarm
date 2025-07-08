using System;
using System.Collections.Generic;
using DefaultNamespace.Handlers;
using DefaultNamespace.Models.Interfaces;
using DefaultNamespace.ScriptableObjects;
using DefaultNamespace.ScriptableObjects.Service;
using DefaultNamespace.Signals;
using Service;
using UniRx;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.Models
{
    public class PlantModel :IMoonPhaseDependent, IPlantStateContext, IDisposable
    {
        public PlantData Data { get; }
        public DateTime PlantedAt { get; private set; }
        public TimeSpan GrowthDuration { get; private set; }
        public TimeSpan DryDuration { get; private set; }
        public DateTime DryedAt { get; private set; }
        public TimeSpan TimeWithoutWater { get; private set; }
        public ReactiveProperty<TimeSpan> CurrentGrowthTime { get;}
        public ReactiveProperty<PlantStatus> CurrentStatus { get;}
        public ReactiveCollection<ConsumableData> Boosters { get; }

        private MoonPhase _currentMoonPhase;

        private int _amount;


        public PlantModel(PlantData plantData)
        {
            Data = plantData;
            PlantedAt = DateTime.UtcNow;
            CurrentStatus = new ReactiveProperty<PlantStatus>();
            DryDuration = TimeSpan.FromMinutes(0.5);
            CurrentGrowthTime = new ReactiveProperty<TimeSpan>(TimeSpan.FromMinutes(Data.growthDurationInMinutes));
            TimeWithoutWater = TimeSpan.FromMinutes(0.5);
            DryedAt =  DateTime.UtcNow + TimeSpan.FromMinutes(0.5);
            Boosters = new ReactiveCollection<ConsumableData>(new HashSet<ConsumableData>());
            GrowthDuration = TimeSpan.FromMinutes(Data.growthDurationInMinutes);
        }

        public void AddWater(TimeSpan waterDuration)
        {
            DryedAt = DateTime.UtcNow + waterDuration;
        }
        
        public void UpdateMoonPhase(MoonPhase phase)
        {
            
        }
        public void Dispose()
        {
            CurrentStatus.Value = PlantStatus.Harvested;
        }
        
        public bool TryHarvest(out PlantHarvestedSignal  harvestedSignal)
        {
            harvestedSignal = null;
            
            if (CurrentStatus.Value != PlantStatus.CanBeHarvested)
                return false;
            
            harvestedSignal = new PlantHarvestedSignal(Data, _amount);
            return true;
        }
    }
}

