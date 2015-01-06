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
using TicTacToeEngine;
using Microsoft.Contracts;

namespace TicTacToeForm
{
    public partial class Form1 : Form
    {
        public static TicTacToe engine = new TicTacToe();
        //public static TicTacToeForm gameForm = new TicTacToeForm();
        public enum marks { none = 10, X, O }

        public Form1()
        {
            InitializeComponent();

            //Write the introduction text and draw the board
            introText();
            //Get whether the user or the computer goes first
            int turn = chooseFirstPlayer();
            //Assign the marks respectively to who goes first
            engine.assignMarks(turn);
            //While there is no winner
            while (engine.checkEndGame() == 0)
            {
                //If it's the user's turn ask for a move, place the move into the array and draw the board in the console
                if (turn == 0)
                {
                    textBox1.AppendText("Make your move. Please enter a number between 1 and 9.");
                    textBox1.AppendText(Environment.NewLine);
                    string consoleInput = Console.ReadLine();
                    while (consoleInput == "")
                    {
                        textBox1.AppendText("Please enter a valid number.");
                        textBox1.AppendText(Environment.NewLine);
                        consoleInput = Console.ReadLine();
                    }
                    while (engine.placeMove(turn, int.Parse(consoleInput)) == 2)
                    {
                        textBox1.AppendText("Error. The number you have entered already has a mark or does not correspond to a square on the board. Please enter a different number.");
                        textBox1.AppendText(Environment.NewLine);
                        consoleInput = Console.ReadLine();
                    }
                    drawBoard();
                    turn++;
                }
                //If it's the computer's turn, calculate the computers move, place it and draw the board.
                else if (turn == 1)
                {
                    textBox1.AppendText("The computer will make a move.");
                    textBox1.AppendText(Environment.NewLine);
                    engine.compMove();
                    drawBoard();
                    turn--;
                }
            }
            //Check for winner
            int winSituation = engine.checkEndGame();

            //If the user wins, display winning message
            if (winSituation == 1)
            {
                textBox1.AppendText("You win!");
                textBox1.AppendText(Environment.NewLine);
            }
            //If the computer wins, display losing message
            else if (winSituation == 2)
            {
                textBox1.AppendText("You lose!");
                textBox1.AppendText(Environment.NewLine);
            }
            //If the game is a draw, display tie message
            else if (winSituation == 3)
            {
                textBox1.AppendText("The game ends in a draw!");
                textBox1.AppendText(Environment.NewLine);
            }

        }

        public void introText()
        {
            textBox1.AppendText("Tic-Tac-Toe");
            textBox1.AppendText("By: Cyril Silverman");
            textBox1.AppendText(Environment.NewLine);
            drawBoard();
        }

        //Have user choose who goes first
        public int chooseFirstPlayer()
        {
            Contract.Ensures(Contract.Result<int>() <= 1);
            Contract.Ensures(0 <= Contract.Result<int>());
            textBox1.AppendText("Do you want to go first? Type 0 to start, type 1 for the computer to start");
            textBox1.AppendText(Environment.NewLine);
            string number = Button.
            int whosFirst = int.Parse(number.Equals("") ? "3" : number);
            while (whosFirst < 0 || 1 < whosFirst)
            {
                textBox1.AppendText("Incorrect number. Type 0 for you to start, type 1 for the computer to start");
                textBox1.AppendText(Environment.NewLine);
                number = Console.ReadLine();
                whosFirst = int.Parse(number.Equals("") ? "3" : number);
            }

            return whosFirst;
        }

        //Draw the board
        //If an array space has a number greater than 9, then it means there is mark there.
        //The number 11 and 12 correspond respectively to X and O in the "marks" enum
        //I have it so it gets the Enum string using the number in the array as the indexpublic void drawBoard()
        public void drawBoard()
        {
            Contract.Requires(engine.board != null);
            button1.Text = 9 < engine.board[0, 0] ? Enum.GetName(typeof(marks), engine.board[0, 0]) : engine.board[0, 0].ToString();
            button2.Text = 9 < engine.board[0, 1] ? Enum.GetName(typeof(marks), engine.board[0, 1]) : engine.board[0, 1].ToString();
            button3.Text = 9 < engine.board[0, 2] ? Enum.GetName(typeof(marks), engine.board[0, 2]) : engine.board[0, 2].ToString();
            button4.Text = 9 < engine.board[1, 0] ? Enum.GetName(typeof(marks), engine.board[1, 0]) : engine.board[1, 0].ToString();
            button5.Text = 9 < engine.board[1, 1] ? Enum.GetName(typeof(marks), engine.board[1, 1]) : engine.board[1, 1].ToString();
            button6.Text = 9 < engine.board[1, 2] ? Enum.GetName(typeof(marks), engine.board[1, 2]) : engine.board[1, 2].ToString();
            button7.Text = 9 < engine.board[2, 0] ? Enum.GetName(typeof(marks), engine.board[2, 0]) : engine.board[2, 0].ToString();
            button8.Text = 9 < engine.board[2, 1] ? Enum.GetName(typeof(marks), engine.board[2, 1]) : engine.board[2, 1].ToString();
            button9.Text = 9 < engine.board[2, 2] ? Enum.GetName(typeof(marks), engine.board[2, 2]) : engine.board[2, 2].ToString();
        }

        public void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public void button1_Click(object sender, EventArgs e)
        {
  
        }

        public void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
