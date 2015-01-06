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
using RockPaperScissorsEngine;
using Microsoft.Contracts;
using System.Diagnostics.Contracts;

namespace RockPaperScissorsForm
{
    public partial class Form1 : Form
    {
        public static RockPaperScissors engine = Form2.engine;
        public Dictionary<int, string> winnersDict = new Dictionary<int, string>();
        int initialNumberOfPlayers = engine.numberOfPlayers;
        public enum gesture { rock = 1, paper, scissors }
        int endTurnWin = 0;
        int roundNumber = 0;
        int i = 0;

        public Form1()
        {
            CodeContract.Requires(engine != null);
            CodeContract.Requires(initialNumberOfPlayers != 0);

            InitializeComponent();
            engine.InitializePlayers(engine.numberOfPlayers - 1);
            console.AppendText("Choose a gesture to throw" + Environment.NewLine + Environment.NewLine);
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void rock_Click(object sender, EventArgs e)
        {
            Turn((int)gesture.rock);
        }

        private void paper_Click(object sender, EventArgs e)
        {
            Turn((int)gesture.paper);
        }

        private void scissors_Click(object sender, EventArgs e)
        {
            Turn((int)gesture.scissors);
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            engine = new RockPaperScissors();
            engine.numberOfPlayers = initialNumberOfPlayers;
            engine.InitializePlayers(engine.numberOfPlayers - 1);
            roundNumber = 0;
            console.Clear();
        }

        private void exitGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void WriteAllGestures()
        {
            CodeContract.Requires(engine.players != null);
            CodeContract.Requires(console.Enabled == true);

            foreach (string playerName in engine.players.Keys)
            {
                console.AppendText(playerName + " played " + Enum.GetName(typeof(gesture), engine.players[playerName]) + Environment.NewLine);
            }
        }

        public void Turn (int gestureID)
        {
            CodeContract.Requires(gestureID > 0);
            CodeContract.Requires(i == winnersDict.Count);

            if (engine.players.Count != 1)
            {
                roundNumber++;

                console.AppendText("---------Round " + roundNumber + "---------" + Environment.NewLine);

                if (engine.players.ContainsKey("User"))
                    engine.players["User"] = gestureID;
                else
                    console.AppendText(Environment.NewLine + "You are not in the game anymore" + Environment.NewLine + Environment.NewLine);

                engine.GetAllCompGestures();
                WriteAllGestures();

                endTurnWin = engine.CalculateWinner();

                if (endTurnWin == (int)gesture.rock)
                    console.AppendText(Environment.NewLine + "Rock wins." +  Environment.NewLine + Environment.NewLine);
                else if (endTurnWin == (int)gesture.paper)
                    console.AppendText(Environment.NewLine + "Paper wins." +  Environment.NewLine + Environment.NewLine);
                else if (endTurnWin == (int)gesture.scissors)
                    console.AppendText(Environment.NewLine + "Scissors wins."  + Environment.NewLine + Environment.NewLine);
                else if (endTurnWin == 4)
                    console.AppendText(Environment.NewLine + "Rock and paper tie." + Environment.NewLine + Environment.NewLine);
                else if (endTurnWin == 5)
                    console.AppendText(Environment.NewLine + "Paper and scissors tie." + Environment.NewLine + Environment.NewLine);
                else if (endTurnWin == 6)
                    console.AppendText(Environment.NewLine + "Scissors and rock tie." + Environment.NewLine + Environment.NewLine);
                else if (endTurnWin == 0)
                    console.AppendText(Environment.NewLine + "Draw." + Environment.NewLine + Environment.NewLine);
                else if (endTurnWin == 7)
                    console.AppendText(Environment.NewLine + "AN ERROR HAS OCCURED" + Environment.NewLine + Environment.NewLine);

                if (engine.players.Count != 1)
                {
                    if (engine.players.ContainsKey("User"))
                        console.AppendText("Make your next gesture." + Environment.NewLine + Environment.NewLine);
                    else
                        console.AppendText("Click any button to see the next round." + Environment.NewLine + Environment.NewLine);
                }
                else
                {
                    winnersDict.Add(i, engine.players.Keys.ToArray().GetValue(0).ToString());
                    if (winners.Items.Contains((winnersDict.Count(name => name.Value == engine.players.Keys.ToArray().GetValue(0).ToString()) - 1)  + " ------ " + engine.players.Keys.ToArray().GetValue(0).ToString()))
                    {
                        winners.Items.Remove((winnersDict.Count(name => name.Value == engine.players.Keys.ToArray().GetValue(0).ToString()) - 1)  + " ------ " + engine.players.Keys.ToArray().GetValue(0).ToString());  
                    }
                    winners.Items.Add(winnersDict.Count(name => name.Value == engine.players.Keys.ToArray().GetValue(0).ToString()) + " ------ " + engine.players.Keys.ToArray().GetValue(0).ToString());
                    i++;
                    MessageBox.Show(engine.players.Keys.ToArray().GetValue(0).ToString() + " is the winner.");
                    console.AppendText(engine.players.Keys.ToArray().GetValue(0).ToString() + " is the winner.");
                }
            }
        }

        private void editNumberOfPlayersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
