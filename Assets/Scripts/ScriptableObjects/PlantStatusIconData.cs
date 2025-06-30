using System;
using System.Collections.Generic;
using Service;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace DefaultNamespace.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlantStatusIconData", menuName = "ScriptableObjects/PlantStatusIconData", order = 1)]
    public class PlantStatusIconData : SerializedScriptableObject
    { 
        public int maxIconsCount = 4; // Maximum number of icons to display
        public Dictionary<PlantStatus, Sprite> statusIcons; 
    }  
}