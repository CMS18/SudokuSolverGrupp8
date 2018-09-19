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
        static void Main(string[] args)
        {
            int[,] boardArray = new int[9, 9];
            string sudokuString = "619030040270061008000047621486302079000014580031009060005720806320106057160400030";

            FillBoardArrayWithSudokuString(sudokuString, boardArray);

            PrintBoardArray(boardArray);

            Console.ReadLine();
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

                    //Console.Write(boardArray[i, j]);    // om 0 istället för understreck

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

        private static void FillBoardArrayWithSudokuString(string sudokuString, int[,] boardArray)
        {
            int row = -1;

            for (int i = 0; i < sudokuString.Length; i++)
            {
                int num = int.Parse(sudokuString[i].ToString());
                if (i % 9 == 0) { row++; }

                boardArray[row, i % 9] = num;   //fyll varje rad för rad med index 0-8


                //Thread.Sleep(450); //För att se arrayen
                //PrintBoardArray(boardArray);
            }

        }



    }
}