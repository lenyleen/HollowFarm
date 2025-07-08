using System;
using DefaultNamespace.Models;
using Handlers.ClickHandler;
using Service;
using UnityEngine;

namespace StateMachine.States.Plant
{
    public class Growing : PlantStateAbstract
    {
        public override event Action<PlantStatus> OnChangeState;
        protected readonly IPlantStateContext _plantStateContext;

        public Growing(IPlantStateContext plantStateContext) : base( plantStateContext)
        {
            _plantStateContext = plantStateContext;
        }
        public override void Enter()
        {
            _plantStateContext.CurrentStatus.Value = PlantStatus.Growing;
            Debug.Log("Grow");
        }

        public override void FixedTick()
        {
            CheckWater(); 
            Grow();
        }

        public override void Tick()
        { }
        
        public override void Exit()
        {
            
        }

       
    }
}