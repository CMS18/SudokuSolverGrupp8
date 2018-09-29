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
        static Dictionary<string, string> SudokuTest = new Dictionary<string, string>
        {
            {"easy1", "003020600900305001001806400008102900700000008006708200002609500800203009005010300" },
            {"easy2","619030040270061008000047621486302079000014580031009060005720806320106057160400030"},
            {"medium1", "037060000205000800006908000000600024001503600650009000000302700009000402000050360" },
            {"diabolic1", "000000000000003085001020000000507000004000100090000000500000073002010000000040009" },
            {"diabolic2", "900040000000010200370000005000000090001000400000705000000020100580300000000000000" },
            {"zen", "000000000000000000000000000000000000000010000000000000000000000000000000000000000" },
            {"unsolvable1", "..9.287..8.6..4..5..3.....46.........2.71345.........23.....5..9..4..8.7..125.3.." },
            {"unsolvable2", ".9.3....1....8..46......8..4.5.6..3...32756...6..1.9.4..1......58..2....2....7.6." },
            {"unsolvable3", "....41....6.....2...2......32.6.........5..417.......2......23..48......5.1..2..." },
            {"unsolvable4", "9..1....4.14.3.8....3....9....7.8..18....3..........3..21....7...9.4.5..5...16..3" },
            {"unsolvable5", ".4.1..35.............2.5......4.89..26.....12.5.3....7..4...16.6....7....1..8..2." },
        };

        static void Main(string[] args)
        {
            int[,] sudokuBoard = new int[9, 9];
            //string sudokuString = "619030040270061008000047621486302079000014580031009060005720806320106057160400030";
            //string sudokuString2 = diabolic2;

            foreach (var stringBoard in SudokuTest)//stringboard är elementet "key,value-pair"
            {
                FillBoardArrayWithSudokuString(stringBoard.Value, sudokuBoard);

                //Sudoku.PrintBoardArray(boardArray);
                Console.WriteLine();
                Console.WriteLine("Is "+stringBoard.Key+" solvable?");
                Console.WriteLine("---  " + Sudoku.SolveBoard(sudokuBoard) + "  ---");
                Console.WriteLine();
                Console.WriteLine("----------------------------");
            }

            Console.ReadLine();
        }

        private static void FillBoardArrayWithSudokuString(string sudokuString, int[,] sudokuBoard)
        {
            int row = -1;

            for (int i = 0; i < sudokuString.Length; i++)
            {
                int num = 0;
                if (int.TryParse(sudokuString[i].ToString(), out num) == false)
                {
                    num = 0;
                }
                if (i % 9 == 0) { row++; }

                sudokuBoard[row, i % 9] = num;
            }

        }
    }
}