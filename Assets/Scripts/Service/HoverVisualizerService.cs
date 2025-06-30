using System.Collections.Generic;
using DefaultNamespace.View;
using Service.UI;
using UnityEngine;

namespace Service
{
    public class HoverVisualizerService : IHoverVisualizerService
    {
        private readonly SoilTileHoverVisualizer.Pool _pool;
        private Dictionary<Vector3Int,SoilTileHoverVisualizer> _spawnedVisualizers;

        public HoverVisualizerService(SoilTileHoverVisualizer.Pool pool)
        {
            _pool = pool;
            _spawnedVisualizers = new Dictionary<Vector3Int, SoilTileHoverVisualizer>();
        }

        public void Hover(Vector3Int position, int direction = 1)
        {
            if(direction < 0)
            {
                Unhover(position);
                return;
            }
            _spawnedVisualizers.TryAdd(position, _pool.Spawn(position));
        }

        public void Unhover(Vector3Int position)
        {
            if(!_spawnedVisualizers.Remove(position, out var visualizer)) return;

            _pool.Despawn(visualizer);
        }
    }
}