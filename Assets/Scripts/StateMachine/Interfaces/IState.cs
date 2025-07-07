using System;

namespace Handlers.ClickHandler
{
    public interface IState<TEnum> where TEnum : Enum
    {
        public event Action<TEnum> OnChangeState;
        public void Enter();
        public void Exit();
        public void Tick();
    }
}