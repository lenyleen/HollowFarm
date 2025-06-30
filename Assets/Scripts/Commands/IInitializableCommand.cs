using System.Collections.Generic;
using DefaultNamespace.ViewModels.Interfaces;

namespace DefaultNamespace.Commands
{
    public interface IInitializableCommand : ICommand<ICommandPerformer>
    {
        public void Initialize(IEnumerable<ICommandPerformer> targets);
    }
}