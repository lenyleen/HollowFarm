using DefaultNamespace.ViewModels.Interfaces;

namespace DefaultNamespace.Models.Interfaces
{
    public interface ITileClickedPublisher
    {
        
        
        public void Unsubscribe(ITileClickObserver observer);
        
        public void Subscribe(ITileClickObserver observer);
        
    }
}