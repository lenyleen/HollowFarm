using DefaultNamespace.ViewModels.UiVm;

namespace Service
{
    public interface IUiGroupService 
    {
        public void RegisterGroup(IUiService group, UiType type);
        public void UnregisterGroup(IUiService group, UiType type);
        public void Show<TUi, TParams>(TParams args, UiType type) where TUi : IUiElement<TParams>;
        public TUi ShowDialogMenu<TUi, TParams, TResult>(TParams args, UiType type) where TUi : IDialogMenu<TParams, TResult>;
        public void Close();
    }
}