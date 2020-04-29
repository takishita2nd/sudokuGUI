using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudokuGUI
{
    class Button : ObjectBase
    {
        private string _text;
        private const int fontOffsetX = 39;
        private const int fontOffsetY = 9;
        protected bool enable = true;

        public Button(int x, int y, string text)
        {
            width = 256;
            height = 64;
            _x = x;
            _y = y;
            _text = text;

            _backTexture = new asd.TextureObject2D();
            _backTexture.Position = new asd.Vector2DF(_x, _y);

            _valueText = new asd.TextObject2D();
            _valueText.Text = _text;
            _valueText.Font = Resource.getTextFont();
            _valueText.Position = new asd.Vector2DF(_x + fontOffsetX, _y + fontOffsetY);
        }

        public void updateTexture(asd.Vector2DF pos)
        {
            if (pos.X > _x && pos.X < _x + width
                && pos.Y > _y && pos.Y < _y + height)
            {
                _backTexture.Texture = Resource.getButtonTexture();
            }
            else
            {
                _backTexture.Texture = null;
            }
        }

        public void setEnable(bool enable)
        {
            this.enable = enable;
        }

        public virtual void onClick(SquareObject[,] squareObjects, Message message)
        {
            throw new NotImplementedException();
        }
    }
}
