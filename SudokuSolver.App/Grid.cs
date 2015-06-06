using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver.App
{
    public class Grid
    {
        private int unitSize;

        public List<Cell> Cells { get; set; }

        public Grid(int unitSize)
        {
            this.unitSize = unitSize;
            InitCells();
        }

        private void InitCells()
        {
            Cells = new List<Cell>();
            for (var rowIndex = 1; rowIndex <= unitSize; rowIndex++)
            {
                for (var colIndex = 1; colIndex <= unitSize; colIndex++)
                {
                    Cells.Add(new Cell(rowIndex, colIndex, unitSize));
                }
            }
        }

        public Cell CellAt(int row, int col)
        {
            // TODO: guard
            return Cells.Where(c => c.RowNum == row && c.ColNum == col).FirstOrDefault();
        }

        public void SetValue(Cell cell, int value)
        {
            cell.SetValue(value);
            foreach (var c in UnitCellsFor(cell))
            {
                c.RemovePossibleValue(value);
            }
        }

        public List<Cell> UnitCellsFor(Cell cell)
        {
            var unitSharers = new List<Cell>();
            unitSharers.AddRange(
                Cells.Where(
                    c => c != CellAt(cell.RowNum, cell.ColNum)
                         && (c.RowNum == cell.RowNum || c.ColNum == cell.ColNum || c.BlockNum == cell.BlockNum)));
            return unitSharers;
        }
    }
}
