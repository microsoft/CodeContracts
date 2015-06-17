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
using Microsoft.Contracts;
using System.Diagnostics.Contracts;

namespace RockPaperScissorsEngine
{
    public class RockPaperScissors
    {
        public int numberOfPlayers = 0;
        public enum gesture { rock = 1, paper, scissors }
        Random random = new Random();
        public Dictionary<string, int> players = new Dictionary<string, int>();
        public string[] playersInGame;
        public int roundNumber = 0;

        public static void Main()
        {
            //EMPTY
        }

        public void InitializePlayers(int numberOfComputers)
        {
            players.Add("User", 0);

            for (int i = 1; i <= numberOfComputers; i++)
            {
                string computerKey = "Computer" + i.ToString();
                players.Add(computerKey, 0);
            }
        }

        public void GetAllCompGestures()
        {
            CodeContract.Requires(1 < numberOfPlayers);
            playersInGame = new string[numberOfPlayers];
            int i = 0;
            foreach (string a in players.Keys)
            {
                playersInGame[i] = a;
                i++;
            }

            foreach (string a in playersInGame)
            {
                if (a != "User")
                    players[a] = GetCompGesture();
            }
        }

        public int CalculateWinner()
        {
            CodeContract.Ensures(0 <= CodeContract.Result<int>());
            CodeContract.Ensures(CodeContract.Result<int>() <= 7);

            int rockCount = players.Count(pair => pair.Value == (int)gesture.rock);
            int paperCount = players.Count(pair => pair.Value == (int)gesture.paper);
            int scissorsCount = players.Count(pair => pair.Value == (int)gesture.scissors);
            int winningRockCount = (rockCount * scissorsCount) - (rockCount * paperCount);
            int winningPaperCount = (paperCount * rockCount) - (paperCount * scissorsCount);
            int winningScissorsCount = (scissorsCount * paperCount) - (scissorsCount * rockCount);

            //If Rock wins
            if (winningRockCount > winningPaperCount & winningRockCount > winningScissorsCount)
            {
                numberOfPlayers = numberOfPlayers - (paperCount + scissorsCount);

                foreach (string playerName in playersInGame)
                {
                    if (players[playerName] == (int)gesture.paper || players[playerName] == (int)gesture.scissors)
                        players.Remove(playerName);
                }
                return (int)gesture.rock;
            }
            //If paper wins
            else if (winningPaperCount > winningRockCount & winningPaperCount > winningScissorsCount)
            {
                numberOfPlayers = numberOfPlayers - (rockCount + scissorsCount);

                foreach (string playerName in playersInGame)
                {
                    if (players[playerName] == (int)gesture.rock || players[playerName] == (int)gesture.scissors)
                        players.Remove(playerName);
                }
                return (int)gesture.paper;
            }
            //If scissors wins
            else if (winningScissorsCount > winningRockCount & winningScissorsCount > winningPaperCount)
            {
                numberOfPlayers = numberOfPlayers - (rockCount + paperCount);

                foreach (string playerName in playersInGame)
                {
                    if (players[playerName] == (int)gesture.rock || players[playerName] == (int)gesture.paper)
                        players.Remove(playerName);
                }
                return (int)gesture.scissors;
            }
            //if Rock and paper tie
            else if (winningRockCount == winningPaperCount & winningRockCount != winningScissorsCount)
            {
                numberOfPlayers = numberOfPlayers - (scissorsCount);

                foreach (string playerName in playersInGame)
                {
                    if (players[playerName] == (int)gesture.scissors)
                        players.Remove(playerName);
                }
                return 4;
            }
            //If Scissors and Rock tie
            else if (winningScissorsCount == winningRockCount & winningScissorsCount != winningPaperCount)
            {
                numberOfPlayers = numberOfPlayers - (paperCount);

                foreach (string playerName in playersInGame)
                {
                    if (players[playerName] == (int)gesture.paper)
                        players.Remove(playerName);
                }
                return 5;
            }
            //If paper and scissors tie
            else if (winningPaperCount == winningScissorsCount & winningScissorsCount != winningRockCount)
            {
                numberOfPlayers = numberOfPlayers - (rockCount);

                foreach (string playerName in playersInGame)
                {
                    if (players[playerName] == (int)gesture.rock)
                        players.Remove(playerName);
                }
                return 6;
            }
            //If draw
            else if (winningRockCount == winningPaperCount & winningRockCount == winningScissorsCount)
            {
                return 0;
            }
            return 7;
        }

        public int GetUserGesture()
        {
            bool error = true;
            int input = 0;
            while (error == true)
            {
                Console.WriteLine("Choose your gesture. Type 1 for rock, 2 for paper or 3 for scissors.");
                try
                {
                    input = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                    if (input == (int)gesture.rock || input == (int)gesture.paper || input == (int)gesture.scissors)
                        error = false;
                    else
                        Console.WriteLine("Please choose a valid number");
                }
                catch
                {
                    Console.WriteLine("Incorect number");
                }
            }
            return input;
        }

        public int GetCompGesture()
        {
            int gestureID = random.Next(1, 4);
            return gestureID;
        }

    }
}
