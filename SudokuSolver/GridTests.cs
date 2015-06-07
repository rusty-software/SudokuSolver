using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using SudokuSolver.App;
using System.Collections.Generic;

namespace SudokuSolver
{
    [TestClass]
    public class GridTests
    {
        int smallUnitSize = 4;

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

            var unsolved = grid.Cells.Where(c => c.PossibleValues.Count() > 1);
            Assert.IsTrue(grid.IsSolved);
        }
    }
}
