using System.Collections.Generic;
using DefaultNamespace.Models;
using DefaultNamespace.ScriptableObjects;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace DefaultNamespace.Handlers
{
    public class SoilGrid
    {
        private Dictionary<Vector3Int, Soil> _grid;
        private SoilData _defaultSoilData;
        
        public SoilGrid(Tilemap tilemap, SoilData defaultSoilData)
        {
            _grid = new Dictionary<Vector3Int, Soil>();
            _defaultSoilData = defaultSoilData;
            BoundsInt bounds = tilemap.cellBounds;
            foreach (var pos in bounds.allPositionsWithin)
            {
                if (tilemap.HasTile(pos))
                {
                    _grid[pos] = new Soil( _defaultSoilData, pos,tilemap.CellToWorld(pos));
                }
            }
        }

        public Soil GetSoilAt(Vector3Int position) => _grid.GetValueOrDefault(position);
    }
}

