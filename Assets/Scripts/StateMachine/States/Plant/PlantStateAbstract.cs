using System;
using DefaultNamespace.Boosters.Interfaces;
using DefaultNamespace.Boosters.ScriptableObjects;
using DefaultNamespace.Models;
using DefaultNamespace.ScriptableObjects;
using Handlers.ClickHandler;
using Service;
using UnityEngine;

namespace StateMachine.States.Plant
{
    public abstract class PlantStateAbstract : IState<PlantStatus>
    {
        public abstract event Action<PlantStatus> OnChangeState;
        protected IPlantStateContext _plantStateContext;

        protected PlantStateAbstract(IPlantStateContext plantStateContext)
        {
            _plantStateContext = plantStateContext;
        }

        public abstract void Enter();
        public abstract void Exit();
        public abstract void FixedTick();

        protected void Grow()
        {
            var deltaTime = TimeSpan.FromSeconds(1);
            if (_plantStateContext.TryGetPlantModifier(PlantProperty.GrowthSpeed, out var modifier))
            {
                deltaTime *= modifier.Value;
            }
            _plantStateContext.CurrentGrowthTime.Value -= deltaTime; //++++ бустеры
            Debug.Log($"{_plantStateContext.CurrentGrowthTime}");
            if(_plantStateContext.CurrentGrowthTime.Value <=  TimeSpan.Zero)
                _plantStateContext.CurrentStatus.Value = PlantStatus.CanBeHarvested;
        }
        protected void CheckWater()
        {
            var dryTime = DateTime.UtcNow - _plantStateContext.DryedAt;
            Debug.Log($"{dryTime}");
            
            if(dryTime >= _plantStateContext.TimeWithoutWater)
                _plantStateContext.CurrentStatus.Value = PlantStatus.NeedWatering;
        }

        public abstract void Tick();
    }
}