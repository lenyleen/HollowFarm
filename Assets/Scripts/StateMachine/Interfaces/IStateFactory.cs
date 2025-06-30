using Handlers.ClickHandler;
using Zenject;

namespace DefaultNamespace.Factory
{
    public interface IStateFactory<TEnum> : IFactory<TEnum, IState<TEnum>> where TEnum : System.Enum
    {
        
    }
}