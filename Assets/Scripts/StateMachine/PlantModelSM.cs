using System;
using System.Collections.Generic;
using DefaultNamespace.Factory;
using DefaultNamespace.ScriptableObjects;
using Handlers.ClickHandler;
using Service;
using StateMachine.Interfaces;
using Zenject;

namespace DefaultNamespace.Models.PlantModelStates
{
    public class PlantModelSM<TState> where TState : IState<PlantStatus> 
    {
        public IState<PlantStatus> CurrentState { get; private set; }
        
        public void ChangeState(TState state)
        {
            if(CurrentState != null)
                CurrentState.Exit();
            
            CurrentState = state;
            CurrentState.Enter();
        }
    }
}