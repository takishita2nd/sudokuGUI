using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudokuGUI
{
    class PaletteBatsuSquareObject : PaletteSquareObject
    {
        public PaletteBatsuSquareObject(int row, int col) : base(row, col)
        {
            _valueText.Font = Resource.getFontBatsu();
        }

        public void showBatsu()
        {
            _valueText.Text = "×";
        }

        public new void setPosition(asd.Vector2DF pos)
        {
            _x = _row * width + (int)pos.X;
            _y = _col * height + (int)pos.Y;

            _backTexture.Position = new asd.Vector2DF(_x, _y);
            _valueText.Position = new asd.Vector2DF(_x + fontOffsetX - 8, _y + fontOffsetY);
        }
    }
}
