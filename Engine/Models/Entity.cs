using GameEngine.Core;
using GameEngine.Core.SceneSystem;
using GameEngine.Specs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Models
{
    public abstract class Entity : GameObject, IUpdate, IDraw
    {
        public Transform Transform { get; }
        public int OrderInLayer { get; set; }

        protected Entity(Vector2 position, Vector2 scale, float rotation = 0f, bool isStatic = false) : base(isStatic)
        {
            Transform = new Transform
            {
                LocalPosition = position,
                LocalScale = scale,
                LocalRotation = rotation
            };
        }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(GameTime gameTime, SpriteBatch sb) { }

        public void Destroy() => SceneManager.Remove(this);
        public override void OnToggled(bool value) {
            if (value) SceneManager.Add(this);
            else SceneManager.Remove(this);
        }
    }
}