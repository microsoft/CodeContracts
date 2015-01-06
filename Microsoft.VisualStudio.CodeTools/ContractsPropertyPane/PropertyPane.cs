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
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualStudio.CodeTools;

namespace Microsoft.VisualStudio.Contracts
{
  [Guid("A6911A01-8795-4fe7-8E51-2904E8B5F27B")]
  public partial class PropertyPane : UserControl
                                   , Microsoft.VisualStudio.CodeTools.IPropertyPane
  {
    #region Constructor
    public PropertyPane()
    {
      InitializeComponent();  // Initialize the "UserControl"
    }
    #endregion

    #region Changed properties
    private Microsoft.VisualStudio.CodeTools.IPropertyPaneHost host; // = null;

    private void PropertiesChanged()
    {
      if (host != null) {
        host.PropertiesChanged();
      }
    }

    public void SetHost( Microsoft.VisualStudio.CodeTools.IPropertyPaneHost h )
    {
      host = h;
    }
    #endregion

    #region Attributes
    public string Title
    {
      get { return "Contracts"; }
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

    #region Property storage
    // Initialize the UI from the property storage
    public void LoadProperties(string[] configNames, IPropertyStorage storage)
    {
      this.useContractsCheck.CheckState = CheckState.Unchecked;
      this.produceContractAssemblyCheck.CheckState = CheckState.Unchecked;
      this.useVerifier.CheckState = CheckState.Unchecked;
      this.publicPreconditions.CheckState = CheckState.Unchecked;
      this.allOtherPreAndPostConditions.CheckState = CheckState.Unchecked;
      this.assumes.CheckState = CheckState.Unchecked;
      this.invariants.CheckState = CheckState.Unchecked;
      this.publicContracts.CheckState = CheckState.Unchecked;
      this.allOtherContracts.CheckState = CheckState.Unchecked;
      this.nonNullByDefault.CheckState = CheckState.Unchecked;
      this.checkContracts.CheckState = CheckState.Unchecked;
      this.checkPurity.CheckState = CheckState.Unchecked;

      // contracts checkbox.
      object propUseContracts = storage.GetProperties(true, configNames, "UseContracts", false);
      if (propUseContracts != null){
        this.useContractsCheck.Checked = (bool)propUseContracts;
        this.publicPreconditions.Checked = true;
        this.allOtherPreAndPostConditions.Checked = true;
        this.assumes.Checked = true;
        this.invariants.Checked = true;
        this.publicContracts.Checked = true;
        this.allOtherContracts.Checked = true;
        this.checkContracts.Checked = true;
      }

      // produce contract assembly checkbox
      object produceContractAssembly = storage.GetProperties(true, configNames, "ProduceContractAssembly", false);
      if (produceContractAssembly != null)
        this.produceContractAssemblyCheck.Checked = (bool)produceContractAssembly;

      // verifier checkbox.
      object propUseVerifier = storage.GetProperties(true, configNames, "Verify", false);
      if (propUseVerifier != null)
        this.useVerifier.Checked = (bool)propUseVerifier;

      // verifier options
      object propVerifierOptions = storage.GetProperties(true, configNames, "ProgramVerifierCommandLineOptions", "");
      if (propVerifierOptions != null)
        this.verifyopts.Text = (string)propVerifierOptions;

      //non null default checkbox
      object referenceTypesAreNonNullByDefault = storage.GetProperties(true, configNames, "ReferenceTypesAreNonNullByDefault", false);
      if (referenceTypesAreNonNullByDefault != null)
        this.nonNullByDefault.Checked = (bool)referenceTypesAreNonNullByDefault;

      //contract admissibility checkbox
      object checkAdmissibility = storage.GetProperties(true, configNames, "CheckContractAdmissibility", true);
      if (checkAdmissibility != null)
        this.checkContracts.Checked = (bool)checkAdmissibility;

      //method purity checkbox
      object checkPurity = storage.GetProperties(true, configNames, "CheckPurity", false);
      if (checkPurity != null)
        this.checkPurity.Checked = (bool)checkPurity;

      // other checkboxes
      string disableString = storage.GetProperties(true, configNames, "DisabledContractFeatures", "") as string;
      if (disableString != null){
        string[] disableList = disableString.Split(';');
        foreach (string ds in disableList){
          switch (ds){
            case "ac":
            case "assumechecks":
              this.assumes.Checked = false;
              break;
            case "dc":
            case "defensivechecks":
              this.publicPreconditions.Checked = false;
              break;
            case "gcc":
            case "guardedclasseschecks":
              this.invariants.Checked = false;
              break;
            case "ic":
            case "internalchecks":
              this.allOtherPreAndPostConditions.Checked = false;
              break;
            case "icm":
            case "internalcontractsmetadata":
              this.allOtherContracts.Checked = false;
              break;
            case "pcm":
            case "publiccontractsmetadata":
              this.publicContracts.Checked = false;
              break;
          }
        }
      }
    }

    // Save the UI settings to the property storage
    public void SaveProperties(string[] configNames, IPropertyStorage storage)
    {
      storage.SetProperties(true, configNames, "UseContracts", this.useContractsCheck.Checked);
      storage.SetProperties(true, configNames, "ProduceContractAssembly", this.produceContractAssemblyCheck.Checked);
      storage.SetProperties(true, configNames, "Verify", this.useVerifier.Checked);
      storage.SetProperties(true, configNames, "ReferenceTypesAreNonNullByDefault", this.nonNullByDefault.Checked);
      storage.SetProperties(true, configNames, "ProgramVerifierCommandLineOptions", this.verifyopts.Text);
      storage.SetProperties(true, configNames, "DisabledContractFeatures", this.GetDisabledList());
      storage.SetProperties(true, configNames, "CheckContractAdmissibility", this.checkContracts.Checked);
      storage.SetProperties(true, configNames, "CheckPurity", this.checkPurity.Checked);
    }
    
    private string GetDisabledList(){
      System.Text.StringBuilder sb = new System.Text.StringBuilder();
      if (!this.assumes.Checked) 
        sb.Append("assumechecks");
      if (!this.publicPreconditions.Checked){
        if (sb.Length > 0) sb.Append(';');
        sb.Append("defensivechecks");
      }
      if (!this.invariants.Checked) {
        if (sb.Length > 0) sb.Append(';');
        sb.Append("guardedclasseschecks");
      }
      if (!this.allOtherPreAndPostConditions.Checked) {
        if (sb.Length > 0) sb.Append(';');
        sb.Append("internalchecks");
      }
      if (!this.allOtherContracts.Checked) {
        if (sb.Length > 0) sb.Append(';');
        sb.Append("internalcontractsmetadata");
      }
      if (!this.publicContracts.Checked) {
        if (sb.Length > 0) sb.Append(';');
        sb.Append("publiccontractsmetadata");
      }
      return sb.ToString();
    }
    #endregion

    #region Events
    // track changes to UI
    private void useContractsCheck_CheckedChanged(object sender, EventArgs e)
    {
      this.produceContractAssemblyCheck.Enabled = useContractsCheck.Checked;
      this.compileTimeChecks.Enabled = useContractsCheck.Checked;
      this.customAttributes.Enabled = useContractsCheck.Checked;
      this.runtimeChecks.Enabled = useContractsCheck.Checked;
      PropertiesChanged();
    }

    private void produceContractAssemblyCheck_CheckedChanged(object sender, EventArgs e) {
      PropertiesChanged();
    }

    private void useVerifier_CheckedChanged(object sender, EventArgs e) {
      if (this.useVerifier.Checked) {
        this.allOtherContracts.Checked = true;
        this.publicContracts.Checked = true;
      }
      this.verifyopts.Enabled = this.useVerifier.Checked;
      PropertiesChanged();
    }

    public void verifyopts_TextChanged(object sender, EventArgs e) {
      PropertiesChanged();
    }

    private void PublicPreconditions_CheckedChanged(object sender, EventArgs e) {
      PropertiesChanged();
    }

    private void allOtherPreAndPostConditions_CheckedChanged(object sender, EventArgs e) {
      PropertiesChanged();
    }

    private void assumes_CheckedChanged(object sender, EventArgs e) {
      PropertiesChanged();
    }

    private void invariants_CheckedChanged(object sender, EventArgs e) {
      PropertiesChanged();
    }

    private void publicContracts_CheckedChanged(object sender, EventArgs e) {
      if (!this.publicContracts.Checked)
        this.useVerifier.Checked = false;
      PropertiesChanged();
    }

    private void allOtherContracts_CheckedChanged(object sender, EventArgs e) {
      if (!this.allOtherContracts.Checked)
        this.useVerifier.Checked = false;
      PropertiesChanged();
    }

    private void NonNullByDefault_CheckedChanged(object sender, EventArgs e) {
      PropertiesChanged();
    }
    private void CheckContractAdmissibility_CheckedChanged(object sender, EventArgs e) {
      PropertiesChanged();
    }
    private void CheckPurity_CheckedChanged(object sender, EventArgs e) {
      PropertiesChanged();
    }

    private void verifyoptsLabel_Click(object sender, EventArgs e)
    {
    }

    private void PropertyPane_Load(object sender, EventArgs e)
    {
    }
    #endregion

  }
}
