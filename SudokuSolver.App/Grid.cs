using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver.App
{
    public class Grid
    {
        private int unitSize;
        private List<Cell> givens;

        public List<Cell> Cells { get; set; }

        public bool IsSolved
        {
            get
            {
                return Cells.All(c => c.PossibleValues.Count() == 1);
            }
        }

        public Grid(int unitSize)
        {
            this.unitSize = unitSize;
            InitCells();
        }

        public Grid(int unitSize, List<Cell> givens) : this(unitSize)
        {
            this.givens = givens;
            foreach (var given in givens)
            {
                var cellIndex = Cells.FindIndex(c => c.RowNum == given.RowNum && c.ColNum == given.ColNum);
                Cells[cellIndex] = given;
                RemovePossibleValueSharedWith(given, given.PossibleValues.First());
            }
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
            return Cells.Where(c => c.RowNum == row && c.ColNum == col).FirstOrDefault();
        }

        public void SetValue(Cell cell, int value)
        {
            cell.SetValue(value);
            RemovePossibleValueSharedWith(cell, value);
        }

        private void RemovePossibleValueSharedWith(Cell cell, int value)
        {
            var unsolvedCells = UnitCellsFor(cell).Where(c => c.PossibleValues.Count() > 1).ToList();
            foreach (var c in unsolvedCells)
            {
                c.RemovePossibleValue(value);
                if (c.PossibleValues.Count() == 1)
                {
                    RemovePossibleValueSharedWith(c, c.PossibleValues.First());
                }
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

        public void Solve()
        {

        }
    }
}
