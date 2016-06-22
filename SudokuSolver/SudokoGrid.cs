using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class SudokoGrid
    {
        char[,] sudokuArray;
        string allowedChars = " 123456789";

        public char GetNumber(int row, int col)
        {
            return sudokuArray[row, col];
        }

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
                // todo, possible efficiency; while computing this keep track of which row/square/col has greatest # of entries, use as place to enter next

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
                var squareEntries = new List<char>();
                // 0,0 -> 2,2
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (sudokuArray[i, j] == ' ')
                            continue;
                        if (squareEntries.Contains(sudokuArray[i, j]))
                            return false;
                        else
                            squareEntries.Add(sudokuArray[i, j]);
                    }
                }
                // 0,6 -> 2,8
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 6; j < 9; j++)
                    {
                        if (sudokuArray[i, j] == ' ')
                            continue;
                        if (squareEntries.Contains(sudokuArray[i, j]))
                            return false;
                        else
                            squareEntries.Add(sudokuArray[i, j]);
                    }
                }
                // 3,3 -> 5,5
                for (int i = 3; i < 6; i++)
                {
                    for (int j = 3; j < 6; j++)
                    {
                        if (sudokuArray[i, j] == ' ')
                            continue;
                        if (squareEntries.Contains(sudokuArray[i, j]))
                            return false;
                        else
                            squareEntries.Add(sudokuArray[i, j]);
                    }
                }
                // 6,0 -> 8,2
                for (int i = 6; i < 9; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (sudokuArray[i, j] == ' ')
                            continue;
                        if (squareEntries.Contains(sudokuArray[i, j]))
                            return false;
                        else
                            squareEntries.Add(sudokuArray[i, j]);
                    }
                }
                // 6,6 -> 8,8
                for (int i = 6; i < 9; i++)
                {
                    for (int j = 6; j < 9; j++)
                    {
                        if (sudokuArray[i, j] == ' ')
                            continue;
                        if (squareEntries.Contains(sudokuArray[i, j]))
                            return false;
                        else
                            squareEntries.Add(sudokuArray[i, j]);
                    }
                }

                return true;
            }
        }


    }
}
