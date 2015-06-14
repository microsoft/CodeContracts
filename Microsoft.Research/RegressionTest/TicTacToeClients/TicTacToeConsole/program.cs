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
using System.Linq;
using System.Text;
using TicTacToeEngine;
using System.Diagnostics.Contracts;

namespace TicTacToeConsole
{
    class TicTacToeConsole
    {
        //Create new instance of the TicTacToe game engine
        public static TicTacToe engine = new TicTacToe();
        public static TicTacToeConsole gameConsole = new TicTacToeConsole();
        public enum marks { none = 10, X, O }

        static void Main()
        {
            //Write the introduction text and draw the board
            gameConsole.introText();
            //Get whether the user or the computer goes first
            int turn = gameConsole.chooseFirstPlayer();
            //Assign the marks respectively to who goes first
            engine.assignMarks(turn);
            //While there is no winner
            while (engine.checkEndGame() == 0)
            {
                //If it's the user's turn ask for a move, place the move into the array and draw the board in the console
                if (turn == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Make your move. Please enter a number between 1 and 9.");
                    string consoleInput = Console.ReadLine();
                    while (consoleInput == "")
                    {
                        Console.WriteLine("Please enter a valid number.");
                        consoleInput = Console.ReadLine();
                    }
                    while (engine.placeMove(turn, int.Parse(consoleInput)) == 2)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Error. The number you have entered already has a mark or does not correspond to a square on the board. Please enter a different number.");
                        consoleInput = Console.ReadLine();
                    }
                    Console.Clear();
                    gameConsole.drawBoard();
                    turn++;
                }
                //If it's the computer's turn, calculate the computers move, place it and draw the board.
                else if (turn == 1)
                {

                    Console.WriteLine();
                    Console.WriteLine("The computer will make a move.");
                    engine.compMove();
                    gameConsole.drawBoard();
                    turn--;
                }
            }
            //Check for winner
            int winSituation = engine.checkEndGame();

            //If the user wins, display winning message
            if (winSituation == 1)
            {
                Console.WriteLine("You win!");
                Console.ReadLine();
            }
            //If the computer wins, display losing message
            else if (winSituation == 2)
            {
                Console.WriteLine("You lose!");
                Console.ReadLine();
            }
            //If the game is a draw, display tie message
            else if (winSituation == 3)
            {
                Console.WriteLine("The game ends in a draw!");
                Console.ReadLine();
            }

        }

        public void introText()
        {
            Console.WriteLine("Tic-Tac-Toe");
            Console.WriteLine("By: Cyril Silverman");
            Console.WriteLine();
            drawBoard();
        }

        //Have user choose who goes first
        public int chooseFirstPlayer()
        {
            CodeContract.Ensures(CodeContract.Result<int>() <= 1);
            CodeContract.Ensures(0 <= CodeContract.Result<int>());
            Console.WriteLine("Do you want to go first? Type 0 to start, type 1 for the computer to start");
            string number = Console.ReadLine();
            int whosFirst = int.Parse(number.Equals("") ? "3" : number);
            while (whosFirst < 0 || 1 < whosFirst)
            {
                Console.WriteLine("Incorrect number. Type 0 for you to start, type 1 for the computer to start");
                number = Console.ReadLine();
                whosFirst = int.Parse(number.Equals("") ? "3" : number);
            }

            return whosFirst;
        }

        //Draw the board
        //If an array space has a number greater than 9, then it means there is mark there.
        //The number 11 and 12 correspond respectively to X and O in the "marks" enum
        //I have it so it gets the Enum string using the number in the array as the index
        public void drawBoard()
        {
            CodeContract.Requires(engine.board != null);
            Console.WriteLine();
            Console.WriteLine(" {0} | {1} | {2} ",
                9 < engine.board[0, 0] ? Enum.GetName(typeof(marks), engine.board[0, 0]) : engine.board[0, 0].ToString(),
                9 < engine.board[0, 1] ? Enum.GetName(typeof(marks), engine.board[0, 1]) : engine.board[0, 1].ToString(),
                9 < engine.board[0, 2] ? Enum.GetName(typeof(marks), engine.board[0, 2]) : engine.board[0, 2].ToString()); 
            Console.WriteLine("___|___|___");
            Console.WriteLine(" {0} | {1} | {2} ", 
                9 < engine.board[1, 0] ? Enum.GetName(typeof(marks), engine.board[1, 0]) : engine.board[1, 0].ToString(),
                9 < engine.board[1, 1] ? Enum.GetName(typeof(marks), engine.board[1, 1]) : engine.board[1, 1].ToString(),
                9 < engine.board[1, 2] ? Enum.GetName(typeof(marks), engine.board[1, 2]) : engine.board[1, 2].ToString()); 
            Console.WriteLine("___|___|___");
            Console.WriteLine(" {0} | {1} | {2} ", 
                9 < engine.board[2, 0] ? Enum.GetName(typeof(marks), engine.board[2, 0]) : engine.board[2, 0].ToString(),
                9 < engine.board[2, 1] ? Enum.GetName(typeof(marks), engine.board[2, 1]) : engine.board[2, 1].ToString(),
                9 < engine.board[2, 2] ? Enum.GetName(typeof(marks), engine.board[2, 2]) : engine.board[2, 2].ToString()); 
            Console.WriteLine("   |   |   ");
            Console.WriteLine();
        }

    }
}
