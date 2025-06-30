using UnityEngine;

namespace Handlers.ClickHandler.States
{
    public class ClickContext
    {
        public float ClickTime { get; set; }
        public Vector3Int? StartTile { get; set; }
    }
}