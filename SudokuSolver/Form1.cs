using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class Form1 : Form
    {
        TextBox[,] textBoxArray;
        SudokoGrid savedSudoku;

        public Form1()
        {
            InitializeComponent();
            textBoxArray = new TextBox[9, 9];

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    var myTextBox = new TextBox();
                    myTextBox.TextAlign = HorizontalAlignment.Center;
                    if (i < 3 && j < 3 || i < 3 && j>5 || i > 2 && i < 6 && j > 2 && j < 6 || i >5 && j < 3 || i > 5 && j > 5)
                    {
                        myTextBox.BackColor = Color.LightBlue;
                    }
                    textBoxArray[i, j] = myTextBox;
                    tableLayoutPanel1.Controls.Add(myTextBox, j, i);
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            savedSudoku = GetSudokuGridFromTextBoxes();
        }
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            if (savedSudoku == null)
                return;
            OutputSudokuToTextBoxes(savedSudoku);
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            ResetTextBoxes();
        }

        private void buttonSolve_Click(object sender, EventArgs e)
        {
            var SudokoGridToSolve = GetSudokuGridFromTextBoxes();
            if (!SudokoGridToSolve.IsValid)
            {
                MessageBox.Show("Invalid sudoku");
            }
            else
            {
                //MessageBox.Show("Go in these coords:" + SudokoGridToSolve.NextPositionToPopulate().Item1 + "," + SudokoGridToSolve.NextPositionToPopulate().Item2);
            }
            
        }

        private SudokoGrid RecursiveSolve(SudokoGrid gridToSolve)
        {
            // todo: put this as a static method in the sudokugrid class? Not really part of the form logic

            // check if grid is valid, if not return null
            // check if grid is complete, if it is, return that grid

            // else we have a grid that's incomplete and might have solutions, so:
            // get the next place to play from the nextpositiontopopulate method, and call self with that for all suggested values. If any of those return a grid, stop and return it.
            
            
            // otherwise the grid we've been given has no solutions, so
            return null;
        }

        private SudokoGrid GetSudokuGridFromTextBoxes()
        {
            var formCharArray = new char[9, 9];

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (textBoxArray[i, j].Text == "")
                    {
                        formCharArray[i, j] = ' ';
                    }
                    else
                    {
                        formCharArray[i, j] = textBoxArray[i, j].Text[0];
                    }
                }
            }

            var mySudokuGrid = new SudokoGrid(formCharArray);
            return mySudokuGrid;
        }
        private void ResetTextBoxes()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    textBoxArray[i, j].Text = "";
                }
            }
        }
        private void OutputSudokuToTextBoxes(SudokoGrid inputGrid)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    textBoxArray[i, j].Text = Convert.ToString(inputGrid.GetNumber(i, j));
                }
            }
        }


    }
}
