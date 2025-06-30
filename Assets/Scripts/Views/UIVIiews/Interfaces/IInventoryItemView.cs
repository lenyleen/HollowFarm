using System;

namespace DefaultNamespace.Views.UIVIiews.Interfaces
{
    public interface IInventoryItemView : IDisposable
    {
        public void UpdateQuantity(int quantity);

        public void Dispose();
    }
}