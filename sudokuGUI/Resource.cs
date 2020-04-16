using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudokuGUI
{
    static class Resource
    {
        private static asd.Texture2D _texture = null;
        private static asd.Texture2D _palette = null;
        private static asd.Font _font = null;

        public static asd.Texture2D getTexture()
        {
            if(_texture == null)
            {
                _texture = asd.Engine.Graphics.CreateTexture2D("square.png");
            }
            return _texture;
        }

        public static asd.Texture2D getPalette()
        {
            if (_palette == null)
            {
                _palette = asd.Engine.Graphics.CreateTexture2D("palette.png");
            }
            return _palette;
        }

        public static asd.Font getFont()
        {
            if (_font == null)
            {
                _font = asd.Engine.Graphics.CreateFont("number.aff");
            }
            return _font;
        }
    }
}
