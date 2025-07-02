using System.Collections.Generic;
using Service;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace DefaultNamespace.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SoilActionButtonData", menuName = "ScriptableObjects/SoilActionButtonData")]
    public class SoilActionsButtonsData : SerializedScriptableObject
    {
        public Dictionary<SoilActionType, Sprite> Actions = new Dictionary<SoilActionType, Sprite>();
    }
}  