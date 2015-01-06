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

namespace TicTacToeForm
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
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.button1 = new System.Windows.Forms.Button();
        this.button2 = new System.Windows.Forms.Button();
        this.button3 = new System.Windows.Forms.Button();
        this.button4 = new System.Windows.Forms.Button();
        this.button6 = new System.Windows.Forms.Button();
        this.button7 = new System.Windows.Forms.Button();
        this.button8 = new System.Windows.Forms.Button();
        this.button9 = new System.Windows.Forms.Button();
        this.button5 = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // textBox1
        // 
        this.textBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
        this.textBox1.Location = new System.Drawing.Point(100, 378);
        this.textBox1.Multiline = true;
        this.textBox1.Name = "textBox1";
        this.textBox1.ReadOnly = true;
        this.textBox1.Size = new System.Drawing.Size(500, 125);
        this.textBox1.TabIndex = 0;
        this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
        // 
        // button1
        // 
        this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F);
        this.button1.Location = new System.Drawing.Point(194, 34);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(100, 100);
        this.button1.TabIndex = 1;
        this.button1.Text = "1";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(this.button1_Click);
        // 
        // button2
        // 
        this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F);
        this.button2.Location = new System.Drawing.Point(300, 34);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(100, 100);
        this.button2.TabIndex = 2;
        this.button2.Text = "2";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(this.button2_Click);
        // 
        // button3
        // 
        this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F);
        this.button3.Location = new System.Drawing.Point(406, 34);
        this.button3.Name = "button3";
        this.button3.Size = new System.Drawing.Size(100, 100);
        this.button3.TabIndex = 3;
        this.button3.Text = "3";
        this.button3.UseVisualStyleBackColor = true;
        // 
        // button4
        // 
        this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F);
        this.button4.Location = new System.Drawing.Point(194, 140);
        this.button4.Name = "button4";
        this.button4.Size = new System.Drawing.Size(100, 100);
        this.button4.TabIndex = 4;
        this.button4.Text = "4";
        this.button4.UseVisualStyleBackColor = true;
        // 
        // button6
        // 
        this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F);
        this.button6.Location = new System.Drawing.Point(406, 140);
        this.button6.Name = "button6";
        this.button6.Size = new System.Drawing.Size(100, 100);
        this.button6.TabIndex = 6;
        this.button6.Text = "6";
        this.button6.UseVisualStyleBackColor = true;
        // 
        // button7
        // 
        this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F);
        this.button7.Location = new System.Drawing.Point(194, 246);
        this.button7.Name = "button7";
        this.button7.Size = new System.Drawing.Size(100, 100);
        this.button7.TabIndex = 7;
        this.button7.Text = "7";
        this.button7.UseVisualStyleBackColor = true;
        // 
        // button8
        // 
        this.button8.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F);
        this.button8.Location = new System.Drawing.Point(300, 246);
        this.button8.Name = "button8";
        this.button8.Size = new System.Drawing.Size(100, 100);
        this.button8.TabIndex = 8;
        this.button8.Text = "8";
        this.button8.UseVisualStyleBackColor = true;
        // 
        // button9
        // 
        this.button9.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F);
        this.button9.Location = new System.Drawing.Point(406, 246);
        this.button9.Name = "button9";
        this.button9.Size = new System.Drawing.Size(100, 100);
        this.button9.TabIndex = 9;
        this.button9.Text = "9";
        this.button9.UseVisualStyleBackColor = true;
        // 
        // button5
        // 
        this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F);
        this.button5.Location = new System.Drawing.Point(300, 140);
        this.button5.Name = "button5";
        this.button5.Size = new System.Drawing.Size(100, 100);
        this.button5.TabIndex = 5;
        this.button5.Text = "5";
        this.button5.UseVisualStyleBackColor = true;
        // 
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(700, 550);
        this.Controls.Add(this.button9);
        this.Controls.Add(this.button8);
        this.Controls.Add(this.button7);
        this.Controls.Add(this.button6);
        this.Controls.Add(this.button5);
        this.Controls.Add(this.button4);
        this.Controls.Add(this.button3);
        this.Controls.Add(this.button2);
        this.Controls.Add(this.button1);
        this.Controls.Add(this.textBox1);
        this.Name = "Form1";
        this.Text = "TicTacToe Game";
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.Button button4;
    private System.Windows.Forms.Button button6;
    private System.Windows.Forms.Button button7;
    private System.Windows.Forms.Button button8;
    private System.Windows.Forms.Button button9;
    private System.Windows.Forms.Button button5;

  }
}

