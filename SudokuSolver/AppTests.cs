using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace SudokuSolver.Tests
{
    [TestClass]
    public class AppTests
    {
        [TestMethod]
        public void Cell_WithoutGiven_WithAllPossibleValues()
        {
            var gridWidth = 2;
            var expectedPossibleValues = new[] { 1, 2, 3, 4 };

            var cell = new Cell(gridWidth);

            CollectionAssert.AreEquivalent(expectedPossibleValues, cell.PossibleValues);
        }

        [TestMethod]
        public void Cell_WithGiven_HasNoOtherPossibleValues()
        {
            var gridWidth = 2;
            var cell = new Cell(gridWidth, 3);

            Assert.AreEqual(0, cell.PossibleValues.Count());
            Assert.AreEqual(3, cell.Given);
        }
    }

    internal class Cell
    {
        public int Given { get; set; }

        public List<int> PossibleValues { get; private set; }

        public Cell(int gridWidth)
        {
            PossibleValues = Enumerable.Range(1, gridWidth * gridWidth).ToList();
        }

        public Cell(int gridWidth, int given)
        {
            Given = given;
            PossibleValues = new List<int>();
        }
    }
}
