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

        public NeedWater(IPlantStateContext plantStateContext) :  base(plantStateContext)
        {
            _plantStateContext = plantStateContext;
        }

        public override void Enter()
        {
            Debug.Log("Water");
        }

        public override void Tick()
        {
            var currentDryTime = DateTime.UtcNow - _plantStateContext.DryedAt;
            
            if(currentDryTime < _plantStateContext.DryDuration)
                return;

            _plantStateContext.CurrentStatus.Value = PlantStatus.Died;
        }
        
        public override void Exit()
        {}

        
    }
}