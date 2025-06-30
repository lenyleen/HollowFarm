using System.Collections.Generic;
using UnityEngine;

namespace Service.UI
{
    public interface IHoverVisualizerService
    {
        public void Hover(Vector3Int position, int direction = 1);
        public void Unhover(Vector3Int position);
    }
}