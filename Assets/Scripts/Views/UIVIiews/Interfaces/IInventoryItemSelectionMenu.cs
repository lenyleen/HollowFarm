using DefaultNamespace.ViewModels.UiVm;
using Service.UI;

namespace DefaultNamespace.Views.UIVIiews.Interfaces
{
    public interface IInventoryItemSelectionMenu : IUiElement
    {
        void Show();
        void Hide();
        void ShowButton(InventoryItemButton _button);
    }
}