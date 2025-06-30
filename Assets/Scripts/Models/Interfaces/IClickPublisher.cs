

using UnityEngine;

namespace DefaultNamespace.Models.Interfaces
{
    public interface IClickPublisher
    {
        
        public void Subscribe(IClickObserver clickObserver);
        
        public void Unsubscribe(IClickObserver clickObserver);
    }
}