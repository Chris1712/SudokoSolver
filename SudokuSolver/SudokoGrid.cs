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
                return true;
            }


        }

        public bool IsSolved
        {
            get
            {
                return true;
            }
        }


    }
}
