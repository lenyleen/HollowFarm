using Service;

namespace DefaultNamespace.Commands
{
    public interface ICommand <TTarget>
    {
        public SoilActionType  Action { get; }
        public void Execute();
    }
}