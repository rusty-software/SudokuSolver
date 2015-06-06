using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using SudokuSolver.App;

namespace SudokuSolver
{
    [TestClass]
    public class CellTests
    {
        int smallUnitSize = 4;

        [TestMethod]
        public void Ctor_WithoutGiven_WithAllPossibleValues()
        {
            var expectedPossibleValues = new List<int> { 1, 2, 3, 4 };

            var cell = new Cell(1, 1, smallUnitSize);

            CollectionAssert.AreEquivalent(expectedPossibleValues, cell.PossibleValues);
        }

        [TestMethod]
        public void Ctor_WithGiven_HasNoOtherPossibleValues()
        {
            var cell = new Cell(1, 1, smallUnitSize, 3);

            Assert.AreEqual(1, cell.PossibleValues.Count());
            Assert.AreEqual(3, cell.PossibleValues.First());
        }

        [TestMethod]
        public void BlockNum_SmallUnit_CalculatesCorrectly()
        {
            var cell1 = new Cell(1, 1, smallUnitSize);
            var cell7 = new Cell(2, 3, smallUnitSize);
            var cell12 = new Cell(3, 4, smallUnitSize);
            var cell14 = new Cell(4, 2, smallUnitSize);

            Assert.AreEqual(1, cell1.BlockNum);
            Assert.AreEqual(2, cell7.BlockNum);
            Assert.AreEqual(4, cell12.BlockNum);
            Assert.AreEqual(3, cell14.BlockNum);
        }
    }
}
