using sudoku;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudokuGUI
{
    class AnalyzeButton : Button
    {
        public AnalyzeButton() : base(600, 570, "解析開始")
        {

        }

        public override void onClick(SquareObject[,] squareObjects)
        {
            if(enable == false)
            {
                return;
            }

            Square[,] squares = new Square[9, 9];
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    squares[row, col] = new Square(squareObjects[row, col].getValue(), row, col);
                }
            }
            Sudoku sudoku = new Sudoku(squares);
            var ret = sudoku.run();
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    squareObjects[row, col].setValue(ret[row, col].GetValue());
                }
            }
        }
    }
}
