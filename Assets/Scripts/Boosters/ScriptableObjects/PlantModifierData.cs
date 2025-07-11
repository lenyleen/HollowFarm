using UnityEngine;

namespace DefaultNamespace.Boosters.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlantModifierData", menuName = "ScriptableObjects/PlantModifierData")]
    public class PlantModifierData : ScriptableObject
    {
        [Header("Main")] 
        [SerializeField] private string _name;
        [SerializeField] private PlanModifierValueType _valueType;
        
        [Header("Modifier Values")] 
        [SerializeField] private float value;
        [SerializeField] private Color _color;
        [SerializeField] private PlantProperty _plantProperty;
        
        
        public string Name => _name;
        public float Duration { get; set; }
        public Color Color => _color;
        public float Value => value;

        
        public PlanModifierValueType ValueType => _valueType;
        
        public PlantProperty PlantProperty => _plantProperty;
    }

    public enum PlanModifierValueType
    {
        Float,
        Bool
    }
    
    public enum PlantProperty
    {
        GrowthSpeed,
        WaterRetention,
        HarvestMultiplier,
        CanRevive,
        IsImmortal,
        AutoWater
    }
}