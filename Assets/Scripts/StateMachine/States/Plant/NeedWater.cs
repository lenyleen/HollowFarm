using System;
using DefaultNamespace.Models;
using Handlers.ClickHandler;
using Service;
using UnityEngine;

namespace StateMachine.States.Plant
{
    public class NeedWater : PlantStateAbstract
    {
        public override event Action<PlantStatus> OnChangeState;
        private readonly IPlantStateContext _plantStateContext;
        private DateTime _toDieDuration;

        public NeedWater(IPlantStateContext plantStateContext) :  base(plantStateContext)
        {
            _plantStateContext = plantStateContext;
        }

        public override void Enter()
        {
            _plantStateContext.CurrentStatus.Value = PlantStatus.NeedWatering;
            _toDieDuration = DateTime.UtcNow + _plantStateContext.DryDuration;
            Debug.Log("Water");
        }

        public override void FixedTick()
        {
            if(DateTime.UtcNow < _toDieDuration)
                return;

            

            _plantStateContext.CurrentStatus.Value = PlantStatus.Died; 
        }

        public override void Tick()
        {
            if (DateTime.UtcNow >= _plantStateContext.DryedAt) 
                return;
            
            _plantStateContext.CurrentStatus.Value = PlantStatus.Growing;
        }
        
        public override void Exit()
        {}

        
    }
}