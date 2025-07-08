using System;
using DefaultNamespace.Models;
using Handlers.ClickHandler;
using Service;
using UnityEngine;

namespace StateMachine.States.Plant
{
    public class Died : PlantStateAbstract
    {
        public Died(IPlantStateContext plantStateContext) : base(plantStateContext)
        {
        }

        public override event Action<PlantStatus> OnChangeState;

        
        public override void Enter()
        {
            _plantStateContext.CurrentStatus.Value = PlantStatus.Died;
            Debug.Log("died");
        }

        public override void Exit()
        {
        }

        public override void FixedTick()
        {
        }

        public override void Tick()
        {
            
        }
    }
}