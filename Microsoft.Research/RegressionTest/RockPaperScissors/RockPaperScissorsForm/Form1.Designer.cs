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

namespace RockPaperScissorsForm
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rock = new System.Windows.Forms.Button();
            this.paper = new System.Windows.Forms.Button();
            this.scissors = new System.Windows.Forms.Button();
            this.console = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editNumberOfPlayersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.winners = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rock
            // 
            this.rock.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.rock.Location = new System.Drawing.Point(12, 50);
            this.rock.Name = "rock";
            this.rock.Size = new System.Drawing.Size(100, 40);
            this.rock.TabIndex = 0;
            this.rock.Text = "Rock";
            this.rock.UseVisualStyleBackColor = true;
            this.rock.Click += new System.EventHandler(this.rock_Click);
            // 
            // paper
            // 
            this.paper.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.paper.Location = new System.Drawing.Point(116, 50);
            this.paper.Name = "paper";
            this.paper.Size = new System.Drawing.Size(100, 40);
            this.paper.TabIndex = 1;
            this.paper.Text = "Paper";
            this.paper.UseVisualStyleBackColor = true;
            this.paper.Click += new System.EventHandler(this.paper_Click);
            // 
            // scissors
            // 
            this.scissors.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.scissors.Location = new System.Drawing.Point(222, 50);
            this.scissors.Name = "scissors";
            this.scissors.Size = new System.Drawing.Size(100, 40);
            this.scissors.TabIndex = 2;
            this.scissors.Text = "Scissors";
            this.scissors.UseVisualStyleBackColor = true;
            this.scissors.Click += new System.EventHandler(this.scissors_Click);
            // 
            // console
            // 
            this.console.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.console.Font = new System.Drawing.Font("Calibri", 9F);
            this.console.Location = new System.Drawing.Point(12, 124);
            this.console.Multiline = true;
            this.console.Name = "console";
            this.console.ReadOnly = true;
            this.console.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.console.Size = new System.Drawing.Size(312, 551);
            this.console.TabIndex = 3;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(334, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem,
            this.editNumberOfPlayersToolStripMenuItem,
            this.exitGameToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.fileToolStripMenuItem.Text = "Game";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.newGameToolStripMenuItem.Text = "New Game";
            this.newGameToolStripMenuItem.Click += new System.EventHandler(this.newGameToolStripMenuItem_Click);
            // 
            // editNumberOfPlayersToolStripMenuItem
            // 
            this.editNumberOfPlayersToolStripMenuItem.Name = "editNumberOfPlayersToolStripMenuItem";
            this.editNumberOfPlayersToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.editNumberOfPlayersToolStripMenuItem.Text = "Edit Number of Players";
            this.editNumberOfPlayersToolStripMenuItem.Click += new System.EventHandler(this.editNumberOfPlayersToolStripMenuItem_Click);
            // 
            // exitGameToolStripMenuItem
            // 
            this.exitGameToolStripMenuItem.Name = "exitGameToolStripMenuItem";
            this.exitGameToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.exitGameToolStripMenuItem.Text = "Exit Game";
            this.exitGameToolStripMenuItem.Click += new System.EventHandler(this.exitGameToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 10F);
            this.label1.Location = new System.Drawing.Point(9, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Console:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 10F);
            this.label2.Location = new System.Drawing.Point(9, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Gestures:";
            // 
            // winners
            // 
            this.winners.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.winners.Font = new System.Drawing.Font("Calibri", 10F);
            this.winners.FormattingEnabled = true;
            this.winners.Location = new System.Drawing.Point(12, 700);
            this.winners.MaxDropDownItems = 100;
            this.winners.Name = "winners";
            this.winners.Size = new System.Drawing.Size(312, 23);
            this.winners.Sorted = true;
            this.winners.TabIndex = 7;
            this.winners.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 10F);
            this.label3.Location = new System.Drawing.Point(9, 680);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "# of Wins per Player:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 735);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.winners);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.console);
            this.Controls.Add(this.scissors);
            this.Controls.Add(this.paper);
            this.Controls.Add(this.rock);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Rock-Paper-Scissors";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button rock;
        private System.Windows.Forms.Button paper;
        private System.Windows.Forms.Button scissors;
        public System.Windows.Forms.TextBox console;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitGameToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem editNumberOfPlayersToolStripMenuItem;
        public System.Windows.Forms.ComboBox winners;
        private System.Windows.Forms.Label label3;
    }
}

