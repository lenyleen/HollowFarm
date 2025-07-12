using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Views.GameViews.OutlineManager
{
    public class OutlineManager : IOutlineManager, IDisposable
    {
        private readonly Dictionary<Renderer, MaterialPropertyBlock> _propertyBlocks = new();
        
        private static readonly int OutlineColorID = Shader.PropertyToID("_OutlineColor");

        public void RegisterRenderer(Renderer renderer)
        {
            if (_propertyBlocks.ContainsKey(renderer)) return;
            
            var block = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(block);
            _propertyBlocks[renderer] = block;
        }
        public void SetObjectOutline(Renderer renderer, Color color)
        {
            if (!_propertyBlocks.ContainsKey(renderer))
                RegisterRenderer(renderer);
            
            var block = _propertyBlocks[renderer];
            block.SetColor(OutlineColorID, color);
            

            renderer.SetPropertyBlock(block);
        }

        public void UnregisterRenderer(Renderer renderer)
        {
            _propertyBlocks.Remove(renderer);
        }

        public void Dispose()
        {
            foreach (var kvp in _propertyBlocks)
            {
                SetObjectOutline(kvp.Key, Color.clear);
            }
            _propertyBlocks.Clear();
        }
    } 
    [System.Serializable]
    public class OutlineData
    {
        public Renderer renderer;
        public Color color = Color.yellow;
        public float thickness = 0.005f;
    }
}