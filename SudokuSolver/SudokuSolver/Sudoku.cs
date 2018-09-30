using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class Sudoku
    {
        public int[,] BoardArray { get; set; }
        public string SudokuString { get; set; }

        public Sudoku(string sudokuString)
        {
            BoardArray = new int[9, 9];
            SudokuString = sudokuString;
            FillBoardArrayWithSudokuString(SudokuString, BoardArray);
            BoardArray = SolveArray(BoardArray);
        }

        public void Solve()
        {
            FillBoardArrayWithSudokuString(SudokuString, BoardArray);
            SolveArray(BoardArray);
            PrintBoardArray(BoardArray);
        }

        public int[,] SolveArray(int[,] BoardArray)
        {
            bool placed;
            while (true)
            {
                placed = false;
                for (int row = 0; row < 9; row++)
                {
                    for (int col = 0; col < 9; col++)
                    {
                        if (BoardArray[row, col] == 0)//fixa row, col senare.
                        {
                            string numbers = GetRowColBlockNum(BoardArray, row, col);
                            string possibleSolutions = "";
                            for (int i = 1; i < 10; i++)
                            {
                                if (!numbers.Contains(i.ToString()))
                                {
                                    possibleSolutions += i.ToString();
                                }
                            }

                            if (possibleSolutions.Length == 1)
                            {
                                BoardArray[row, col] = int.Parse(possibleSolutions);
                                placed = true;
                                //Thread.Sleep(100);
                                //Console.Clear();
                                //PrintBoardArray(boardArray);
                            }
                        }
                    }
                }
                if (!placed)
                {
                    break;
                }
            }
            return BoardArray;
        }

        public static string GetRowColBlockNum(int[,] BoardArray, int row, int col)
        {
            string allNumbers = "";
            allNumbers += GetRow(BoardArray, row);
            allNumbers += GetCol(BoardArray, col);
            allNumbers += GetBlock(BoardArray, row, col);
            return allNumbers;
        }

        public static string GetBlock(int[,] BoardArray, int startRow, int startCol)
        {
            string numbersInBlock = "";

            startCol = (startCol > -1 && startCol < 3 ? startCol = 0 :
                        startCol > 2 && startCol < 6 ? startCol = 3 :
                        startCol > 5 && startCol < 9 ? startCol = 6 : startCol = 100);

            startRow = (startRow > -1 && startRow < 3 ? startRow = 0 :
                        startRow > 2 && startRow < 6 ? startRow = 3 :
                        startRow > 5 && startRow < 9 ? startRow = 6 : startRow = 100);

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    numbersInBlock += BoardArray[startRow + row, startCol + col].ToString();
                }
            }
            return numbersInBlock;
        }

        public static string GetRow(int[,] BoardArray, int row)
        {
            string numbersInRow = "";

            for (int i = 0; i < 9; i++)
            {
                numbersInRow += BoardArray[row, i].ToString();
            }

            return numbersInRow;
        }

        public static string GetCol(int[,] BoardArray, int col)
        {
            string numbersInCol = "";

            for (int i = 0; i < 9; i++)
            {
                numbersInCol += BoardArray[i, col].ToString();
            }

            return numbersInCol;
        }

        public void PrintBoardArray(int[,] BoardArray)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (BoardArray[i, j] == 0) //om understreck istället för noll
                    {
                        Console.Write("_");
                    }
                    else
                    {
                        Console.Write(BoardArray[i, j]);
                    }

                    Console.Write(" ");

                    if (j == 2 || j == 5)
                    {
                        Console.Write("| ");
                    }
                }

                Console.WriteLine();

                if (i == 2 || i == 5)
                {
                    Console.WriteLine("---------------------");

                }
            }
            Console.WriteLine();
        }

        public void FillBoardArrayWithSudokuString(string sudokuString, int[,] BoardArray)
        {
            int row = -1;

            for (int i = 0; i < sudokuString.Length; i++)
            {
                int num = int.Parse(sudokuString[i].ToString());
                if (i % 9 == 0) { row++; }

                BoardArray[row, i % 9] = num;//fyll varje rad för rad med index 0-8                
            }
        }
    }
}
