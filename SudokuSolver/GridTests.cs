using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using SudokuSolver.App;
using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace SudokuSolver
{
    [TestClass]
    public class GridTests
    {
        int smallUnitSize = 4;
        int mediumUnitSize = 9;

        [TestMethod]
        public void CTor_HasCorrectNumberOfCells()
        {
            var grid = new Grid(smallUnitSize);

            Assert.AreEqual(smallUnitSize * smallUnitSize, grid.Cells.Count());
        }

        [TestMethod]
        public void SetValue_RemovesUnitCellsPossibleValues()
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

        [TestMethod]
        public void Solve_WithEnoughGivens_ProducesSolution()
        {
            var givens = new List<Cell>();
            givens.Add(new Cell(1, 2, smallUnitSize, 4));
            givens.Add(new Cell(2, 1, smallUnitSize, 2));
            givens.Add(new Cell(2, 4, smallUnitSize, 4));
            givens.Add(new Cell(3, 1, smallUnitSize, 1));
            givens.Add(new Cell(3, 4, smallUnitSize, 3));
            givens.Add(new Cell(4, 3, smallUnitSize, 1));
            var grid = new Grid(smallUnitSize, givens);

            grid.Solve();

            var unsolved = grid.Cells.Where(c => c.PossibleValues.Count() > 1).ToList();

            Assert.IsTrue(grid.IsSolved);
        }

        [TestMethod]
        public void Solve_HarderSmallPuzzle_ProducesSolution()
        {
            var givens = new List<Cell>();
            givens.Add(new Cell(1, 2, smallUnitSize, 2));
            givens.Add(new Cell(1, 4, smallUnitSize, 1));
            givens.Add(new Cell(2, 3, smallUnitSize, 2));
            givens.Add(new Cell(3, 2, smallUnitSize, 4));
            givens.Add(new Cell(4, 1, smallUnitSize, 3));
            var grid = new Grid(smallUnitSize, givens);

            grid.Solve();

            var unsolved = grid.Cells.Where(c => c.PossibleValues.Count() > 1).ToList();

            Assert.IsTrue(grid.IsSolved);
        }

        [TestMethod]
        public void Solve_EasyNineUnitPuzzle_ProducesSolution()
        {
            var givens = new List<Cell>();
            givens.Add(new Cell(1, 1, mediumUnitSize, 8));
            givens.Add(new Cell(1, 4, mediumUnitSize, 9));
            givens.Add(new Cell(1, 6, mediumUnitSize, 6));
            givens.Add(new Cell(1, 7, mediumUnitSize, 1));
            givens.Add(new Cell(1, 8, mediumUnitSize, 5));
            givens.Add(new Cell(1, 9, mediumUnitSize, 7));

            givens.Add(new Cell(2, 2, mediumUnitSize, 9));
            givens.Add(new Cell(2, 3, mediumUnitSize, 5));
            givens.Add(new Cell(2, 5, mediumUnitSize, 7));
            givens.Add(new Cell(2, 6, mediumUnitSize, 1));
            givens.Add(new Cell(2, 7, mediumUnitSize, 4));
            givens.Add(new Cell(2, 8, mediumUnitSize, 8));

            givens.Add(new Cell(3, 1, mediumUnitSize, 6));
            givens.Add(new Cell(3, 5, mediumUnitSize, 4));
            givens.Add(new Cell(3, 9, mediumUnitSize, 3));

            givens.Add(new Cell(4, 2, mediumUnitSize, 3));
            givens.Add(new Cell(4, 4, mediumUnitSize, 6));
            givens.Add(new Cell(4, 5, mediumUnitSize, 5));
            givens.Add(new Cell(4, 6, mediumUnitSize, 2));
            givens.Add(new Cell(4, 7, mediumUnitSize, 8));

            givens.Add(new Cell(5, 2, mediumUnitSize, 2));
            givens.Add(new Cell(5, 3, mediumUnitSize, 7));
            givens.Add(new Cell(5, 5, mediumUnitSize, 8));
            givens.Add(new Cell(5, 9, mediumUnitSize, 4));

            givens.Add(new Cell(6, 1, mediumUnitSize, 1));
            givens.Add(new Cell(6, 5, mediumUnitSize, 3));
            givens.Add(new Cell(6, 7, mediumUnitSize, 9));

            givens.Add(new Cell(7, 1, mediumUnitSize, 7));
            givens.Add(new Cell(7, 3, mediumUnitSize, 6));
            givens.Add(new Cell(7, 4, mediumUnitSize, 4));
            givens.Add(new Cell(7, 8, mediumUnitSize, 1));
            givens.Add(new Cell(7, 9, mediumUnitSize, 2));

            givens.Add(new Cell(8, 2, mediumUnitSize, 1));
            givens.Add(new Cell(8, 3, mediumUnitSize, 4));
            givens.Add(new Cell(8, 4, mediumUnitSize, 2));
            givens.Add(new Cell(8, 6, mediumUnitSize, 5));
            givens.Add(new Cell(8, 8, mediumUnitSize, 3));

            givens.Add(new Cell(9, 3, mediumUnitSize, 2));
            givens.Add(new Cell(9, 4, mediumUnitSize, 8));
            givens.Add(new Cell(9, 7, mediumUnitSize, 6));
            givens.Add(new Cell(9, 9, mediumUnitSize, 9));

            var grid = new Grid(mediumUnitSize, givens);

            grid.Solve();

            var unsolved = grid.Cells.Where(c => c.PossibleValues.Count() > 1).ToList();

            Assert.IsTrue(grid.IsSolved);
        }

        [TestMethod]
        public void Ctor_FromArray_InitializesCorrectly()
        {
            var row9 = new int[9] { 8, 0, 0, 5, 0, 0, 3, 0, 0 };
            var row8 = new int[9] { 0, 2, 9, 0, 0, 0, 0, 8, 6 };
            var row7 = new int[9] { 0, 0, 0, 0, 0, 1, 0, 0, 0 };
            var row6 = new int[9] { 5, 0, 0, 8, 6, 0, 0, 0, 9 };
            var row5 = new int[9] { 3, 0, 0, 0, 0, 0, 0, 0, 0 };
            var row4 = new int[9] { 0, 0, 6, 9, 7, 2, 0, 0, 0 };
            var row3 = new int[9] { 0, 0, 0, 0, 0, 6, 0, 0, 0 };
            var row2 = new int[9] { 0, 0, 0, 0, 0, 0, 7, 9, 4 };
            var row1 = new int[9] { 0, 3, 0, 0, 0, 0, 0, 2, 0 };

            var gridRows = new int[][] { row1, row2, row3, row4, row5, row6, row7, row8, row9 };
            var grid = new Grid(gridRows);

            Assert.AreEqual(81, grid.Cells.Count());
            Assert.AreEqual(9, grid.Cells.Max(c => c.RowNum));
            Assert.AreEqual(9, grid.Cells.Max(c => c.ColNum));
            Assert.AreEqual(9, grid.Cells.Max(c => c.BlockNum));
            Assert.AreEqual(1, grid.Cells.Min(c => c.RowNum));
            Assert.AreEqual(1, grid.Cells.Min(c => c.ColNum));
            Assert.AreEqual(1, grid.Cells.Min(c => c.BlockNum));


        }

        [TestMethod, Ignore]
        public void Solve_HardNineUnitPuzzle_ProducesSolution()
        {
            var givens = new List<Cell>();
            givens.Add(new Cell(1, 2, mediumUnitSize, 3));
            givens.Add(new Cell(1, 8, mediumUnitSize, 2));

            givens.Add(new Cell(2, 7, mediumUnitSize, 7));
            givens.Add(new Cell(2, 8, mediumUnitSize, 9));
            givens.Add(new Cell(2, 9, mediumUnitSize, 4));

            givens.Add(new Cell(3, 6, mediumUnitSize, 6));

            givens.Add(new Cell(4, 3, mediumUnitSize, 6));
            givens.Add(new Cell(4, 4, mediumUnitSize, 9));
            givens.Add(new Cell(4, 5, mediumUnitSize, 7));
            givens.Add(new Cell(4, 6, mediumUnitSize, 2));

            givens.Add(new Cell(5, 1, mediumUnitSize, 3));

            givens.Add(new Cell(6, 1, mediumUnitSize, 5));
            givens.Add(new Cell(6, 4, mediumUnitSize, 8));
            givens.Add(new Cell(6, 5, mediumUnitSize, 6));
            givens.Add(new Cell(6, 9, mediumUnitSize, 9));


            var grid = new Grid(mediumUnitSize, givens);

            grid.Solve();

            var unsolved = grid.Cells.Where(c => c.PossibleValues.Count() > 1).ToList();

            Assert.IsTrue(grid.IsSolved);
        }
    }
}
