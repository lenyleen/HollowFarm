using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.ViewModels.UiVm;
using Service;

namespace DefaultNamespace.Handlers
{
    public class GeneralUiHandler : UiGroupHandler
    {
        public GeneralUiHandler(List<IUiElement> uiElements, UiType type) 
            : base(uiElements, type)
        {
        }

        public override void Close()
        {
            if(!ShowedUiElements.Any()) return;
            
            var popUp = ShowedUiElements.Pop();
            popUp.Hide();

            if (!ShowedUiElements.Any())
                IsFilled = false;
        }
    }
}