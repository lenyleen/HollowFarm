using Service;

namespace DefaultNamespace.Handlers
{
    public interface IUiGroupHandler : IUiService
    {
        public UiType GroupType { get; }
        public bool IsFilled { get; }
    }
}