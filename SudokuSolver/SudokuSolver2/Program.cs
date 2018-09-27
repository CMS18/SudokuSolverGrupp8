using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class Program
    {
        public static int test = 0;
        static void Main(string[] args)
        {
            int[,] boardArray = new int[9, 9];
            string sudokuString = "037060000205000800006908000000600024001503600650009000000302700009000402000050360";

            bool parsed = false;
            FillBoardArrayWithSudokuString(sudokuString, boardArray, ref parsed);
            if (parsed)
            {
                Solve(boardArray);
                PrintBoardArray(boardArray);
                Console.WriteLine("Så här långt kom jag på " + test + " försök");
            }

            Console.ReadKey();
        }

        public static bool Solve(int[,] boardArray)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (boardArray[row, col] == 0)
                    {
                        test++;
                        for (int num = 9; num > 0; num--)
                        {
                            string numbers = GetRowColBlockNum(boardArray, row, col);

                            if (!numbers.Contains(num.ToString()))
                            {
                                boardArray[row, col] = num;
                                if (Solve(boardArray))
                                {
                                    return true;
                                }
                                boardArray[row, col] = 0;
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }

        private static void PrintBoardArray(int[,] boardArray)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (boardArray[i, j] == 0) //om understreck istället för noll
                    {
                        Console.Write("_");
                    }
                    else
                    {
                        Console.Write(boardArray[i, j]);
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

        private static string GetRowColBlockNum(int[,] boardArray, int row, int col)
        {
            string allNumbers = "";
            allNumbers += GetRow(boardArray, row);
            allNumbers += GetCol(boardArray, col);
            allNumbers += GetBlock(boardArray, row, col);
            return allNumbers;
        }

        private static string GetBlock(int[,] boardArray, int startRow, int startCol)
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
                    numbersInBlock += boardArray[startRow + row, startCol + col].ToString();
                }
            }
            return numbersInBlock;
        }

        private static string GetRow(int[,] boardArray, int row)
        {
            string numbersInRow = "";

            for (int i = 0; i < 9; i++)
            {
                numbersInRow += boardArray[row, i].ToString();
            }

            return numbersInRow;
        }

        private static string GetCol(int[,] boardArray, int col)
        {
            string numbersInCol = "";

            for (int i = 0; i < 9; i++)
            {
                numbersInCol += boardArray[i, col].ToString();
            }

            return numbersInCol;
        }

        private static void FillBoardArrayWithSudokuString(string sudokuString, int[,] boardArray, ref bool parsed)
        {
            int row = -1;
            int test = 0;

            parsed = true;
            for (int i = 0; i < sudokuString.Length; i++)
            {
                if (!(int.TryParse(sudokuString[i].ToString(), out test)))
                {
                    parsed = false;
                }
            }
            if (parsed)
            {
                for (int i = 0; i < sudokuString.Length; i++)
                {
                    int num = int.Parse(sudokuString[i].ToString());
                    if (i % 9 == 0) { row++; }
                    boardArray[row, i % 9] = num;
                }
            }
            else { Console.WriteLine("Felaktigt bräde"); }
        }
    }
}