using System;
using DefaultNamespace.Views;

namespace Views.GameViews
{
    public interface IDespawnable
    {
        public void InitializeAsDespawnable(Action<PlantView> onDespawn);
    }
}