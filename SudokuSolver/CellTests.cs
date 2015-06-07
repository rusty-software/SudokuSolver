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

        [TestMethod]
        public void BlockNum_NineUnit_CalculatesCorrectly()
        {
            var nineUnit = 9;
            var midBlock1 = new Cell(2, 2, nineUnit);
            var midBlock2 = new Cell(2, 5, nineUnit);
            var midBlock3 = new Cell(2, 8, nineUnit);
            var midBlock4 = new Cell(5, 2, nineUnit);
            var midBlock5 = new Cell(5, 5, nineUnit);
            var midBlock6 = new Cell(5, 8, nineUnit);
            var midBlock7 = new Cell(8, 2, nineUnit);
            var midBlock8 = new Cell(8, 5, nineUnit);
            var midBlock9 = new Cell(8, 8, nineUnit);

            Assert.AreEqual(1, midBlock1.BlockNum);
            Assert.AreEqual(2, midBlock2.BlockNum);
            Assert.AreEqual(3, midBlock3.BlockNum);
            Assert.AreEqual(4, midBlock4.BlockNum);
            Assert.AreEqual(5, midBlock5.BlockNum);
            Assert.AreEqual(6, midBlock6.BlockNum);
            Assert.AreEqual(7, midBlock7.BlockNum);
            Assert.AreEqual(8, midBlock8.BlockNum);
            Assert.AreEqual(9, midBlock9.BlockNum);
        }
    }
}
