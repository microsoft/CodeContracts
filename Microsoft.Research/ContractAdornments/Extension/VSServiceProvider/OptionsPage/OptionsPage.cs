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
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using ContractAdornments.Interfaces;
using MSVSIP = Microsoft.VisualStudio.Shell;

namespace ContractAdornments.OptionsPage {
  [Guid(GuidList.guidOptionsPageGeneralString)]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  public class ContractOptionsPage : MSVSIP.DialogPage, IContractOptionsPage
  {
    [Category("Metadata")]
    [Description("Display adornments in metadata files with contract information.")]
    [DefaultValue(true)]
    public bool Metadata { get; set; }

    [Category("Metadata")]
    [Description("Collapse contracts on metdata views by default.")]
    [DefaultValue("false")]
    public bool CollapseMetadataContracts { get; set; }

    [Category("Editor")]
    [Description("Display contracts in Quick Info (triggered when hovering over members) IntelliSense popups.")]
    [DefaultValue(true)]
    public bool QuickInfo { get; set; }
    
    [Category("Editor")]
    [Description("Dispaly contracts in Signature Help (triggered when typing method calls) IntelliSense popups.")]
    [DefaultValue(true)]
    public bool SignatureHelp { get; set; }

    [Category("Logging")]
    [Description("Enable logging that records useful runtime information of the extension.")]
    [DefaultValue(true)]
    public bool Logging { get; set; }

    [Category("Logging")]
    [Description("The output location that the log file will be saved to.")]
    [DefaultValue("")]
    public string OutputPath { get; set; }

    [Category("Contract formatting")]
    [Description("Simplifies the appearance of contracts by replacing strings like Contract.Result<...>() with 'result'.")]
    [DefaultValue(true)]
    public bool SmartFormatting { get; set; }

    [Category("Contract formatting")]
    [Description("Colors contracts in inheritance adornments according to the user's Visual Studio's text editor settings. (When this is disabled, the default color becomes a dim gray.)")]
    [DefaultValue(true)]
    public bool SyntaxColoring { get; set; }

    [Category("Advanced")]
    [Description("Toggle caching for CCI reference building. (Disabling this will reduce preformance)")]
    [DefaultValue(true)]
    public bool Caching { get; set; }

    [Category("Advanced")]
    [Description("Extra semicolon separated paths to search for contract reference assemblies.")]
    [DefaultValue("")]
    public string ContractPaths { get; set; }



    public ContractOptionsPage() {
      ResetSettings();
    }

    public override void ResetSettings() {
      base.ResetSettings();

      Metadata = true;
      QuickInfo = true;
      SignatureHelp = true;
      Logging = false;
      OutputPath = "";
      SmartFormatting = true;
      SyntaxColoring = true;
      Caching = true;

      //Try get the temp dir
      var appDataDir = Environment.GetEnvironmentVariable("APPDATA");
      if (appDataDir != null) {
        var codeContractsDir = Path.Combine(appDataDir, @"Microsoft\CodeContacts");
        if (!Directory.Exists(codeContractsDir))
          Directory.CreateDirectory(codeContractsDir);
        OutputPath = Path.Combine(appDataDir, codeContractsDir);
      }
    }

    protected override void OnActivate(CancelEventArgs e) {
      base.OnActivate(e);
    }
    protected override void OnClosed(EventArgs e) {
    }
    protected override void OnDeactivate(CancelEventArgs e) {
    }
    protected override void OnApply(PageApplyEventArgs e) {
      base.OnApply(e);
    }
  }
}
