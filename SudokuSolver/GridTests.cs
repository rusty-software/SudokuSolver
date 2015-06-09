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
        public void Solve_WithEnoughGivens_ProducesSolution()
        {
            var row4 = new int[4] { 0, 0, 1, 0 };
            var row3 = new int[4] { 1, 0, 0, 3 };
            var row2 = new int[4] { 2, 0, 0, 4 };
            var row1 = new int[4] { 0, 4, 0, 0 };

            var gridRows = new int[][] { row1, row2, row3, row4 };
            var grid = new Grid(gridRows);

            Assert.IsTrue(grid.IsSolved(grid.Cells));
        }

        [TestMethod]
        public void Solve_SmallHardPuzzle_Works()
        {
            var row4 = new int[4] { 0, 0, 3, 0 };
            var row3 = new int[4] { 1, 0, 0, 2 };
            var row2 = new int[4] { 4, 0, 0, 0 };
            var row1 = new int[4] { 0, 1, 0, 0 };

            var gridRows = new int[][] { row1, row2, row3, row4 };
            var grid = new Grid(gridRows);

            Assert.IsTrue(grid.IsSolved(grid.Cells));
        }

        [TestMethod]
        public void Solve_AnotherSmallHardPuzzle_Works()
        {
            var row4 = new int[4] { 3, 0, 0, 1 };
            var row3 = new int[4] { 1, 0, 0, 0 };
            var row2 = new int[4] { 0, 0, 0, 0 };
            var row1 = new int[4] { 0, 0, 2, 0 };

            var gridRows = new int[][] { row1, row2, row3, row4 };
            var grid = new Grid(gridRows);

            Assert.IsTrue(grid.IsSolved(grid.Cells));
        }

        [TestMethod]
        public void Solve_NineUnit_Works()
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

            Assert.IsTrue(grid.IsSolved(grid.Cells));
        }
    }
}
