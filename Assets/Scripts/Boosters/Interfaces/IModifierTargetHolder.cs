using System;

namespace DefaultNamespace.Boosters.Interfaces
{
    public interface IModifierTargetHolder
    { 
        public event Action<IPlantModifierApplicable> OnModifierApplied;
    }
}