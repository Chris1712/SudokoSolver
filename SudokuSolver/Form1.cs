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
        SudokuGrid savedSudoku;

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
            var SudokuGridToSolve = GetSudokuGridFromTextBoxes();
            if (!SudokuGridToSolve.IsValid)
            {
                MessageBox.Show("Invalid sudoku");
            }
            else
            {
                OutputSudokuToTextBoxes(SudokuGrid.RecursiveSolve(SudokuGridToSolve));
            }
            
        }
        private void buttonRandomise_Click(object sender, EventArgs e)
        {
            RandomiseTextBoxes(0.03);
        }

        private SudokuGrid GetSudokuGridFromTextBoxes()
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

            var mySudokuGrid = new SudokuGrid(formCharArray);
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
        private void OutputSudokuToTextBoxes(SudokuGrid inputGrid)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    textBoxArray[i, j].Text = Convert.ToString(inputGrid.sudokuArray[i, j]);
                }
            }
        }
        private void RandomiseTextBoxes(double density)
        {
            var myRand = new Random();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (myRand.NextDouble() < density)
                    {
                        textBoxArray[i, j].Text = myRand.Next(1, 10).ToString();
                    }
                }
            }
        }


    }
}
