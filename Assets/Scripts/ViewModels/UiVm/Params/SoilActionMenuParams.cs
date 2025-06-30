using System.Collections.Generic;
using DefaultNamespace.ViewModels.Interfaces;

namespace DefaultNamespace.ViewModels.UiVm.Params
{
    public class SoilActionMenuParams
    {
        public List<ICommandPerformer> CommandPerformers { get; }

        public SoilActionMenuParams(List<ICommandPerformer> commandPerformers)
        {
            CommandPerformers = commandPerformers;
        }
    }
}