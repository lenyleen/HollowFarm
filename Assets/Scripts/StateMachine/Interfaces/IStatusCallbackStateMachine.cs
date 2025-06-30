using System;
using Handlers.ClickHandler;

namespace StateMachine.Interfaces
{
    public interface IStatusCallbackStateMachine<TEnum> : IStateMachine<TEnum> where TEnum : Enum
    {
        public event Action<TEnum> OnChangeStatus; 
    }
}