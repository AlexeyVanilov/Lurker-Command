using GameEngine.Core;
using GameEngine.Core.InputSystem;
using GameEngine.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace LurkerCommand.Core {
    public sealed class CameraMovement : Entity
    {
        public Camera2D camera;
        private const float Speed = 15f;
        private Vector2 _inputDirection;
        private const float ZoomFactor = 0.1f;
        private const float EdgeThreshold = 10f;

        private float minX, minY, maxX, maxY;
        public CameraMovement(Camera2D camera, Vector2 startPosition) : base(Vector2.Zero, Vector2.One, 0f, false)
        {
            this.camera = camera;
            MoveCamera(startPosition);

            Bind(Keys.W, Keys.Up, new Vector2(0, -1));
            Bind(Keys.S, Keys.Down, new Vector2(0, 1));
            Bind(Keys.A, Keys.Left, new Vector2(-1, 0));
            Bind(Keys.D, Keys.Right, new Vector2(1, 0));
        }
        private void Bind(Keys k1, Keys k2, Vector2 direction)
        {
            InputManager.Add(k1, KeyType.Held, () => _inputDirection += direction);
            InputManager.Add(k2, KeyType.Held, () => _inputDirection += direction);
        }
        public override void Update(GameTime gameTime)
        {
            Vector2 finalMovement = Vector2.Zero;

            if (InputManager.IsMouseButtonDown(MouseButton.Middle))
            {
                finalMovement = -InputManager.MouseDelta / camera.Zoom;
            }
            else
            {
                if (_inputDirection != Vector2.Zero)
                {
                    finalMovement = Vector2.Normalize(_inputDirection) * Speed;
                }

                finalMovement += GetEdgeDirection() * Speed;
            }

            if (finalMovement != Vector2.Zero) MoveCamera(camera.Position + finalMovement);

            HandleZoom();

            _inputDirection = Vector2.Zero;
        }

        private Vector2 GetEdgeDirection()
        {
            Vector2 dir = Vector2.Zero;
            Vector2 mousePos = InputManager.MousePosition;
            var viewport = camera.graphics.Viewport;

            if (mousePos.X <= EdgeThreshold) dir.X = -1;
            else if (mousePos.X >= viewport.Width - EdgeThreshold) dir.X = 1;

            if (mousePos.Y <= EdgeThreshold) dir.Y = -1;
            else if (mousePos.Y >= viewport.Height - EdgeThreshold) dir.Y = 1;

            return dir;
        }

        private void HandleZoom()
        {
            int scroll = InputManager.ScrollDelta;
            if (scroll != 0)
            {
                camera.Zoom = MathHelper.Clamp(camera.Zoom + (Math.Sign(scroll) * ZoomFactor), camera.MinZoom, camera.MaxZoom);

                UpdateBounds();

                ClampPosition(camera.Position);
            }
        }

        private void UpdateBounds()
        {
            var viewport = camera.graphics.Viewport;
            float invZoom = 1.0f / camera.Zoom;

            float halfViewWidth = (viewport.Width * 0.5f) * invZoom;
            float halfViewHeight = (viewport.Height * 0.5f) * invZoom;

            minX = halfViewWidth;
            maxX = Field.MapWidth - halfViewWidth;
            minY = halfViewHeight;
            maxY = Field.MapHeight - halfViewHeight;
        }

        public void MoveCamera(Vector2 targetPosition)
        {
            UpdateBounds();
            ClampPosition(targetPosition);
        }

        public void ClampPosition(Vector2 targetPosition)
        {
            camera.Position = new Vector2(
                maxX > minX ? MathHelper.Clamp(targetPosition.X, minX, maxX) : Field.MapWidth * 0.5f,
                maxY > minY ? MathHelper.Clamp(targetPosition.Y, minY, maxY) : Field.MapHeight * 0.5f
            );
        }
    }
}