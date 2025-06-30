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
            Debug.Log("Grow");
        }
        public override void Tick()
        {
            CheckWater(); 
            Grow();
        }
        
        public override void Exit()
        {
            
        }

       
    }
}