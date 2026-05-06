using Microsoft.Xna.Framework;

namespace GameEngine.Services {
    public interface IDraggable
    {
        void OnDragStart(MouseButton mb);
        void OnDragUpdate(MouseButton mb, Vector2 position);
        void OnDragEnd(MouseButton mb);
    }
}