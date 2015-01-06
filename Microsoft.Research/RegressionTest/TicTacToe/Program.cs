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
using System.Diagnostics;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace TicTacToeEngine
{
    public class TicTacToe
    {
        //Enumerates the possible marks
        public enum marks { none=10, X, O }
        //Make the board array
        public int[,] board = { {1,2,3}, {4,5,6}, {7,8,9}};
        //int[,] board2 = { {1,2,3}, {4,5,6}, {7,8,9}};

        public int yourMark = (int)marks.none;
        public int compMark = (int)marks.none;

        public void ObjectInvariant()
        {
            CodeContract.Invariant(board != null);
        }

        public static void Main()
        {
            //Empty
        }

        //public int[,] Board2
        //{
        //    get
        //    {
        //        CodeContract.Ensures(CodeContract.Result<int[,]>() != null);
        //        return board2;
        //    }
        //}

        //Check to see if the board has any marks on it, thus determining if it's the first turn or not.
        public bool checkFirstTurn()
        {
            if (compMark == (int)marks.none & yourMark == (int)marks.none)
            {
                return true;
            }
            return false;
        }

        //Returns the move the player or the computer makes
        public void assignMarks(int turn)
        {
            CodeContract.Requires(0 <= turn);
            CodeContract.Requires(turn <= 1);

            //check to see if this is the first turn
            bool firstTurn = checkFirstTurn();

            if (firstTurn == true)
            {
                //If it's the players, assign the marks accordingly
                if (turn == 0)
                {
                    yourMark = (int)marks.X;
                    compMark = (int)marks.O;
                }
                //If it's not the user's turn, and thus it is the computer's, assign marks accordingly
                else if (turn == 1)
                {
                    yourMark = (int)marks.O;
                    compMark = (int)marks.X;
                }
            }
        }

        //Places the move on the board, and then writes it in the Console
        public int placeMove(int turn, int squareNumber)
        {
            CodeContract.Requires(0 <= turn);
            CodeContract.Requires(turn <= 1);
            CodeContract.Requires(0 <= squareNumber);
            CodeContract.Requires(squareNumber <= 9);
            CodeContract.Ensures(CodeContract.Result<int>() != 2);
            //CodeContract.Ensures(CodeContract.Result<int>() <= 2);

            //if it was the user's turn, plug his move into the array
            if (turn == 0 || turn == 2)
            {

                if (squareNumber.Equals(1) & board[0, 0].Equals(1))
                {
                    board[0, 0] = yourMark;
                }
                else if (squareNumber.Equals(2) & board[0, 1].Equals(2))
                {
                    board[0, 1] = yourMark;
                }
                else if (squareNumber.Equals(3) & board[0, 2].Equals(3))
                {
                    board[0, 2] = yourMark;
                }
                else if (squareNumber.Equals(4) & board[1, 0].Equals(4))
                {
                    board[1, 0] = yourMark;
                }
                else if (squareNumber.Equals(5) & board[1, 1].Equals(5))
                {
                    board[1, 1] = yourMark;
                }
                else if (squareNumber.Equals(6) & board[1, 2].Equals(6))
                {
                    board[1, 2] = yourMark;
                }
                else if (squareNumber.Equals(7) & board[2, 0].Equals(7))
                {
                    board[2, 0] = yourMark;
                }
                else if (squareNumber.Equals(8) & board[2, 1].Equals(8))
                {
                    board[2, 1] = yourMark;
                }
                else if (squareNumber.Equals(9) & board[2, 2].Equals(9))
                {
                    board[2, 2] = yourMark;
                }
                //If he inputed a space where there is already another mark there
                else
                {
                    //2 = incorrect space
                    return 2;
                }
            }
            return turn;
        }

        //Checks to see if there is a winner yet.
        public int checkEndGame()
        {
            CodeContract.Ensures(CodeContract.Result<int>() <= 3);
            CodeContract.Ensures(0 <= CodeContract.Result<int>());

            //Checks to see if the user won
            if (checkWinner(yourMark).Equals(1))
            {
                //1 = user win
                return 1;
            }
            //Checks to see if the computer won
            else if (checkWinner(compMark).Equals(1))
            {
                //2 = comp win
                return 2;
            }
            //Check for draw
            else if (!board[0, 0].Equals(1)
                & !board[0, 1].Equals(2)
                & !board[0, 2].Equals(3)
                & !board[1, 0].Equals(4)
                & !board[1, 1].Equals(5)
                & !board[1, 2].Equals(6)
                & !board[2, 0].Equals(7)
                & !board[2, 1].Equals(8)
                & !board[2, 2].Equals(9)
                )
            {
                //3 = draw
                return 3;
            }
            //0 = no win
            return 0;
        }

        //Checks to see if there is a winner yet
        public int checkWinner(int mark)
        {
            CodeContract.Ensures(CodeContract.Result<int>() <= 1);
            CodeContract.Ensures(0 <= CodeContract.Result<int>());


            if (//horizontal ways to win
                board[0, 0].Equals(mark) & board[0, 1].Equals(mark) & board[0, 2].Equals(mark)
                || board[1, 0].Equals(mark) & board[1, 1].Equals(mark) & board[1, 2].Equals(mark)
                || board[2, 0].Equals(mark) & board[2, 1].Equals(mark) & board[2, 2].Equals(mark)
                //vertical ways to win
                || board[0, 0].Equals(mark) & board[1, 0].Equals(mark) & board[2, 0].Equals(mark)
                || board[0, 1].Equals(mark) & board[1, 1].Equals(mark) & board[2, 1].Equals(mark)
                || board[0, 2].Equals(mark) & board[1, 2].Equals(mark) & board[2, 2].Equals(mark)
                //diagonal ways to win
                || board[0, 0].Equals(mark) & board[1, 1].Equals(mark) & board[2, 2].Equals(mark)
                || board[0, 2].Equals(mark) & board[1, 1].Equals(mark) & board[2, 0].Equals(mark)
                )
            {
                return 1;
            }

            return 0;
        }

        //How the computer makes it's move
        public void compMove()
        {
            //PLACE MARK TO WIN
            //Place a mark at 1 if it leads to a win
            if (
                //Horizontal
                board[0, 1].Equals(compMark) & board[0, 2].Equals(compMark) & board[0, 0].Equals(1)
                //Vertical
                || board[1, 0].Equals(compMark) & board[2, 0].Equals(compMark) & board[0, 0].Equals(1)
                //Diagonal
                || board[1, 1].Equals(compMark) & board[2, 2].Equals(compMark) & board[0, 0].Equals(1))
            {
                board[0, 0] = compMark;
            }
            //Place mark at 2 if it leads to a win
            else if (
                //Horizontal
                board[0, 0].Equals(compMark) & board[0, 2].Equals(compMark) & board[0, 1].Equals(2)
                //Vertical
                || board[1, 1].Equals(compMark) & board[2, 1].Equals(compMark) & board[0, 1].Equals(2))
            {
                board[0, 1] = compMark;
            }
            //Place mark at 3 if it leads to a win
            else if (
                //Horizontal
                board[0, 0].Equals(compMark) & board[0, 1].Equals(compMark) & board[0, 2].Equals(3)
                //Vertical
                || board[1, 2].Equals(compMark) & board[2, 2].Equals(compMark) & board[0, 2].Equals(3)
                //Diagonal
                || board[1, 1].Equals(compMark) & board[2, 0].Equals(compMark) & board[0, 2].Equals(3))
            {
                board[0, 2] = compMark;
            }
            //Place mark at 4 if it leads to a win
            else if (
                //horizontal
                board[1, 1].Equals(compMark) & board[1, 2].Equals(compMark) & board[1, 0].Equals(4)
                //vertical
                || board[0, 0].Equals(compMark) & board[2, 0].Equals(compMark) & board[1, 0].Equals(4))
            {
                board[1, 0] = compMark;
            }
            //Place mark at 5 if it leads to a winn
            else if (
                //horizontal
                board[1, 0].Equals(compMark) & board[1, 2].Equals(compMark) & board[1, 1].Equals(5)
                //vertical
                || board[1, 1].Equals(compMark) & board[2, 1].Equals(compMark) & board[1, 1].Equals(5)
                //diagonal +
                || board[2, 0].Equals(compMark) & board[0, 2].Equals(compMark) & board[1, 1].Equals(5)
                //diagonal -
                || board[0, 0].Equals(compMark) & board[2, 2].Equals(compMark) & board[1, 1].Equals(5)
                )
            {
                board[1, 1] = compMark;
            }
            //Place mark at 6 if it leads to a win
            else if (
                //horizontal
                board[1, 0].Equals(compMark) & board[1, 1].Equals(compMark) & board[1, 2].Equals(6)
                //vertical
                || board[0, 2].Equals(compMark) & board[2, 2].Equals(compMark) & board[1, 2].Equals(6))
            {
                board[1, 2] = compMark;
            }
            //Place mark at 7 if it leads to a win
            else if (
                //Horizontal
                board[2, 1].Equals(compMark) & board[2, 2].Equals(compMark) & board[2, 0].Equals(7)
                //Vertical
                || board[0, 0].Equals(compMark) & board[1, 0].Equals(compMark) & board[2, 0].Equals(7)
                //Diagonal
                || board[1, 1].Equals(compMark) & board[0, 2].Equals(compMark) & board[2, 0].Equals(7)
                )
            {
                board[2, 0] = compMark;
            }
            //Place mark at 8 if it leads to a win
            else if (
                //Horizontal
                board[2, 0].Equals(compMark) & board[2, 2].Equals(compMark) & board[2, 1].Equals(8)
                //Vertical
                || board[0, 1].Equals(compMark) & board[1, 1].Equals(compMark) & board[2, 1].Equals(8)
                )
            {
                board[2, 1] = compMark;
            }
            //Place mark at 9 if it leads to a win
            else if (
                //Horizontal
                board[2, 0].Equals(compMark) & board[2, 1].Equals(compMark) & board[2, 2].Equals(9)
                //Vertical
                || board[0, 2].Equals(compMark) & board[1, 2].Equals(compMark) & board[2, 2].Equals(9)
                //Diagonal
                || board[0, 0].Equals(compMark) & board[1, 1].Equals(compMark) & board[2, 2].Equals(9))
            {
                board[2, 2] = compMark;
            }
            // <BLOCK WIN>
            //Place mark at 1 if it blocks a win
            else if (
                //Horizontal
                board[0, 1].Equals(yourMark) & board[0, 2].Equals(yourMark) & board[0, 0].Equals(1)
                //Vertical
                || board[1, 0].Equals(yourMark) & board[2, 0].Equals(yourMark) & board[0, 0].Equals(1)
                //Diagonal
                || board[1, 1].Equals(yourMark) & board[2, 2].Equals(yourMark) & board[0, 0].Equals(1))
            {
                board[0, 0] = compMark;
            }
            //Place mark at 2 if it blocks a win
            else if (
                //Horizontal
                board[0, 0].Equals(yourMark) & board[0, 2].Equals(yourMark) & board[0, 1].Equals(2)
                //Vertical
                || board[1, 1].Equals(yourMark) & board[2, 1].Equals(yourMark) & board[0, 1].Equals(2))
            {
                board[0, 1] = compMark;
            }
            //Place mark at 3 if it block a win
            else if (
                //Horizontal
                board[0, 0].Equals(yourMark) & board[0, 1].Equals(yourMark) & board[0, 2].Equals(3)
                //Vertical
                || board[1, 2].Equals(yourMark) & board[2, 2].Equals(yourMark) & board[0, 2].Equals(3)
                //Diagonal
                || board[1, 1].Equals(yourMark) & board[2, 0].Equals(yourMark) & board[0, 2].Equals(3))
            {
                board[0, 2] = compMark;
            }
            //Place mark at 4 if it blocks a win
            else if (
                //horizontal
                board[1, 1].Equals(yourMark) & board[1, 2].Equals(yourMark) & board[1, 0].Equals(4)
                //vertical
                || board[0, 0].Equals(yourMark) & board[2, 0].Equals(yourMark) & board[1, 0].Equals(4))
            {
                board[1, 0] = compMark;
            }
            //Place mark at 5 if it blocks a win
            else if (
                //horizontal
                board[1, 0].Equals(yourMark) & board[1, 2].Equals(yourMark) & board[1, 1].Equals(5)
                //vertical
                || board[1, 1].Equals(yourMark) & board[2, 1].Equals(yourMark) & board[1, 1].Equals(5)
                //diagonal +
                || board[2, 0].Equals(yourMark) & board[0, 2].Equals(yourMark) & board[1, 1].Equals(5)
                //diagonal -
                || board[0, 0].Equals(yourMark) & board[2, 2].Equals(yourMark) & board[1, 1].Equals(5)
                )
            {
                board[1, 1] = compMark;
            }
            //Place mark at 6 if it blocks a win
            else if (
                //horizontal
                board[1, 0].Equals(yourMark) & board[1, 1].Equals(yourMark) & board[1, 2].Equals(6)
                //vertical
                || board[0, 2].Equals(yourMark) & board[2, 2].Equals(yourMark) & board[1, 2].Equals(6))
            {
                board[1, 2] = compMark;
            }
            //Place mark at 7 if it blocks a win
            else if (
                //Horizontal
                board[2, 1].Equals(yourMark) & board[2, 2].Equals(yourMark) & board[2, 0].Equals(7)
                //Vertical
                || board[0, 0].Equals(yourMark) & board[1, 0].Equals(yourMark) & board[2, 0].Equals(7)
                //Diagonal
                || board[1, 1].Equals(yourMark) & board[0, 2].Equals(yourMark) & board[2, 0].Equals(7)
                )
            {
                board[2, 0] = compMark;
            }
            //Place mark at 8 if it blocks a win
            else if (
                //Horizontal
                board[2, 0].Equals(yourMark) & board[2, 2].Equals(yourMark) & board[2, 1].Equals(8)
                //Vertical
                || board[0, 1].Equals(yourMark) & board[1, 1].Equals(yourMark) & board[2, 1].Equals(8)
                )
            {
                board[2, 1] = compMark;
            }
            //Place mark at 9 if it blocks a win
            else if (
                //Horizontal
                board[2, 0].Equals(yourMark) & board[2, 1].Equals(yourMark) & board[2, 2].Equals(9)
                //Vertical
                || board[0, 2].Equals(yourMark) & board[1, 2].Equals(yourMark) & board[2, 2].Equals(9)
                //Diagonal
                || board[0, 0].Equals(yourMark) & board[1, 1].Equals(yourMark) & board[2, 2].Equals(9))
            {
                board[2, 2] = compMark;
            }
            //<MAKE FORK>
            //Place a mark at 1 if it makes a fork
            else if (board[0, 2].Equals(compMark) & board[2, 0].Equals(compMark) & board[1, 0].Equals(4) & board[0, 1].Equals(2) & board[0, 0].Equals(1))
            {
                board[0, 0] = compMark;
            }
            //PLace a mark at 2 if it makes a fork
            else if (board[0, 2].Equals(compMark) & board[1, 1].Equals(compMark) & board[2, 1].Equals(8) & board[1, 0].Equals(4) & board[0, 1].Equals(2))
            {
                board[0, 1] = compMark;
            }
            //Place a mark at 3 if it makes a fork
            else if (board[0, 0].Equals(compMark) & board[2, 2].Equals(compMark) & board[0, 1].Equals(2) & board[1, 2].Equals(6) & board[0, 2].Equals(3))
            {
                board[0, 2] = compMark;
            }
            //Place a mark at 4 if it makes a fork
            else if (board[0, 0].Equals(compMark) & board[1, 1].Equals(compMark) & board[1, 2].Equals(6) & board[2, 0].Equals(7) & board[1, 0].Equals(4))
            {
                board[1, 0] = compMark;
            }
            //Place a mark at 6 if it makes a fork
            else if (board[1, 1].Equals(compMark) & board[2, 0].Equals(compMark) & board[0, 2].Equals(3) & board[1, 0].Equals(4) & board[1, 2].Equals(6))
            {
                board[1, 2] = compMark;
            }
            //Place a mark at 7 if it makes a fork
            else if (board[0, 0].Equals(compMark) & board[2, 2].Equals(compMark) & board[1, 0].Equals(6) & board[2, 1].Equals(7) & board[2, 0].Equals(7))
            {
                board[2, 0] = compMark;
            }
            //Place a mark at 8 if it makes a fork
            else if (board[1, 1].Equals(compMark) & board[2, 0].Equals(compMark) & board[0, 1].Equals(2) & board[2, 2].Equals(9) & board[2, 0].Equals(8))
            {
                board[2, 1] = compMark;
            }
            //Place a mark at 9 if it makes a fork
            else if (board[0, 2].Equals(compMark) & board[2, 0].Equals(compMark) & board[2, 1].Equals(8) & board[1, 2].Equals(6) & board[2, 2].Equals(9))
            {
                board[2, 2] = compMark;
            }
            //<BLOCK FORK>
            //Place a mark at 1 if it blocks a fork
            else if (board[0, 2].Equals(yourMark) & board[2, 0].Equals(yourMark) & board[1,0].Equals(4) & board[0,1].Equals(2) & board[0, 0].Equals(1) )
            {
                board[0, 0] = compMark;
            }
            //PLace a mark at 2 if it blocks a fork
            else if (board[0, 2].Equals(yourMark) & board[1, 1].Equals(yourMark) & board[2, 1].Equals(8) & board[1, 0].Equals(4) & board[0, 1].Equals(2))
            {
                board[0, 1] = compMark;
            }
            //Place a mark at 3 if it blocks a fork
            else if (board[0, 0].Equals(yourMark) & board[2, 2].Equals(yourMark) & board[0, 1].Equals(2) & board[1, 2].Equals(6) & board[0, 2].Equals(3))
            {
                board[0, 2] = compMark;
            }
            //Place a mark at 4 if it blocks a fork
            else if (board[0, 0].Equals(yourMark) & board[1, 1].Equals(yourMark) & board[1, 2].Equals(6) & board[2, 0].Equals(7) & board[1, 0].Equals(4))
            {
                board[1, 0] = compMark;
            }
            //Place a mark at 6 if it blocks a fork
            else if (board[1, 1].Equals(yourMark) & board[2, 0].Equals(yourMark) & board[0, 2].Equals(3) & board[1, 0].Equals(4) & board[1, 2].Equals(6))
            {
                board[1, 2] = compMark;
            }
            //Place a mark at 7 if it blocks a fork
            else if (board[0, 0].Equals(yourMark) & board[2, 2].Equals(yourMark) & board[1, 0].Equals(6) & board[2, 1].Equals(7) & board[2, 0].Equals(7))
            {
                board[2, 0] = compMark;
            }
            //Place a mark at 8 if it blocks a fork
            else if (board[1, 1].Equals(yourMark) & board[2, 0].Equals(yourMark) & board[0, 1].Equals(2) & board[2, 2].Equals(9) & board[2, 0].Equals(8))
            {
                board[2, 1] = compMark;
            }
            //Place a mark at 9 if it blocks a fork
            else if (board[0, 2].Equals(yourMark) & board[2, 0].Equals(yourMark) & board[2, 1].Equals(8) & board[1, 2].Equals(6) & board[2, 2].Equals(9))
            {
                board[2, 2] = compMark;
            }
            //<PLAY CENTER>
            else if (board[1, 1].Equals(5))
            {
                board[1, 1] = compMark;
            }
            //PLAY CORNER
            else if (board[0, 0].Equals(1))
            {
                board[0, 0] = compMark;
            }
            else if (board[0, 2].Equals(3))
            {
                board[0, 2] = compMark;
            }
            else if (board[2, 0].Equals(7))
            {
                board[2, 0] = compMark;
            }
            else if (board[2, 2].Equals(9))
            {
                board[2, 2] = compMark;
            }
            //PLAY SIDE
            else if (board[0, 1].Equals(2))
            {
                board[0, 1] = compMark;
            }
            else if (board[1, 0].Equals(4))
            {
                board[1, 0] = compMark;
            }
            else if (board[1, 2].Equals(6))
            {
                board[1, 2] = compMark;
            }
            else if (board[2, 1].Equals(8))
            {
                board[2, 1] = compMark;
            }

        }
    }
}
