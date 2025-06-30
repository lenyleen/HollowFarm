using System;
using UnityEngine.InputSystem;

namespace Handlers.ClickHandler.States
{
    public class Idle : IState<ClickStateType>
    {
        public event Action<ClickStateType> OnChangeState;
        public void Enter() { }

        public void Tick()
        {
            if (!Mouse.current.leftButton.wasPressedThisFrame) return;
            
            OnChangeState?.Invoke(ClickStateType.Click);
        }

        public void Exit()
        { }

        
    }
}