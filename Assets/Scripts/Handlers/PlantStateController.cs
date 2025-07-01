using System;
using System.Collections.Generic;
using DefaultNamespace.Models;
using DefaultNamespace.Models.Interfaces;
using DefaultNamespace.Models.PlantModelStates;
using Handlers.ClickHandler;
using Service;
using UniRx;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.Handlers
{
    public class PlantStateController : IInitializable, IDisposable, ITimeUpdatable 
    {
        private readonly IPlantStateContext _plantStateContext;
        private readonly PlantModelSM<IState<PlantStatus>> _stateMachine;
        private readonly IFactory<PlantStatus,IPlantStateContext,IState<PlantStatus>>  _plantStateFactory;
        private readonly ITimeHandler _timeHandler;
        private readonly List<(Func<bool> condition, PlantStatus status)> _prioritizedStates;
        private CompositeDisposable  _disposables;
        private float second;

        public PlantStateController(IPlantStateContext plantStateContext, PlantModelSM<IState<PlantStatus>> stateMachine, 
            IFactory<PlantStatus,IPlantStateContext,IState<PlantStatus>> plantStateFactory, ITimeHandler timeHandler)
        {
            _plantStateContext = plantStateContext;
            _stateMachine = stateMachine;
            _plantStateFactory = plantStateFactory;
            _timeHandler = timeHandler;
            _disposables = new CompositeDisposable();
        }

        public void Initialize()
        {
            _plantStateContext.CurrentStatus.Skip(1).ThrottleFrame(1).Subscribe(ChangeState).AddTo(_disposables);
            
            _stateMachine.ChangeState(_plantStateFactory.Create(PlantStatus.Growing,_plantStateContext));
            
            _timeHandler.Subscribe(this);
            Debug.Log("initialized");
        }


        private void ChangeState(PlantStatus status)
        {
            if (status == PlantStatus.Harvested)
            {
                Dispose();
                return;
            }
            
            if(status == PlantStatus.CanBeHarvested)
                _timeHandler.Unsubscribe(this);
            
            _stateMachine.ChangeState(_plantStateFactory.Create(status, _plantStateContext));
        }
       
        public void UpdateTime()
        {
            _stateMachine.CurrentState.Tick();
            second++;
            Debug.Log(second);
        }
        public void Dispose()
        {
            _timeHandler.Unsubscribe(this);
            _disposables.Dispose();
        }

        
    }
}