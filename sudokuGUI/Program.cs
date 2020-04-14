using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudokuGUI
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // Altseedを初期化する。
            asd.Engine.Initialize("数独解析ツール", 1000, 800, new asd.EngineOption());

            // 下地
            var background = new asd.GeometryObject2D();
            asd.Engine.AddObject2D(background);
            var bgRect = new asd.RectangleShape();
            bgRect.DrawingArea = new asd.RectF(0, 0, 1000, 800);
            background.Shape = bgRect;

            // テクスチャー
            asd.Texture2D texture = asd.Engine.Graphics.CreateTexture2D("squares.png");

            // マス
            int offsetX = 10;
            int offsetY = 10;
            var square = new asd.TextureObject2D();
            square.Position = new asd.Vector2DF(offsetX, offsetY);
            square.Texture = texture;
            asd.Engine.AddObject2D(square);

            SquareObject[,] squareObjects = new SquareObject[9, 9];
            for(int row = 0; row < 9; row++)
            {
                for(int col = 0; col < 9; col++)
                {
                    var obj = new SquareObject(row, col);
                    obj.setValue(0);
                    asd.Engine.AddObject2D(obj.getBackTexture());
                    asd.Engine.AddObject2D(obj.getTextObject());
                    squareObjects[row, col] = obj;
                }
            }

            // Altseedが進行可能かチェックする。
            while (asd.Engine.DoEvents())
            {
                asd.Vector2DF pos = asd.Engine.Mouse.Position;
                for (int row = 0; row < 9; row++)
                {
                    for (int col = 0; col < 9; col++)
                    {
                        squareObjects[row, col].updateTexture(pos);
                    }
                }

                // Altseedを更新する。
                asd.Engine.Update();
            }

            // Altseedを終了する。
            asd.Engine.Terminate();
        }
    }
}
