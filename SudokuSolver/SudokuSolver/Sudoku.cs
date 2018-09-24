using System;
using System.Linq;

namespace SudokuSolver
{
    public class Sudoku
    {
        static string mustContainAllCharacters = "123456789";

        public static void Solve(int[,] sudokuBoard)
        {
            while (true)
            {
                //CHECK ROWS
                //CHECK COLUMNS
                //CHECK BOXES

                if (AreAllRowsComplete(sudokuBoard) && AreAllBoxesComplete(sudokuBoard) && AreAllColumnsComplete(sudokuBoard))
                {
                    Console.WriteLine("Sudoku solved");
                    break;
                }
                else
                {
                    Console.WriteLine("Sudoku solving...");
                    Console.WriteLine("");
                }
            }
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