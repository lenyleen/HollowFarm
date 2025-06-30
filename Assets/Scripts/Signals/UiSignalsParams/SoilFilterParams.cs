

using UnityEngine;

namespace DefaultNamespace.Signals.UiSignalsParams
{
    public class SoilFilterParams
    {
        public Vector3 Position { get; }
        
        public SoilFilterParams(Vector3 position)
        {
            Position = position;
        }
    }
}