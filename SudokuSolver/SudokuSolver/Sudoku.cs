using System;
using System.Linq;
using System.Net;

namespace SudokuSolver
{
    public class Sudoku
    {
        static string mustContainAllCharacters = "123456789";

        public static void Solve(int[,] sudokuBoard)
        {
            //CHECK ROWS
            //CHECK COLUMNS
            //CHECK BOXES
            if (AreAllRowsComplete(sudokuBoard) && AreAllBoxesComplete(sudokuBoard) && AreAllColumnsComplete(sudokuBoard))
            {
                //Console.WriteLine("Sudoku solved");
            }
            else
            {
                //Flyttar ut lite printar till Main

                //Console.WriteLine("Sudoku solving...");
                //Console.WriteLine("");
                //Console.WriteLine("Is sudoku solvable? true/false ");
                //Console.WriteLine("");
                //Console.WriteLine("");
                Console.WriteLine("---  " + SolveBoard(sudokuBoard) + "  ---");
            }
        }


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
        }


        private static bool SolveBoard(int[,] sudokuBoard)
        {
            Point nextPoint = FindNextUnassigned(sudokuBoard);
            if (nextPoint == null)
            {
                PrintBoardArray(sudokuBoard);

                //Behöver man verkligen Dubbelkolla?
                if (AreAllRowsComplete(sudokuBoard) && AreAllBoxesComplete(sudokuBoard) && AreAllColumnsComplete(sudokuBoard))
                {
                    // If there is no unassigned location, we are done 
                    return true;
                }
            }

            int row = nextPoint.row;
            int column = nextPoint.column;

            //Använd alla potentiella siffror som ett sdukou bräde kan innehålla
            for (int num = 1; num <= 9; num++)
            {
                if (IsNumberAllowed(sudokuBoard, row, column, num))
                {
                    sudokuBoard[row, column] = num;
                    if (SolveBoard(sudokuBoard))
                        return true;

                    sudokuBoard[row, column] = 0;
                }
            }

            return false;
        }

        private static Point FindNextUnassigned(int[,] sudokuBoard)
        {

            for (int j = 0; j < 9; j++)
            {
                for (int k = 0; k < 9; k++)
                {
                    if (sudokuBoard[k, j] == 0)
                    {
                        return new Point { row = k, column = j };
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

        private static bool AreAllBoxesComplete(int[,] sudokuBoard)
        {
            for (int i = 1; i <= 7; i += 3)
            {
                string boxValues = "";
                for (int j = 1; j <= 7; j += 3)
                {
                    boxValues = GetBoxValues(i, j, sudokuBoard);
                    if (!IsRowOrColumnOrBoxComplete(boxValues))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static string GetBoxValues(int i, int j, int[,] sudokuBoard)
        {
            string boxValues = "";

            for (int k = -1; k <= 1; k++)
            {
                for (int l = -1; l <= 1; l++)
                {
                    boxValues += sudokuBoard[i + k, j + l];
                }
            }

            return boxValues;
        }

        private static bool AreAllColumnsComplete(int[,] sudokuBoard)
        {
            for (int i = 0; i < 9; i++)
            {
                string prepareColumns = "";
                for (int j = 0; j < 9; j++)
                {
                    prepareColumns += sudokuBoard[j, i].ToString();
                }
                if (!IsRowOrColumnOrBoxComplete(prepareColumns))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool AreAllRowsComplete(int[,] sudokuBoard)
        {
            for (int i = 0; i < 9; i++)
            {
                string prepareRow = "";
                for (int j = 0; j < 9; j++)
                {
                    prepareRow += sudokuBoard[i, j].ToString();
                }
                if (!IsRowOrColumnOrBoxComplete(prepareRow))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsRowOrColumnOrBoxComplete(string value)
        {

            foreach (var c in value)
            {
                int totalOf = value.Count(x => x == c);

                if (!mustContainAllCharacters.Contains(c.ToString()) && value.Count(x => x == c) != 1)
                {
                    return false;
                }
            }

            return true;
        }
    }
}