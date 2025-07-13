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
            Debug.Log(Shader.PropertyToID("_OutlineColor"));
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

            // Для SpriteRenderer — всегда актуальный спрайт!
            if (renderer is SpriteRenderer spriteRenderer && spriteRenderer.sprite != null)
            {
                renderer.material.SetTexture("_MainTex", spriteRenderer.sprite.texture);
                
                renderer.material.SetColor("_OutlineColor", color);
                renderer.material.SetTexture("_MainTex", spriteRenderer.sprite.texture);
            }
            /*else if (renderer.sharedMaterial.HasProperty("_MainTex"))
                block.SetTexture("_MainTex", renderer.sharedMaterial.GetTexture("_MainTex"));*/

            /*block.SetColor(OutlineColorID, color);

            renderer.SetPropertyBlock(block);*/
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