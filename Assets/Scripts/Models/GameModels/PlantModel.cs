using System;
using System.Collections.Generic;
using DefaultNamespace.Boosters.Interfaces;
using DefaultNamespace.Boosters.ScriptableObjects;
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
    public class PlantModel :IMoonPhaseDependent, IPlantStateContext, IDisposable, IPlantModifierApplicable
    {
        public PlantData Data { get; }
        public DateTime PlantedAt { get; private set; }
        public TimeSpan GrowthDuration { get; private set; }
        public TimeSpan DryDuration { get; private set; }
        public DateTime DryedAt { get; private set; }
        public TimeSpan TimeWithoutWater { get; private set; }
        public ReactiveProperty<TimeSpan> CurrentGrowthTime { get;}
        public ReactiveProperty<PlantStatus> CurrentStatus { get;}
        public ReactiveDictionary<PlantProperty, IPlantModifier> Modifiers { get;} =  new ();

        private MoonPhase _currentMoonPhase;


        public PlantModel(PlantData plantData)
        {
            Data = plantData;
            PlantedAt = DateTime.UtcNow;
            CurrentStatus = new ReactiveProperty<PlantStatus>();
            DryDuration = TimeSpan.FromMinutes(0.5);
            CurrentGrowthTime = new ReactiveProperty<TimeSpan>(TimeSpan.FromMinutes(Data.growthDurationInMinutes));
            TimeWithoutWater = TimeSpan.FromMinutes(0.5);
            DryedAt =  DateTime.UtcNow + TimeSpan.FromMinutes(0.5);
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
        
        public bool TryHarvest(out ItemAddSignal<PlantData>  harvestedSignal)
        {
            harvestedSignal = null;
            
            if (CurrentStatus.Value != PlantStatus.CanBeHarvested)
                return false;

            var finalAmount = Data.Amount;
            
            if (TryGetPlantModifier(PlantProperty.HarvestMultiplier, out var modifier))
                finalAmount = (int)(Data.Amount * modifier.Value);
            
            harvestedSignal = new ItemAddSignal<PlantData>(Data, finalAmount);
            return true;
        }

        public bool TryGetPlantModifier(PlantProperty plantProperty, out IPlantModifier plantModifier)
        {
            return Modifiers.TryGetValue(plantProperty, out plantModifier);
        }

        public void ApplyModifier(PlantProperty plantProperty, IPlantModifier modifier)
        {
            Modifiers.TryAdd(plantProperty, modifier);
        }

        public void UpdateModifiers(float deltaTime)
        {
            foreach (var modifier in Modifiers.Values)
            {
                modifier.Update(deltaTime);
            }
        }
    }
}

