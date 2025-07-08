using System;
using DefaultNamespace.Models;
using Handlers.ClickHandler;
using Service;

namespace StateMachine.States.Plant
{
    public class CanBeHarwested : PlantStateAbstract
    {
        public CanBeHarwested(IPlantStateContext plantStateContext) : base(plantStateContext)
        {
        }

        public override event Action<PlantStatus> OnChangeState;
        public override void Enter()
        {
            _plantStateContext.CurrentStatus.Value = PlantStatus.CanBeHarvested;
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