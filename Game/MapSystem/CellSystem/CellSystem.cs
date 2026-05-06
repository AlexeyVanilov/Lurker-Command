using LurkerCommand.GameSystem;
using Microsoft.Xna.Framework;

namespace LurkerCommand.MapSystem {
    public static class CellSystem {
        public const float tintFactor = 0.25f;
        public static void Capture(Cell cell, Team team)
        {
            if (!cell.canCaptured || cell.IsEmpty) return;
            cell.OwnerTeam = team;
            cell.IsCaptured = true;

            cell.tintedColor = Color.Lerp(Color.White, team.TeamColor, tintFactor);

            if (cell.IsVisible) cell.cellImage.Color = cell.tintedColor;
        }

        public static void Uncapture(Cell cell)
        {
            cell.IsCaptured = false;
            cell.OwnerTeam = null;
            if (cell.IsVisible) cell.cellImage.Color = cell.defaultColor;
        }

        public static void BindUnit(Cell cell, Unit unit)
        {
            unit.giveBonus = true;
            cell.currentUnit = unit;
            cell.IsEmpty = false;
            if (unit != null) unit.isVisible = cell.IsVisible;
        }

        public static void Unbind(Cell cell)
        {
            if (cell.currentUnit == null) return;
            cell.currentUnit.giveBonus = false;
            cell.currentUnit = null;
            cell.IsEmpty = true;
        }
    }
}
