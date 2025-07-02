using System;
using DefaultNamespace.Factory;
using Handlers.ClickHandler.States;
using Service;
using UnityEngine;
using Zenject;

namespace Handlers.ClickHandler
{
    public class ClickStateFactory : IStateFactory<ClickStateType>
    {
        private readonly DiContainer _container;
        private readonly IUiService _uiService;
        private readonly ClickContext _clickContext;

        public ClickStateFactory(DiContainer container, IUiService uiService, ClickContext clickContext)
        {
            _container = container;
            _uiService = uiService;
            _clickContext = clickContext;
        }

        public IState<ClickStateType> Create(ClickStateType type)
        {
            return type switch
            {
                ClickStateType.Idle => _container.Instantiate<Idle>(),
                ClickStateType.Click => _container.Instantiate<Click>(new[] { _clickContext }),
                ClickStateType.Hold => _container.Instantiate<Hold>(new[] { _clickContext }),
                ClickStateType.Ui => _container.Instantiate<UiState>(new [] {(Action)(() => _uiService.Close())}),
                _ => throw new Exception("Not implemented state")
            };
        }
    }
}