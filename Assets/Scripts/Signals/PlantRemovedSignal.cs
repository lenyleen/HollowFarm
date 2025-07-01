using DefaultNamespace.Models;

namespace DefaultNamespace.Signals
{
    public class PlantRemovedSignal
    {
        public PlantModel Model { get; }

        public PlantRemovedSignal(PlantModel model)
        {
            Model = model;
        }
    }
}