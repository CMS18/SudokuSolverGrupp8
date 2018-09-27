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
        private const string easy1 = "003020600900305001001806400008102900700000008006708200002609500800203009005010300";
        private const string easy2 = "619030040270061008000047621486302079000014580031009060005720806320106057160400030";
        private const string medium1 = "037060000205000800006908000000600024001503600650009000000302700009000402000050360";
        private const string diabolic1 = "000000000000003085001020000000507000004000100090000000500000073002010000000040009";
        private const string diabolic2 = "900040000000010200370000005000000090001000400000705000000020100580300000000000000";
        private const string zen = "000000000000000000000000000000000000000010000000000000000000000000000000000000000";
        private const string unsolvable1 = "..9.287..8.6..4..5..3.....46.........2.71345.........23.....5..9..4..8.7..125.3..";
        private const string unsolvable2 = ".9.3....1....8..46......8..4.5.6..3...32756...6..1.9.4..1......58..2....2....7.6.";
        private const string unsolvable3 = "....41....6.....2...2......32.6.........5..417.......2......23..48......5.1..2...";
        private const string unsolvable4 = "9..1....4.14.3.8....3....9....7.8..18....3..........3..21....7...9.4.5..5...16..3";
        private const string unsolvable5 = ".4.1..35.............2.5......4.89..26.....12.5.3....7..4...16.6....7....1..8..2.";

        static void Main(string[] args)
        {
            int[,] boardArray = new int[9, 9];

            string sudokuString = diabolic1;

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