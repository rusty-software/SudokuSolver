using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver.App
{
    public class Cell
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
