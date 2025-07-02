using System.Collections.Generic;
using DefaultNamespace.ViewModels.Interfaces;

namespace DefaultNamespace.ViewModels.UiVm.Params
{
    public class SoilActionMenuParams
    {
        public IEnumerable<ICommandPerformer> CommandPerformers { get; }

        public SoilActionMenuParams(IEnumerable<ICommandPerformer> commandPerformers)
        {
            CommandPerformers = commandPerformers;
        }
    }
}