using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudokuGUI
{
    class Message : ObjectBase
    {
        private string _text;
        private const int fontOffsetX = 0;
        private const int fontOffsetY = 0;

        public Message(int x, int y, string text)
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

        public void show()
        {
            _valueText.Text = _text;
        }

        public void hide()
        {
            _valueText.Text = "";
        }
    }
}
