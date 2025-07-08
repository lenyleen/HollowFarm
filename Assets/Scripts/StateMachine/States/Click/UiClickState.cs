using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Handlers.ClickHandler.States
{
    public class UiState : IState<ClickStateType>
    {
        private Action _onClicked;

        public event Action<ClickStateType> OnChangeState;

        public UiState(Action onClicked)
        {
            _onClicked = onClicked;
        }
        public void Enter() {}
        
        public void Tick()
        {
            if(!Mouse.current.leftButton.wasPressedThisFrame)
                return;

            if (IsPointerOverUI()) return;
            
            _onClicked?.Invoke();
        }

        public void FixedTick()
        {}

        private bool IsPointerOverUI()
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Mouse.current.position.ReadValue()
            };

            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            return results.Count > 0;
        }

        public void Exit()
        {
            _onClicked = null;
            
        }
    }
}