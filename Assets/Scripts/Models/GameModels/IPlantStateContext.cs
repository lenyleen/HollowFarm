using System;
using System.Collections.Generic;
using DefaultNamespace.Boosters.Interfaces;
using DefaultNamespace.ScriptableObjects;
using Service;
using UniRx;

namespace DefaultNamespace.Models
{
    public interface IPlantStateContext : IPlantModifierApplicable
    {
        public DateTime PlantedAt { get; }
        public TimeSpan DryDuration { get; }
        public DateTime DryedAt { get; }
        public TimeSpan TimeWithoutWater { get; }
        
        public ReactiveProperty<TimeSpan> CurrentGrowthTime { get;}
        public ReactiveProperty<PlantStatus> CurrentStatus { get;} 
    }
}