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
                // Check rows have no repeated values:
                for (int i = 0; i < 9; i++)
                {
                    var rowArray = new List<char>();
                    for (int j = 0; j < 9; j++)
                    {
                        if (sudokuArray[i, j] == ' ')
                            continue;
                        if (rowArray.Contains(sudokuArray[i,j]))
                            return false;
                        else
                            rowArray.Add(sudokuArray[i, j]);
                    }
                }
                // Check cols have no repeated values:
                for (int j = 0; j < 9; j++)
                {
                    var colArray = new List<char>();
                    for (int i = 0; i < 9; i++)
                    {
                        if (sudokuArray[i, j] == ' ')
                            continue;
                        if (colArray.Contains(sudokuArray[i, j]))
                            return false;
                        else
                            colArray.Add(sudokuArray[i, j]);
                    }
                }

                // Check squares have no repeated values:
                // TODO

                return true;
            }
        }


    }
}
