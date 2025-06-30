using Service.UI;

namespace DefaultNamespace.Views.UIVIiews.Interfaces
{
    public interface IInventoryItemSelectionMenu
    {
        void Show();
        void Hide();
        void ShowButton(InventoryItemButton _button);
    }
}