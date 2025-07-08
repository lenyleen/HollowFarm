using System;

namespace DefaultNamespace.Models.Interfaces
{
    public interface ITimeUpdatable
    {
        public void UpdateTime();
        public void FixedUpdateTime();
    }
}