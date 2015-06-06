using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace SudokuSolver.Tests
{
    [TestClass]
    public class AppTests
    {
        int smallUnitSize = 4;

        [TestMethod]
        public void Cell_WithoutGiven_WithAllPossibleValues()
        {
            var expectedPossibleValues = new List<int> { 1, 2, 3, 4 };

            var cell = new Cell(1, 1, smallUnitSize);

            CollectionAssert.AreEquivalent(expectedPossibleValues, cell.PossibleValues);
        }

        [TestMethod]
        public void Cell_WithGiven_HasNoOtherPossibleValues()
        {
            var cell = new Cell(1, 1, smallUnitSize, 3);

            Assert.AreEqual(1, cell.PossibleValues.Count());
            Assert.AreEqual(3, cell.PossibleValues.First());
        }

        [TestMethod]
        public void Grid_HasCorrectNumberOfCells()
        {
            var grid = new Grid(smallUnitSize);

            Assert.AreEqual(smallUnitSize * smallUnitSize, grid.Cells.Count());
        }

        [TestMethod]
        public void Grid_CellAt_ReturnsCellsFromPlacement()
        {
            var grid = new Grid(smallUnitSize);
            var cell1 = grid.CellAt(1, 1);
            var cell7 = grid.CellAt(2, 3);
            var cell12 = grid.CellAt(3, 4);
            var cell14 = grid.CellAt(4, 2);

            Assert.AreEqual(1, cell1.BlockNum);
            Assert.AreEqual(2, cell7.BlockNum);
            Assert.AreEqual(4, cell12.BlockNum);
            Assert.AreEqual(3, cell14.BlockNum);
        }

        [TestMethod]
        public void Cells_AddingValue_RemovesUnitCellsPossibleValues()
        {
            var expected = new List<int> { 2, 3, 4 };
            var grid = new Grid(smallUnitSize);
            var cell = grid.CellAt(1, 1);

            grid.SetValue(cell, 1);
            var unitCells = grid.UnitCellsFor(cell);

            Assert.AreEqual(7, unitCells.Count());
            foreach (var c in unitCells)
            {
                CollectionAssert.AreEquivalent(expected, c.PossibleValues);
            }
        }
    }

    internal class Grid
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
            foreach(var c in UnitCellsFor(cell))
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

    internal class Cell
    {
        private int unitSize;

        private int blockWidth()
        {
            return (int)Math.Sqrt(unitSize);
        }

        private int rowBlock()
        {
            var dividend = Math.Ceiling(RowNum / (decimal)blockWidth());
            return (int)dividend;
        }

        private int rowOffset()
        {
            return rowBlock() - 1;
        }

        private int colBlock()
        {
            var dividend = Math.Ceiling(ColNum / (decimal)blockWidth());
            return (int)dividend;
        }

        private int colOffset()
        {
            return (int)(Math.Sqrt(unitSize) - colBlock());
        }

        public void SetValue(int value)
        {
            PossibleValues = new List<int> { value };
        }

        public void RemovePossibleValue(int value)
        {
            PossibleValues.Remove(value);
        }

        public List<int> PossibleValues { get; private set; }

        public int RowNum { get; set; }

        public int ColNum { get; set; }

        public int BlockNum
        {
            get
            {
                return (rowBlock() * colBlock()) + (rowOffset() * colOffset());
            }
        }

        public Cell(int rowNum, int colNum, int unitSize)
        {
            this.unitSize = unitSize;
            RowNum = rowNum;
            ColNum = colNum;
            PossibleValues = Enumerable.Range(1, unitSize).ToList();
        }

        public Cell(int rowNum, int colNum, int unitSize, int value) : this(rowNum, colNum, unitSize)
        {
            PossibleValues = new List<int> { value };
        }
    }
}
