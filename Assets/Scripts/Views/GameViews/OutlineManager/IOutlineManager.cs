using UnityEngine;

namespace Views.GameViews.OutlineManager
{
    public interface IOutlineManager
    {
        public void SetObjectOutline(Renderer renderer, Color color);
        public void UnregisterRenderer(Renderer renderer);
        public void RegisterRenderer(Renderer renderer);
    }
}