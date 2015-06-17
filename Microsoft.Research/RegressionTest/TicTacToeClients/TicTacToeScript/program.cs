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

namespace TicTacToeScript
{
    class TicTacToeScript
    {
        public enum marks {none = 10, X, O}
        public static TicTacToeScript script = new TicTacToeScript();

        static void Main()
        {
            Console.WriteLine("Press any key to run positive tests");
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine();
            Console.ReadLine();

            script.test1();
            script.test2();
            script.test3();
            script.test4();
            script.test7();

            Console.WriteLine();
            Console.WriteLine("Press any key to run negative tests");
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------");
            Console.ReadLine();

            script.test5();

            Console.ReadLine();

            script.test6();

            Console.ReadLine();
        }

        //User wins
        public void test1()
        {
            TicTacToe engine = new TicTacToe();
            Console.WriteLine("Test 1");
            engine.assignMarks(0);
            engine.placeMove(0,5);
            engine.compMove();
            engine.placeMove(0, 4);
            engine.compMove();
            engine.board[1, 2] = engine.yourMark;

            DrawBoard(engine);

            if (engine.checkEndGame() == 0)
            {
                Console.WriteLine("There is no winner yet");
            }
            else if (engine.checkEndGame() == 1)
            {
                Console.WriteLine("You win!");
            }
            else if (engine.checkEndGame() == 2)
            {
                Console.WriteLine("You lose!");
            }
            else if (engine.checkEndGame() == 3)
            {
                Console.WriteLine("Draw!");
            }
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine();
        }

        //Comp wins
        public void test2()
        {
            Console.WriteLine("Test 2");
            TicTacToe engine = new TicTacToe();
            engine.assignMarks(1);
            engine.compMove();
            engine.placeMove(0, 7);
            engine.compMove();
            engine.placeMove(0, 4);
            engine.compMove();

            DrawBoard(engine);

            if (engine.checkEndGame() == 0)
            {
                Console.WriteLine("There is no winner yet");
            }
            else if (engine.checkEndGame() == 1)
            {
                Console.WriteLine("You win!");
            }
            else if (engine.checkEndGame() == 2)
            {
                Console.WriteLine("You lose!");
            }
            else if (engine.checkEndGame() == 3)
            {
                Console.WriteLine("Draw!");
            }
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine();
        }
        
        //No win yet
        public void test3()
        {
            Console.WriteLine("Test 3");
            TicTacToe engine = new TicTacToe();
            engine.assignMarks(1);
            engine.compMove();
            engine.placeMove(0, 7);

            DrawBoard(engine);

            if (engine.checkEndGame() == 0)
            {
                Console.WriteLine("There is no winner yet");
            }
            else if (engine.checkEndGame() == 1)
            {
                Console.WriteLine("You win!");
            }
            else if (engine.checkEndGame() == 2)
            {
                Console.WriteLine("You lose!");
            }
            else if (engine.checkEndGame() == 3)
            {
                Console.WriteLine("Draw!");
            }
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine();
        }
        
        //draw
        public void test4()
        {
            Console.WriteLine("Test 4");
            TicTacToe engine = new TicTacToe();
            engine.assignMarks(1);
            engine.compMove();
            engine.placeMove(0, 7);
            engine.compMove();
            engine.placeMove(0, 9);
            engine.compMove();
            engine.placeMove(0, 2);
            engine.compMove();
            engine.placeMove(0, 4);
            engine.compMove();

            DrawBoard(engine);

            if (engine.checkEndGame() == 0)
            {
                Console.WriteLine("There is no winner yet");
            }
            else if (engine.checkEndGame() == 1)
            {
                Console.WriteLine("You win!");
            }
            else if (engine.checkEndGame() == 2)
            {
                Console.WriteLine("You lose!");
            }
            else if (engine.checkEndGame() == 3)
            {
                Console.WriteLine("Draw!");
            }
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------");
        }

        //Test what happens when assignMarks doesn't have a valid turn number
        public void test5()
        {
            TicTacToe engine = new TicTacToe();
            Console.WriteLine("Test 5");
            Console.WriteLine();
            engine.assignMarks(3);
            if (engine.compMark == (int)marks.none & engine.yourMark == (int)marks.none)
                Console.WriteLine("ERROR: No marks have been assigned.");
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine();
        }

        //What happens when you try and make a mark on an already made mark
        public void test6()
        {
            TicTacToe engine = new TicTacToe();
            Console.WriteLine("Test 6");
            engine.assignMarks(1);
            engine.compMove();
            int error = engine.placeMove(0, 5);

            DrawBoard(engine);

            if (error == 2)
            {
                Console.WriteLine("There is already a mark on square 5");
            }

        }

        //Test for the first turn
        public void test7()
        {
            TicTacToe engine = new TicTacToe();
            Console.WriteLine("Test 7");
            Console.WriteLine();
            bool firstTurn = engine.checkFirstTurn();
            if (firstTurn == true)
            {
                Console.WriteLine("It is currently the first turn of the game.");
            }
                        Console.WriteLine();
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine();
        }

        private static void DrawBoard(TicTacToe engine)
        {
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
