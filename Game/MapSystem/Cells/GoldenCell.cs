using LurkerCommand.GameScripts.Game.MapSystem.Cells;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LurkerCommand.MapSystem
{
    public sealed class GoldenCell : Cell
    {
        public override byte idleBonus => 2;
        public override bool canCaptured => true;
        public override CellType cellType => CellType.GoldenCell;
        public GoldenCell(Texture2D texture, Vector2 position, Vector2 scale) : base(texture, position, scale)
        {

        }
    }
}