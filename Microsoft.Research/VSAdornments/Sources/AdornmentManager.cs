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
using System.Diagnostics.Contracts;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;
using Microsoft.VisualStudio.Text.Outlining;
using Microsoft.Cci;
using UtilitiesNamespace;

namespace Adornments {
  /// <summary>
  /// Used to manager adornments on a particular text view's adornment layer (<see cref="IAdornmentLayer"/>).
  /// </summary>
  public class AdornmentManager {
    readonly IWpfTextView _textView;
    readonly IAdornmentLayer _layer;
    readonly IOutliningManager _outliningManager;
    /// <summary>
    /// A collection of adornments, indexed by a special "Tag" object, that should be rendered in this's adornment layer.
    /// </summary>
    readonly IDictionary<object, IAdornment> _adornments = new Dictionary<object, IAdornment>();
    public IDictionary<object, IAdornment> Adornments
    {
      get
      {
        Contract.Ensures(Contract.Result<IDictionary<object, IAdornment>>() != null);
        return this._adornments;
      }
    }
    readonly IList<IStaticAdornment> _staticAdornments = new List<IStaticAdornment>();
    ///// <summary>
    ///// An array of "Adorments"'s keys sorted by appearance in text view.
    ///// </summary>
    //public object[] SortedAdornmentKeys;
#if TRACKING_REMOVED_ADORNMENTS
    bool _adornmentWasRemoved = false;
#endif
    /// <summary>
    /// Checks to see if this AdornmentManager has an adornment for the method specified by a given "Tag".
    /// </summary>
    public bool ContainsAdornment(object tag) {
      return Adornments.ContainsKey(tag);
    }
    bool _newStaticAdornments = false;
    //KeyValuePair<object, IAdornment>? currentCaretAdornment = null;
    readonly Logger _logger;
    //bool _shouldRefreshLineTransformer = false;

    [ContractInvariantMethod]
    void ObjectInvariant() {
      Contract.Invariant(_textView != null);
      Contract.Invariant(_logger != null);
      Contract.Invariant(_adornments != null);
    }

    /// <summary>
    /// Gets or creates an adornment manager for a particular text view.
    /// </summary>
    public static AdornmentManager GetOrCreateAdornmentManager(IWpfTextView textView, string adornmentLayer, IOutliningManager outliningManager, Logger logger) {
      Contract.Requires(textView != null);
      Contract.Requires(!String.IsNullOrEmpty(adornmentLayer));
      Contract.Requires(outliningManager != null);
      Contract.Requires(logger != null);
      Contract.Ensures(Contract.Result<AdornmentManager>() != null);
      return textView.Properties.GetOrCreateSingletonProperty<AdornmentManager>(adornmentLayer, delegate { return new AdornmentManager(textView, adornmentLayer, outliningManager, logger); });
    }

    /// <summary>
    /// Tries to get an adornment manager for a particular text view.
    /// </summary>
    public static bool TryGetAdornmentManager(IWpfTextView textView, string adornmentLayer, out AdornmentManager adornmentManager) {
      Contract.Requires(textView != null);
      
      return textView.Properties.TryGetProperty<AdornmentManager>(adornmentLayer, out adornmentManager);
    }

    /// <summary>
    /// Initializes a new AdornmentManger.
    /// </summary>
    /// <remarks>
    /// Use <see cref="AdornmentManager.GetOrCreateAdornmentManager"/> to create a new AdornmentManager.
    /// </remarks>
    private AdornmentManager(IWpfTextView textView, string adornmentLayer, IOutliningManager outliningManager, Logger logger) {
      Contract.Requires(textView != null);
      Contract.Requires(!String.IsNullOrEmpty(adornmentLayer));
      Contract.Requires(outliningManager != null);
      Contract.Requires(logger != null);

      _logger = logger;
      _logger.Failed += OnFailed;
      _textView = textView;
      _layer = textView.GetAdornmentLayer(adornmentLayer);
      _textView.LayoutChanged += OnLayoutChanged;
      _textView.Closed += OnTextViewClosed;
      _textView.ViewportHeightChanged += UpdateStaticAdornments;
      _textView.ViewportWidthChanged += UpdateStaticAdornments;
      //_textView.Caret.PositionChanged += OnCaretMoved;
      //textView.MouseHover += OnMouseHover;
      _outliningManager = outliningManager;
      _outliningManager.RegionsCollapsed += OnRegionsCollapsed;
      _outliningManager.RegionsExpanded += OnRegionsExpanded;
    }

    void OnFailed() {
      _logger.Failed -= OnFailed;
      UnsubscribeFromAllEvents();
    }

    public void QueueRefreshLineTransformer() {
      _logger.QueueWorkItem(_textView.RefreshLineTransformer);
    }
    
    void UpdateStaticAdornments(object sender, EventArgs e) {
      foreach (var adornment in _staticAdornments) {
        _layer.RemoveAdornment(adornment.Visual);
        if (adornment.OffsetLeft) {
          Canvas.SetLeft(adornment.Visual, adornment.OffsetWidth);
        } else {
          Canvas.SetLeft(adornment.Visual, _textView.ViewportRight - adornment.OffsetWidth - adornment.Visual.ActualWidth);
        }
        if (adornment.OffsetTop) {
          Canvas.SetTop(adornment.Visual, _textView.ViewportTop + adornment.OffsetHeight);
        } else {
          Canvas.SetTop(adornment.Visual, _textView.ViewportBottom - adornment.OffsetWidth - adornment.Visual.ActualHeight);
        }
        _layer.AddAdornment(AdornmentPositioningBehavior.ViewportRelative, null, null, adornment.Visual, null);
      }
    }

    void OnTextViewClosed(object sender, EventArgs e) {
      _logger.PublicEntry(() => {
        UnsubscribeFromAllEvents();
      }, "OnTextViewClosed");
    }
    void UnsubscribeFromAllEvents() {
      _textView.LayoutChanged -= OnLayoutChanged;
      _textView.Closed -= OnTextViewClosed;
      _textView.ViewportHeightChanged -= UpdateStaticAdornments;
      _textView.ViewportWidthChanged -= UpdateStaticAdornments;
      _outliningManager.RegionsCollapsed -= OnRegionsCollapsed;
      _outliningManager.RegionsExpanded -= OnRegionsExpanded;
      Adornments.Clear();
      _staticAdornments.Clear();
      _layer.RemoveAllAdornments();
      _logger.WriteToLog("Emptied adornments from the adornments manager.");
    }

    /// <summary>
    /// If that layout is updated, add this's adornments back onto this's text view's adornment layer.
    /// </summary>
    /// <remarks>
    /// When a text view's layout is updated, any adornments that were attached to the new or reformatted lines are removed.
    /// </remarks>
    void OnLayoutChanged(object sender, TextViewLayoutChangedEventArgs e) {
      try {
        //if (adornmentWasRemoved) {
        foreach (var line in e.NewOrReformattedLines)
          CreateVisuals(line);
#if TRACKING_REMOVED_ADORNMENTS
        _adornmentWasRemoved = false;
#endif
        //}
        if (_newStaticAdornments)
          UpdateStaticAdornments(null, null);
      } catch (Exception exn) {
        _logger.PublicEntryException(exn, "OnLayoutChanged");
      }
    }

    /// <summary>
    /// When a region is expanded, we want to make sure all adornments within that region are visable.
    /// </summary>
    void OnRegionsExpanded(object sender, RegionsExpandedEventArgs e) {
      //return;//TODO: Dead?
      _logger.PublicEntry(() => {
        foreach (var region in e.ExpandedRegions) {
          ShowVisuals(region.Extent);
        }
      }, "OnRegionsExpanded");
    }
    /// <summary>
    /// When a region is collapsed, we want to make sure all adornments within that region are hidden.
    /// </summary>
    void OnRegionsCollapsed(object sender, RegionsCollapsedEventArgs e) {
      //return;//TODO: Dead?
      _logger.PublicEntry(() => {
        foreach (var region in e.CollapsedRegions) {
          CollapseVisuals(region.Extent);
        }
      }, "OnRegionsCollapsed");
    }

    /// <summary>
    /// Hide all adornments within the given extent.
    /// </summary>
    public void CollapseVisuals(ITrackingSpan extent) {
      Contract.Requires(extent != null);

      var extentSpan = extent.GetSpan(_textView.TextBuffer.CurrentSnapshot.Version);
      foreach (var entry in Adornments) {
        IAdornment adornment = entry.Value;
        var adornmentSpan = adornment.Span.GetSpan(_textView.TextBuffer.CurrentSnapshot);
        if (extentSpan.IntersectsWith(adornmentSpan)) {
          adornment.CollapsedRegionDepth++;
          QueueRefreshLineTransformer();
        }
      }
    }
    public void CollapseVisuals() {
      foreach (var entry in Adornments) {
        IAdornment adornment = entry.Value;
        adornment.IsClosedByUser = true;
      }
      QueueRefreshLineTransformer();
    }
    /// <summary>
    /// Shoe all adornments within the given extent.
    /// </summary>
    public void ShowVisuals(ITrackingSpan extent) {
      Contract.Requires(extent != null);

      var extentSpan = extent.GetSpan(_textView.TextBuffer.CurrentSnapshot.Version);
      foreach (var entry in Adornments) {
        IAdornment adornment = entry.Value;
        var adornmentSpan = adornment.Span.GetSpan(_textView.TextBuffer.CurrentSnapshot);
        if (extentSpan.IntersectsWith(adornmentSpan)) {
          adornment.CollapsedRegionDepth--;
          QueueRefreshLineTransformer();
        }
      }
    }
    public void ShowVisuals() {
      foreach (var entry in Adornments) {
        IAdornment adornment = entry.Value;
        adornment.IsClosedByUser = false;
      }
      QueueRefreshLineTransformer();
    }

    /// <summary>
    /// Adds all of this's adornments onto this's text view's adornment layer.
    /// </summary>
    void CreateVisuals() {
      foreach (var line in _textView.TextViewLines)
        CreateVisuals(line);
#if TRACKING_REMOVED_ADORNMENTS
      _adornmentWasRemoved = false;
#endif
      //foreach (var entry in adornments) {
      //  IAdornment adornment = entry.Value;
      //  object tag = entry.Key;
      //  CreateVisual(adornment, tag);
      //}
    }
    /// <summary>
    /// Adds all of this's adornments that pertain to a particular text view line back onto this's text view's adornment layer.
    /// </summary>
    void CreateVisuals(ITextViewLine line) {
      foreach (var entry in Adornments) {
        var adornment = entry.Value;
        var tag = entry.Key;
        if (line.ContainsBufferPosition(adornment.Span.GetStartPoint(_textView.TextSnapshot))) {
          _layer.RemoveAdornmentsByTag(tag);
          CreateVisual(line, adornment, tag);
        }
      }
    }

    /// <summary>
    /// Adds a particular adornment onto this's text view's adornment layer.
    /// </summary>
    void CreateVisual(IAdornment adornment, object tag) {
      ITextViewLine line = _textView.TextViewLines.GetTextViewLineContainingBufferPosition(adornment.Span.GetStartPoint(_textView.TextSnapshot));
      if (line == null)
        return;
      CreateVisual(line, adornment, tag);
    }
    /// <summary>
    /// Adds a particular adornment onto this's text view's adornment layer.
    /// </summary>
    void CreateVisual(ITextViewLine line, IAdornment adornment, object tag) {
      FrameworkElement elem = adornment.Visual;
      var span = adornment.Span.GetSpan(_textView.TextSnapshot);
      Geometry g = _textView.TextViewLines.GetMarkerGeometry(new SnapshotSpan(_textView.TextSnapshot, Span.FromBounds(span.Start, line.End)));
      if (g != null) {
        //position the adornment such that it is at the top left corner of the line
        Canvas.SetLeft(elem, g.Bounds.Left);
        Canvas.SetTop(elem, g.Bounds.Top - adornment.Visual.ActualHeight);
      } else {
        _logger.WriteToLog("Failed to get marker geometry for adornment.");
      }
      AddAdornment(elem, line.Extent, tag);
    }

    void EvaluateAdornmentRegionDepth(IAdornment adornment) {
      Contract.Requires(adornment != null && adornment.Span != null);
      Contract.Requires(_outliningManager != null);
      Contract.Requires(_textView != null);

      try {
        var workingSnapshot = _textView.TextSnapshot;
        var collapsedRegionsAdornmentIsIn = _outliningManager.GetCollapsedRegions(adornment.Span.GetSpan(workingSnapshot));
        Contract.Assume(collapsedRegionsAdornmentIsIn != null, "Let's make the assumption explicit");
        adornment.CollapsedRegionDepth = collapsedRegionsAdornmentIsIn.Count();
      } catch (ObjectDisposedException e) {
        if (e.ObjectName == "OutliningManager")
          _logger.WriteToLog("Error: The 'OutliningManager' is disposed, adornments may not be formatted correctly.");
        else
          throw e;
      }
    }

    /// <summary>
    /// Adds a particular adornment onto this's text view's adornment layer.
    /// </summary>
    void AddAdornment(UIElement element, SnapshotSpan span, object tag) {
      _layer.AddAdornment(behavior: AdornmentPositioningBehavior.TextRelative,
                          visualSpan: span,
                          tag: tag,
                          adornment: element,
                          removedCallback: OnAdornmentRemoved);
    }

    /// <summary>
    /// Adds a particular adornment onto this's text view's adornment layer.
    /// </summary>
    public void AddAdornment(IAdornment adornment, object tag) {
      Contract.Requires(adornment != null);

      Adornments[tag] = adornment;
      CreateVisual(adornment, tag);
      EvaluateAdornmentRegionDepth(adornment);
    }

    public void AddStaticAdornment(IStaticAdornment staticAdornment) {
      _staticAdornments.Add(staticAdornment);
      //staticAdornment.Visual.UpdateLayout();
      _newStaticAdornments = true;
    }

    /// <summary>
    /// Notifies this AdornmentManager that an adornment was removed so that on the next layout update, this's adornments can be updated.
    /// </summary>
    void OnAdornmentRemoved(object tag, UIElement visual) {
      //Public Entry
#if TRACKING_REMOVED_ADORNMENTS
      _adornmentWasRemoved = true;
#endif
    }

    /// <summary>
    /// Gets a particular adornment.
    /// </summary>
    public IAdornment GetAdornment(object tag) {
      IAdornment result;
      if (Adornments.TryGetValue(tag, out result))
        return result;
      return null;
    }

    /// <summary>
    /// Removes a particular adornment.
    /// </summary>
    public void RemoveAdornment(object tag) {
      Adornments.Remove(tag);
      _layer.RemoveAdornmentsByTag(tag);
    }
  }
  static class ExtensionsHelper {
    /// <summary>
    /// Forces the line transformer to revaluate its transforms.
    /// </summary>
    /// <remarks>
    /// Should not be called during the LayoutChanged event
    /// </remarks>
    public static void RefreshLineTransformer(this ITextView textView) {
      var line = textView.TextViewLines.FirstVisibleLine;
      textView.DisplayTextLineContainingBufferPosition(line.Start, line.Top - textView.ViewportTop, ViewRelativePosition.Top);
    }
  }
}
