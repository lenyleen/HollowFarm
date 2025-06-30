using System;
using Unity.VisualScripting;

namespace Handlers.ClickHandler
{
    public interface IStateMachine <TEnumType>  where TEnumType : Enum
    {
        public IState<TEnumType> CurrentState { get;}
        
        public void ChangeState(TEnumType newState);
    }
}