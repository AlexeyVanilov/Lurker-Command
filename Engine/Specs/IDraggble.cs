using GameEngine.Core.InputSystem;
using Microsoft.Xna.Framework;

namespace GameEngine.Specs {
    public interface IDraggable
    {
        void OnDragStart(MouseButton mb);
        void OnDragUpdate(MouseButton mb, Vector2 position);
        void OnDragEnd(MouseButton mb);
    }
}