using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Extensions;
using DefaultNamespace.Signals;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;
using Zenject;

namespace Handlers.ClickHandler.States
{
    public class Hold : IState<ClickStateType>
    {
        private readonly ClickContext _context;
        private readonly Tilemap _tilemap;
        private readonly SignalBus _signalBus;
        private HashSet<Vector3Int> _currentSelection;
        

        public Hold(ClickContext context, Tilemap tilemap, SignalBus signalBus)
        {
            _context = context;
            _tilemap = tilemap;
            _signalBus = signalBus;
        }

        public event Action<ClickStateType> OnChangeState;
        public void Enter(){}
        
        public void Tick()
        {
            if (Mouse.current.leftButton.isPressed && _context.StartTile.HasValue)
            {
                Vector3Int endTile = TilemapUtils.GetHoveredTile(_tilemap);
                _currentSelection = GetRectangle(_context.StartTile.Value, endTile);
                Highlight(_currentSelection);
            }

            if (!Mouse.current.leftButton.wasReleasedThisFrame) return;
            
            FinalizeSelection(_currentSelection);
        }

        public void FixedTick()
        {}

        public void Exit()
        {
            _tilemap.RefreshAllTiles();
        }
        
        private HashSet<Vector3Int> GetRectangle(Vector3Int a, Vector3Int b)
        {
            var result = new HashSet<Vector3Int>();
            int minX = Mathf.Min(a.x, b.x);
            int maxX = Mathf.Max(a.x, b.x);
            int minY = Mathf.Min(a.y, b.y);
            int maxY = Mathf.Max(a.y, b.y);

            for (int x = minX; x <= maxX; x++)
            for (int y = minY; y <= maxY; y++)
                result.Add(new Vector3Int(x, y, 0));

            return result;
        }

        private void Highlight(HashSet<Vector3Int> tiles)
        {
            _tilemap.RefreshAllTiles();

            foreach (var tile in tiles)
            {
                if (!_tilemap.HasTile(tile)) continue;
                
                _tilemap.SetTileFlags(tile, TileFlags.None);
                _tilemap.SetColor(tile, Color.green);
            }
        }

        private void FinalizeSelection(HashSet<Vector3Int> tiles)
        {
            if(tiles == null || tiles.Count == 0 )
            {
                Debug.Log("Выделено 0 клеток.");
                OnChangeState?.Invoke(ClickStateType.Idle);
                return;
            }
            Debug.Log($"Выделено {tiles.Count} клеток. Первая клетка: {tiles.First()}");
            _signalBus.Fire(new SoilSelectedSignal(){positions =  tiles});
            OnChangeState?.Invoke(ClickStateType.Idle);
            
            _context.StartTile = null;
            _context.ClickTime= 0;
        }
    }
}