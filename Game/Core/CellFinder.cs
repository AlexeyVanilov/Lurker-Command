using LurkerCommand.Models;
using System;

namespace LurkerCommand.Core {
    public sealed class CellFinder
    {
        private readonly Random _rand = new Random();

        public Cell FindBestCell(Unit unit)
        {
            if (unit.Value <= CellFinderData.disadvantageousValue) return null;

            var available = Field.GetAvailableCells(unit.currentCell, unit.Value);
            if (available.Length == 0) return null;

            return available[_rand.Next(available.Length)];
        }
    }
}