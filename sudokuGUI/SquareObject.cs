using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudokuGUI
{
    class SquareObject : ObjectBase
    {
        public enum FontColor
        {
            Black,
            Red
        }

        protected int _row;
        protected int _col;
        private int _value;
        protected const int offsetX = 10;
        protected const int offsetY = 10;
        protected const int fontOffsetX = 19;
        protected const int fontOffsetY = 9;

        public SquareObject(int row, int col)
        {
            width = 64;
            height = 64;
            _row = row;
            _col = col;
            _x = col * width + offsetX;
            _y = row * height + offsetY;
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

        public void hide()
        {
            _backTexture.Texture = null;
            _valueText.Text = "";
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

        public int getValue()
        {
            return _value;
        }

        public bool isSetValue()
        {
            if(_value == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void setFontColor(FontColor color)
        {
            switch (color)
            {
                case FontColor.Black:
                    _valueText.Font = Resource.getFont();
                    break;
                case FontColor.Red:
                    _valueText.Font = Resource.getFontRed();
                    break;
                default:
                    break;
            }
        }
    }
}
