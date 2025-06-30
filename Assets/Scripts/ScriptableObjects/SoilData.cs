using DefaultNamespace.ScriptableObjects.Service;
using UnityEngine;

namespace DefaultNamespace.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SoilData", menuName = "ScriptableObjects/SoilData", order = 1)]
    public class SoilData : ScriptableObject
    {
        public float wateredDurationInMinutes = 5f;
        public float fertilizedDurationInMinutes = 10f;
        
        public float waterMultiplier = 1.05f;
        public float nutrientMultiplier = 1.05f;
    }
}