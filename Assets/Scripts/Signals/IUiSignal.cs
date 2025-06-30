using DefaultNamespace.ViewModels.UiVm;
using UnityEngine.EventSystems;

namespace DefaultNamespace.Signals
{
    public interface IUiSignal <TUi, TParams> where TUi : IUiElement<TParams>
    {
        public TParams GetParams();
    }
}