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

using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;
using Microsoft.VisualStudio.Utilities;
using Adornments;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.Text.Outlining;
using System;
using System.Diagnostics.Contracts;
using UtilitiesNamespace;

namespace ContractAdornments {
  /// <summary>
  /// Provides a line transform source so that line transforms can be properly calcualted for contract adornments.
  /// </summary>
  [Export(typeof(ILineTransformSourceProvider))]
  [ContentType("code")]
  [TextViewRole(PredefinedTextViewRoles.Document)]
  internal sealed class LineTransformSourceProvider : ILineTransformSourceProvider {

    /// <summary>
    /// Used by AdornmentManagers to track region collapsed/expanded events
    /// </summary>
    [Import]
    internal IOutliningManagerService OutliningManagerService = null;

    public ILineTransformSource Create(IWpfTextView textView) {
      Contract.Assume(textView != null);

      if (VSServiceProvider.Current == null || VSServiceProvider.Current.ExtensionHasFailed) {
        //If the VSServiceProvider is not initialize, we can't do anything.
        return null;// new DummyLineTransformSource();
      }

      try {
        VSServiceProvider.Current.ExtensionFailed += OnFailed;

        if (hasFailed) return null;

        Contract.Assume(this.OutliningManagerService != null, "Import attribute guarantees this.");

        var outliningManager = OutliningManagerService.GetOutliningManager(textView);
        if (outliningManager == null)
          return null;//new DummyLineTransformSource();

        var inheritanceManager = AdornmentManager.GetOrCreateAdornmentManager(textView, "InheritanceAdornments", outliningManager, VSServiceProvider.Current.Logger);
        var metadataManager = AdornmentManager.GetOrCreateAdornmentManager(textView, "MetadataAdornments", outliningManager, VSServiceProvider.Current.Logger);

        return new LineTransformSource(VSServiceProvider.Current.Logger, inheritanceManager.Adornments.Values, metadataManager.Adornments.Values);
      } catch (Exception exn) {
        VSServiceProvider.Current.Logger.PublicEntryException(exn, "Create");
        return null;// new DummyLineTransformSource();
      }
    }

    bool hasFailed;
    void OnFailed() {
      hasFailed = true;
    }
  }
  /// <summary>
  /// A custom line transform source that transforms lines to fit adornments as needed.
  /// </summary>
  class LineTransformSource : ILineTransformSource {
    readonly IEnumerable<IAdornment>[] adornments;
    double? lineHeight;
    readonly Logger _logger;
    bool hasFailed;

    [ContractInvariantMethod]
    void ObjectInvariant() {
      Contract.Invariant(_logger != null);
      Contract.Invariant(adornments != null);
    }

    public LineTransformSource(Logger logger, params IEnumerable<IAdornment>[] adornments) {
      Contract.Requires(logger != null);
      Contract.Requires(adornments != null);

      _logger = logger;
      logger.Failed += OnFailed;
      this.adornments = adornments;
    }

    void OnFailed() {
      hasFailed = true;
    }
    
    public LineTransform GetLineTransform(ITextViewLine line, double yPosition, ViewRelativePosition placement) {
      Contract.Assume(line != null, "interface we don't control");

      try {
        if (hasFailed)
          return new LineTransform(0, 0, 1);
        if (lineHeight == null)
          lineHeight = line.Height;
        foreach (IEnumerable<IAdornment> adornmentsGroup in adornments)
        {
          if (adornmentsGroup == null) continue;
          foreach (IAdornment adornment in adornmentsGroup)
          {
            if (adornment == null) continue;
            if (adornment.LineTransformBehavior == LineTransformBehavior.None)
              continue;
            if (line.ContainsBufferPosition(new SnapshotPoint(line.Snapshot, adornment.Span.GetStartPoint(line.Snapshot).Position)))
            {
              if (adornment.LineTransformBehavior == LineTransformBehavior.Above)
                return new LineTransform(adornment.Visual.DesiredSize.Height, 0d, 1d);
              else if (adornment.LineTransformBehavior == LineTransformBehavior.Below)
                return new LineTransform(0d, adornment.Visual.DesiredSize.Height, 1d);
              else if (adornment.LineTransformBehavior == LineTransformBehavior.BelowWithOneLineAbove)
              {
                return new LineTransform(lineHeight.Value, adornment.Visual.DesiredSize.Height, 1d);
              }
            }
          }
        }
        return new LineTransform(0, 0, 1);
      } catch (Exception exn) {
        _logger.PublicEntryException(exn, "GetLineTransform");
        return new LineTransform(0, 0, 1);
      }
    }    
  }

  //class DummyLineTransformSource : ILineTransformSource {
  //  public LineTransform GetLineTransform(ITextViewLine line, double yPosition, ViewRelativePosition placement) {
  //    return new LineTransform();
  //  }
  //}
}
