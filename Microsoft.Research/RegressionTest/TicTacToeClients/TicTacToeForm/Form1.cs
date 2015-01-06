// CodeContracts
// 
// Copyright (c) Microsoft Corporation
// 
// All rights reserved. 
// 
// MIT License
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics.Contracts;
using TicTacToeEngine;

namespace TicTacToeForm
{

    public partial class TicTacToeForm : Form
    {
        public TicTacToe engine = new TicTacToe();
        public enum marks { none = 10, X, O }
        int turn;
        int endGame;

        public TicTacToeForm()
        {
            InitializeComponent();
            introText();
            bool userFirst = true;
            //Show a simple Yes No Dialog box where No is like cancel.
            userFirst = System.Windows.Forms.MessageBox.Show(
                            "Do you want to go first?", "Who goes first?", System.Windows.Forms.MessageBoxButtons.YesNo,
                            System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes;

            if (userFirst == false)
            {
                turn = 1;
                engine.assignMarks(turn);
                engine.compMove();
                drawBoard();
                turn--;
            }
            else
            {
                turn = 0;
                engine.assignMarks(turn);
            }
        }

        public void introText()
        {
            console.AppendText("Tic-Tac-Toe");
            console.AppendText(Environment.NewLine);
            console.AppendText("By: Cyril Silverman");
            console.AppendText(Environment.NewLine);
            console.AppendText(Environment.NewLine);
            drawBoard();
        }

        public void drawBoard()
        {
            CodeContract.Requires(engine.board != null);
            square1.Text = 9 < engine.board[0, 0] ? Enum.GetName(typeof(marks), engine.board[0, 0]) : "";
            square2.Text = 9 < engine.board[0, 1] ? Enum.GetName(typeof(marks), engine.board[0, 1]) : "";
            square3.Text = 9 < engine.board[0, 2] ? Enum.GetName(typeof(marks), engine.board[0, 2]) : "";
            square4.Text = 9 < engine.board[1, 0] ? Enum.GetName(typeof(marks), engine.board[1, 0]) : "";
            square5.Text = 9 < engine.board[1, 1] ? Enum.GetName(typeof(marks), engine.board[1, 1]) : "";
            square6.Text = 9 < engine.board[1, 2] ? Enum.GetName(typeof(marks), engine.board[1, 2]) : "";
            square7.Text = 9 < engine.board[2, 0] ? Enum.GetName(typeof(marks), engine.board[2, 0]) : "";
            square8.Text = 9 < engine.board[2, 1] ? Enum.GetName(typeof(marks), engine.board[2, 1]) : "";
            square9.Text = 9 < engine.board[2, 2] ? Enum.GetName(typeof(marks), engine.board[2, 2]) : "";
        }

        private void exitGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void square1_Click(object sender, EventArgs e)
        {
            CodeContract.Requires(square1.Text != null);
            if (endGame == 0)
            {
                if (square1.Text.Equals(""))
                {
                    engine.placeMove(turn, 1);
                    if (engine.checkEndGame() == 0)
                    {
                        engine.compMove();
                    }
                    drawBoard();
                    writeEnd();
                }
                else
                {
                    console.AppendText("There is already a mark there!");
                    console.AppendText(Environment.NewLine);
                }
            }
            else
            {
                console.AppendText("The game is already over!");
                console.AppendText(Environment.NewLine);
            }
        }

        private void square2_Click(object sender, EventArgs e)
        {
            CodeContract.Requires(square2.Text != null);
            if (endGame == 0)
            {
                if (square2.Text.Equals(""))
                {
                    engine.placeMove(turn, 2);
                    if (engine.checkEndGame() == 0)
                    {
                        engine.compMove();
                    }
                    drawBoard();
                    writeEnd();
                }
                else
                {
                    console.AppendText("There is already a mark there!");
                    console.AppendText(Environment.NewLine);
                }
            }
            else
            {
                console.AppendText("The game is already over!");
                console.AppendText(Environment.NewLine);
            }
        }

        private void square3_Click(object sender, EventArgs e)
        {
            CodeContract.Requires(square3.Text != null);
            if (endGame == 0)
            {
                if (square3.Text.Equals(""))
                {
                    engine.placeMove(turn, 3);
                    if (engine.checkEndGame() == 0)
                    {
                        engine.compMove();
                    }
                    drawBoard();
                    writeEnd();
                }
                else
                {
                    console.AppendText("There is already a mark there!");
                    console.AppendText(Environment.NewLine);
                }
            }
            else
            {
                console.AppendText("The game is already over!");
                console.AppendText(Environment.NewLine);
            }
        }

        private void square4_Click(object sender, EventArgs e)
        {
            CodeContract.Requires(square4.Text != null);
            if (endGame == 0)
            {
                if (square4.Text.Equals(""))
                {
                    engine.placeMove(turn, 4);
                    if (engine.checkEndGame() == 0)
                    {
                        engine.compMove();
                    }
                    drawBoard();
                    writeEnd();
                }
                else
                {
                    console.AppendText("There is already a mark there!");
                    console.AppendText(Environment.NewLine);
                }
            }
            else
            {
                console.AppendText("The game is already over!");
                console.AppendText(Environment.NewLine);
            }
        }

        private void square5_Click(object sender, EventArgs e)
        {
            CodeContract.Requires(square5.Text != null);
            if (endGame == 0)
            {
                if (square5.Text.Equals(""))
                {
                    engine.placeMove(turn, 5);
                    if (engine.checkEndGame() == 0)
                    {
                        engine.compMove();
                    }
                    drawBoard();
                    writeEnd();
                }
                else
                {
                    console.AppendText("There is already a mark there!");
                    console.AppendText(Environment.NewLine);
                }
            }
            else
            {
                console.AppendText("The game is already over!");
                console.AppendText(Environment.NewLine);
            }
        }

        private void square6_Click(object sender, EventArgs e)
        {
            CodeContract.Requires(square6.Text != null);
            if (endGame == 0)
            {
                if (square6.Text.Equals(""))
                {
                    engine.placeMove(turn, 6);
                    if (engine.checkEndGame() == 0)
                    {
                        engine.compMove();
                    }
                    drawBoard();
                    writeEnd();
                }
                else
                {
                    console.AppendText("There is already a mark there!");
                    console.AppendText(Environment.NewLine);
                }
            }
            else
            {
                console.AppendText("The game is already over!");
                console.AppendText(Environment.NewLine);
            }

        }

        private void square7_Click(object sender, EventArgs e)
        {
            CodeContract.Requires(square7.Text != null);
            if (endGame == 0)
            {
                if (square7.Text.Equals(""))
                {
                    engine.placeMove(turn, 7);
                    if (engine.checkEndGame() == 0)
                    {
                        engine.compMove();
                    }
                    drawBoard();
                    writeEnd();
                }
                else
                {
                    console.AppendText("There is already a mark there!");
                    console.AppendText(Environment.NewLine);
                }
            }
            else
            {
                console.AppendText("The game is already over!");
                console.AppendText(Environment.NewLine);
            }
        }

        private void square8_Click(object sender, EventArgs e)
        {
            CodeContract.Requires(square8.Text != null);
            if (endGame == 0)
            {
                if (square8.Text.Equals(""))
                {
                    engine.placeMove(turn, 8);
                    if (engine.checkEndGame() == 0)
                    {
                        engine.compMove();
                    }
                    drawBoard();
                    writeEnd();
                }
                else
                {
                    console.AppendText("There is already a mark there!");
                    console.AppendText(Environment.NewLine);
                }
            }
            else
            {
                console.AppendText("The game is already over!");
                console.AppendText(Environment.NewLine);
            }
        }

        private void square9_Click(object sender, EventArgs e)
        {
            CodeContract.Requires(square9.Text != null);
            if (endGame == 0)
            {
                if (square9.Text.Equals(""))
                {
                    engine.placeMove(turn, 9);
                    if (engine.checkEndGame() == 0)
                    {
                        engine.compMove();
                    }
                    drawBoard();
                    writeEnd();
                }
                else
                {
                    console.AppendText("There is already a mark there!");
                    console.AppendText(Environment.NewLine);
                }
            }
            else
            {
                console.AppendText("The game is already over!");
                console.AppendText(Environment.NewLine);
            }
        }



        private void console_TextChanged(object sender, EventArgs e)
        {

        }

        public void writeEnd()
        {
            CodeContract.Requires(endGame <= 3);
            CodeContract.Requires(0 <= endGame);

            endGame = engine.checkEndGame();
            if (endGame == 1)
                console.AppendText("You win!" + Environment.NewLine);
            else if (endGame == 2)
                console.AppendText("You lose!" + Environment.NewLine);
            else if (endGame == 3)
                console.AppendText("The game ends in a draw!" + Environment.NewLine);
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void TicTacToeForm_Load(object sender, EventArgs e)
        {

        }
    }
}
