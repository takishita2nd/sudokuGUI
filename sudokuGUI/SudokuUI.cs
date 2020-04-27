using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudokuGUI
{
    class SudokuUI
    {
        private bool mouseHold;
        private Palette palette = null;
        private SquareObject clickedSquareObject = null;
        private const int offsetX = 10;
        private const int offsetY = 10;
        private const int width = 576;
        private const int height = 576;

        public SudokuUI()
        {
            mouseHold = false;
            clickedSquareObject = null;
        }

        public void Run()
        {
            // Altseedを初期化する。
            asd.Engine.Initialize("数独解析ツール", 1000, 800, new asd.EngineOption());

            // 下地
            var background = new asd.GeometryObject2D();
            asd.Engine.AddObject2D(background);
            var bgRect = new asd.RectangleShape();
            bgRect.DrawingArea = new asd.RectF(0, 0, 1000, 800);
            background.Shape = bgRect;


            // マス
            asd.Texture2D texture = asd.Engine.Graphics.CreateTexture2D("squares.png");
            var square = new asd.TextureObject2D();
            square.Position = new asd.Vector2DF(offsetX, offsetY);
            square.Texture = texture;
            asd.Engine.AddObject2D(square);

            SquareObject[,] squareObjects = new SquareObject[9, 9];
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    var obj = new SquareObject(row, col);
                    obj.setValue(0);
                    asd.Engine.AddObject2D(obj.getBackTexture());
                    asd.Engine.AddObject2D(obj.getTextObject());
                    squareObjects[row, col] = obj;
                }
            }

            // ボタン
            List<Button> buttons = new List<Button>();
            Button clear = new ClearButton();
            asd.Engine.AddObject2D(clear.getBackTexture());
            asd.Engine.AddObject2D(clear.getTextObject());
            buttons.Add(clear);

            Button start = new AnalyzeButton();
            asd.Engine.AddObject2D(start.getBackTexture());
            asd.Engine.AddObject2D(start.getTextObject());
            buttons.Add(start);

            // パレット
            palette = new Palette();
            palette.setEngine();




            // Altseedが進行可能かチェックする。
            while (asd.Engine.DoEvents())
            {
                asd.Vector2DF pos = asd.Engine.Mouse.Position;
                if (!mouseHold)
                {
                    for (int row = 0; row < 9; row++)
                    {
                        for (int col = 0; col < 9; col++)
                        {
                            squareObjects[row, col].updateTexture(pos);
                        }
                    }
                    foreach(Button button in buttons)
                    {
                        button.updateTexture(pos);
                    }
                }
                else
                {
                    palette.updateTexture(pos);
                }

                if (asd.Engine.Mouse.LeftButton.ButtonState == asd.ButtonState.Push)
                {
                    if (mouseHold)
                    {
                        if (!palette.isClick(pos))
                        {
                            palette.hide();
                            mouseHold = false;
                        }
                        else
                        {
                            int value = palette.getClickValue(pos);
                            if(clickedSquareObject != null)
                            {
                                clickedSquareObject.setValue(value);
                                palette.hide();
                                mouseHold = false;
                            }
                            bool conflict = checkInputParameter(squareObjects);
                            if(conflict == true)
                            {
                                start.setEnable(false);
                            }
                            else
                            {
                                for (int row = 0; row < 9; row++)
                                {
                                    for (int col = 0; col < 9; col++)
                                    {
                                        squareObjects[row, col].setFontColor(SquareObject.FontColor.Black);
                                    }
                                }
                                start.setEnable(true);
                            }
                        }
                    }
                    else
                    {
                        bool isButtonClisk = false;
                        foreach (Button button in buttons)
                        {
                            if (button.isClick(pos))
                            {
                                button.onClick(squareObjects);
                                isButtonClisk = true;
                            }
                        }

                        if (isButtonClisk == false)
                        {
                            if (isClick(pos) == true)
                            {
                                mouseHold = true;
                                palette.show(pos);
                                for (int row = 0; row < 9; row++)
                                {
                                    for (int col = 0; col < 9; col++)
                                    {
                                        if (squareObjects[row, col].isClick(pos) == true)
                                        {
                                            clickedSquareObject = squareObjects[row, col];
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                // Altseedを更新する。
                asd.Engine.Update();
            }

            // Altseedを終了する。
            asd.Engine.Terminate();

        }

        private bool isClick(asd.Vector2DF pos)
        {
            if (pos.X > offsetX && pos.X < offsetX + width &&
                pos.Y > offsetY && pos.Y < offsetY + height)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool checkInputParameter(SquareObject[,] squareObjects)
        {
            bool conflict = false;
            for(int row = 0; row < 9; row++)
            {
                for(int col = 0; col < 9; col++)
                {
                    if(squareObjects[row,col].isSetValue() == true)
                    {
                        int value = squareObjects[row,col].getValue();
                        bool ret = checkRowNumber(squareObjects, row, col, value);
                        ret |= checkColNumber(squareObjects, row, col, value);
                        ret |= check9AreaNumber(squareObjects, row, col, value);
                        if(ret == true)
                        {
                            squareObjects[row, col].setFontColor(SquareObject.FontColor.Red);
                            conflict = true;
                        }
                    }
                }
            }
            return conflict;
        }

        private bool checkRowNumber(SquareObject[,] squareObjects, int row, int col, int value)
        {
            bool ret = false;
            for(int c = 0; c < 9; c++)
            {
                if(c != col &&
                    squareObjects[row, c].getValue() == value)
                {
                    squareObjects[row, c].setFontColor(SquareObject.FontColor.Red);
                    ret = true;
                }
            }
            return ret;
        }

        private bool checkColNumber(SquareObject[,] squareObjects, int row, int col, int value)
        {
            bool ret = false;
            for (int r = 0; r < 9; r++)
            {
                if (r != row &&
                    squareObjects[r, col].getValue() == value)
                {
                    squareObjects[r, col].setFontColor(SquareObject.FontColor.Red);
                    ret = true;
                }
            }
            return ret;
        }

        private bool check9AreaNumber(SquareObject[,] squareObjects, int row, int col, int value)
        {
            bool ret = false;
            int rowStart;
            int colStart;
            getRowCol9Area(row, col, out rowStart, out colStart);

            for (int r = rowStart; r < rowStart + 3; r++)
            {
                for (int c = colStart; c < colStart + 3; c++)
                {
                    if (r != row && c != col &&
                        squareObjects[r, c].getValue() == value)
                    {
                        squareObjects[r, c].setFontColor(SquareObject.FontColor.Red);
                        ret = true;
                    }
                }
            }
            return ret;
        }

        private void getRowCol9Area(int row, int col, out int rowStart, out int colStart)
        {
            if (row >= 0 && row <= 2)
            {
                rowStart = 0;
            }
            else if (row >= 3 && row <= 5)
            {
                rowStart = 3;
            }
            else
            {
                rowStart = 6;
            }

            if (col >= 0 && col <= 2)
            {
                colStart = 0;
            }
            else if (col >= 3 && col <= 5)
            {
                colStart = 3;
            }
            else
            {
                colStart = 6;
            }
        }
    }

}
