using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

namespace DefaultNamespace.Extensions
{
    public class TilemapUtils
    {
        public static Vector3Int GetHoveredTile(Tilemap tilemap)
        {
            var mousePos = Mouse.current.position.ReadValue();
            var worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            worldPos.z = 0;
            return tilemap.WorldToCell(worldPos);
        }
        
        public static Vector3 GetWorldCenter(List<Vector3Int> cellPositions, Tilemap tilemap)
        {
            if (cellPositions == null || cellPositions.Count == 0)
                return Vector3.zero;

            int sumX = 0, sumY = 0, sumZ = 0;

            foreach (var pos in cellPositions)
            {
                sumX += pos.x;
                sumY += pos.y;
                sumZ += pos.z;
            }

            int count = cellPositions.Count;
            var centerCell = new Vector3Int(sumX / count, sumY / count, sumZ / count);

            return tilemap.GetCellCenterWorld(centerCell);
        }
        
    }
}