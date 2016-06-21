﻿using System;
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

        void ResetTextBoxes()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    textBoxArray[i, j].Text = "";
                }
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            ResetTextBoxes();
        }
    }
}