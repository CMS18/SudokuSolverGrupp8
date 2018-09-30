namespace SudokuSolver
{
    using System;
    using System.Linq;

    namespace SudokuSolver
    {
        public class Sudoku
        {
            public static void PrintBoardArray(int[,] boardArray)
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
            }

            public static bool SolveBoard(int[,] sudokuBoard)
            {
                Point nextPoint = FindNextUnassigned(sudokuBoard);

                if (nextPoint == null)
                {
                    PrintBoardArray(sudokuBoard);

                        //Finns det inget kvar att fylla i SolveBoard return true;
                        return true;
                }

                //Döper om variablerna för att använda kortare namn
                int row = nextPoint.row;
                int column = nextPoint.column;

                //Använd alla potentiella siffror som ett sudoku bräde kan innehålla
                for (int guess = 1; guess <= 9; guess++)
                {
                    if (IsNumberAllowed(sudokuBoard, row, column, guess))
                    {
                        sudokuBoard[row, column] = guess;
                        if (SolveBoard(sudokuBoard))//Det här startar själva Rekursionen och testar en "gren" av lösningsalternativ
                            return true;

                        sudokuBoard[row, column] = 0;//Sätter värdet tillbaka till 0 om lösningen misslyckats
                    }
                }

                return false; //backar i trädets gren om en gren av lösningar misslyckats
            }

            private static Point FindNextUnassigned(int[,] sudokuBoard)
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (sudokuBoard[j, i] == 0)
                        {
                            return new Point { row = j, column = i };
                        }
                    }
                }

                return null;
            }


            private static bool IsNumberAllowed(int[,] sudokuBoard, int row, int column, int numberToAssign)
            {
                var test = sudokuBoard[row, column];

                if (IsAllowedInRow(sudokuBoard, row, numberToAssign) && IsAllowedInColumn(sudokuBoard, column, numberToAssign) && IsAllowedInBox(sudokuBoard, row - row % 3, column - column % 3, numberToAssign))
                {
                    return true;
                }

                return false;
            }

            private static bool IsAllowedInBox(int[,] sudokuBoard, int boxStartRow, int boxStartCol, int num)
            {
                //Tack vare modulu i parametern som skickas in behöver vi bara kolla en 3 * 3 grid
                for (int row = 0; row < 3; row++)
                {
                    for (int col = 0; col < 3; col++)
                    {
                        if (sudokuBoard[row + boxStartRow, col + boxStartCol] == num)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
            private static bool IsAllowedInRow(int[,] sudokuBoard, int row, int numberToAssign)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (sudokuBoard[row, i] == numberToAssign)
                    {
                        return false;
                    }
                }

                return true;
            }
            private static bool IsAllowedInColumn(int[,] sudokuBoard, int column, int numberToAssign)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (sudokuBoard[i, column] == numberToAssign)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public class Point
        {
            public int row { get; set; }
            public int column { get; set; }
        }
    }
}