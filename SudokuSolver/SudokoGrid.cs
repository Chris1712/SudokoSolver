using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class SudokoGrid
    {
        public readonly char[,] sudokuArray;
        string allowedChars = " 123456789";

        public SudokoGrid(char[,] array)
        {
            sudokuArray = new char[9, 9];

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (!allowedChars.Contains(array[i, j]))
                    {
                        throw new System.ArgumentException();
                    }
                    sudokuArray[i, j] = array[i, j];
                }
            }
        }

        public Tuple<int,int,List<Char>> NextPositionToPopulate()
        {
            // figure out where in the grid we have the most information
            // for empty squares, which has largest count of nonempty (row entries, col entries, subsquare entries)
            // this returns a location ( int[,]) and a list of values  to try
            var ShortestPossibleEntryList = new List<Char> { '1', '2', '3', '4', '5', '6', '7', '8', '9', ' ' }; // has 10 elts so will always be beaten
            int iPositionToPopulate = 0; int jPositionToPopulate = 0;


            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if ((sudokuArray[i, j]) != ' ')
                        continue;
                    var possibleEntries = new List<Char> { '1', '2', '3', '4', '5', '6', '7', '8', '9' };

                    // Strike out anything in this position's row
                    for (int k = 0; k < 8; k++)
                    {
                        possibleEntries.Remove(sudokuArray[i, k]);
                    }
                    // Strike out anything in this position's col
                    for (int k = 0; k < 8; k++)
                    {
                        possibleEntries.Remove(sudokuArray[k, j]);
                    }
                    // Strike out anything in this position's subsquare
                    int x = i / 3; int y = j / 3; // the row and col of the subsquare we're in.

                    for (int subSquarei = 0; subSquarei < 3; subSquarei++)
                    {
                        for (int subSquarej = 0; subSquarej < 3; subSquarej++)
                        {
                            possibleEntries.Remove(sudokuArray[(3 * x) + subSquarei, (3 * y) + subSquarej]);
                        }
                    }

                    // We've now removed everything from possibleEntries that we can.
                    if (possibleEntries.Count() < ShortestPossibleEntryList.Count)
                    {
                        ShortestPossibleEntryList = new List<Char>(possibleEntries);
                        iPositionToPopulate = i; jPositionToPopulate = j;

                    }
                }
            }

            return Tuple.Create(iPositionToPopulate, jPositionToPopulate, ShortestPossibleEntryList);
        }

        public bool IsComplete {
            get
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (sudokuArray[i,j] == ' ')
                        {
                            return false;
                        }
                    }
                }
                return true;
            }


        }
        public bool IsValid
        {
            get
            {
                // Check rows have no repeated values:
                for (int i = 0; i < 9; i++)
                {
                    var rowList = new List<char>();
                    for (int j = 0; j < 9; j++)
                    {
                        if (sudokuArray[i, j] == ' ')
                            continue;
                        if (rowList.Contains(sudokuArray[i,j]))
                            return false;
                        else
                            rowList.Add(sudokuArray[i, j]);
                    }
                }
                // Check cols have no repeated values:
                for (int j = 0; j < 9; j++)
                {
                    var colList = new List<char>();
                    for (int i = 0; i < 9; i++)
                    {
                        if (sudokuArray[i, j] == ' ')
                            continue;
                        if (colList.Contains(sudokuArray[i, j]))
                            return false;
                        else
                            colList.Add(sudokuArray[i, j]);
                    }
                }

                // Check subsquares have no repeated values:
                for (int x = 0; x < 3; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        // We are in the subsquare row x col y
                        var squareEntries = new List<char>();
                        for (int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                if (sudokuArray[(3*x) + i, (3*y)+j] == ' ')
                                    continue;
                                if (squareEntries.Contains(sudokuArray[(3 * x) + i, (3 * y) + j]))
                                    return false;
                                else
                                    squareEntries.Add(sudokuArray[(3 * x) + i, (3 * y) + j]);
                            }
                        }

                    }
                }
                

                return true;
            }
        }

        public static SudokoGrid RecursiveSolve(SudokoGrid gridToSolve)
        {
            if (!gridToSolve.IsValid)
            {
                return null;
            }
            if (gridToSolve.IsComplete)
            {
                return gridToSolve;
            }

            // So we have a grid that's incomplete and might have solutions, so:
            var nextPosTuple = gridToSolve.NextPositionToPopulate();

            foreach (var possibleEntry in nextPosTuple.Item3)
            {
                var sudokuGridToAmend = new char[9, 9];
                Array.Copy(gridToSolve.sudokuArray, sudokuGridToAmend, 81);

                sudokuGridToAmend[nextPosTuple.Item1, nextPosTuple.Item2] = possibleEntry;

                // Recursion is magic!
                var newGridToSolve = new SudokoGrid(sudokuGridToAmend);
                var solvedGrid = RecursiveSolve(newGridToSolve);

                if (solvedGrid != null)
                {
                    return solvedGrid;
                }
            }

            // Otherwise the grid we've been given has no solutions, so
            return null;
        }
    }
}
