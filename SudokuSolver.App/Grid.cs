using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver.App
{
    public class Grid
    {
        private int unitSize;
        private List<Cell> givens;

        public List<Cell> Cells { get; set; }

        public bool IsSolved(List<Cell> puzzle)
        {
            return puzzle.All(c => c.PossibleValues.Count() == 1);
        }

        private void Init(int unitSize)
        {
            this.unitSize = unitSize;
            InitCells();
        }

        public Grid(int unitSize)
        {
            Init(unitSize);
        }

        public Grid(int[][] gridRows) : this(gridRows.Length)
        {
            var givens = new List<Cell>();
            var arrayLen = gridRows.Length;
            for (var rowIndex = 0; rowIndex < arrayLen; rowIndex++)
            {
                for (var colIndex = 0; colIndex < arrayLen; colIndex++)
                {
                    var val = gridRows[rowIndex][colIndex];
                    if (val != 0)
                    {
                        givens.Add(new Cell(rowIndex + 1, colIndex + 1, arrayLen, val));
                    }
                }
            }

            InitFromGivens(givens);
        }

        private void InitFromGivens(List<Cell> givens)
        {
            this.givens = givens;
            foreach (var given in givens)
            {
                var cellIndex = Cells.FindIndex(c => c.RowNum == given.RowNum && c.ColNum == given.ColNum);
                Cells[cellIndex] = given;
                RemovePossibleValueSharedWith(Cells, given, given.PossibleValues.First());
            }

            if (!IsSolved(Cells))
            {
                Solve(Cells);
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

        private void RemovePossibleValueSharedWith(List<Cell> puzzle, Cell cell, int value)
        {
            var unsolvedCells = UnitCellsFor(puzzle, cell).Where(c => c.PossibleValues.Count() > 1).ToList();
            foreach (var c in unsolvedCells)
            {
                c.RemovePossibleValue(value);
                if (c.PossibleValues.Count() == 1)
                {
                    RemovePossibleValueSharedWith(puzzle, c, c.PossibleValues.First());
                }
            }
        }

        public List<Cell> UnitCellsFor(List<Cell> puzzle, Cell cell)
        {
            var ident = puzzle.First(c => c.RowNum == cell.RowNum && c.ColNum == cell.ColNum);
            var unitSharers = new List<Cell>();
            unitSharers.AddRange(
                puzzle.Where(
                    c => c != ident
                         && (c.RowNum == cell.RowNum || c.ColNum == cell.ColNum || c.BlockNum == cell.BlockNum)));
            return unitSharers;
        }

        public List<Cell> Solve(List<Cell> puzzle)
        {
            var copy = puzzle.ConvertAll(c => new Cell(c.RowNum, c.ColNum, puzzle.Count()));
            var minPossibleValueCount = puzzle.Where(c => c.PossibleValues.Count() > 1).Min(c => c.PossibleValues.Count());
            var unsolvedCell = puzzle.First(c => c.PossibleValues.Count() == minPossibleValueCount);
            var index = puzzle.FindIndex(c => c.RowNum == unsolvedCell.RowNum && c.ColNum == unsolvedCell.ColNum);
            foreach (var possibleValue in unsolvedCell.PossibleValues)
            {
                unsolvedCell.SetValue(possibleValue);
                copy[index] = unsolvedCell;
                RemovePossibleValueSharedWith(copy, unsolvedCell, unsolvedCell.PossibleValues.First());
                if (IsSolved(copy))
                {
                    return copy;
                }
            }

            return Solve(copy);
        }
    }
}
