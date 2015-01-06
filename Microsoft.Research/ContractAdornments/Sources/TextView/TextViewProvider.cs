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

using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;
using Microsoft.VisualStudio.Text.Outlining;
using Microsoft.VisualStudio.Utilities;
using Adornments;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Diagnostics;
using System.Windows.Media;

namespace ContractAdornments {
  [Export(typeof(IWpfTextViewCreationListener))]
  [ContentType("csharp")]
  [TextViewRole(PredefinedTextViewRoles.Document)]
  internal sealed class TextViewProvider : IWpfTextViewCreationListener {

    /// <summary>
    /// Export the adornment layer that the contract adornments will be added to.
    /// </summary>
    [Export(typeof(AdornmentLayerDefinition))]
    [Name("InheritanceAdornments")]
    [Order(After = PredefinedAdornmentLayers.Text)]//"Text"
    [TextViewRole(PredefinedTextViewRoles.Document)]
    public AdornmentLayerDefinition inheritanceAdornmentLayer = null;

    /// <summary>
    /// Defines the adornment layer for the scarlet adornment. This layer is ordered 
    /// after the selection layer in the Z-order
    /// </summary>
    [Export(typeof(AdornmentLayerDefinition))]
    [Name("MetadataAdornments")]
    [Order(After = PredefinedAdornmentLayers.Text)]
    [TextViewRole(PredefinedTextViewRoles.Document)]
    public AdornmentLayerDefinition metadataAdornmentLayer = null;

    /// <summary>
    /// Used by AdornmentTrackers to track region collapsed/expanded events
    /// </summary>
    [Import]
    internal IOutliningManagerService OutliningManagerService = null;

    [Import]
    internal IClassificationFormatMapService FormatMapService = null;
    [Import]
    internal IClassificationTypeRegistryService ClassificationTypeRegistryService = null;

    ITextView lastFocus;  // can be null
    VSTextPropertiesProvider VSTextPropertiesProvider {
      get {
        if (vsTextPropertiesProvider == null)
          vsTextPropertiesProvider = new VSTextPropertiesProvider(FormatMapService, ClassificationTypeRegistryService);
        return vsTextPropertiesProvider;
      }
    }
    VSTextPropertiesProvider vsTextPropertiesProvider;

    /// <summary>
    /// When a new text view is created, hook up the various "trackers" (<see cref="TextViewTracker"/>, <see cref="InheritanceTracker"/>, <see cref="QuickInfoTracker"/>).
    /// </summary>
    /// <param name="textView"></param>
    public void TextViewCreated(IWpfTextView textView) {
      Contract.Assume(textView != null);

      if (VSServiceProvider.Current == null || VSServiceProvider.Current.ExtensionHasFailed) {
        //If the VSServiceProvider is not initialize, we can't do anything.
        return;
      }

      VSServiceProvider.Current.Logger.PublicEntry(() => {
        #region Check if textView is valid
        var fileName = textView.GetFileName();
        if (fileName == null) {
          VSServiceProvider.Current.Logger.WriteToLog("Couldn't retrieve file name for current view.");
          return;
        }
        if (!File.Exists(fileName)) {
          VSServiceProvider.Current.Logger.WriteToLog("Couldn't find file for current view.");
          return;
        }
        #endregion
        VSServiceProvider.Current.Logger.WriteToLog("Text view found: " + fileName);
        #region Check if textView is a CSharp file
        var IsCSharpFile = Path.GetExtension(fileName) == ".cs";
        #endregion
        #region Get text view properties
        var vsTextProperties = VSTextPropertiesProvider.GetVSTextProperties(textView);
        #endregion
        var vsProject = VSServiceProvider.Current.GetProjectForFile(fileName);//May be null!
        ProjectTracker projectTracker = null;
        if (vsProject != null) {
          projectTracker = ProjectTracker.GetOrCreateProjectTracker(vsProject);
          if (projectTracker != null)
            textView.Properties.AddProperty(typeof(ProjectTracker), projectTracker);
          else
            VSServiceProvider.Current.Logger.WriteToLog("Warning: Couldn't create a 'ProjectTracker', we won't be able to show inheritance contract information for this text view. Close and reopen the text view to try again!");
        }
        #region Check if textView is an editable code file
        var IsEditableCodeFile = /*IsCSharpFile &&*/textView.Roles.Contains("editable") && projectTracker != null; //TODO: We need a stronger check to see if it is a editable code file
        #endregion
        if (IsEditableCodeFile) {
          textView.GotAggregateFocus += NewFocus;
          var textViewTracker = TextViewTracker.GetOrCreateTextViewTracker(textView, projectTracker, vsTextProperties);
          //if (VSServiceProvider.Current.VSOptionsPage != null && (VSServiceProvider.Current.VSOptionsPage.InheritanceOnMethods || VSServiceProvider.Current.VSOptionsPage.InheritanceOnProperties)) {
          //  //var inheritanceAdornmentManager = AdornmentManager.GetOrCreateAdornmentManager(textView, "InheritanceAdornments", outliningManager, VSServiceProvider.Current.Logger);
          //  //var inheritanceTracker = InheritanceTracker.GetOrCreateAdornmentTracker(textViewTracker);
          //}
          //var quickInfoTracker = QuickInfoTracker.GetOrCreateQuickInfoTracker(textViewTracker); //Disabled for now, unfinished
        }
        #region Check if textView is a metadata file
        var IsMetadataFile = !IsEditableCodeFile && fileName.Contains('$'); //TODO: We need a strong check to see if it is a metadata file
        #endregion
        if (IsMetadataFile) {
          if (lastFocus == null || !lastFocus.Properties.ContainsProperty(typeof(ProjectTracker))) {
            VSServiceProvider.Current.Logger.WriteToLog("Couldn't find project for metadata file.");
          } else {
            var outliningManager = OutliningManagerService.GetOutliningManager(textView);
            if (outliningManager == null)
            {
                VSServiceProvider.Current.Logger.WriteToLog("Couldn't get outlining manager for current view.");
                return;
            }
            projectTracker = lastFocus.Properties.GetProperty<ProjectTracker>(typeof(ProjectTracker));
            textView.Properties.AddProperty(typeof(ProjectTracker), projectTracker);
            textView.GotAggregateFocus += NewFocus;
            if (VSServiceProvider.Current.VSOptionsPage != null && VSServiceProvider.Current.VSOptionsPage.Metadata) {
              var metadataAdornmentManager = AdornmentManager.GetOrCreateAdornmentManager(textView, "MetadataAdornments", outliningManager, VSServiceProvider.Current.Logger);
              var metadataTracker = MetadataTracker.GetOrCreateMetadataTracker(textView, projectTracker, vsTextProperties);
            }
          }
        }
      }, "TextViewCreated");
    }

    void NewFocus(object sender, System.EventArgs e) {
      try {// Public Entry
        lastFocus = sender as ITextView;
      } catch (Exception exn) {
        VSServiceProvider.Current.Logger.PublicEntryException(exn, "NewFocus");
      }
    }
  }
}