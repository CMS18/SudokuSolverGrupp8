﻿using System;
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
            string sudokuString = "037060000205000800006908000000600024001503600650009000000302700009000402000050360";

            FillBoardArrayWithSudokuString(sudokuString, boardArray);
            boardArray = SolveArray(boardArray);
            PrintBoardArray(boardArray);

            Console.ReadKey();
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

        private static int[,] SolveArray(int[,] boardArray)
        {
            bool placed;
            while (true)
            {
                placed = false;
                for (int row = 0; row < 9; row++)
                {
                    for (int col = 0; col < 9; col++)
                    {
                        if (boardArray[row, col] == 0)//fixa row, col senare.
                        {
                            string numbers = GetRowColBlockNum(boardArray, row, col);
                            string possibleSolutions = "";
                            for (int i = 1; i < 10; i++)
                            {
                                if (!numbers.Contains(i.ToString()))
                                {
                                    possibleSolutions += i.ToString();
                                }
                            }
                            //Console.WriteLine(possibleSolutions);
                            //Console.WriteLine("ran: "+times);
                            if (possibleSolutions.Length == 1)
                            {
                                boardArray[row, col] = int.Parse(possibleSolutions);
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
                    //boardArray = SolveHarderArrays(boardArray);
                    break;
                }
            }
            return boardArray;
        }

        private static int[,] SolveHarderArrays(int[,] boardArray)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (boardArray[row, col] == 0)
                    {
                        boardArray[row, col] = 9;
                    }
                }
            }
            return boardArray;
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

            //if (startRow > -1 && startRow < 3)
            //{
            //    startRow = 0;
            //    Console.WriteLine("kördes1row");
            //}
            //else if (startRow > 2 && startRow < 6)
            //{
            //    startRow = 3;
            //    Console.WriteLine("kördes2row");
            //}
            //else if (startRow > 5 && startRow < 9)
            //{
            //    startRow = 6;
            //    Console.WriteLine("kördes3row");
            //}

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

        private static void FillBoardArrayWithSudokuString(string sudokuString, int[,] boardArray)
        {
            int row = -1;

            for (int i = 0; i < sudokuString.Length; i++)
            {
                int num = int.Parse(sudokuString[i].ToString());
                if (i % 9 == 0) { row++; }

                boardArray[row, i % 9] = num;//fyll varje rad för rad med index 0-8                
                //PrintBoardArray(boardArray);
            }
        }
    }
}