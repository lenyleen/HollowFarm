using System;
using System.Collections.Generic;
using DefaultNamespace.Models.Interfaces;
using DefaultNamespace.Signals;
using Handlers.ClickHandler;
using NUnit.Framework;
using Service;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using Zenject;

namespace DefaultNamespace.Handlers
{
    public class InputHandler : ITickable, IInitializable, IDisposable
    {
        private readonly IStateMachine<ClickStateType> _stateMachine;
        private readonly IClickBlockerService _clickBlockerService;
        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public InputHandler(IStateMachine<ClickStateType> stateMachine, IClickBlockerService clickBlockerService)
        {
            _stateMachine = stateMachine;
            _clickBlockerService = clickBlockerService;
        }
        
        public void Initialize()
        {
            _clickBlockerService.IsUiClosingClicksEnabled.Subscribe(EnterUiState)
                .AddTo(_disposable);
        }
        
        public void Tick()
       {
           _stateMachine.CurrentState.Tick();
       }

        private void EnterUiState(bool enter)
        {
            _stateMachine.ChangeState(enter ? ClickStateType.Ui : ClickStateType.Idle);
        }

       

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}