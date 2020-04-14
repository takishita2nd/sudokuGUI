using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudokuGUI
{
    class SquareObject
    {
        private int _x;
        private int _y;
        private int _row;
        private int _col;
        private int _value;
        private asd.TextureObject2D _backTexture;
        private asd.TextObject2D _valueText;
        private const int offsetX = 10;
        private const int offsetY = 10;
        private const int fontOffsetX = 19;
        private const int fontOffsetY = 9;
        private const int width = 64;
        private const int height = 64;

        public SquareObject(int row, int col)
        {
            _row = row;
            _col = col;
            _x = row * 64 + offsetX;
            _y = col * 64 + offsetY;
            _value = 0;

            _backTexture = new asd.TextureObject2D();
            _backTexture.Position = new asd.Vector2DF(_x, _y);

            _valueText = new asd.TextObject2D();
            _valueText.Font = Resource.getFont();
            _valueText.Position = new asd.Vector2DF(_x + fontOffsetX, _y + fontOffsetY);

        }
        
        public void setValue(int value)
        {
            _value = value;
            if(value == 0)
            {
                _valueText.Text = "";
            }
            else
            {
                _valueText.Text = value.ToString();
            }
        }

        public asd.TextureObject2D getBackTexture()
        {
            return _backTexture;
        }

        public asd.TextObject2D getTextObject()
        {
            return _valueText;
        }

        public void updateTexture(asd.Vector2DF pos)
        {
            if(pos.X > _x && pos.X < _x + width 
                && pos.Y > _y && pos.Y < _y + height)
            {
                _backTexture.Texture = Resource.getTexture();
            }
            else
            {
                _backTexture.Texture = null;
            }
        }
    }
}
