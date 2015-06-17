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

namespace AssemblyInfo
{
	partial class PropertyPane
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
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.EnableCheckBox = new System.Windows.Forms.CheckBox();
			this.FormatTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// EnableCheckBox
			// 
			this.EnableCheckBox.AutoSize = true;
			this.EnableCheckBox.Location = new System.Drawing.Point(3, 3);
			this.EnableCheckBox.Name = "EnableCheckBox";
			this.EnableCheckBox.Size = new System.Drawing.Size(127, 17);
			this.EnableCheckBox.TabIndex = 0;
			this.EnableCheckBox.Text = "Enable Assembly Info";
			this.EnableCheckBox.UseVisualStyleBackColor = true;
			this.EnableCheckBox.CheckedChanged += new System.EventHandler(this.EnableCheckBox_CheckedChanged);
			// 
			// FormatTextBox
			// 
			this.FormatTextBox.Location = new System.Drawing.Point(86, 20);
			this.FormatTextBox.Name = "FormatTextBox";
			this.FormatTextBox.Size = new System.Drawing.Size(212, 20);
			this.FormatTextBox.TabIndex = 1;
			this.FormatTextBox.Text = "Assembly: {0}, Version: {1}, Types: {2}";
			this.FormatTextBox.TextChanged += new System.EventHandler(this.FormatTextBox_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(67, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Format string";
			// 
			// PropertyPane
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.Controls.Add(this.label1);
			this.Controls.Add(this.FormatTextBox);
			this.Controls.Add(this.EnableCheckBox);
			this.Name = "PropertyPane";
			this.Size = new System.Drawing.Size(318, 58);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox EnableCheckBox;
		private System.Windows.Forms.TextBox FormatTextBox;
		private System.Windows.Forms.Label label1;
	}
}
