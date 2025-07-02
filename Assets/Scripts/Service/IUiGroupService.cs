namespace Service
{
    public interface IUiGroupService : IUiService
    {
        public void RegisterGroup(IUiService group, UiType type);
        public void UnregisterGroup(IUiService group, UiType type);
    }
}