using DefaultNamespace.Models;

namespace DefaultNamespace.ViewModels.Interfaces
{
    public interface ITileClickObserver
    {
        public void TileClicked(Soil soil);
    }
}