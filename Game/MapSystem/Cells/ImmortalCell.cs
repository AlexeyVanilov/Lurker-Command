using LurkerCommand.GameScripts.Game.MapSystem.Cells;
using LurkerCommand.GameSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LurkerCommand.MapSystem;

public sealed class ImmortalCell : Cell {
    public override byte idleBonus => 0;
    public override bool canCaptured => false;
    public override CellType cellType => CellType.ImmortalCell;
    public override Color defaultColor { get; }
    public override Color hiddenColor { get; }

    public ImmortalCell(Texture2D texture, Vector2 position, Vector2 scale, int teamIndex)
        : base(texture, position, scale)
    {
        var team = TeamManager.GetTeamByIndex(teamIndex);

        defaultColor = team != null ? team.TeamColor : Color.Gray;

        hiddenColor = new Color(defaultColor.R / 2, defaultColor.G / 2, defaultColor.B / 2);

        if (cellImage != null)
        {
            cellImage.Color = defaultColor;
        }
    }
}