using DefaultNamespace.Models.Interfaces;

namespace DefaultNamespace.Handlers
{
    public interface ITimeHandler 
    {
        public void Subscribe(ITimeUpdatable updatable);
        public void Unsubscribe(ITimeUpdatable updatable);
    }
}