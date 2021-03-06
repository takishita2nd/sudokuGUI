﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    class Sudoku
    {
        private Square[,] _square;
        private int kariokiCount = 0;

        /**
         * コンストラクタ
         */
        public Sudoku(Square[,] square)
        {
            _square = square;
        }

        /**
         * 実行
         */
        public Square[,] run()
        {
            bool roop = true;
            int now_count = 0;
            int prev_coount = 0;
            while (roop)
            {
                for (int row = 0; row < 9; row++)
                {
                    for (int col = 0; col < 9; col++)
                    {
                        if (_square[row, col].isConfirmed() == false)
                        {
                            Candidate candidate = new Candidate();
                            searchRowLine(_square, row, candidate);
                            searchColLine(_square, col, candidate);
                            search9Area(_square, row, col, candidate);
                            _square[row, col].checkCandidate(candidate);
                        }
                    }
                }
                searchNumber(_square);

                prev_coount = now_count;
                now_count = countInputedNumber(_square);

                if (prev_coount == now_count)
                {
                    kariokiCount = 0;
                    Square s = doKarioki(_square);
                    if (s == null)
                    {
                        Console.WriteLine("失敗しました");
                        return null;
                    }
                    else
                    {
                        _square[s.Row, s.Col].SetValue(s.GetValue());
                    }
                }

                roop = !checkEnd(_square);
            }

            return _square;
        }

        /**
         * 列に対してマス値候補を調べる
         * 
         * squares マス
         * row 列
         * candidate マス値候補
         */
        private void searchRowLine(Square[,] squares, int row, Candidate candidate)
        {
            for (int i = 0; i < 9; i++)
            {
                int val = squares[row, i].GetValue();
                if (val != 0)
                {
                    candidate.value[val - 1] = true;
                }
            }
        }

        /**
         * 行に対してマス値候補を調べる
         * 
         * squares マス
         * col 行
         * candidate マス値候補
         */
        private void searchColLine(Square[,] squares, int col, Candidate candidate)
        {
            for (int i = 0; i < 9; i++)
            {
                int val = squares[i, col].GetValue();
                if (val != 0)
                {
                    candidate.value[val - 1] = true;
                }
            }
        }

        /**
         * ９マスエリアに対してマス値候補を調べる
         * 
         * squares マス
         * row 列
         * col 行
         * candidate マス値候補
         */
        private void search9Area(Square[,] squares, int row, int col, Candidate candidate)
        {
            int rowStart;
            int colStart;
            getRowCol9Area(row, col, out rowStart, out colStart);

            for (int r = rowStart; r < rowStart + 3; r++)
            {
                for (int c = colStart; c < colStart + 3; c++)
                {
                    int val = squares[r, c].GetValue();
                    if (val != 0)
                    {
                        candidate.value[val - 1] = true;
                    }
                }
            }
        }

        /**
         * ラインチェックを実行する
         *
         * squares マス
         */
        private void searchNumber(Square[,] squares)
        {
            for (int number = 1; number <= 9; number++)
            {
                bool[,] tempTable = new bool[9, 9];
                // 初期化
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        tempTable[i, j] = false;
                    }
                }

                // 数字が入らないところをtrueにする
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (tempTable[i, j] == false)
                        {
                            tempTable[i, j] = squares[i, j].isConfirmed();
                            if (squares[i, j].GetValue() == number)
                            {
                                for (int row = 0; row < 9; row++)
                                {
                                    tempTable[row, j] = true;
                                }
                                for (int col = 0; col < 9; col++)
                                {
                                    tempTable[i, col] = true;
                                }

                                int rowStart;
                                int colStart;
                                getRowCol9Area(i, j, out rowStart, out colStart);
                                for (int r = rowStart; r < rowStart + 3; r++)
                                {
                                    for (int c = colStart; c < colStart + 3; c++)
                                    {
                                        tempTable[r, c] = true;
                                    }
                                }
                            }
                        }
                    }
                }

                // 結果を確認する
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (tempTable[i, j] == false)
                        {
                            int rowStart;
                            int colStart;
                            getRowCol9Area(i, j, out rowStart, out colStart);

                            int count = 0;
                            for (int r = rowStart; r < rowStart + 3; r++)
                            {
                                for (int c = colStart; c < colStart + 3; c++)
                                {
                                    if (tempTable[r, c] == false)
                                    {
                                        count++;
                                    }
                                }
                            }
                            if (count == 1)
                            {
                                squares[i, j].SetValue(number);
                            }
                        }
                    }
                }
            }
        }

        /**
         * ９マスエリアの始点行列を算出する
         * 
         * row 列
         * col 行
         * rowStart 開始列
         * colStart 開始行
         */
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

        /**
         * 解析終了判定
         *
         * squares マス
         */
        private bool checkEnd(Square[,] squares)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (squares[i, j].isConfirmed() == false)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /**
         * マスのクローンを作成する
         *
         * squares マス
         */
        private Square[,] makeClone(Square[,] _square)
        {
            Square[,] ret = new Square[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    ret[i, j] = _square[i, j].Clone();
                }
            }

            return ret;
        }

        /**
         * 入力済みマスの数を数える
         *
         * squares マス
         */
        private int countInputedNumber(Square[,] _square)
        {
            int ret = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (_square[i, j].isConfirmed())
                    {
                        ret++;
                    }
                }
            }
            return ret;
        }

        /**
         * 仮置き処理
         *
         * squares マス
         */
        private Square doKarioki(Square[,] squares)
        {
            Square ret = null;
            List<Square> kariokiList = searchKariokiSquare(squares);
            kariokiCount++;
            if(kariokiCount >= 100)
            {
                return null;
            }

            foreach (var s in kariokiList)
            {
                bool roop = true;
                int kariValue = GetUnconfirmedValue(s.GetCandidate());
                if (kariValue == 0)
                {
                    return null;
                }
                Square[,] copySquare = makeClone(squares);
                copySquare[s.Row, s.Col].SetValue(kariValue);
                int now_count = 0;
                int prev_coount = 0;
                while (roop)
                {
                    for (int row = 0; row < 9; row++)
                    {
                        for (int col = 0; col < 9; col++)
                        {
                            if (copySquare[row, col].isConfirmed() == false)
                            {
                                Candidate candidate = new Candidate();
                                searchRowLine(copySquare, row, candidate);
                                searchColLine(copySquare, col, candidate);
                                search9Area(copySquare, row, col, candidate);
                                copySquare[row, col].checkCandidate(candidate);
                            }
                        }
                    }
                    searchNumber(copySquare);

                    if (checkContradict(copySquare))
                    {
                        break;
                    }

                    prev_coount = now_count;
                    now_count = countInputedNumber(copySquare);

                    if (prev_coount == now_count)
                    {
                        Square s2 = doKarioki(copySquare);
                        if (s2 == null)
                        {
                            break;
                        }
                        else
                        {
                            copySquare[s2.Row, s2.Col].SetValue(s2.GetValue());
                        }
                    }

                    if (checkEnd(copySquare) == true)
                    {
                        roop = false;
                        s.SetValue(kariValue);
                        ret = s;
                    }
                }
                if(ret != null)
                {
                    break;
                }
            }
            return ret;
        }

        /**
         * 仮置き対象のマスを抽出する
         *
         * squares マス
         */
        private List<Square> searchKariokiSquare(Square[,] squares)
        {
            List<Square> ret = null;
            for (int row = 0; row < 9; row += 3)
            {
                for (int col = 0; col < 9; col += 3)
                {
                    List<Square> temp = new List<Square>();
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (squares[row + i, col + j].isConfirmed() == false)
                            {
                                temp.Add(squares[row + i, col + j]);
                            }
                        }
                    }
                    if (ret != null)
                    {
                        if (ret.Count > temp.Count && temp.Count != 0)
                        {
                            ret = temp;
                        }
                    }
                    else if(temp.Count != 0)
                    {
                        ret = temp;
                    }
                }
            }
            return ret;
        }

        /**
         * 確定していない値の小さい方を検出する
         *
         * candidate マス値候補
         */
        private int GetUnconfirmedValue(Candidate candidate)
        {
            for (int i = 0; i < candidate.value.Length; i++)
            {
                if (candidate.value[i] == false)
                {
                    return i + 1;
                }
            }
            return 0;
        }

        /**
         * 矛盾チェック
         *
         * squares マス
         */
        private bool checkContradict(Square[,] squares)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if(squares[row,col].isConfirmed() == false)
                    {
                        Candidate candidate = new Candidate();
                        searchRowLine(squares, row, candidate);
                        searchColLine(squares, col, candidate);
                        search9Area(squares, row, col, candidate);
                        if(candidate.Count() == 9)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }


    }
}
