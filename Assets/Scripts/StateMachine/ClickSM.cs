using DefaultNamespace.Factory;
using Handlers.ClickHandler;
using Handlers.ClickHandler.States;
using Unity.VisualScripting;
using Zenject;


namespace DefaultNamespace.Handlers.ClickHandler
{
    public class ClickSM : IStateMachine<ClickStateType>
    {
        public IState<ClickStateType> CurrentState { get; private set; }
        
        private readonly IStateFactory<ClickStateType> _stateFactory;

        public ClickSM(IStateFactory<ClickStateType> stateFactory)
        {
            _stateFactory = stateFactory;
            CurrentState = _stateFactory.Create(ClickStateType.Idle);
            CurrentState.OnChangeState += ChangeState;
        }

        public void ChangeState(ClickStateType newState)
        {
            CurrentState.Exit();
            CurrentState.OnChangeState -= ChangeState;
            CurrentState = _stateFactory.Create(newState);
            CurrentState.Enter();
            CurrentState.OnChangeState += ChangeState;
        }
    }
}