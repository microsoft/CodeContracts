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

namespace Microsoft.VisualStudio.Contracts
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
      this.components = new System.ComponentModel.Container();
      this.useContractsCheck = new System.Windows.Forms.CheckBox();
      this.produceContractAssemblyCheck = new System.Windows.Forms.CheckBox();
      this.contractsToolTip = new System.Windows.Forms.ToolTip(this.components);
      this.verifyopts = new System.Windows.Forms.TextBox();
      this.useVerifier = new System.Windows.Forms.CheckBox();
      this.runtimeChecks = new System.Windows.Forms.GroupBox();
      this.invariants = new System.Windows.Forms.CheckBox();
      this.assumes = new System.Windows.Forms.CheckBox();
      this.allOtherPreAndPostConditions = new System.Windows.Forms.CheckBox();
      this.publicPreconditions = new System.Windows.Forms.CheckBox();
      this.customAttributes = new System.Windows.Forms.GroupBox();
      this.allOtherContracts = new System.Windows.Forms.CheckBox();
      this.publicContracts = new System.Windows.Forms.CheckBox();
      this.compileTimeChecks = new System.Windows.Forms.GroupBox();
      this.checkPurity = new System.Windows.Forms.CheckBox();
      this.checkContracts = new System.Windows.Forms.CheckBox();
      this.nonNullByDefault = new System.Windows.Forms.CheckBox();
      this.verifyoptsLabel = new System.Windows.Forms.Label();
      this.runtimeChecks.SuspendLayout();
      this.customAttributes.SuspendLayout();
      this.compileTimeChecks.SuspendLayout();
      this.SuspendLayout();
      // 
      // useContractsCheck
      // 
      this.useContractsCheck.AutoSize = true;
      this.useContractsCheck.Location = new System.Drawing.Point(9, 11);
      this.useContractsCheck.Margin = new System.Windows.Forms.Padding(2);
      this.useContractsCheck.Name = "useContractsCheck";
      this.useContractsCheck.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.useContractsCheck.Size = new System.Drawing.Size(107, 17);
      this.useContractsCheck.TabIndex = 0;
      this.useContractsCheck.Text = "Enable Contracts";
      this.useContractsCheck.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
      this.contractsToolTip.SetToolTip(this.useContractsCheck, "Enable contracts and non-null analysis. Only used when optimization is disabled.");
      this.useContractsCheck.CheckedChanged += new System.EventHandler(this.useContractsCheck_CheckedChanged);
      // 
      // verifyopts
      // 
      this.verifyopts.Enabled = this.useVerifier.Checked;
      this.verifyopts.Location = new System.Drawing.Point(109, 112);
      this.verifyopts.Name = "verifyopts";
      this.verifyopts.Size = new System.Drawing.Size(148, 20);
      this.verifyopts.TabIndex = 7;
      this.contractsToolTip.SetToolTip(this.verifyopts, "Verification Options");
      this.verifyopts.TextChanged += new System.EventHandler(this.verifyopts_TextChanged);
      // 
      // useVerifier
      // 
      this.useVerifier.AutoSize = true;
      this.useVerifier.Location = new System.Drawing.Point(6, 91);
      this.useVerifier.Name = "useVerifier";
      this.useVerifier.Size = new System.Drawing.Size(167, 17);
      this.useVerifier.TabIndex = 6;
      this.useVerifier.Text = "Verify contracts when building";
      this.useVerifier.CheckedChanged += new System.EventHandler(this.useVerifier_CheckedChanged);
      // 
      // runtimeChecks
      // 
      this.runtimeChecks.Controls.Add(this.invariants);
      this.runtimeChecks.Controls.Add(this.assumes);
      this.runtimeChecks.Controls.Add(this.allOtherPreAndPostConditions);
      this.runtimeChecks.Controls.Add(this.publicPreconditions);
      this.runtimeChecks.Enabled = false;
      this.runtimeChecks.Location = new System.Drawing.Point(23, 32);
      this.runtimeChecks.Margin = new System.Windows.Forms.Padding(2);
      this.runtimeChecks.Name = "runtimeChecks";
      this.runtimeChecks.Padding = new System.Windows.Forms.Padding(2);
      this.runtimeChecks.Size = new System.Drawing.Size(262, 110);
      this.runtimeChecks.TabIndex = 5;
      this.runtimeChecks.TabStop = false;
      this.runtimeChecks.Text = "Runtime checks";
      // 
      // invariants
      // 
      this.invariants.AutoSize = true;
      this.invariants.Location = new System.Drawing.Point(6, 91);
      this.invariants.Name = "invariants";
      this.invariants.Size = new System.Drawing.Size(220, 17);
      this.invariants.TabIndex = 3;
      this.invariants.Text = "Invariants, ownership, concurrent access";
      this.invariants.CheckedChanged += new System.EventHandler(this.invariants_CheckedChanged);
      // 
      // assumes
      // 
      this.assumes.AutoSize = true;
      this.assumes.Location = new System.Drawing.Point(6, 67);
      this.assumes.Name = "assumes";
      this.assumes.Size = new System.Drawing.Size(117, 17);
      this.assumes.TabIndex = 2;
      this.assumes.Text = "Assume statements";
      this.assumes.CheckedChanged += new System.EventHandler(this.assumes_CheckedChanged);
      // 
      // allOtherPreAndPostConditions
      // 
      this.allOtherPreAndPostConditions.Location = new System.Drawing.Point(6, 43);
      this.allOtherPreAndPostConditions.Name = "allOtherPreAndPostConditions";
      this.allOtherPreAndPostConditions.Size = new System.Drawing.Size(216, 17);
      this.allOtherPreAndPostConditions.TabIndex = 1;
      this.allOtherPreAndPostConditions.Text = "All other pre and post conditions";
      this.allOtherPreAndPostConditions.CheckedChanged += new System.EventHandler(this.allOtherPreAndPostConditions_CheckedChanged);
      // 
      // publicPreconditions
      // 
      this.publicPreconditions.AutoSize = true;
      this.publicPreconditions.Location = new System.Drawing.Point(6, 19);
      this.publicPreconditions.Name = "publicPreconditions";
      this.publicPreconditions.Size = new System.Drawing.Size(245, 17);
      this.publicPreconditions.TabIndex = 0;
      this.publicPreconditions.Text = "Preconditions of public and protected methods";
      this.publicPreconditions.CheckedChanged += new System.EventHandler(this.PublicPreconditions_CheckedChanged);
      // 
      // customAttributes
      // 
      this.customAttributes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
      this.customAttributes.Controls.Add(this.allOtherContracts);
      this.customAttributes.Controls.Add(this.publicContracts);
      this.customAttributes.Enabled = false;
      this.customAttributes.Location = new System.Drawing.Point(23, 153);
      this.customAttributes.Name = "customAttributes";
      this.customAttributes.Size = new System.Drawing.Size(262, 64);
      this.customAttributes.TabIndex = 7;
      this.customAttributes.TabStop = false;
      this.customAttributes.Text = "Custom attributes";
      // 
      // allOtherContracts
      // 
      this.allOtherContracts.AutoSize = true;
      this.allOtherContracts.Location = new System.Drawing.Point(6, 43);
      this.allOtherContracts.Name = "allOtherContracts";
      this.allOtherContracts.Size = new System.Drawing.Size(111, 17);
      this.allOtherContracts.TabIndex = 1;
      this.allOtherContracts.Text = "All other contracts";
      this.allOtherContracts.CheckedChanged += new System.EventHandler(this.allOtherContracts_CheckedChanged);
      // 
      // publicContracts
      // 
      this.publicContracts.AutoSize = true;
      this.publicContracts.Location = new System.Drawing.Point(6, 19);
      this.publicContracts.Name = "publicContracts";
      this.publicContracts.Size = new System.Drawing.Size(226, 17);
      this.publicContracts.TabIndex = 0;
      this.publicContracts.Text = "Contracts of public and protected methods";
      this.publicContracts.CheckedChanged += new System.EventHandler(this.publicContracts_CheckedChanged);
      // 
      // compileTimeChecks
      // 
      this.compileTimeChecks.Controls.Add(this.checkPurity);
      this.compileTimeChecks.Controls.Add(this.checkContracts);
      this.compileTimeChecks.Controls.Add(this.nonNullByDefault);
      this.compileTimeChecks.Controls.Add(this.verifyoptsLabel);
      this.compileTimeChecks.Controls.Add(this.verifyopts);
      this.compileTimeChecks.Controls.Add(this.useVerifier);
      this.compileTimeChecks.Enabled = false;
      this.compileTimeChecks.Location = new System.Drawing.Point(23, 232);
      this.compileTimeChecks.Name = "compileTimeChecks";
      this.compileTimeChecks.Size = new System.Drawing.Size(262, 145);
      this.compileTimeChecks.TabIndex = 8;
      this.compileTimeChecks.TabStop = false;
      this.compileTimeChecks.Text = "Compile time checks";
      // 
      // checkPurity
      // 
      this.checkPurity.AutoSize = true;
      this.checkPurity.Location = new System.Drawing.Point(6, 67);
      this.checkPurity.Name = "checkPurity";
      this.checkPurity.Size = new System.Drawing.Size(123, 17);
      this.checkPurity.TabIndex = 11;
      this.checkPurity.Text = "Check method purity";
      this.checkPurity.UseVisualStyleBackColor = true;
      this.checkPurity.CheckedChanged += new System.EventHandler(this.CheckPurity_CheckedChanged);
      // 
      // checkContracts
      // 
      this.checkContracts.AutoSize = true;
      this.checkContracts.Location = new System.Drawing.Point(6, 44);
      this.checkContracts.Name = "checkContracts";
      this.checkContracts.Size = new System.Drawing.Size(156, 17);
      this.checkContracts.TabIndex = 10;
      this.checkContracts.Text = "Check contract admissibility";
      this.checkContracts.UseVisualStyleBackColor = true;
      this.checkContracts.CheckedChanged += new System.EventHandler(this.CheckContractAdmissibility_CheckedChanged);
      // 
      // nonNullByDefault
      // 
      this.nonNullByDefault.AutoSize = true;
      this.nonNullByDefault.Location = new System.Drawing.Point(6, 19);
      this.nonNullByDefault.Name = "nonNullByDefault";
      this.nonNullByDefault.Size = new System.Drawing.Size(231, 17);
      this.nonNullByDefault.TabIndex = 9;
      this.nonNullByDefault.Text = "Reference types are non nullable by default";
      this.nonNullByDefault.UseVisualStyleBackColor = true;
      this.nonNullByDefault.CheckedChanged += new System.EventHandler(this.NonNullByDefault_CheckedChanged);
      // 
      // verifyoptsLabel
      // 
      this.verifyoptsLabel.AutoSize = true;
      this.verifyoptsLabel.Location = new System.Drawing.Point(20, 112);
      this.verifyoptsLabel.Name = "verifyoptsLabel";
      this.verifyoptsLabel.Size = new System.Drawing.Size(81, 13);
      this.verifyoptsLabel.TabIndex = 8;
      this.verifyoptsLabel.Text = "Verifier Options:";
      this.verifyoptsLabel.Click += new System.EventHandler(this.verifyoptsLabel_Click);
      // 
      // produceContractAssemblyCheck
      // 
      this.produceContractAssemblyCheck.AutoSize = true;
      this.produceContractAssemblyCheck.Location = new System.Drawing.Point(121, 10);
      this.produceContractAssemblyCheck.Name = "produceContractAssemblyCheck";
      this.produceContractAssemblyCheck.Size = new System.Drawing.Size(165, 17);
      this.produceContractAssemblyCheck.TabIndex = 9;
      this.produceContractAssemblyCheck.Text = "Produce out of band contract";
      this.produceContractAssemblyCheck.UseVisualStyleBackColor = true;
      this.produceContractAssemblyCheck.CheckedChanged += new System.EventHandler(this.produceContractAssemblyCheck_CheckedChanged);
      // 
      // PropertyPane
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoScroll = true;
      this.AutoScrollMargin = new System.Drawing.Size(2, 2);
      this.Controls.Add(this.produceContractAssemblyCheck);
      this.Controls.Add(this.compileTimeChecks);
      this.Controls.Add(this.customAttributes);
      this.Controls.Add(this.runtimeChecks);
      this.Controls.Add(this.useContractsCheck);
      this.Margin = new System.Windows.Forms.Padding(2);
      this.Name = "PropertyPane";
      this.Size = new System.Drawing.Size(300, 405);
      this.Load += new System.EventHandler(this.PropertyPane_Load);
      this.runtimeChecks.ResumeLayout(false);
      this.runtimeChecks.PerformLayout();
      this.customAttributes.ResumeLayout(false);
      this.customAttributes.PerformLayout();
      this.compileTimeChecks.ResumeLayout(false);
      this.compileTimeChecks.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.CheckBox useContractsCheck;
    private System.Windows.Forms.CheckBox produceContractAssemblyCheck;
    private System.Windows.Forms.ToolTip contractsToolTip;
    private System.Windows.Forms.GroupBox runtimeChecks;
    private System.Windows.Forms.CheckBox useVerifier;
    private System.Windows.Forms.CheckBox publicPreconditions;
    private System.Windows.Forms.CheckBox allOtherPreAndPostConditions;
    private System.Windows.Forms.CheckBox assumes;
    private System.Windows.Forms.GroupBox customAttributes;
    private System.Windows.Forms.CheckBox allOtherContracts;
    private System.Windows.Forms.CheckBox publicContracts;
    private System.Windows.Forms.CheckBox invariants;
    private System.Windows.Forms.GroupBox compileTimeChecks;
    private System.Windows.Forms.TextBox verifyopts;
    private System.Windows.Forms.Label verifyoptsLabel;
    private System.Windows.Forms.CheckBox nonNullByDefault;
    private System.Windows.Forms.CheckBox checkContracts;
    private System.Windows.Forms.CheckBox checkPurity;

  }
}
