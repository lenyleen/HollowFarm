using System;
using DefaultNamespace.Factory;
using DefaultNamespace.Models;
using Service;
using StateMachine.States.Plant;
using Zenject;

namespace Handlers.ClickHandler
{
    public class PlantStateFactory : IFactory<PlantStatus,IPlantStateContext,IState<PlantStatus>>
    {
        public IState<PlantStatus> Create(PlantStatus state, IPlantStateContext context)
        {
            return state switch
            {
                PlantStatus.NeedWatering => new NeedWater(context),
                PlantStatus.Died => new Died(context),
                PlantStatus.CanBeHarvested => new CanBeHarwested(context),
                PlantStatus.Growing => new Growing(context),
                _ => throw new Exception("Not implemented state")
            };
        }
    }
}