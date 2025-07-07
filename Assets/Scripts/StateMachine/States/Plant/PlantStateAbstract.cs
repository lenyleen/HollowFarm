using System;
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

        protected void Grow()
        {
            _plantStateContext.CurrentGrowthTime.Value -= TimeSpan.FromSeconds(1); //++++ бустеры
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