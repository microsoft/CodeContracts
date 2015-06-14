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

namespace Microsoft.Contracts.VisualStudio {
  partial class PropertyPane {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropertyPane));
      this.EnableRuntimeCheckingBox = new System.Windows.Forms.CheckBox();
      this.EnableStaticCheckingBox = new System.Windows.Forms.CheckBox();
      this.LibPathTextBox = new System.Windows.Forms.TextBox();
      this.LibPathLabel = new System.Windows.Forms.Label();
      this.ExtraRuntimeCheckerOptionsLabel = new System.Windows.Forms.Label();
      this.ExtraRuntimeCheckingOptionsBox = new System.Windows.Forms.TextBox();
      this.CustomRewriterMethodsClassTextBox = new System.Windows.Forms.TextBox();
      this.CustomRewriterMethodsClassLabel = new System.Windows.Forms.Label();
      this.CustomRewriterMethodsAssemblyLabel = new System.Windows.Forms.Label();
      this.CustomRewriterMethodsAssemblyTextBox = new System.Windows.Forms.TextBox();
      this.CustomRewriterMethodsLabel = new System.Windows.Forms.Label();
      this.RuntimeCheckingGroup = new System.Windows.Forms.GroupBox();
      this.SkipQuantifiersBox = new System.Windows.Forms.CheckBox();
      this.CallSiteRequiresBox = new System.Windows.Forms.CheckBox();
      this.AssertOnFailureBox = new System.Windows.Forms.CheckBox();
      this.OnlyPublicSurfaceContractsBox = new System.Windows.Forms.CheckBox();
      this.RuntimeCheckingLevelDropDown = new System.Windows.Forms.ComboBox();
      this.AdvancedGroup = new System.Windows.Forms.GroupBox();
      this.ExtraCodeAnalysisOptionsBox = new System.Windows.Forms.TextBox();
      this.AdditionalCodeAnalysisOptionsLabel = new System.Windows.Forms.Label();
      this.StaticCheckingGroup = new System.Windows.Forms.GroupBox();
      this.SuggestCalleeAssumptionsCheckBox = new System.Windows.Forms.CheckBox();
      this.skipAnalysisIfCannotConnectToCache = new System.Windows.Forms.CheckBox();
      this.checkMissingPublicEnsures = new System.Windows.Forms.CheckBox();
      this.linkUnderstandingTheStaticChecker = new System.Windows.Forms.LinkLabel();
      this.NecessaryEnsuresCheckBox = new System.Windows.Forms.CheckBox();
      this.AssertsToContractsCheckBox = new System.Windows.Forms.CheckBox();
      this.RedundantTestsCheckBox = new System.Windows.Forms.CheckBox();
      this.SuggestReadonlyCheckBox = new System.Windows.Forms.CheckBox();
      this.BeingOptmisticOnExternalCheckBox = new System.Windows.Forms.CheckBox();
      this.missingPublicRequiresAsWarningsCheckBox = new System.Windows.Forms.CheckBox();
      this.SQLServerLabel = new System.Windows.Forms.Label();
      this.SQLServerTextBox = new System.Windows.Forms.TextBox();
      this.CacheResultsCheckBox = new System.Windows.Forms.CheckBox();
      this.SuggestAssumptionsCheckBox = new System.Windows.Forms.CheckBox();
      this.InferObjectInvariantsCheckBox = new System.Windows.Forms.CheckBox();
      this.SuggestObjectInvariantsCheckBox = new System.Windows.Forms.CheckBox();
      this.WarningFullLabel = new System.Windows.Forms.Label();
      this.WarningLowLabel = new System.Windows.Forms.Label();
      this.WarningLevelTrackBar = new System.Windows.Forms.TrackBar();
      this.WarningLevelLabel = new System.Windows.Forms.Label();
      this.redundantAssumptionsCheckBox = new System.Windows.Forms.CheckBox();
      this.InferRequiresCheckBox = new System.Windows.Forms.CheckBox();
      this.InferEnsuresCheckBox = new System.Windows.Forms.CheckBox();
      this.SuggestRequiresCheckBox = new System.Windows.Forms.CheckBox();
      this.BaseLineUpdateButton = new System.Windows.Forms.Button();
      this.ImplicitArithmeticObligationsBox = new System.Windows.Forms.CheckBox();
      this.FailBuildOnWarningsCheckBox = new System.Windows.Forms.CheckBox();
      this.ShowSquiggliesBox = new System.Windows.Forms.CheckBox();
      this.RunInBackgroundBox = new System.Windows.Forms.CheckBox();
      this.BaseLineBox = new System.Windows.Forms.CheckBox();
      this.BaseLineTextBox = new System.Windows.Forms.TextBox();
      this.ImplicitArrayBoundObligationsBox = new System.Windows.Forms.CheckBox();
      this.ImplicitNonNullObligationsBox = new System.Windows.Forms.CheckBox();
      this.ImplicitEnumWritesBox = new System.Windows.Forms.CheckBox();
      this.WarningLevelToolTip = new System.Windows.Forms.ToolTip(this.components);
      this.EmitContractDocumentationCheckBox = new System.Windows.Forms.CheckBox();
      this.ContractReferenceAssemblySelection = new System.Windows.Forms.ComboBox();
      this.VersionLabel = new System.Windows.Forms.LinkLabel();
      this.contractReferenceAssemblyGroup = new System.Windows.Forms.GroupBox();
      this.AssemblyModeLabel = new System.Windows.Forms.Label();
      this.AssemblyModeDropDown = new System.Windows.Forms.ComboBox();
      this.PrecisionLevelToolTip = new System.Windows.Forms.ToolTip(this.components);
      this.docLink = new System.Windows.Forms.LinkLabel();
      this.help_Link = new System.Windows.Forms.LinkLabel();
      this.InferEnsuresAutoPropertiesCheckBox = new System.Windows.Forms.CheckBox();
      this.RuntimeCheckingGroup.SuspendLayout();
      this.AdvancedGroup.SuspendLayout();
      this.StaticCheckingGroup.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.WarningLevelTrackBar)).BeginInit();
      this.contractReferenceAssemblyGroup.SuspendLayout();
      this.SuspendLayout();
      // 
      // EnableRuntimeCheckingBox
      // 
      resources.ApplyResources(this.EnableRuntimeCheckingBox, "EnableRuntimeCheckingBox");
      this.EnableRuntimeCheckingBox.Name = "EnableRuntimeCheckingBox";
      this.EnableRuntimeCheckingBox.UseVisualStyleBackColor = true;
      this.EnableRuntimeCheckingBox.CheckedChanged += new System.EventHandler(this.EnableContractCheckingBox_CheckedChanged);
      // 
      // EnableStaticCheckingBox
      // 
      resources.ApplyResources(this.EnableStaticCheckingBox, "EnableStaticCheckingBox");
      this.EnableStaticCheckingBox.Name = "EnableStaticCheckingBox";
      this.EnableStaticCheckingBox.UseVisualStyleBackColor = true;
      this.EnableStaticCheckingBox.CheckedChanged += new System.EventHandler(this.EnableStaticCheckingBox_CheckedChanged);
      // 
      // LibPathTextBox
      // 
      resources.ApplyResources(this.LibPathTextBox, "LibPathTextBox");
      this.LibPathTextBox.Name = "LibPathTextBox";
      this.LibPathTextBox.TextChanged += new System.EventHandler(this.LibPathTextBox_TextChanged);
      // 
      // LibPathLabel
      // 
      resources.ApplyResources(this.LibPathLabel, "LibPathLabel");
      this.LibPathLabel.Name = "LibPathLabel";
      // 
      // ExtraRuntimeCheckerOptionsLabel
      // 
      resources.ApplyResources(this.ExtraRuntimeCheckerOptionsLabel, "ExtraRuntimeCheckerOptionsLabel");
      this.ExtraRuntimeCheckerOptionsLabel.Name = "ExtraRuntimeCheckerOptionsLabel";
      // 
      // ExtraRuntimeCheckingOptionsBox
      // 
      resources.ApplyResources(this.ExtraRuntimeCheckingOptionsBox, "ExtraRuntimeCheckingOptionsBox");
      this.ExtraRuntimeCheckingOptionsBox.Name = "ExtraRuntimeCheckingOptionsBox";
      this.ExtraRuntimeCheckingOptionsBox.TextChanged += new System.EventHandler(this.PlatformTextBox_TextChanged);
      // 
      // CustomRewriterMethodsClassTextBox
      // 
      resources.ApplyResources(this.CustomRewriterMethodsClassTextBox, "CustomRewriterMethodsClassTextBox");
      this.CustomRewriterMethodsClassTextBox.Name = "CustomRewriterMethodsClassTextBox";
      this.CustomRewriterMethodsClassTextBox.TextChanged += new System.EventHandler(this.CustomRewriterMethodsClassTextBox_TextChanged);
      // 
      // CustomRewriterMethodsClassLabel
      // 
      resources.ApplyResources(this.CustomRewriterMethodsClassLabel, "CustomRewriterMethodsClassLabel");
      this.CustomRewriterMethodsClassLabel.Name = "CustomRewriterMethodsClassLabel";
      // 
      // CustomRewriterMethodsAssemblyLabel
      // 
      resources.ApplyResources(this.CustomRewriterMethodsAssemblyLabel, "CustomRewriterMethodsAssemblyLabel");
      this.CustomRewriterMethodsAssemblyLabel.Name = "CustomRewriterMethodsAssemblyLabel";
      // 
      // CustomRewriterMethodsAssemblyTextBox
      // 
      resources.ApplyResources(this.CustomRewriterMethodsAssemblyTextBox, "CustomRewriterMethodsAssemblyTextBox");
      this.CustomRewriterMethodsAssemblyTextBox.Name = "CustomRewriterMethodsAssemblyTextBox";
      this.CustomRewriterMethodsAssemblyTextBox.TextChanged += new System.EventHandler(this.CustomRewriterMethodsAssemblyTextBox_TextChanged);
      // 
      // CustomRewriterMethodsLabel
      // 
      resources.ApplyResources(this.CustomRewriterMethodsLabel, "CustomRewriterMethodsLabel");
      this.CustomRewriterMethodsLabel.Name = "CustomRewriterMethodsLabel";
      // 
      // RuntimeCheckingGroup
      // 
      this.RuntimeCheckingGroup.Controls.Add(this.SkipQuantifiersBox);
      this.RuntimeCheckingGroup.Controls.Add(this.CallSiteRequiresBox);
      this.RuntimeCheckingGroup.Controls.Add(this.AssertOnFailureBox);
      this.RuntimeCheckingGroup.Controls.Add(this.OnlyPublicSurfaceContractsBox);
      this.RuntimeCheckingGroup.Controls.Add(this.RuntimeCheckingLevelDropDown);
      this.RuntimeCheckingGroup.Controls.Add(this.EnableRuntimeCheckingBox);
      this.RuntimeCheckingGroup.Controls.Add(this.CustomRewriterMethodsLabel);
      this.RuntimeCheckingGroup.Controls.Add(this.CustomRewriterMethodsClassTextBox);
      this.RuntimeCheckingGroup.Controls.Add(this.CustomRewriterMethodsAssemblyTextBox);
      this.RuntimeCheckingGroup.Controls.Add(this.CustomRewriterMethodsAssemblyLabel);
      this.RuntimeCheckingGroup.Controls.Add(this.CustomRewriterMethodsClassLabel);
      resources.ApplyResources(this.RuntimeCheckingGroup, "RuntimeCheckingGroup");
      this.RuntimeCheckingGroup.Name = "RuntimeCheckingGroup";
      this.RuntimeCheckingGroup.TabStop = false;
      // 
      // SkipQuantifiersBox
      // 
      resources.ApplyResources(this.SkipQuantifiersBox, "SkipQuantifiersBox");
      this.SkipQuantifiersBox.Name = "SkipQuantifiersBox";
      this.SkipQuantifiersBox.UseVisualStyleBackColor = true;
      this.SkipQuantifiersBox.CheckedChanged += new System.EventHandler(this.SkipQuantifiersBox_CheckedChanged);
      // 
      // CallSiteRequiresBox
      // 
      resources.ApplyResources(this.CallSiteRequiresBox, "CallSiteRequiresBox");
      this.CallSiteRequiresBox.Name = "CallSiteRequiresBox";
      this.CallSiteRequiresBox.UseVisualStyleBackColor = true;
      this.CallSiteRequiresBox.CheckedChanged += new System.EventHandler(this.CallSiteRequiresBox_CheckedChanged);
      // 
      // AssertOnFailureBox
      // 
      resources.ApplyResources(this.AssertOnFailureBox, "AssertOnFailureBox");
      this.AssertOnFailureBox.Name = "AssertOnFailureBox";
      this.AssertOnFailureBox.UseVisualStyleBackColor = true;
      this.AssertOnFailureBox.CheckedChanged += new System.EventHandler(this.AssertOnFailureBox_CheckedChanged);
      // 
      // OnlyPublicSurfaceContractsBox
      // 
      resources.ApplyResources(this.OnlyPublicSurfaceContractsBox, "OnlyPublicSurfaceContractsBox");
      this.OnlyPublicSurfaceContractsBox.Name = "OnlyPublicSurfaceContractsBox";
      this.OnlyPublicSurfaceContractsBox.UseVisualStyleBackColor = true;
      this.OnlyPublicSurfaceContractsBox.CheckedChanged += new System.EventHandler(this.OnlyPublicSurfaceContractsBox_CheckedChanged_1);
      // 
      // RuntimeCheckingLevelDropDown
      // 
      this.RuntimeCheckingLevelDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      resources.ApplyResources(this.RuntimeCheckingLevelDropDown, "RuntimeCheckingLevelDropDown");
      this.RuntimeCheckingLevelDropDown.FormattingEnabled = true;
      this.RuntimeCheckingLevelDropDown.Items.AddRange(new object[] {
            resources.GetString("RuntimeCheckingLevelDropDown.Items"),
            resources.GetString("RuntimeCheckingLevelDropDown.Items1"),
            resources.GetString("RuntimeCheckingLevelDropDown.Items2"),
            resources.GetString("RuntimeCheckingLevelDropDown.Items3"),
            resources.GetString("RuntimeCheckingLevelDropDown.Items4")});
      this.RuntimeCheckingLevelDropDown.Name = "RuntimeCheckingLevelDropDown";
      this.RuntimeCheckingLevelDropDown.SelectedIndexChanged += new System.EventHandler(this.runtimeCheckingLevel_SelectedIndexChanged);
      // 
      // AdvancedGroup
      // 
      this.AdvancedGroup.Controls.Add(this.ExtraCodeAnalysisOptionsBox);
      this.AdvancedGroup.Controls.Add(this.AdditionalCodeAnalysisOptionsLabel);
      this.AdvancedGroup.Controls.Add(this.LibPathLabel);
      this.AdvancedGroup.Controls.Add(this.ExtraRuntimeCheckingOptionsBox);
      this.AdvancedGroup.Controls.Add(this.LibPathTextBox);
      this.AdvancedGroup.Controls.Add(this.ExtraRuntimeCheckerOptionsLabel);
      resources.ApplyResources(this.AdvancedGroup, "AdvancedGroup");
      this.AdvancedGroup.Name = "AdvancedGroup";
      this.AdvancedGroup.TabStop = false;
      // 
      // ExtraCodeAnalysisOptionsBox
      // 
      resources.ApplyResources(this.ExtraCodeAnalysisOptionsBox, "ExtraCodeAnalysisOptionsBox");
      this.ExtraCodeAnalysisOptionsBox.Name = "ExtraCodeAnalysisOptionsBox";
      this.ExtraCodeAnalysisOptionsBox.TextChanged += new System.EventHandler(this.ExtraCodeAnalysisOptionsBox_TextChanged);
      // 
      // AdditionalCodeAnalysisOptionsLabel
      // 
      resources.ApplyResources(this.AdditionalCodeAnalysisOptionsLabel, "AdditionalCodeAnalysisOptionsLabel");
      this.AdditionalCodeAnalysisOptionsLabel.Name = "AdditionalCodeAnalysisOptionsLabel";
      // 
      // StaticCheckingGroup
      // 
      this.StaticCheckingGroup.Controls.Add(this.InferEnsuresAutoPropertiesCheckBox);
      this.StaticCheckingGroup.Controls.Add(this.SuggestCalleeAssumptionsCheckBox);
      this.StaticCheckingGroup.Controls.Add(this.skipAnalysisIfCannotConnectToCache);
      this.StaticCheckingGroup.Controls.Add(this.checkMissingPublicEnsures);
      this.StaticCheckingGroup.Controls.Add(this.linkUnderstandingTheStaticChecker);
      this.StaticCheckingGroup.Controls.Add(this.NecessaryEnsuresCheckBox);
      this.StaticCheckingGroup.Controls.Add(this.AssertsToContractsCheckBox);
      this.StaticCheckingGroup.Controls.Add(this.RedundantTestsCheckBox);
      this.StaticCheckingGroup.Controls.Add(this.SuggestReadonlyCheckBox);
      this.StaticCheckingGroup.Controls.Add(this.BeingOptmisticOnExternalCheckBox);
      this.StaticCheckingGroup.Controls.Add(this.missingPublicRequiresAsWarningsCheckBox);
      this.StaticCheckingGroup.Controls.Add(this.SQLServerLabel);
      this.StaticCheckingGroup.Controls.Add(this.SQLServerTextBox);
      this.StaticCheckingGroup.Controls.Add(this.SuggestAssumptionsCheckBox);
      this.StaticCheckingGroup.Controls.Add(this.InferObjectInvariantsCheckBox);
      this.StaticCheckingGroup.Controls.Add(this.SuggestObjectInvariantsCheckBox);
      this.StaticCheckingGroup.Controls.Add(this.WarningFullLabel);
      this.StaticCheckingGroup.Controls.Add(this.WarningLowLabel);
      this.StaticCheckingGroup.Controls.Add(this.WarningLevelTrackBar);
      this.StaticCheckingGroup.Controls.Add(this.WarningLevelLabel);
      this.StaticCheckingGroup.Controls.Add(this.CacheResultsCheckBox);
      this.StaticCheckingGroup.Controls.Add(this.redundantAssumptionsCheckBox);
      this.StaticCheckingGroup.Controls.Add(this.InferRequiresCheckBox);
      this.StaticCheckingGroup.Controls.Add(this.InferEnsuresCheckBox);
      this.StaticCheckingGroup.Controls.Add(this.SuggestRequiresCheckBox);
      this.StaticCheckingGroup.Controls.Add(this.BaseLineUpdateButton);
      this.StaticCheckingGroup.Controls.Add(this.ImplicitArithmeticObligationsBox);
      this.StaticCheckingGroup.Controls.Add(this.FailBuildOnWarningsCheckBox);
      this.StaticCheckingGroup.Controls.Add(this.ShowSquiggliesBox);
      this.StaticCheckingGroup.Controls.Add(this.RunInBackgroundBox);
      this.StaticCheckingGroup.Controls.Add(this.BaseLineBox);
      this.StaticCheckingGroup.Controls.Add(this.BaseLineTextBox);
      this.StaticCheckingGroup.Controls.Add(this.ImplicitArrayBoundObligationsBox);
      this.StaticCheckingGroup.Controls.Add(this.ImplicitNonNullObligationsBox);
      this.StaticCheckingGroup.Controls.Add(this.ImplicitEnumWritesBox);
      this.StaticCheckingGroup.Controls.Add(this.EnableStaticCheckingBox);
      resources.ApplyResources(this.StaticCheckingGroup, "StaticCheckingGroup");
      this.StaticCheckingGroup.Name = "StaticCheckingGroup";
      this.StaticCheckingGroup.TabStop = false;
      // 
      // SuggestCalleeAssumptionsCheckBox
      // 
      resources.ApplyResources(this.SuggestCalleeAssumptionsCheckBox, "SuggestCalleeAssumptionsCheckBox");
      this.SuggestCalleeAssumptionsCheckBox.Name = "SuggestCalleeAssumptionsCheckBox";
      this.SuggestCalleeAssumptionsCheckBox.UseVisualStyleBackColor = true;
      this.SuggestCalleeAssumptionsCheckBox.CheckedChanged += new System.EventHandler(this.SuggestCalleeAssumptionsCheckBox_CheckedChanged);
      // 
      // skipAnalysisIfCannotConnectToCache
      // 
      resources.ApplyResources(this.skipAnalysisIfCannotConnectToCache, "skipAnalysisIfCannotConnectToCache");
      this.skipAnalysisIfCannotConnectToCache.Name = "skipAnalysisIfCannotConnectToCache";
      this.skipAnalysisIfCannotConnectToCache.UseVisualStyleBackColor = true;
      this.skipAnalysisIfCannotConnectToCache.CheckedChanged += new System.EventHandler(this.skipAnalysisIfCannotConnectToCache_CheckedChanged);
      // 
      // checkMissingPublicEnsures
      // 
      resources.ApplyResources(this.checkMissingPublicEnsures, "checkMissingPublicEnsures");
      this.checkMissingPublicEnsures.Name = "checkMissingPublicEnsures";
      this.checkMissingPublicEnsures.UseVisualStyleBackColor = true;
      this.checkMissingPublicEnsures.CheckedChanged += new System.EventHandler(this.checkMissingPublicEnsures_CheckedChanged);
      // 
      // linkUnderstandingTheStaticChecker
      // 
      resources.ApplyResources(this.linkUnderstandingTheStaticChecker, "linkUnderstandingTheStaticChecker");
      this.linkUnderstandingTheStaticChecker.Name = "linkUnderstandingTheStaticChecker";
      this.linkUnderstandingTheStaticChecker.TabStop = true;
      this.linkUnderstandingTheStaticChecker.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkUnderstandingTheStaticChecker_LinkClicked);
      // 
      // NecessaryEnsuresCheckBox
      // 
      resources.ApplyResources(this.NecessaryEnsuresCheckBox, "NecessaryEnsuresCheckBox");
      this.NecessaryEnsuresCheckBox.Checked = true;
      this.NecessaryEnsuresCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
      this.NecessaryEnsuresCheckBox.Name = "NecessaryEnsuresCheckBox";
      this.NecessaryEnsuresCheckBox.UseVisualStyleBackColor = true;
      this.NecessaryEnsuresCheckBox.CheckedChanged += new System.EventHandler(this.NecessaryEnsuresCheckBox_CheckedChanged);
      // 
      // AssertsToContractsCheckBox
      // 
      resources.ApplyResources(this.AssertsToContractsCheckBox, "AssertsToContractsCheckBox");
      this.AssertsToContractsCheckBox.Checked = true;
      this.AssertsToContractsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
      this.AssertsToContractsCheckBox.Name = "AssertsToContractsCheckBox";
      this.AssertsToContractsCheckBox.UseVisualStyleBackColor = true;
      this.AssertsToContractsCheckBox.CheckedChanged += new System.EventHandler(this.AssertsToContractsCheckBox_CheckedChanged);
      // 
      // RedundantTestsCheckBox
      // 
      resources.ApplyResources(this.RedundantTestsCheckBox, "RedundantTestsCheckBox");
      this.RedundantTestsCheckBox.Checked = true;
      this.RedundantTestsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
      this.RedundantTestsCheckBox.Name = "RedundantTestsCheckBox";
      this.RedundantTestsCheckBox.UseVisualStyleBackColor = true;
      this.RedundantTestsCheckBox.CheckedChanged += new System.EventHandler(this.RedundantTestsCheckBox_CheckedChanged);
      // 
      // SuggestReadonlyCheckBox
      // 
      resources.ApplyResources(this.SuggestReadonlyCheckBox, "SuggestReadonlyCheckBox");
      this.SuggestReadonlyCheckBox.Checked = true;
      this.SuggestReadonlyCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
      this.SuggestReadonlyCheckBox.Name = "SuggestReadonlyCheckBox";
      this.SuggestReadonlyCheckBox.UseVisualStyleBackColor = true;
      // 
      // BeingOptmisticOnExternalCheckBox
      // 
      resources.ApplyResources(this.BeingOptmisticOnExternalCheckBox, "BeingOptmisticOnExternalCheckBox");
      this.BeingOptmisticOnExternalCheckBox.Checked = true;
      this.BeingOptmisticOnExternalCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
      this.BeingOptmisticOnExternalCheckBox.Name = "BeingOptmisticOnExternalCheckBox";
      this.BeingOptmisticOnExternalCheckBox.UseVisualStyleBackColor = true;
      this.BeingOptmisticOnExternalCheckBox.CheckedChanged += new System.EventHandler(this.BeingOptmisticOnExternalCheckBox_CheckedChanged);
      // 
      // missingPublicRequiresAsWarningsCheckBox
      // 
      resources.ApplyResources(this.missingPublicRequiresAsWarningsCheckBox, "missingPublicRequiresAsWarningsCheckBox");
      this.missingPublicRequiresAsWarningsCheckBox.Checked = true;
      this.missingPublicRequiresAsWarningsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
      this.missingPublicRequiresAsWarningsCheckBox.Name = "missingPublicRequiresAsWarningsCheckBox";
      this.missingPublicRequiresAsWarningsCheckBox.UseVisualStyleBackColor = true;
      this.missingPublicRequiresAsWarningsCheckBox.CheckedChanged += new System.EventHandler(this.missingPublicRequiresAsWarningsCheckBox_CheckedChanged);
      // 
      // SQLServerLabel
      // 
      resources.ApplyResources(this.SQLServerLabel, "SQLServerLabel");
      this.SQLServerLabel.Name = "SQLServerLabel";
      // 
      // SQLServerTextBox
      // 
      this.SQLServerTextBox.Enabled = this.CacheResultsCheckBox.Enabled;
      resources.ApplyResources(this.SQLServerTextBox, "SQLServerTextBox");
      this.SQLServerTextBox.Name = "SQLServerTextBox";
      this.SQLServerTextBox.TextChanged += new System.EventHandler(this.SQLServerTextBox_TextChanged);
      // 
      // CacheResultsCheckBox
      // 
      resources.ApplyResources(this.CacheResultsCheckBox, "CacheResultsCheckBox");
      this.CacheResultsCheckBox.Checked = true;
      this.CacheResultsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
      this.CacheResultsCheckBox.Name = "CacheResultsCheckBox";
      this.CacheResultsCheckBox.UseVisualStyleBackColor = true;
      this.CacheResultsCheckBox.CheckedChanged += new System.EventHandler(this.CacheResultsCheckBox_CheckedChanged);
      // 
      // SuggestAssumptionsCheckBox
      // 
      resources.ApplyResources(this.SuggestAssumptionsCheckBox, "SuggestAssumptionsCheckBox");
      this.SuggestAssumptionsCheckBox.Name = "SuggestAssumptionsCheckBox";
      this.SuggestAssumptionsCheckBox.UseVisualStyleBackColor = true;
      this.SuggestAssumptionsCheckBox.CheckedChanged += new System.EventHandler(this.SuggestAssumptions_CheckedChanged);
      // 
      // InferObjectInvariantsCheckBox
      // 
      resources.ApplyResources(this.InferObjectInvariantsCheckBox, "InferObjectInvariantsCheckBox");
      this.InferObjectInvariantsCheckBox.Name = "InferObjectInvariantsCheckBox";
      this.InferObjectInvariantsCheckBox.UseVisualStyleBackColor = true;
      this.InferObjectInvariantsCheckBox.CheckedChanged += new System.EventHandler(this.InferObjectInvariantsCheckBox_CheckedChanged);
      // 
      // SuggestObjectInvariantsCheckBox
      // 
      resources.ApplyResources(this.SuggestObjectInvariantsCheckBox, "SuggestObjectInvariantsCheckBox");
      this.SuggestObjectInvariantsCheckBox.Name = "SuggestObjectInvariantsCheckBox";
      this.SuggestObjectInvariantsCheckBox.UseVisualStyleBackColor = true;
      this.SuggestObjectInvariantsCheckBox.CheckedChanged += new System.EventHandler(this.SuggestObjectInvariantsCheckBox_CheckedChanged);
      // 
      // WarningFullLabel
      // 
      resources.ApplyResources(this.WarningFullLabel, "WarningFullLabel");
      this.WarningFullLabel.Name = "WarningFullLabel";
      // 
      // WarningLowLabel
      // 
      resources.ApplyResources(this.WarningLowLabel, "WarningLowLabel");
      this.WarningLowLabel.Name = "WarningLowLabel";
      // 
      // WarningLevelTrackBar
      // 
      this.WarningLevelTrackBar.LargeChange = 1;
      resources.ApplyResources(this.WarningLevelTrackBar, "WarningLevelTrackBar");
      this.WarningLevelTrackBar.Maximum = 3;
      this.WarningLevelTrackBar.Name = "WarningLevelTrackBar";
      this.WarningLevelTrackBar.Scroll += new System.EventHandler(this.WarningLevelTrackBar_Scroll);
      // 
      // WarningLevelLabel
      // 
      resources.ApplyResources(this.WarningLevelLabel, "WarningLevelLabel");
      this.WarningLevelLabel.Name = "WarningLevelLabel";
      // 
      // redundantAssumptionsCheckBox
      // 
      resources.ApplyResources(this.redundantAssumptionsCheckBox, "redundantAssumptionsCheckBox");
      this.redundantAssumptionsCheckBox.Checked = true;
      this.redundantAssumptionsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
      this.redundantAssumptionsCheckBox.Name = "redundantAssumptionsCheckBox";
      this.redundantAssumptionsCheckBox.UseVisualStyleBackColor = true;
      this.redundantAssumptionsCheckBox.CheckedChanged += new System.EventHandler(this.redundantAssumptionsCheckBox_CheckedChanged);
      // 
      // InferRequiresCheckBox
      // 
      resources.ApplyResources(this.InferRequiresCheckBox, "InferRequiresCheckBox");
      this.InferRequiresCheckBox.Checked = true;
      this.InferRequiresCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
      this.InferRequiresCheckBox.Name = "InferRequiresCheckBox";
      this.InferRequiresCheckBox.UseVisualStyleBackColor = true;
      this.InferRequiresCheckBox.CheckedChanged += new System.EventHandler(this.InferRequires_CheckedChanged);
      // 
      // InferEnsuresCheckBox
      // 
      resources.ApplyResources(this.InferEnsuresCheckBox, "InferEnsuresCheckBox");
      this.InferEnsuresCheckBox.Name = "InferEnsuresCheckBox";
      this.InferEnsuresCheckBox.UseVisualStyleBackColor = true;
      this.InferEnsuresCheckBox.CheckedChanged += new System.EventHandler(this.InferEnsuresCheckBox_CheckedChanged);
      // 
      // SuggestRequiresCheckBox
      // 
      resources.ApplyResources(this.SuggestRequiresCheckBox, "SuggestRequiresCheckBox");
      this.SuggestRequiresCheckBox.Name = "SuggestRequiresCheckBox";
      this.SuggestRequiresCheckBox.UseVisualStyleBackColor = true;
      this.SuggestRequiresCheckBox.CheckedChanged += new System.EventHandler(this.SuggestRequiresCheckBox_CheckedChanged);
      // 
      // BaseLineUpdateButton
      // 
      resources.ApplyResources(this.BaseLineUpdateButton, "BaseLineUpdateButton");
      this.BaseLineUpdateButton.Name = "BaseLineUpdateButton";
      this.BaseLineUpdateButton.UseVisualStyleBackColor = true;
      this.BaseLineUpdateButton.Click += new System.EventHandler(this.updateBaselineButton_Click);
      // 
      // ImplicitArithmeticObligationsBox
      // 
      resources.ApplyResources(this.ImplicitArithmeticObligationsBox, "ImplicitArithmeticObligationsBox");
      this.ImplicitArithmeticObligationsBox.Checked = true;
      this.ImplicitArithmeticObligationsBox.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ImplicitArithmeticObligationsBox.Name = "ImplicitArithmeticObligationsBox";
      this.ImplicitArithmeticObligationsBox.UseVisualStyleBackColor = true;
      this.ImplicitArithmeticObligationsBox.CheckedChanged += new System.EventHandler(this.ImplicitArithmeticObligationsBox_CheckedChanged);
      // 
      // FailBuildOnWarningsCheckBox
      // 
      resources.ApplyResources(this.FailBuildOnWarningsCheckBox, "FailBuildOnWarningsCheckBox");
      this.FailBuildOnWarningsCheckBox.Name = "FailBuildOnWarningsCheckBox";
      this.FailBuildOnWarningsCheckBox.UseVisualStyleBackColor = true;
      this.FailBuildOnWarningsCheckBox.CheckedChanged += new System.EventHandler(this.FailBuildOnWarningsCheckBox_CheckedChanged);
      // 
      // ShowSquiggliesBox
      // 
      resources.ApplyResources(this.ShowSquiggliesBox, "ShowSquiggliesBox");
      this.ShowSquiggliesBox.Checked = true;
      this.ShowSquiggliesBox.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ShowSquiggliesBox.Name = "ShowSquiggliesBox";
      this.ShowSquiggliesBox.UseVisualStyleBackColor = true;
      this.ShowSquiggliesBox.CheckedChanged += new System.EventHandler(this.SquiggliesBox_CheckedChanged);
      // 
      // RunInBackgroundBox
      // 
      resources.ApplyResources(this.RunInBackgroundBox, "RunInBackgroundBox");
      this.RunInBackgroundBox.Checked = true;
      this.RunInBackgroundBox.CheckState = System.Windows.Forms.CheckState.Checked;
      this.RunInBackgroundBox.Name = "RunInBackgroundBox";
      this.RunInBackgroundBox.UseVisualStyleBackColor = true;
      this.RunInBackgroundBox.CheckedChanged += new System.EventHandler(this.RunInBackgroundBox_CheckedChanged);
      // 
      // BaseLineBox
      // 
      resources.ApplyResources(this.BaseLineBox, "BaseLineBox");
      this.BaseLineBox.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolTip;
      this.BaseLineBox.Name = "BaseLineBox";
      this.BaseLineBox.UseVisualStyleBackColor = true;
      this.BaseLineBox.CheckedChanged += new System.EventHandler(this.BaseLineBox_CheckedChanged);
      // 
      // BaseLineTextBox
      // 
      resources.ApplyResources(this.BaseLineTextBox, "BaseLineTextBox");
      this.BaseLineTextBox.Name = "BaseLineTextBox";
      this.BaseLineTextBox.TextChanged += new System.EventHandler(this.BaseLineTextBox_TextChanged);
      // 
      // ImplicitArrayBoundObligationsBox
      // 
      resources.ApplyResources(this.ImplicitArrayBoundObligationsBox, "ImplicitArrayBoundObligationsBox");
      this.ImplicitArrayBoundObligationsBox.Checked = true;
      this.ImplicitArrayBoundObligationsBox.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ImplicitArrayBoundObligationsBox.Name = "ImplicitArrayBoundObligationsBox";
      this.ImplicitArrayBoundObligationsBox.UseVisualStyleBackColor = true;
      this.ImplicitArrayBoundObligationsBox.CheckedChanged += new System.EventHandler(this.ImplicitArrayBoundObligationsBox_CheckedChanged);
      // 
      // ImplicitNonNullObligationsBox
      // 
      resources.ApplyResources(this.ImplicitNonNullObligationsBox, "ImplicitNonNullObligationsBox");
      this.ImplicitNonNullObligationsBox.Checked = true;
      this.ImplicitNonNullObligationsBox.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ImplicitNonNullObligationsBox.Name = "ImplicitNonNullObligationsBox";
      this.ImplicitNonNullObligationsBox.UseVisualStyleBackColor = true;
      this.ImplicitNonNullObligationsBox.CheckedChanged += new System.EventHandler(this.ImplicitNonNullObligationsBox_CheckedChanged);
      // 
      // ImplicitEnumWritesBox
      // 
      resources.ApplyResources(this.ImplicitEnumWritesBox, "ImplicitEnumWritesBox");
      this.ImplicitEnumWritesBox.Checked = true;
      this.ImplicitEnumWritesBox.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ImplicitEnumWritesBox.Name = "ImplicitEnumWritesBox";
      this.ImplicitEnumWritesBox.UseVisualStyleBackColor = true;
      this.ImplicitEnumWritesBox.CheckedChanged += new System.EventHandler(this.ImplicitEnumWritesBox_CheckedChanged);
      // 
      // EmitContractDocumentationCheckBox
      // 
      resources.ApplyResources(this.EmitContractDocumentationCheckBox, "EmitContractDocumentationCheckBox");
      this.EmitContractDocumentationCheckBox.Name = "EmitContractDocumentationCheckBox";
      this.EmitContractDocumentationCheckBox.UseVisualStyleBackColor = true;
      this.EmitContractDocumentationCheckBox.CheckedChanged += new System.EventHandler(this.EmitContractDocumentationCheckBox_CheckedChanged);
      // 
      // ContractReferenceAssemblySelection
      // 
      this.ContractReferenceAssemblySelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.ContractReferenceAssemblySelection.FormattingEnabled = true;
      this.ContractReferenceAssemblySelection.Items.AddRange(new object[] {
            resources.GetString("ContractReferenceAssemblySelection.Items"),
            resources.GetString("ContractReferenceAssemblySelection.Items1"),
            resources.GetString("ContractReferenceAssemblySelection.Items2")});
      resources.ApplyResources(this.ContractReferenceAssemblySelection, "ContractReferenceAssemblySelection");
      this.ContractReferenceAssemblySelection.Name = "ContractReferenceAssemblySelection";
      this.ContractReferenceAssemblySelection.SelectedIndexChanged += new System.EventHandler(this.ContractReferenceAssemblySelection_SelectedIndexChanged);
      // 
      // VersionLabel
      // 
      resources.ApplyResources(this.VersionLabel, "VersionLabel");
      this.VersionLabel.Name = "VersionLabel";
      this.VersionLabel.TabStop = true;
      this.VersionLabel.Click += new System.EventHandler(this.VersionLabel_Click);
      // 
      // contractReferenceAssemblyGroup
      // 
      this.contractReferenceAssemblyGroup.Controls.Add(this.ContractReferenceAssemblySelection);
      this.contractReferenceAssemblyGroup.Controls.Add(this.EmitContractDocumentationCheckBox);
      resources.ApplyResources(this.contractReferenceAssemblyGroup, "contractReferenceAssemblyGroup");
      this.contractReferenceAssemblyGroup.Name = "contractReferenceAssemblyGroup";
      this.contractReferenceAssemblyGroup.TabStop = false;
      // 
      // AssemblyModeLabel
      // 
      resources.ApplyResources(this.AssemblyModeLabel, "AssemblyModeLabel");
      this.AssemblyModeLabel.Name = "AssemblyModeLabel";
      // 
      // AssemblyModeDropDown
      // 
      this.AssemblyModeDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.AssemblyModeDropDown.FormattingEnabled = true;
      this.AssemblyModeDropDown.Items.AddRange(new object[] {
            resources.GetString("AssemblyModeDropDown.Items"),
            resources.GetString("AssemblyModeDropDown.Items1")});
      resources.ApplyResources(this.AssemblyModeDropDown, "AssemblyModeDropDown");
      this.AssemblyModeDropDown.Name = "AssemblyModeDropDown";
      this.AssemblyModeDropDown.SelectedIndexChanged += new System.EventHandler(this.AssemblyModeDropDown_SelectedIndexChanged);
      // 
      // docLink
      // 
      resources.ApplyResources(this.docLink, "docLink");
      this.docLink.Name = "docLink";
      this.docLink.TabStop = true;
      this.docLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.docLink_LinkClicked);
      // 
      // help_Link
      // 
      resources.ApplyResources(this.help_Link, "help_Link");
      this.help_Link.Name = "help_Link";
      this.help_Link.TabStop = true;
      this.help_Link.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.help_Link_LinkClicked);
      // 
      // InferEnsuresAutoPropertiesCheckBox
      // 
      resources.ApplyResources(this.InferEnsuresAutoPropertiesCheckBox, "InferEnsuresAutoPropertiesCheckBox");
      this.InferEnsuresAutoPropertiesCheckBox.Checked = true;
      this.InferEnsuresAutoPropertiesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
      this.InferEnsuresAutoPropertiesCheckBox.Name = "InferEnsuresAutoPropertiesCheckBox";
      this.InferEnsuresAutoPropertiesCheckBox.UseVisualStyleBackColor = true;
      this.InferEnsuresAutoPropertiesCheckBox.CheckedChanged += new System.EventHandler(this.InferEnsuresAutoPropertiesCheckBox_CheckedChanged);
      // 
      // PropertyPane
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.help_Link);
      this.Controls.Add(this.docLink);
      this.Controls.Add(this.AssemblyModeDropDown);
      this.Controls.Add(this.AssemblyModeLabel);
      this.Controls.Add(this.contractReferenceAssemblyGroup);
      this.Controls.Add(this.VersionLabel);
      this.Controls.Add(this.StaticCheckingGroup);
      this.Controls.Add(this.AdvancedGroup);
      this.Controls.Add(this.RuntimeCheckingGroup);
      this.Name = "PropertyPane";
      this.Load += new System.EventHandler(this.PropertyPane_Load);
      this.RuntimeCheckingGroup.ResumeLayout(false);
      this.RuntimeCheckingGroup.PerformLayout();
      this.AdvancedGroup.ResumeLayout(false);
      this.AdvancedGroup.PerformLayout();
      this.StaticCheckingGroup.ResumeLayout(false);
      this.StaticCheckingGroup.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.WarningLevelTrackBar)).EndInit();
      this.contractReferenceAssemblyGroup.ResumeLayout(false);
      this.contractReferenceAssemblyGroup.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.CheckBox EnableRuntimeCheckingBox;
    private System.Windows.Forms.CheckBox EnableStaticCheckingBox;
    private System.Windows.Forms.TextBox LibPathTextBox;
    private System.Windows.Forms.Label LibPathLabel;
    private System.Windows.Forms.Label ExtraRuntimeCheckerOptionsLabel;
    private System.Windows.Forms.TextBox ExtraRuntimeCheckingOptionsBox;
    private System.Windows.Forms.TextBox CustomRewriterMethodsAssemblyTextBox;
    private System.Windows.Forms.Label CustomRewriterMethodsLabel;
    private System.Windows.Forms.Label CustomRewriterMethodsAssemblyLabel;
    private System.Windows.Forms.TextBox CustomRewriterMethodsClassTextBox;
    private System.Windows.Forms.Label CustomRewriterMethodsClassLabel;
    private System.Windows.Forms.GroupBox RuntimeCheckingGroup;
    private System.Windows.Forms.GroupBox AdvancedGroup;
    private System.Windows.Forms.GroupBox StaticCheckingGroup;
    private System.Windows.Forms.CheckBox ImplicitNonNullObligationsBox;
    private System.Windows.Forms.CheckBox ImplicitArrayBoundObligationsBox;
    private System.Windows.Forms.CheckBox ImplicitEnumWritesBox;
    private System.Windows.Forms.TextBox ExtraCodeAnalysisOptionsBox;
    private System.Windows.Forms.Label AdditionalCodeAnalysisOptionsLabel;
    private System.Windows.Forms.TextBox BaseLineTextBox;
    private System.Windows.Forms.CheckBox BaseLineBox;
    private System.Windows.Forms.ComboBox RuntimeCheckingLevelDropDown;
    private System.Windows.Forms.CheckBox RunInBackgroundBox;
    private System.Windows.Forms.CheckBox ShowSquiggliesBox;
    private System.Windows.Forms.CheckBox ImplicitArithmeticObligationsBox;
    private System.Windows.Forms.LinkLabel VersionLabel;
    private System.Windows.Forms.GroupBox contractReferenceAssemblyGroup;
    private System.Windows.Forms.CheckBox OnlyPublicSurfaceContractsBox;
    private System.Windows.Forms.CheckBox AssertOnFailureBox;
    private System.Windows.Forms.CheckBox CallSiteRequiresBox;
    private System.Windows.Forms.Button BaseLineUpdateButton;
    private System.Windows.Forms.CheckBox EmitContractDocumentationCheckBox;
    private System.Windows.Forms.CheckBox redundantAssumptionsCheckBox;
    private System.Windows.Forms.ComboBox ContractReferenceAssemblySelection;
    private System.Windows.Forms.Label AssemblyModeLabel;
    private System.Windows.Forms.ComboBox AssemblyModeDropDown;
    private System.Windows.Forms.CheckBox CacheResultsCheckBox;
    private System.Windows.Forms.Label WarningLevelLabel;
    private System.Windows.Forms.TrackBar WarningLevelTrackBar;
    private System.Windows.Forms.Label WarningFullLabel;
    private System.Windows.Forms.Label WarningLowLabel;
    private System.Windows.Forms.CheckBox SkipQuantifiersBox;
    private System.Windows.Forms.ToolTip PrecisionLevelToolTip;
    private System.Windows.Forms.ToolTip WarningLevelToolTip;
    private System.Windows.Forms.CheckBox InferRequiresCheckBox;
    private System.Windows.Forms.CheckBox SuggestRequiresCheckBox;
    private System.Windows.Forms.CheckBox InferEnsuresCheckBox;
    private System.Windows.Forms.CheckBox InferObjectInvariantsCheckBox;
    private System.Windows.Forms.CheckBox SuggestObjectInvariantsCheckBox;
    private System.Windows.Forms.CheckBox SuggestAssumptionsCheckBox;
    private System.Windows.Forms.LinkLabel docLink;
    private System.Windows.Forms.Label SQLServerLabel;
    private System.Windows.Forms.TextBox SQLServerTextBox;
    private System.Windows.Forms.CheckBox FailBuildOnWarningsCheckBox;
    private System.Windows.Forms.CheckBox missingPublicRequiresAsWarningsCheckBox;
    private System.Windows.Forms.LinkLabel help_Link;
    private System.Windows.Forms.CheckBox BeingOptmisticOnExternalCheckBox;
    private System.Windows.Forms.CheckBox SuggestReadonlyCheckBox;
    private System.Windows.Forms.CheckBox RedundantTestsCheckBox;
    private System.Windows.Forms.CheckBox AssertsToContractsCheckBox;
    private System.Windows.Forms.CheckBox NecessaryEnsuresCheckBox;
    private System.Windows.Forms.LinkLabel linkUnderstandingTheStaticChecker;
    private System.Windows.Forms.CheckBox checkMissingPublicEnsures;
    private System.Windows.Forms.CheckBox skipAnalysisIfCannotConnectToCache;
    private System.Windows.Forms.CheckBox SuggestCalleeAssumptionsCheckBox;
    private System.Windows.Forms.CheckBox InferEnsuresAutoPropertiesCheckBox;
  }
}
