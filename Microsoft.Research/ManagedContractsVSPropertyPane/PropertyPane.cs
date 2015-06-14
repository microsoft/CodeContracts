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
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.CodeTools;
using System.IO;
using System.Xml.Linq;
using System.Diagnostics.Contracts;

namespace Microsoft.Contracts.VisualStudio {
  [Guid("6962FC39-92C8-4e9b-9E3E-4A52D08D1D83")]
  public partial class PropertyPane : UserControl, Microsoft.VisualStudio.CodeTools.IPropertyPane {

    readonly ProjectProperty[] properties;

    public PropertyPane()
    {
      InitializeComponent();
      var version = typeof(PropertyPane).Assembly.GetName().Version;
      this.VersionLabel.Text = version.ToString();

      this.properties = new ProjectProperty[]{
        new CheckBoxProperty("CodeContractsEnableRuntimeChecking", this.EnableRuntimeCheckingBox, false),
        new CheckBoxProperty("CodeContractsRuntimeOnlyPublicSurface", this.OnlyPublicSurfaceContractsBox, false),
        new NegatedCheckBoxProperty("CodeContractsRuntimeThrowOnFailure", this.AssertOnFailureBox, false),
        new CheckBoxProperty("CodeContractsRuntimeCallSiteRequires", this.CallSiteRequiresBox, false),
        new CheckBoxProperty("CodeContractsRuntimeSkipQuantifiers", this.SkipQuantifiersBox, false),
        new CheckBoxProperty("CodeContractsRunCodeAnalysis", this.EnableStaticCheckingBox, false),

        new CheckBoxProperty("CodeContractsNonNullObligations", this.ImplicitNonNullObligationsBox, true),
        new CheckBoxProperty("CodeContractsBoundsObligations", this.ImplicitArrayBoundObligationsBox, true),
        new CheckBoxProperty("CodeContractsArithmeticObligations", this.ImplicitArithmeticObligationsBox, true),
        new CheckBoxProperty("CodeContractsEnumObligations", this.ImplicitEnumWritesBox, true),
#if INCLUDE_UNSAFE_ANALYSIS
        // new CheckBoxProperty("CodeContractsPointerObligations", this.ImplicitPointerUseObligationsBox, false),
#endif
        new CheckBoxProperty("CodeContractsRedundantAssumptions", this.redundantAssumptionsCheckBox, true),
        new CheckBoxProperty("CodeContractsAssertsToContractsCheckBox", this.AssertsToContractsCheckBox, true),
        new CheckBoxProperty("CodeContractsRedundantTests", this.RedundantTestsCheckBox, true),
        new CheckBoxProperty("CodeContractsMissingPublicRequiresAsWarnings", this.missingPublicRequiresAsWarningsCheckBox, true),
        new CheckBoxProperty("CodeContractsMissingPublicEnsuresAsWarnings", this.checkMissingPublicEnsures, false),
        new CheckBoxProperty("CodeContractsInferRequires", this.InferRequiresCheckBox, true),
        new CheckBoxProperty("CodeContractsInferEnsures", this.InferEnsuresCheckBox, false),
        new CheckBoxProperty("CodeContractsInferEnsuresAutoProperties", this.InferEnsuresAutoPropertiesCheckBox, true),
        new CheckBoxProperty("CodeContractsInferObjectInvariants", this.InferObjectInvariantsCheckBox, false),
        
        new CheckBoxProperty("CodeContractsSuggestAssumptions", this.SuggestAssumptionsCheckBox, false),
        new CheckBoxProperty("CodeContractsSuggestAssumptionsForCallees", this.SuggestCalleeAssumptionsCheckBox, false),
        new CheckBoxProperty("CodeContractsSuggestRequires", this.SuggestRequiresCheckBox, false),
        //new CheckBoxProperty("CodeContractsSuggestEnsures", this.SuggestEnsuresCheckBox, false),
        new CheckBoxProperty("CodeContractsNecessaryEnsures", this.NecessaryEnsuresCheckBox, true),
        new CheckBoxProperty("CodeContractsSuggestObjectInvariants", this.SuggestObjectInvariantsCheckBox, false),
        new CheckBoxProperty("CodeContractsSuggestReadonly", this.SuggestReadonlyCheckBox, true),
                              
        new CheckBoxProperty("CodeContractsRunInBackground", this.RunInBackgroundBox, true),
        new CheckBoxProperty("CodeContractsShowSquigglies", this.ShowSquiggliesBox, true),
        new CheckBoxProperty("CodeContractsUseBaseLine", this.BaseLineBox, false),
        new CheckBoxProperty("CodeContractsEmitXMLDocs", this.EmitContractDocumentationCheckBox, false),

        new TextBoxProperty("CodeContractsCustomRewriterAssembly", this.CustomRewriterMethodsAssemblyTextBox, ""),
        new TextBoxProperty("CodeContractsCustomRewriterClass", this.CustomRewriterMethodsClassTextBox, ""),

        new TextBoxProperty("CodeContractsLibPaths", this.LibPathTextBox, ""),
        new TextBoxProperty("CodeContractsExtraRewriteOptions", this.ExtraRuntimeCheckingOptionsBox, ""),
        new TextBoxProperty("CodeContractsExtraAnalysisOptions", this.ExtraCodeAnalysisOptionsBox, ""),

        new TextBoxProperty("CodeContractsSQLServerOption", this.SQLServerTextBox, ""),

        new TextBoxProperty("CodeContractsBaseLineFile", this.BaseLineTextBox, ""),
        new CheckBoxProperty("CodeContractsCacheAnalysisResults", this.CacheResultsCheckBox, true),
        new CheckBoxProperty("CodeContractsSkipAnalysisIfCannotConnectToCache", this.skipAnalysisIfCannotConnectToCache, false),
        new CheckBoxProperty("CodeContractsFailBuildOnWarnings", this.FailBuildOnWarningsCheckBox, false),
        new CheckBoxProperty("CodeContractsBeingOptimisticOnExternal", this.BeingOptmisticOnExternalCheckBox, true)
      };
    }

    #region Changed properties
    private Microsoft.VisualStudio.CodeTools.IPropertyPaneHost host; // = null;

    private void PropertiesChanged()
    {
      if (host != null) {
        host.PropertiesChanged();
      }
    }

    private void EnableDisableRuntimeDependentUI()
    {
      // Enable/Disable dependent options
      this.CustomRewriterMethodsAssemblyLabel.Enabled = IsChecked(this.EnableRuntimeCheckingBox.CheckState);
      this.CustomRewriterMethodsClassLabel.Enabled = IsChecked(this.EnableRuntimeCheckingBox.CheckState);
      this.CustomRewriterMethodsAssemblyTextBox.Enabled = IsChecked(this.EnableRuntimeCheckingBox.CheckState);
      this.CustomRewriterMethodsClassTextBox.Enabled = IsChecked(this.EnableRuntimeCheckingBox.CheckState);
      this.RuntimeCheckingLevelDropDown.Enabled = IsChecked(this.EnableRuntimeCheckingBox.CheckState);
      this.OnlyPublicSurfaceContractsBox.Enabled = IsChecked(this.EnableRuntimeCheckingBox.CheckState);
      this.AssertOnFailureBox.Enabled = IsChecked(this.EnableRuntimeCheckingBox.CheckState);
      this.CallSiteRequiresBox.Enabled = IsChecked(this.EnableRuntimeCheckingBox.CheckState);
      this.SkipQuantifiersBox.Enabled = IsChecked(this.EnableRuntimeCheckingBox.CheckState);
    }

    private void EnableDisableStaticDependentUI()
    {
#if EXCLUDE_STATIC_ANALYSIS
      this.EnableStaticCheckingBox.Visible = false;
      this.RunInBackgroundBox.Visible = false;
      this.ShowSquiggliesBox.Visible = false;
      this.ImplicitPointerUseObligationsBox.Visible = false;
      this.ImplicitArrayBoundObligationsBox.Visible = false;
      this.ImplicitArithmeticObligationsBox.Visible = false;
      this.redundantAssumptionsCheckBox.Visible = false;
      this.ImplicitNonNullObligationsBox.Visible = false;
      this.BaseLineBox.Visible = false;
      this.BaseLineTextBox.Visible = false;
      this.WarningLevelTrackBar.Visible = false;
      this.WarningLevelLabel.Visible = false;
      this.StaticCheckingGroup.Text = "Static Checking (not included in this version of CodeContracts. Download the Premium edition.)";
#else

      // Enable/Disable dependent options
      bool enabled = IsChecked(this.EnableStaticCheckingBox.CheckState);

      this.RunInBackgroundBox.Enabled = enabled;
      this.ShowSquiggliesBox.Enabled = enabled;

#if INCLUDE_UNSAFE_ANALYSIS
     //  this.ImplicitPointerUseObligationsBox.Enabled = enabled;
      // this.ImplicitPointerUseObligationsBox.Visible = true;
#else
      // this.ImplicitPointerUseObligationsBox.Visible = false;
#endif
      this.ImplicitArrayBoundObligationsBox.Enabled = enabled;
      this.ImplicitArithmeticObligationsBox.Enabled = enabled;
      this.redundantAssumptionsCheckBox.Enabled = enabled;
      this.ImplicitNonNullObligationsBox.Enabled = enabled;
      this.ImplicitEnumWritesBox.Enabled = enabled;
      this.BaseLineBox.Enabled = enabled;
      this.BaseLineTextBox.Enabled = enabled && IsChecked(this.BaseLineBox.CheckState);
      this.CacheResultsCheckBox.Enabled = enabled;
      this.FailBuildOnWarningsCheckBox.Enabled = enabled;
      this.WarningLevelTrackBar.Enabled = enabled;

      this.InferEnsuresCheckBox.Enabled = enabled;
      this.InferEnsuresAutoPropertiesCheckBox.Enabled = enabled;
      this.InferRequiresCheckBox.Enabled = enabled;
      this.InferObjectInvariantsCheckBox.Enabled = enabled;

      this.SuggestAssumptionsCheckBox.Enabled = enabled;
      //this.SuggestEnsuresCheckBox.Enabled = enabled;
      this.SuggestRequiresCheckBox.Enabled = enabled;
      this.SuggestObjectInvariantsCheckBox.Enabled = enabled;
      this.SuggestReadonlyCheckBox.Enabled = enabled;

      // The labels for the warning level
      this.WarningLevelLabel.Enabled = enabled;
      this.WarningLowLabel.Enabled = enabled;
      this.WarningFullLabel.Enabled = enabled;

      this.SQLServerTextBox.Enabled = enabled;
      this.SQLServerLabel.Enabled = enabled;

      this.missingPublicRequiresAsWarningsCheckBox.Enabled = enabled;
      this.checkMissingPublicEnsures.Enabled = enabled;
      this.BeingOptmisticOnExternalCheckBox.Enabled = enabled;

      this.SuggestCalleeAssumptionsCheckBox.Enabled = enabled;
      this.AssertsToContractsCheckBox.Enabled = enabled;
      this.NecessaryEnsuresCheckBox.Enabled = enabled;
      this.RedundantTestsCheckBox.Enabled = enabled;
#endif
    }

    private void EnableDisableContractReferenceDependentUI()
    {
      // Enable/Disable dependent options
      bool enabled = String.Compare(this.ContractReferenceAssemblySelection.SelectedItem as string, "build", true) == 0;

      this.EmitContractDocumentationCheckBox.Enabled = enabled;
    }

    private void EnableDisableBaseLineUI()
    {
      // Enable/Disable dependent options
      this.BaseLineTextBox.Enabled = IsChecked(this.BaseLineBox.CheckState);
      this.BaseLineUpdateButton.Enabled = IsChecked(this.BaseLineBox.CheckState);
      // add default
      if (this.BaseLineTextBox.Enabled && (this.BaseLineTextBox.Text == null || this.BaseLineTextBox.Text == "") )
      {
        this.BaseLineTextBox.Text = @"..\..\baseline.xml";
      }
    }

    private void EnableDisableBackgroundDependentUI()
    {
      // Enable/Disable dependent options
      this.FailBuildOnWarningsCheckBox.Enabled = IsUnChecked(this.RunInBackgroundBox.CheckState) && IsChecked(this.EnableStaticCheckingBox.CheckState);
    }

    private void EnableDisableCacheDependendUI()
    {
      this.SQLServerTextBox.Enabled = IsChecked(this.CacheResultsCheckBox.CheckState) && IsChecked(this.EnableStaticCheckingBox.CheckState);
      this.SQLServerLabel.Enabled = IsChecked(this.CacheResultsCheckBox.CheckState) && IsChecked(this.EnableStaticCheckingBox.CheckState);
    }



    public void SetHost(Microsoft.VisualStudio.CodeTools.IPropertyPaneHost h)
    {
      host = h;
    }
    #endregion

    #region Attributes
    public string Title
    {
      get { return "Code Contracts"; }
    }

    public int HelpContext
    {
      get { return 0; }
    }

    public string HelpFile
    {
      get { return ""; }
    }
    #endregion

    #region IPropertyPane Members

    [Pure]
    CheckState CheckStateOfBool(bool checkedBool)
    {
      if (checkedBool) { return CheckState.Checked; }
      return CheckState.Unchecked;
    }

    [Pure]
    bool IsChecked(CheckState checkState)
    {
      return checkState == CheckState.Checked;
    }
    [Pure]
    bool IsUnChecked(CheckState checkState)
    {
      return checkState == CheckState.Unchecked;
    }

      
    const string BuildContractReferenceAssemblyName = "CodeContractsBuildReferenceAssembly";
    const string RuntimeCheckingLevelName = "CodeContractsRuntimeCheckingLevel";
    const string ContractReferenceAssemblyName = "CodeContractsReferenceAssembly";
    const string AssemblyModeName = "CodeContractsAssemblyMode";
    const string WarningLevelName = "CodeContractsAnalysisWarningLevel";
    const string PrecisionLevelName = "CodeContractsAnalysisPrecisionLevel";

    /// <summary>
    /// Load project file properties and represent them in the UI.
    /// IMPORTANT:
    ///   The defaults must match what the build script expect as defaults!!! This way, not representing a
    ///   property in the project file is equivalent to it having the default.
    /// </summary>
    /// <param name="configNames"></param>
    /// <param name="storage"></param>
    public void LoadProperties(string[] configNames, IPropertyStorage storage)
    {
      bool propertiesChangedOnLoad = false;

      SetGlobalComboBoxState(storage, AssemblyModeDropDown, AssemblyModeName, "0");

      foreach (var prop in this.properties)
      {
        Contract.Assume(prop != null);

        prop.Load(configNames, storage);
      }

      EnableDisableRuntimeDependentUI();

      var runtimeCheckingLevel = SetComboBoxState(configNames, storage, RuntimeCheckingLevelDropDown, RuntimeCheckingLevelName, "Full");
      if (runtimeCheckingLevel == "RequiresAlways")
      {
        // fixup backwards compatible string
        RuntimeCheckingLevelDropDown.SelectedItem = "ReleaseRequires";
        propertiesChangedOnLoad = true;
      }


      EnableDisableStaticDependentUI();

      EnableDisableBaseLineUI();

      EnableDisableBackgroundDependentUI();

      EnableDisableCacheDependendUI();

      bool anyFound, indeterminate;
      string contractReferenceAssemblySelection = storage.GetProperties(false, configNames, ContractReferenceAssemblyName, "(none)", out anyFound, out indeterminate);

      if (!indeterminate)
      {
        if (!anyFound)
        {
          bool anyFound2, indeterminate2;

          // check for legacy
          var legacyBuildContractReferenceAssembly = storage.GetProperties(false, configNames, BuildContractReferenceAssemblyName, false, out anyFound2, out indeterminate2);
          if (anyFound2 && !indeterminate2)
          {
              if (legacyBuildContractReferenceAssembly)
              {
                  // TODO remove property
                  contractReferenceAssemblySelection = "Build";
                  // make sure properties are changed!!!
                  propertiesChangedOnLoad = true;
              }
              else
              {
                  contractReferenceAssemblySelection = "DoNotBuild";
                  propertiesChangedOnLoad = true;
              }
          }
        }
        this.ContractReferenceAssemblySelection.SelectedItem = contractReferenceAssemblySelection;
      }
      else
      {
        this.ContractReferenceAssemblySelection.SelectedItem = null;
      }

      // Load the track bar properties
      LoadTrackBarProperties(this.WarningLevelTrackBar, WarningLevelName, configNames, storage);

      EnableDisableContractReferenceDependentUI();


      if (propertiesChangedOnLoad)
      {
        this.PropertiesChanged();
        SaveProperties(configNames, storage);
      }

    }

    static private void LoadTrackBarProperties(TrackBar trackBar, string name, string[] configNames, IPropertyStorage storage)
    {
      bool anyFound, indeterminate;
      var selection = storage.GetProperties(false, configNames, name, "out", out anyFound, out indeterminate);
      int value;

      if (!indeterminate && Int32.TryParse(selection, out value)
        && value >= trackBar.Minimum && value <= trackBar.Maximum)
      {
        trackBar.Value = value;
      }
      else
      {
        trackBar.Value = 0; // default: low
      }
    }


    private string SetGlobalComboBoxState(IPropertyStorage storage, ComboBox box, string propertyName, string defaultValue)
    {
      string result = storage.GetProperty(false, "", propertyName, defaultValue) as string;
      int index = 0;
      if (result != null)
      {
        if (!Int32.TryParse(result, out index)) {
          index = 0;
        }
      }
      box.SelectedIndex = index;
      return result;
    }

    private string SetComboBoxState(string[] configNames, IPropertyStorage storage, ComboBox box, string propertyName, string defaultValue)
    {
      string result = storage.GetProperties(false, configNames, propertyName, defaultValue) as string;
      box.SelectedItem = result;
      return result;
    }

    public void SaveProperties(string[] configNames, IPropertyStorage storage)
    {
      Contract.Assume(storage != null);

      foreach (var prop in this.properties)
      {
        Contract.Assume(prop != null);
        prop.Save(configNames, storage);
      }

      if (AssemblyModeDropDown.SelectedItem != null)
      {
        storage.SetProperty(false, "", AssemblyModeName, AssemblyModeDropDown.SelectedIndex.ToString());
      }
      if (RuntimeCheckingLevelDropDown.SelectedItem != null)
      {
        storage.SetProperties(false, configNames, RuntimeCheckingLevelName, RuntimeCheckingLevelDropDown.SelectedItem.ToString());
      }
      if (ContractReferenceAssemblySelection.SelectedItem != null)
      {
        storage.SetProperties(false, configNames, ContractReferenceAssemblyName, ContractReferenceAssemblySelection.SelectedItem.ToString());
      }

      storage.SetProperties(false, configNames, WarningLevelName, WarningLevelTrackBar.Value.ToString());
    }

    #endregion

    #region Event handlers
    private void EnableContractCheckingBox_CheckedChanged(object sender, EventArgs e)
    {
      PropertiesChanged();
      EnableDisableRuntimeDependentUI();
    }

    private void EnableStaticCheckingBox_CheckedChanged(object sender, EventArgs e)
    {
      PropertiesChanged();
      EnableDisableStaticDependentUI();
    }

    private void LibPathTextBox_TextChanged(object sender, EventArgs e)
    {
      PropertiesChanged();
    }

    private void PlatformTextBox_TextChanged(object sender, EventArgs e)
    {
      PropertiesChanged();
    }

    private void CustomRewriterMethodsAssemblyTextBox_TextChanged(object sender, EventArgs e)
    {
      PropertiesChanged();
    }

    private void CustomRewriterMethodsClassTextBox_TextChanged(object sender, EventArgs e)
    {
      PropertiesChanged();
    }

    private void ImplicitNonNullObligationsBox_CheckedChanged(object sender, EventArgs e)
    {
      PropertiesChanged();
    }

    private void ImplicitArrayBoundObligationsBox_CheckedChanged(object sender, EventArgs e)
    {
      PropertiesChanged();
    }

    private void ImplicitPointerUseObligationsBox_CheckedChanged(object sender, EventArgs e)
    {
      PropertiesChanged();
    }

    private void ExtraCodeAnalysisOptionsBox_TextChanged(object sender, EventArgs e)
    {
      PropertiesChanged();
    }

    private void BaseLineTextBox_TextChanged(object sender, EventArgs e)
    {
      PropertiesChanged();
    }

    private void BaseLineBox_CheckedChanged(object sender, EventArgs e)
    {
      PropertiesChanged();
      EnableDisableBaseLineUI();
    }

    private void runtimeCheckingLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
      PropertiesChanged();
    }

    private void RunInBackgroundBox_CheckedChanged(object sender, EventArgs e)
    {
      PropertiesChanged();
      EnableDisableBackgroundDependentUI();
    }

    private void SquiggliesBox_CheckedChanged(object sender, EventArgs e)
    {
      PropertiesChanged();
    }

    private void ImplicitArithmeticObligationsBox_CheckedChanged(object sender, EventArgs e)
    {
      PropertiesChanged();
    }

    private void ImplicitEnumWritesBox_CheckedChanged(object sender, EventArgs e)
    {
      PropertiesChanged();
    }

    private void OnlyPublicSurfaceContractsBox_CheckedChanged_1(object sender, EventArgs e)
    {
      this.PropertiesChanged();
    }

    private void AssertOnFailureBox_CheckedChanged(object sender, EventArgs e)
    {
      this.PropertiesChanged();
    }

    private void CallSiteRequiresBox_CheckedChanged(object sender, EventArgs e)
    {
      this.PropertiesChanged();
    }
    
    private void SkipQuantifiersBox_CheckedChanged(object sender, EventArgs e)
    {
      this.PropertiesChanged();
    }

    private void updateBaselineButton_Click(object sender, EventArgs e)
    {
      try
      {
        var baselineName = BaseLineBox.Text;
        if (!File.Exists(baselineName)) return;
        var diffName = baselineName + ".new";
        if (!File.Exists(diffName)) return;
        Contract.Assert(!string.IsNullOrEmpty(baselineName));
        Contract.Assert(!string.IsNullOrEmpty(diffName));
        XElement baseline = XElement.Load(baselineName);
        XElement diffs = XElement.Load(diffName);
        foreach (var elem in diffs.Elements())
        {
          baseline.Add(elem);
        }
        baseline.Save(baselineName);
        diffs.RemoveAll();
        diffs.Save(diffName);
      }
      catch { }
    }

    private void EmitContractDocumentationCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      this.PropertiesChanged();
    }

    private void redundantAssumptionsCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      this.PropertiesChanged();
    }

    private void ContractReferenceAssemblySelection_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.PropertiesChanged();
      EnableDisableContractReferenceDependentUI();
    }

    private void AssemblyModeDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.PropertiesChanged();
    }

    private void CacheResultsCheckBox_CheckedChanged(object sender, EventArgs e) {
      this.PropertiesChanged();
      EnableDisableCacheDependendUI();
    }

    private void WarningLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.PropertiesChanged();
    }

    private void WarningLevelTrackBar_Scroll(object sender, EventArgs e)
    {
      this.PropertiesChanged();
      this.WarningLevelToolTip.SetToolTip(this.WarningLevelTrackBar, WarningLevelToString(this.WarningLevelTrackBar.Value));
    }

    #endregion

    #region ToolTipHelpers
    static private string WarningLevelToString(int value)
    {
      switch (value)
      {
        case 0:
          return "most relevant warnings";

        case 1:
          return "relevant warnings";
          
        case 2:
          return "more warnings";

        case 3:
          return "all warnings";

        default:
          return value.ToString();
      }
    }

    static private string PrecisionLevelToString(int value)
    {
      switch (value)
      {
        case 0:
          return "fast analysis";

        case 1:
          return "more precise";

        case 2:
          return "full power analysis";

        default:
          return value.ToString();
      }
    }
    #endregion

    private void InferRequires_CheckedChanged(object sender, EventArgs e)
    {
      this.PropertiesChanged();
    }

    private void InferEnsuresCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      this.PropertiesChanged();
    }

    private void SuggestRequiresCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      this.PropertiesChanged();
    }

    private void SuggestEnsuresCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      this.PropertiesChanged();
    }

    private void DisjunctivePreconditionsCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      this.PropertiesChanged();
    }

    private void InferObjectInvariantsCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      this.PropertiesChanged();
    }

    private void SuggestObjectInvariantsCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      this.PropertiesChanged();
    }

    private void SuggestAssumptions_CheckedChanged(object sender, EventArgs e)
    {
      this.PropertiesChanged();
    }

    private void docLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      System.Diagnostics.Process.Start("http://research.microsoft.com/en-us/projects/contracts/userdoc.pdf");
    }

    private void PropertyPane_Load(object sender, EventArgs e)
    {

    }

    private void SQLServerTextBox_TextChanged(object sender, EventArgs e)
    {
      this.PropertiesChanged();
    }

    private void FailBuildOnWarningsCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      this.PropertiesChanged();
    }

    private void missingPublicRequiresAsWarningsCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      this.PropertiesChanged();
    }

    private void help_Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      System.Diagnostics.Process.Start("http://social.msdn.microsoft.com/Forums/en-US/codecontracts/threads");
    }

    private void VersionLabel_Click(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("http://research.microsoft.com/en-us/projects/contracts/releasenotes.aspx");
    }

    private void BeingOptmisticOnExternalCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      this.PropertiesChanged();
    }

    private void RedundantTestsCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      this.PropertiesChanged();
    }

    private void AssertsToContractsCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      this.PropertiesChanged();
    }

    private void NecessaryEnsuresCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      this.PropertiesChanged();
    }

    private void linkUnderstandingTheStaticChecker_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      System.Diagnostics.Process.Start("http://research.microsoft.com/en-us/projects/contracts/cccheck.pdf");
    }

    private void checkMissingPublicEnsures_CheckedChanged(object sender, EventArgs e)
    {
      this.PropertiesChanged();
    }
    private void skipAnalysisIfCannotConnectToCache_CheckedChanged(object sender, EventArgs e)
    {
      this.PropertiesChanged();
    }

    private void SuggestCalleeAssumptionsCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      this.PropertiesChanged();
    }

    private void InferEnsuresAutoPropertiesCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      this.PropertiesChanged();
    }
  }
}
