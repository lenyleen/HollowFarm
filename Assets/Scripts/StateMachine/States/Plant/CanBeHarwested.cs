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
        }

        public override void Exit()
        {
        }

        public override void Tick()
        {
            CheckWater();
        }
    }
}