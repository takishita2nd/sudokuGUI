using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudokuGUI
{
    class Palette
    {
        private asd.Vector2DF palettePosition;
        private const int width = 192;
        private const int height = 256;
        private asd.TextureObject2D _texture;

        public Palette()
        {
            _texture = new asd.TextureObject2D();
        }

        public asd.TextureObject2D getPaletteTexture()
        {
            return _texture;
        }

        public void Show(asd.Vector2DF pos)
        {
            palettePosition = new asd.Vector2DF(pos.X, pos.Y - 64);
            _texture.Position = palettePosition;
            _texture.Texture = Resource.getPalette();
        }

        public void Hide()
        {
            _texture.Texture = null;
        }

        public bool isClick(asd.Vector2DF pos)
        {
            if(pos.X > palettePosition.X && pos.X < palettePosition.X + width
                && pos.Y > palettePosition.Y && pos.Y < palettePosition.Y + height)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
