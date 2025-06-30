using System.Collections.Generic;
using DefaultNamespace.DataObjects;
using DefaultNamespace.Models;
using DefaultNamespace.ScriptableObjects;
using DefaultNamespace.ViewModels;
using UnityEngine;

namespace DefaultNamespace.Signals
{
    public class ItemUsedSignal<T> where T : ICanBeDrawnInInventory
    {
        public InventoryItem Seed;
        public List<Vector3Int> Positions;

    }
}
