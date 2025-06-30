using System.Collections.Generic;
using DefaultNamespace.Handlers;
using DefaultNamespace.Models.Interfaces;
using DefaultNamespace.ScriptableObjects;
using DefaultNamespace.ViewModels;
using UniRx;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DefaultNamespace.Models
{
    public class FieldModel
    {
        private readonly Dictionary<Vector3Int, Soil>  _tiles = new Dictionary<Vector3Int, Soil>();
        private readonly SoilData _defaultSoilData;
        public Soil GetSoil(Vector3Int pos) =>
            _tiles.GetValueOrDefault(pos);

        public IEnumerable<Soil> All => _tiles.Values;

        public FieldModel(Tilemap tilemap, SoilData defaultSoilData)
        {
            _tiles = new Dictionary<Vector3Int, Soil>();
            _defaultSoilData = defaultSoilData;
            BoundsInt bounds = tilemap.cellBounds;
            foreach (var pos in bounds.allPositionsWithin)
            {
                if (tilemap.HasTile(pos))
                {
                    _tiles[pos] = new Soil( _defaultSoilData, pos,tilemap.CellToWorld(pos));
                }
            }
        }
    }
}