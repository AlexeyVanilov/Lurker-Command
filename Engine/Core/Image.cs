using GameEngine.Models;
using GameEngine.Specs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Core {
    public class Image : Entity, IRect
    {
        public Texture2D Texture;
        public Color Color;

        public Image(Texture2D texture, Vector2 position, Vector2 scale, Color color, float rotation = 0f, bool isStatic = false)
            : base(position, scale, rotation, isStatic)
        {
            Texture = texture;
            Color = color;
        }

        public Point GetSize() => (Transform.LocalScale * new Vector2(Texture.Width, Texture.Height)).ToPoint();
        public Rectangle GetBounds()
        {
            Vector2 worldPos = Transform.WorldPosition;
            Point size = GetSize();
            return new Rectangle(worldPos.ToPoint(), size);
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            sb.Draw(Texture, Transform.WorldPosition, null, Color,
                Transform.LocalRotation, Vector2.Zero, Transform.LocalScale, SpriteEffects.None, 0f);
        }
    }
}