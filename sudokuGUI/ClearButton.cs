using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudokuGUI
{
    class ClearButton : Button
    {
        public ClearButton() : base(600, 500, "クリア")
        {

        }

        public override void onClick(SquareObject[,] squareObjects)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    squareObjects[row, col].setValue(0);
                }
            }
        }
    }
}
