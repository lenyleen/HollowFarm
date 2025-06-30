using System.Collections.Generic;
using Service;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DefaultNamespace.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SoilActionButtonData", menuName = "ScriptableObjects/SoilActionButtonData")]
    public class SoilActionsButtonsData : SerializedScriptableObject
    {
        public Dictionary<SoilActionType, Sprite> Actions;
    }
}