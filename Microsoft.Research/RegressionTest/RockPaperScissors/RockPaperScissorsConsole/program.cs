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
using RockPaperScissorsEngine;
using Microsoft.Contracts;

namespace RockPaperScissorsConsole
{
    class GameConsole
    {
        public static RockPaperScissors engine = new RockPaperScissors();
        public static GameConsole gameConsole = new GameConsole();
        public enum gesture { rock = 1, paper, scissors }

        static void Main()
        {

            gameConsole.GetNumberOfPlayers();
            engine.InitializePlayers(engine.numberOfPlayers - 1);
            int endTurnWin;

            while (engine.players.Count > 1)
            {
                engine.roundNumber++;
                Console.WriteLine("------Round {0}------" + Environment.NewLine, engine.roundNumber);

                if (engine.players.ContainsKey("User"))
                {
                    engine.players["User"] = engine.GetUserGesture();
                }

                engine.GetAllCompGestures();
                gameConsole.WriteAllGestures();
                endTurnWin = engine.CalculateWinner();

                if (endTurnWin == (int)gesture.rock)
                    Console.WriteLine(Environment.NewLine + "Rock wins." + Environment.NewLine);
                else if (endTurnWin == (int)gesture.paper)
                    Console.WriteLine(Environment.NewLine + "Paper wins." + Environment.NewLine);
                else if (endTurnWin == (int)gesture.scissors)
                    Console.WriteLine(Environment.NewLine + "Scissors wins." + Environment.NewLine);
                else if (endTurnWin == 0)
                    Console.WriteLine(Environment.NewLine + "Draw." + Environment.NewLine);
                else if (endTurnWin == 4)
                    Console.WriteLine(Environment.NewLine + "AN ERROR HAS OCCURED" + Environment.NewLine);
            }

            Console.WriteLine("{0} is the WINNER!", engine.players.Keys.ToArray().GetValue(0).ToString());
            Console.ReadLine();
        }

        public void GetNumberOfPlayers()
        {
            while (engine.numberOfPlayers < 1)
            {
                Console.WriteLine("Please pick how many opponents you want. You must play with at least 1.");
                string consoleInput = Console.ReadLine();
                Console.WriteLine();
                try
                {
                    engine.numberOfPlayers = int.Parse(consoleInput) + 1;
                }
                catch
                {
                    Console.WriteLine("That is an incorrect number.");
                }
            }


        }

        public void WriteAllGestures()
        {
            foreach (string playerName in engine.players.Keys)
            {
                Console.WriteLine("{0} played {1}", playerName, Enum.GetName(typeof(gesture), engine.players[playerName]));
            }
        }

    }
}
