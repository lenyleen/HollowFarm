using UnityEngine;

namespace DefaultNamespace.Boosters.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlantModifierData", menuName = "ScriptableObjects/PlantModifierData")]
    public class PlantModifierData : ScriptableObject
    {
        [Header("Main")] 
        [SerializeField] private string _name;
        [SerializeField] private PlanModifierValueType _valueType;
        [SerializeField] private float _duration = 60f;
        
        [Header("Modifier Values")]
        [SerializeField] 
        
    }

    public enum PlanModifierValueType
    {
        Float,
        Bool
    }
}