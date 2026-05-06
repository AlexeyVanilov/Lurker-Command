using GameEngine.Core;
using GameEngine.Models;
using GameEngine.Specs;
using LurkerCommand.MapSystem;
using LurkerCommand.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LurkerCommand.GameSystem
{
    public sealed class Unit : Entity, IGrid, IDraggable, IRect, IPoolable
    {
        private UnitStats _stats;
        public Team team;
        public bool isPlayer;
        public Text valueText;
        public int Value
        {
            get => _stats.value;
            set
            {
                _stats.value = value;
                if (_stats.value > UnitStats.maxValue) {
                    _stats.value = 1;
                }
                else if(_stats.value < 1) {
                    UnitSystem.Kill(this);
                    return;
                }
                UnitSystem.UpdateText(this);
            }
        }
        public int Moves
        {
            get => _stats.moves;
            set {
                _stats.moves = MathHelper.Clamp(value, 0, UnitStats.maxMoves);
                team?.ConsumeMove();
            }
        }
        public bool giveBonus = true;
        public Point gridPosition { get; set; }
        public bool IsInPool { get; set; }
        public Cell currentCell;
        public bool isVisible = true;
        public Unit unitClone;
        public Unit() : base(Vector2.Zero, Vector2.One) => OrderInLayer = 2;
        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            if (!isPlayer && !isVisible) return;
            valueText?.Draw(gameTime, sb);
        }

        public Rectangle GetBounds() => valueText.GetBounds();

        public void Setup(SpriteFont font, Point startPoint, int initialValue)
        {
            gridPosition = startPoint;
            if (valueText == null)
            {
                valueText = new Text(font, "", Vector2.Zero) { OrderInLayer = OrderInLayer + 1 };
                valueText.Transform.Parent = Transform;
            }
            else valueText.Font = font;

            Value = initialValue;
            Moves = initialValue;
            giveBonus = true;

            Cell bindedCell = Field.GetCell(startPoint);
            if (bindedCell != null) UnitSystem.ForceBind(this, bindedCell);
            IsActive = true;
        }
        public void OnDragStart(MouseButton mouse) {
            if(mouse == MouseButton.Left) {
                UnitSystem.LHandleDrag(this);
            }
            else {
                UnitSystem.RHandleDrag(this);
            }
        }
        public void OnDragUpdate(MouseButton mouse, Vector2 position) {
            if(mouse == MouseButton.Left) {
                if (UnitSystem.CanControl(this)) Transform.LocalPosition = position;
            }
            else {
                if (unitClone != null) unitClone.Transform.LocalPosition = position;
            }
        }
        public void OnDragEnd(MouseButton mouse) {
            if(mouse == MouseButton.Left) {
                UnitSystem.LHandleDrop(this);
            }
            else {
                UnitSystem.RHandleDrop(this);
            }
        }
        public void OnSpawn() => IsActive = true;
        public void OnDespawn() {
            team?.RemoveUnit(this); 
            CellSystem.Unbind(currentCell); 
            currentCell = null; 
            IsActive = false; 
        }
    }
}