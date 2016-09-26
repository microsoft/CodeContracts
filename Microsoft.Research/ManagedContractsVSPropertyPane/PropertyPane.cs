// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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

namespace Microsoft.Contracts.VisualStudio
{
    [Guid("6962FC39-92C8-4e9b-9E3E-4A52D08D1D83")]
    public partial class PropertyPane : UserControl, Microsoft.VisualStudio.CodeTools.IPropertyPane
    {
        private readonly ProjectProperty[] properties;

        public PropertyPane()
        {
            InitializeComponent();
            var version = typeof(PropertyPane).Assembly.GetName().Version;
            VersionLabel.Text = version.ToString();

            properties = new ProjectProperty[]{
        new CheckBoxProperty("CodeContractsEnableRuntimeChecking", EnableRuntimeCheckingBox, false),
        new CheckBoxProperty("CodeContractsRuntimeOnlyPublicSurface", OnlyPublicSurfaceContractsBox, false),
        new NegatedCheckBoxProperty("CodeContractsRuntimeThrowOnFailure", AssertOnFailureBox, false),
        new CheckBoxProperty("CodeContractsRuntimeCallSiteRequires", CallSiteRequiresBox, false),
        new CheckBoxProperty("CodeContractsRuntimeSkipQuantifiers", SkipQuantifiersBox, false),
        new CheckBoxProperty("CodeContractsRunCodeAnalysis", EnableStaticCheckingBox, false),

        new CheckBoxProperty("CodeContractsNonNullObligations", ImplicitNonNullObligationsBox, true),
        new CheckBoxProperty("CodeContractsBoundsObligations", ImplicitArrayBoundObligationsBox, true),
        new CheckBoxProperty("CodeContractsArithmeticObligations", ImplicitArithmeticObligationsBox, true),
        new CheckBoxProperty("CodeContractsEnumObligations", ImplicitEnumWritesBox, true),
#if INCLUDE_UNSAFE_ANALYSIS
        // new CheckBoxProperty("CodeContractsPointerObligations", this.ImplicitPointerUseObligationsBox, false),
#endif
        new CheckBoxProperty("CodeContractsRedundantAssumptions", redundantAssumptionsCheckBox, true),
        new CheckBoxProperty("CodeContractsAssertsToContractsCheckBox", AssertsToContractsCheckBox, true),
        new CheckBoxProperty("CodeContractsRedundantTests", RedundantTestsCheckBox, true),
        new CheckBoxProperty("CodeContractsMissingPublicRequiresAsWarnings", missingPublicRequiresAsWarningsCheckBox, true),
        new CheckBoxProperty("CodeContractsMissingPublicEnsuresAsWarnings", checkMissingPublicEnsures, false),
        new CheckBoxProperty("CodeContractsInferRequires", InferRequiresCheckBox, true),
        new CheckBoxProperty("CodeContractsInferEnsures", InferEnsuresCheckBox, false),
        new CheckBoxProperty("CodeContractsInferEnsuresAutoProperties", InferEnsuresAutoPropertiesCheckBox, true),
        new CheckBoxProperty("CodeContractsInferObjectInvariants", InferObjectInvariantsCheckBox, false),

        new CheckBoxProperty("CodeContractsSuggestAssumptions", SuggestAssumptionsCheckBox, false),
        new CheckBoxProperty("CodeContractsSuggestAssumptionsForCallees", SuggestCalleeAssumptionsCheckBox, false),
        new CheckBoxProperty("CodeContractsSuggestRequires", SuggestRequiresCheckBox, false),
        //new CheckBoxProperty("CodeContractsSuggestEnsures", this.SuggestEnsuresCheckBox, false),
        new CheckBoxProperty("CodeContractsNecessaryEnsures", NecessaryEnsuresCheckBox, true),
        new CheckBoxProperty("CodeContractsSuggestObjectInvariants", SuggestObjectInvariantsCheckBox, false),
        new CheckBoxProperty("CodeContractsSuggestReadonly", SuggestReadonlyCheckBox, true),

        new CheckBoxProperty("CodeContractsRunInBackground", RunInBackgroundBox, true),
        new CheckBoxProperty("CodeContractsShowSquigglies", ShowSquiggliesBox, true),
        new CheckBoxProperty("CodeContractsUseBaseLine", BaseLineBox, false),
        new CheckBoxProperty("CodeContractsEmitXMLDocs", EmitContractDocumentationCheckBox, false),

        new TextBoxProperty("CodeContractsCustomRewriterAssembly", CustomRewriterMethodsAssemblyTextBox, ""),
        new TextBoxProperty("CodeContractsCustomRewriterClass", CustomRewriterMethodsClassTextBox, ""),

        new TextBoxProperty("CodeContractsLibPaths", LibPathTextBox, ""),
        new TextBoxProperty("CodeContractsExtraRewriteOptions", ExtraRuntimeCheckingOptionsBox, ""),
        new TextBoxProperty("CodeContractsExtraAnalysisOptions", ExtraCodeAnalysisOptionsBox, ""),

        new TextBoxProperty("CodeContractsSQLServerOption", SQLServerTextBox, ""),

        new TextBoxProperty("CodeContractsBaseLineFile", BaseLineTextBox, ""),
        new CheckBoxProperty("CodeContractsCacheAnalysisResults", CacheResultsCheckBox, true),
        new CheckBoxProperty("CodeContractsSkipAnalysisIfCannotConnectToCache", skipAnalysisIfCannotConnectToCache, false),
        new CheckBoxProperty("CodeContractsFailBuildOnWarnings", FailBuildOnWarningsCheckBox, false),
        new CheckBoxProperty("CodeContractsBeingOptimisticOnExternal", BeingOptmisticOnExternalCheckBox, true),

        new CheckBoxProperty("CodeContractsDeferCodeAnalysis", DeferAnalysisCheckBox, false)
      };
        }

        #region Changed properties
        private Microsoft.VisualStudio.CodeTools.IPropertyPaneHost host; // = null;

        private void PropertiesChanged()
        {
            if (host != null)
            {
                host.PropertiesChanged();
            }
        }

        private void EnableDisableRuntimeDependentUI()
        {
            // Enable/Disable dependent options
            CustomRewriterMethodsAssemblyLabel.Enabled = IsChecked(EnableRuntimeCheckingBox.CheckState);
            CustomRewriterMethodsClassLabel.Enabled = IsChecked(EnableRuntimeCheckingBox.CheckState);
            CustomRewriterMethodsAssemblyTextBox.Enabled = IsChecked(EnableRuntimeCheckingBox.CheckState);
            CustomRewriterMethodsClassTextBox.Enabled = IsChecked(EnableRuntimeCheckingBox.CheckState);
            RuntimeCheckingLevelDropDown.Enabled = IsChecked(EnableRuntimeCheckingBox.CheckState);
            OnlyPublicSurfaceContractsBox.Enabled = IsChecked(EnableRuntimeCheckingBox.CheckState);
            AssertOnFailureBox.Enabled = IsChecked(EnableRuntimeCheckingBox.CheckState);
            CallSiteRequiresBox.Enabled = IsChecked(EnableRuntimeCheckingBox.CheckState);
            SkipQuantifiersBox.Enabled = IsChecked(EnableRuntimeCheckingBox.CheckState);
        }

        private void EnableDisableStaticDependentUI()
        {
#if EXCLUDE_STATIC_ANALYSIS
            EnableStaticCheckingBox.Visible = false;
            RunInBackgroundBox.Visible = false;
            ShowSquiggliesBox.Visible = false;
            this.ImplicitPointerUseObligationsBox.Visible = false;
            ImplicitArrayBoundObligationsBox.Visible = false;
            ImplicitArithmeticObligationsBox.Visible = false;
            redundantAssumptionsCheckBox.Visible = false;
            ImplicitNonNullObligationsBox.Visible = false;
            BaseLineBox.Visible = false;
            BaseLineTextBox.Visible = false;
            WarningLevelTrackBar.Visible = false;
            WarningLevelLabel.Visible = false;
            StaticCheckingGroup.Text = "Static Checking (not included in this version of CodeContracts. Download the Premium edition.)";
#else

            // Enable/Disable dependent options
            bool enabled = IsChecked(EnableStaticCheckingBox.CheckState);

            RunInBackgroundBox.Enabled = enabled;
            ShowSquiggliesBox.Enabled = enabled;

#if INCLUDE_UNSAFE_ANALYSIS
            //  this.ImplicitPointerUseObligationsBox.Enabled = enabled;
            // this.ImplicitPointerUseObligationsBox.Visible = true;
#else
            // this.ImplicitPointerUseObligationsBox.Visible = false;
#endif
            ImplicitArrayBoundObligationsBox.Enabled = enabled;
            ImplicitArithmeticObligationsBox.Enabled = enabled;
            redundantAssumptionsCheckBox.Enabled = enabled;
            ImplicitNonNullObligationsBox.Enabled = enabled;
            ImplicitEnumWritesBox.Enabled = enabled;
            BaseLineBox.Enabled = enabled;
            BaseLineTextBox.Enabled = enabled && IsChecked(BaseLineBox.CheckState);
            CacheResultsCheckBox.Enabled = enabled;
            FailBuildOnWarningsCheckBox.Enabled = enabled;
            WarningLevelTrackBar.Enabled = enabled;

            InferEnsuresCheckBox.Enabled = enabled;
            InferEnsuresAutoPropertiesCheckBox.Enabled = enabled;
            InferRequiresCheckBox.Enabled = enabled;
            InferObjectInvariantsCheckBox.Enabled = enabled;

            SuggestAssumptionsCheckBox.Enabled = enabled;
            //this.SuggestEnsuresCheckBox.Enabled = enabled;
            SuggestRequiresCheckBox.Enabled = enabled;
            SuggestObjectInvariantsCheckBox.Enabled = enabled;
            SuggestReadonlyCheckBox.Enabled = enabled;

            // The labels for the warning level
            WarningLevelLabel.Enabled = enabled;
            WarningLowLabel.Enabled = enabled;
            WarningFullLabel.Enabled = enabled;

            SQLServerTextBox.Enabled = enabled;
            SQLServerLabel.Enabled = enabled;

            missingPublicRequiresAsWarningsCheckBox.Enabled = enabled;
            checkMissingPublicEnsures.Enabled = enabled;
            BeingOptmisticOnExternalCheckBox.Enabled = enabled;

            SuggestCalleeAssumptionsCheckBox.Enabled = enabled;
            AssertsToContractsCheckBox.Enabled = enabled;
            NecessaryEnsuresCheckBox.Enabled = enabled;
            RedundantTestsCheckBox.Enabled = enabled;

            DeferAnalysisCheckBox.Enabled = enabled;
#endif
        }

        private void EnableDisableContractReferenceDependentUI()
        {
            // Enable/Disable dependent options
            bool enabled = String.Compare(ContractReferenceAssemblySelection.SelectedItem as string, "build", true) == 0;

            EmitContractDocumentationCheckBox.Enabled = enabled;
        }

        private void EnableDisableBaseLineUI()
        {
            // Enable/Disable dependent options
            BaseLineTextBox.Enabled = IsChecked(BaseLineBox.CheckState);
            BaseLineUpdateButton.Enabled = IsChecked(BaseLineBox.CheckState);
            // add default
            if (BaseLineTextBox.Enabled && (BaseLineTextBox.Text == null || BaseLineTextBox.Text == ""))
            {
                BaseLineTextBox.Text = @"..\..\baseline.xml";
            }
        }

        private void EnableDisableBackgroundDependentUI()
        {
            // Enable/Disable dependent options
            FailBuildOnWarningsCheckBox.Enabled = IsUnChecked(RunInBackgroundBox.CheckState) && IsChecked(EnableStaticCheckingBox.CheckState) && IsUnChecked(DeferAnalysisCheckBox.CheckState);
        }

		private void EnableDisableDeferDependentUI()
		{
			RunInBackgroundBox.Enabled = IsUnChecked(DeferAnalysisCheckBox.CheckState) && IsChecked(EnableStaticCheckingBox.CheckState);
			EnableDisableBackgroundDependentUI();
		}

        private void EnableDisableCacheDependendUI()
        {
            SQLServerTextBox.Enabled = IsChecked(CacheResultsCheckBox.CheckState) && IsChecked(EnableStaticCheckingBox.CheckState);
            SQLServerLabel.Enabled = IsChecked(CacheResultsCheckBox.CheckState) && IsChecked(EnableStaticCheckingBox.CheckState);
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
        private CheckState CheckStateOfBool(bool checkedBool)
        {
            if (checkedBool) { return CheckState.Checked; }
            return CheckState.Unchecked;
        }

        [Pure]
        private bool IsChecked(CheckState checkState)
        {
            return checkState == CheckState.Checked;
        }
        [Pure]
        private bool IsUnChecked(CheckState checkState)
        {
            return checkState == CheckState.Unchecked;
        }


        private const string BuildContractReferenceAssemblyName = "CodeContractsBuildReferenceAssembly";
        private const string RuntimeCheckingLevelName = "CodeContractsRuntimeCheckingLevel";
        private const string ContractReferenceAssemblyName = "CodeContractsReferenceAssembly";
        private const string AssemblyModeName = "CodeContractsAssemblyMode";
        private const string WarningLevelName = "CodeContractsAnalysisWarningLevel";
        private const string PrecisionLevelName = "CodeContractsAnalysisPrecisionLevel";

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

            foreach (var prop in properties)
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

            EnableDisableDeferDependentUI();
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
                ContractReferenceAssemblySelection.SelectedItem = contractReferenceAssemblySelection;
            }
            else
            {
                ContractReferenceAssemblySelection.SelectedItem = null;
            }

            // Load the track bar properties
            LoadTrackBarProperties(WarningLevelTrackBar, WarningLevelName, configNames, storage);

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
                if (!Int32.TryParse(result, out index))
                {
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

            foreach (var prop in properties)
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
			EnableDisableDeferDependentUI();
			EnableDisableBackgroundDependentUI();
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

        private void CacheResultsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
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
            WarningLevelToolTip.SetToolTip(WarningLevelTrackBar, WarningLevelToString(WarningLevelTrackBar.Value));
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

        private void DeferAnalysisCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.PropertiesChanged();
			EnableDisableDeferDependentUI();
        }
    }
}
