using UnityEngine;

namespace DefaultNamespace.ScriptableObjects
{
    public interface ICanBeDrawnInInventory
    {
        public string Name { get; }
        public string Description { get; }
        public Sprite Icon { get; }
    }
}