using System;
using System.Buffers;
using UnityEngine;
using UnityEngine.Tilemaps;
using DefaultNamespace.Models;
using Zenject;

namespace DefaultNamespace.View
{
    public class SoilTileHoverVisualizer : MonoBehaviour
    {
        [SerializeField] private float liftHeight = 0.4f;
        [SerializeField] private SpriteRenderer liftedSprite;
        [SerializeField] private Tilemap tilemap;

        public void HoverTile(Vector3Int cellPosition)
        {
            var tileSprite = tilemap.GetSprite(cellPosition);
            liftedSprite.sprite = tileSprite;
            Vector3 liftedPos = tilemap.GetCellCenterWorld(cellPosition) + Vector3.up * liftHeight;
            liftedSprite.sortingOrder = -(int)liftedPos.y;
            liftedSprite.transform.position = liftedPos;
            liftedSprite.enabled = true;
        }
        public void Reset()
        {
            liftedSprite.enabled = false;
            liftedSprite.transform.position = new Vector3(float.MinValue, float.MinValue, float.MinValue);
            liftedSprite.sprite = null;
        }

        public class Pool : MemoryPool<Vector3Int, SoilTileHoverVisualizer>
        {
            protected override void Reinitialize(Vector3Int p1, SoilTileHoverVisualizer item)
            {
                item.Reset();
                item.HoverTile(p1);
            }

            protected override void OnDespawned(SoilTileHoverVisualizer item)
            {
                item.Reset();
            }
        }
    }
}

