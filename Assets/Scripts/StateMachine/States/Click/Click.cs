using System;
using System.Collections.Generic;
using DefaultNamespace.Extensions;
using DefaultNamespace.Handlers;
using DefaultNamespace.Handlers.ClickHandler;
using DefaultNamespace.Signals;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using Zenject;

namespace Handlers.ClickHandler.States
{
    public class Click : IState<ClickStateType>
    {
        private readonly Tilemap _tilemap;
        private ClickContext _context;
        private SignalBus _signalBus;

        public Click(Tilemap tilemap, ClickContext context, SignalBus signalBus)
        {
            _tilemap = tilemap;
            _context = context;
            _signalBus = signalBus;
        }

        public event Action<ClickStateType> OnChangeState;

        public void Enter()
        {
            var tile = TilemapUtils.GetHoveredTile(_tilemap);

            if (!_tilemap.HasTile(tile))
            {
                OnChangeState?.Invoke(ClickStateType.Idle);
                return;
            }
                
            _context.StartTile = tile;
            _context.ClickTime = 0;
        }
        
        public void Tick()
        {
            if (Time.time - _context.ClickTime > 0.6f)
            {
                OnChangeState?.Invoke(ClickStateType.Hold);
                return;
            }
            
            if (!Mouse.current.leftButton.wasReleasedThisFrame) return;

            var poistionToSend = new HashSet<Vector3Int>() { _context.StartTile.Value };
            _signalBus.Fire(new SoilSelectedSignal(){positions = poistionToSend});
            _context.ClickTime = 0;
            _context.StartTile = null;
        }

        public void FixedTick()
        { }

        public void Exit()
        {
           
        }
    }
}