using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudokuGUI
{
    class PaletteSquareObject : SquareObject
    {

        public PaletteSquareObject(int row, int col) : base(row, col)
        {
        }

        public void setPosition(asd.Vector2DF pos)
        {
            _x = _col * width + (int)pos.X;
            _y = _row * height + (int)pos.Y;

            _backTexture.Position = new asd.Vector2DF(_x, _y);
            _valueText.Position = new asd.Vector2DF(_x + fontOffsetX, _y + fontOffsetY);
        }
    }
}
