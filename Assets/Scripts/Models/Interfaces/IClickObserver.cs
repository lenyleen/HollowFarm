

using UnityEngine;

namespace DefaultNamespace.Models.Interfaces
{
    public interface IClickObserver
    {
        public void OnClick(Vector3Int clickPosition);
    }
}