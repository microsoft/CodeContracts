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

namespace CCCheckPulseUI
{
  partial class CCCheckPulseForm
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
      this.components = new System.ComponentModel.Container();
      this.clousotPulseGrid = new System.Windows.Forms.DataGridView();
      this.clearButton = new System.Windows.Forms.Button();
      this.clousotProcessesGrid = new System.Windows.Forms.DataGridView();
      this.label1 = new System.Windows.Forms.Label();
      this.automaticUpdate = new System.Windows.Forms.NumericUpDown();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.clousotResultsGrid = new System.Windows.Forms.DataGridView();
      this.sendToExcelButton = new System.Windows.Forms.Button();
      this.killAllClousots = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.clousotPulseGrid)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.clousotProcessesGrid)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.automaticUpdate)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.clousotResultsGrid)).BeginInit();
      this.SuspendLayout();
      // 
      // clousotPulseGrid
      // 
      this.clousotPulseGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.clousotPulseGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.clousotPulseGrid.Location = new System.Drawing.Point(12, 320);
      this.clousotPulseGrid.Name = "clousotPulseGrid";
      this.clousotPulseGrid.Size = new System.Drawing.Size(1069, 210);
      this.clousotPulseGrid.TabIndex = 0;
      // 
      // clearButton
      // 
      this.clearButton.Location = new System.Drawing.Point(237, 763);
      this.clearButton.Name = "clearButton";
      this.clearButton.Size = new System.Drawing.Size(147, 23);
      this.clearButton.TabIndex = 2;
      this.clearButton.Text = "Clear Tables";
      this.clearButton.UseVisualStyleBackColor = true;
      this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
      // 
      // clousotProcessesGrid
      // 
      this.clousotProcessesGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.clousotProcessesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.clousotProcessesGrid.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
      this.clousotProcessesGrid.Location = new System.Drawing.Point(11, 12);
      this.clousotProcessesGrid.Name = "clousotProcessesGrid";
      this.clousotProcessesGrid.Size = new System.Drawing.Size(1070, 302);
      this.clousotProcessesGrid.TabIndex = 3;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(15, 773);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(90, 13);
      this.label1.TabIndex = 4;
      this.label1.Text = "Automatic update";
      // 
      // automaticUpdate
      // 
      this.automaticUpdate.Location = new System.Drawing.Point(111, 763);
      this.automaticUpdate.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
      this.automaticUpdate.Name = "automaticUpdate";
      this.automaticUpdate.Size = new System.Drawing.Size(120, 20);
      this.automaticUpdate.TabIndex = 5;
      this.automaticUpdate.ValueChanged += new System.EventHandler(this.automaticUpdate_ValueChanged);
      // 
      // clousotResultsGrid
      // 
      this.clousotResultsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.clousotResultsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.clousotResultsGrid.Location = new System.Drawing.Point(11, 536);
      this.clousotResultsGrid.Name = "clousotResultsGrid";
      this.clousotResultsGrid.Size = new System.Drawing.Size(1070, 218);
      this.clousotResultsGrid.TabIndex = 6;
      // 
      // sendToExcelButton
      // 
      this.sendToExcelButton.Location = new System.Drawing.Point(390, 763);
      this.sendToExcelButton.Name = "sendToExcelButton";
      this.sendToExcelButton.Size = new System.Drawing.Size(173, 23);
      this.sendToExcelButton.TabIndex = 7;
      this.sendToExcelButton.Text = "Send to Excel";
      this.sendToExcelButton.UseVisualStyleBackColor = true;
      this.sendToExcelButton.Click += new System.EventHandler(this.sendToExcelButton_Click);
      // 
      // killAllClousots
      // 
      this.killAllClousots.Location = new System.Drawing.Point(569, 762);
      this.killAllClousots.Name = "killAllClousots";
      this.killAllClousots.Size = new System.Drawing.Size(163, 23);
      this.killAllClousots.TabIndex = 8;
      this.killAllClousots.Text = "Kill all instances";
      this.killAllClousots.UseVisualStyleBackColor = true;
      this.killAllClousots.Click += new System.EventHandler(this.killAllClousot_Click);
      // 
      // CCCheckPulseForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1100, 795);
      this.Controls.Add(this.killAllClousots);
      this.Controls.Add(this.sendToExcelButton);
      this.Controls.Add(this.clousotResultsGrid);
      this.Controls.Add(this.automaticUpdate);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.clousotProcessesGrid);
      this.Controls.Add(this.clearButton);
      this.Controls.Add(this.clousotPulseGrid);
      this.Name = "CCCheckPulseForm";
      this.Text = "CCheckPulse";
      ((System.ComponentModel.ISupportInitialize)(this.clousotPulseGrid)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.clousotProcessesGrid)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.automaticUpdate)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.clousotResultsGrid)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DataGridView clousotPulseGrid;
    private System.Windows.Forms.Button clearButton;
    private System.Windows.Forms.DataGridView clousotProcessesGrid;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.NumericUpDown automaticUpdate;
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.DataGridView clousotResultsGrid;
    private System.Windows.Forms.Button sendToExcelButton;
    private System.Windows.Forms.Button killAllClousots;
  }
}

