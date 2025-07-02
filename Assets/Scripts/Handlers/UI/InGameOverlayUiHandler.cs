using System;
using System.Collections.Generic;
using DefaultNamespace.ViewModels.UiVm;
using Service;

namespace DefaultNamespace.Handlers
{
    public class InGameOverlayUiHandler : UiGroupHandler
    {
        public InGameOverlayUiHandler(Dictionary<Type, IUiElement> uiElements, 
            Stack<IUiElement> showedUiElements, UiType type) 
            : base(uiElements, showedUiElements, type)
        {
        }

        public override void Close()
        {
            while(ShowedUiElements.Count > 0)
            {
                var popUp = ShowedUiElements.Pop();
                popUp.Hide();
            }
        }
    }
}