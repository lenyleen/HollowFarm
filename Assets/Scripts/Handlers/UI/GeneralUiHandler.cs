using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.ViewModels.UiVm;
using Service;

namespace DefaultNamespace.Handlers
{
    public class GeneralUiHandler : UiGroupHandler
    {
        public GeneralUiHandler(Dictionary<Type, IUiElement> uiElements, 
            Stack<IUiElement> showedUiElements, UiType type) 
            : base(uiElements, showedUiElements, type)
        {
        }

        public override void Close()
        {
            if(!ShowedUiElements.Any()) return;
            
            var popUp = ShowedUiElements.Pop();
            popUp.Hide();
        }
    }
}