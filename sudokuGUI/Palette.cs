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
        PaletteSquareObject[,] paletteSquareObjects = new PaletteSquareObject[3, 3];
        PaletteBatsuSquareObject PaletteBatsuSquareObject = new PaletteBatsuSquareObject(0, -1);

        public Palette()
        {
            _texture = new asd.TextureObject2D();
            for(int row = 0; row < 3; row++)
            {
                for(int col = 0; col < 3; col++)
                {
                    paletteSquareObjects[row, col] = new PaletteSquareObject(row, col);
                }
            }
        }

        public void setEngine()
        {
            asd.Engine.AddObject2D(_texture);
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    asd.Engine.AddObject2D(paletteSquareObjects[row, col].getBackTexture());
                    asd.Engine.AddObject2D(paletteSquareObjects[row, col].getTextObject());
                }
            }
            asd.Engine.AddObject2D(PaletteBatsuSquareObject.getBackTexture());
            asd.Engine.AddObject2D(PaletteBatsuSquareObject.getTextObject());
        }

        public void show(asd.Vector2DF pos)
        {
            palettePosition = new asd.Vector2DF(pos.X, pos.Y - 64);
            _texture.Position = palettePosition;
            _texture.Texture = Resource.getPalette();
            int value = 1;
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    paletteSquareObjects[row, col].setPosition(pos);
                    paletteSquareObjects[row, col].setValue(value);
                    value++;
                }
            }
            PaletteBatsuSquareObject.setPosition(pos);
            PaletteBatsuSquareObject.showBatsu();
        }

        public void hide()
        {
            _texture.Texture = null;
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    paletteSquareObjects[row, col].hide();
                }
            }
            PaletteBatsuSquareObject.hide();
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
