using System;
using System.Collections.Generic;
using DefaultNamespace.ViewModels.UiVm;
using Service;

namespace DefaultNamespace.Handlers
{
    public class InGameOverlayUiHandler : UiGroupHandler
    {
        public InGameOverlayUiHandler(List<IUiElement> uiElements, UiType type) 
            : base(uiElements, type)
        {
        }

        public override void Close()
        {
            while(ShowedUiElements.Count > 0)
            {
                var popUp = ShowedUiElements.Pop();
                popUp.Hide();
            }

            IsFilled = false;
        }
    }
}