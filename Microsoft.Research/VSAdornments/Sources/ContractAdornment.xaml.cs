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
using System.Diagnostics.Contracts;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Microsoft.Cci;
using Microsoft.Cci.Contracts;
using Microsoft.VisualStudio.Text;
using System.Windows.Media.Effects;
using System.Windows.Input;
using UtilitiesNamespace;

namespace Adornments {
  /// <summary>
  /// The custom adornment responsible for storing and rendering contract information for a particular method.
  /// </summary>
  public abstract partial class ContractAdornment : UserControl, IAdornment {
    /// <summary>
    /// The tracking span that this adornment is attached to.
    /// </summary>
    public ITrackingSpan Span { get; set; }
    /// <summary>
    /// The visual that is rendered for this adornment.
    /// </summary>
    /// <remarks>
    /// This's is visual is the same as this, since this is a <see cref="UserControl"/>.
    /// </remarks>
    public FrameworkElement Visual {
      get {
        return this;
      }
    }
    /// <summary>
    /// True if this adornment should be visible.
    /// </summary>
    /// <remarks>
    /// Checks to see if this adornment is in a region, closed by the user, or is empty; if so, the adornment should not be visible.
    /// </remarks>
    public bool ShouldBeVisible {
      get {
        return !IsInCollapsedRegion && !IsClosedByUser && !IsEmpty;
      }
    }
    /// <summary>
    /// True if this adornment is within a collapsed region. If it is, this adornment is collapsed.
    /// </summary>
    public bool IsInCollapsedRegion {
      get {
        return _isInCollapsedRegion;
      }
      set {
        _isInCollapsedRegion = value;
        if (value) {
          Visibility = Visibility.Collapsed;
        } else if (ShouldBeVisible) {
          Visibility = Visibility.Visible;
        }
      }
    }
    bool _isInCollapsedRegion = false;
    public virtual int CollapsedRegionDepth {
      get {
        return _collapsedRegionDepth;
      }
      set {
        _collapsedRegionDepth = value;
        if (value < 1)
          IsInCollapsedRegion = false;
        else
          IsInCollapsedRegion = true;
      }
    }
    int _collapsedRegionDepth;
    /// <summary>
    /// True if this adornment was closed by the user. If it was, this adornment is collapsed.
    /// </summary>
    public virtual bool IsClosedByUser {
      get {
        return _isClosedByUser;
      }
      set {
        if (value ^ _isCollapsedByUser)
          CollapseAdornment_Click(null, null);
        _isClosedByUser = value;
        if (value)
          Visibility = Visibility.Collapsed;
        else if (ShouldBeVisible)
          Visibility = Visibility.Visible;
      }
    }
    bool _isClosedByUser = false;
    /// <summary>
    /// True if this adornment was collapsed by the user.
    /// </summary>
    /// <remarks>
    /// This is different than this's Visibility being Collapsed.
    /// </remarks>
    protected bool IsCollapsedByUser {
      get {
        return _isCollapsedByUser;
      }
      set {
        _isCollapsedByUser = value;
      }
    }
    bool _isCollapsedByUser = false;
    /// <summary>
    /// True if this adornment is empty.
    /// </summary>
    public bool IsEmpty {
      get {
        return contractsGrid.Children.Count == 0;
      }
    }
    /// <summary>
    /// The tool tip that will be rendered when hovering over this adornment.
    /// </summary>
    protected string ToolTipContent {
      set {
        toolTipContent = value;
        UpdateToolTip();
      }
      get {
        return toolTipContent;
      }
    }
    protected string toolTipContent = "Code contracts.";
    /// <summary>
    /// Invoke this to make sure that the text view this adornment belongs to has properly calculated the space needed for its adornments.
    /// </summary>
    /// <remarks>
    /// Use sparingly. This is neccessary because the text view this adornment belongs to doesn't always update the layout of its adornments
    /// after they are changed and before the line transformer is called. So this must be invoked manually by the adornments to make sure
    /// the lines they belong to have been properly transformed.
    /// </remarks>
    Action _queueRefreshLineTransformer;
    /// <summary>
    /// True if this should invoke <see cref="_queueRefreshLineTransformer"/>.
    /// </summary>
    public bool ShouldRefreshLineTransformer {
      set {
        if (value && _queueRefreshLineTransformer != null)
          _queueRefreshLineTransformer();
      }
    }
    public bool IsCaretFocus {
      get {
        return _isCaretFocus;
      }
      set {
        if (!IsMouseFocus) {
          if (value)
            OnFocused();
          else
            OnUnfocused();
        }
        _isCaretFocus = value;
      }
    }
    bool _isCaretFocus;
    bool IsMouseFocus {
      get {
        return _isMouseFocus;
      }
      set {
        if (!IsCaretFocus) {
          if (value)
            OnFocused();
          else
            OnUnfocused();
        }
        _isMouseFocus = value;
      }
    }
    bool _isMouseFocus;
    public bool IsFocusedOnAdornment {
      get {
        return IsMouseFocus || IsCaretFocus;
      }
    }
    public abstract LineTransformBehavior LineTransformBehavior { get; }
    protected VSTextProperties _vsTextProperties;
    public virtual bool IsPure { get; set; }
    protected FrameworkElement _purityElement;
    protected string _tabsAsSpaces;
    protected bool _hasNonPurityContracts;
    protected readonly Logger _logger;
    protected readonly AdornmentOptions _options;

    [ContractInvariantMethod]
    void ObjectInvariant() {
      Contract.Invariant(_logger != null);
    }


    /// <summary>
    /// Creates a new ContractAdornment for the given method (specified by the tag).
    /// </summary>
    public ContractAdornment(ITrackingSpan span, VSTextProperties vsTextProperties, Logger logger, Action queueRefreshLineTransfomer, AdornmentOptions options = AdornmentOptions.None) {
      Contract.Requires(span != null);
      Contract.Requires(logger != null);

      _logger = logger;
      Span = span;
      InitializeComponent();
      _queueRefreshLineTransformer = queueRefreshLineTransfomer;
      Visibility = Visibility.Collapsed;
      _vsTextProperties = vsTextProperties;
      for (int i = 0; i < vsTextProperties.TabSize; i++) {
        _tabsAsSpaces += " ";
      }
      _options = options;
      LayoutUpdated += OnLayoutUpdated;
      IsVisibleChanged += OnVisibilityChanged;
      Visual.MouseEnter += OnMouseEnter;
      Visual.MouseLeave += OnMouseLeave;

      int size = (int)vsTextProperties.FontSize;
      /*
      if (size <= 9) size = 8;
      else if (size == 10) size = 9;
      else size = size - 2;
      */
      rootGrid.SetValue(TextBlock.FontSizeProperty, (double)size);
      SetVisual();
    }

    void OnMouseLeave(object sender, MouseEventArgs e) {
      try { // Public Entry
        IsMouseFocus = false;
      } catch (Exception exn) {
        _logger.PublicEntryException(exn, "OnMouseLeave");
      }
    }
    void OnMouseEnter(object sender, MouseEventArgs e) {
      try { // Public Entry
        IsMouseFocus = true;
      } catch (Exception exn) {
        _logger.PublicEntryException(exn, "OnMouseLeave");
      }
    }

    protected virtual void OnFocused() { }
    protected virtual void OnUnfocused() { }

    protected virtual void SetVisual() {
      var purityBlock = new TextBlock();
      purityBlock.Inlines.Add(new Run("["));
      var pureRun = new Run("Pure");
      pureRun.Foreground = _vsTextProperties.TypeColor;
      purityBlock.Inlines.Add(pureRun);
      purityBlock.Inlines.Add(new Run("]"));
      purityBlock.RenderTransform = new TranslateTransform(0, _vsTextProperties.LineHeight * -2);
      purityBlock.Visibility = Visibility.Collapsed;
      rootGrid.Children.Add(purityBlock);
      _purityElement = purityBlock;
    }

    void OnVisibilityChanged(object sender, DependencyPropertyChangedEventArgs e) {
      _logger.PublicEntry(() => {
        ShouldRefreshLineTransformer = true;
      }, "OnVisibilityChanged");
    }

    /// <summary>
    /// Makes sure that the line transforms are properly updated when this's layout is changed.
    /// </summary>
    void OnLayoutUpdated(object sender, EventArgs e) {
      try { //Public Entry
        //if (IsMeasureValid) {
        //  if (_queueRefreshLineTransformer != null)
        //    _queueRefreshLineTransformer();
        //  ShouldRefreshLineTransformer = false;
        //}
      } catch (Exception exn) {
        _logger.PublicEntryException(exn, "OnLayoutUpdated");
      }
    }

    /// <summary>
    /// Updates this's visual based on new contract information.
    /// </summary>
    public void SetContracts(IMethodContract contracts, string toolTip) {
      #region Disable if contracts are null or dummy
      if (contracts == null || contracts == ContractDummy.MethodContract) {
        Visibility = Visibility.Collapsed;
        return;
      }
      #endregion
      #region Set tooltip
      if (!string.IsNullOrEmpty(toolTip))
        ToolTipContent = toolTip;
      #endregion
      ClearContracts();
      IsPure = contracts.IsPure;
      _hasNonPurityContracts = contracts.Preconditions.Count() > 0 || contracts.Postconditions.Count() > 0 || contracts.ThrownExceptions.Count() > 0;
      StartFormatContracts();
      FormatContracts(contracts);
      EndFormatContracts();
      if (ShouldBeVisible)
        Visibility = Visibility.Visible;
      else
        Visibility = Visibility.Collapsed;

      ShouldRefreshLineTransformer = true;
    }
    /// <summary>
    /// Updates this's visual based on new contract information.
    /// </summary>
    public void SetContracts(IMethodContract getterContract, IMethodContract setterContract, string toolTip) {
      #region Disable if contracts are null or dummy
      if ((getterContract == null && setterContract == null) || (getterContract == ContractDummy.MethodContract && setterContract == ContractDummy.MethodContract)) {
        Visibility = Visibility.Collapsed;
        return;
      }
      #endregion
      #region Set tooltip
      if (!string.IsNullOrEmpty(toolTip))
        ToolTipContent = toolTip;
      #endregion
      ClearContracts();
      //IsPure = true; //Properties should always be pure.
      IsPure = (getterContract != null && getterContract.IsPure) || (setterContract != null && setterContract.IsPure);
      _hasNonPurityContracts = getterContract.Preconditions.Count() > 0 || getterContract.Postconditions.Count() > 0 || getterContract.ThrownExceptions.Count() > 0
        || setterContract.Preconditions.Count() > 0 || setterContract.Postconditions.Count() > 0 || setterContract.ThrownExceptions.Count() > 0;
      StartFormatContracts();
      FormatContracts(getterContract, setterContract);
      EndFormatContracts();
      if (ShouldBeVisible)
        Visibility = Visibility.Visible;
      else
        Visibility = Visibility.Collapsed;

      ShouldRefreshLineTransformer = true;
    }

    protected virtual void ClearContracts() {
      contractsGrid.Children.Clear();
      contractsGrid.RowDefinitions.Clear();
    }

    protected virtual void StartFormatContracts() { }
    protected virtual void EndFormatContracts() { }

    /// <summary>
    /// Formats this's contracts grid based on the given contract information.
    /// </summary>
    protected virtual void FormatContracts(IMethodContract getterContract, IMethodContract setterContract) {
      if (getterContract != null && getterContract != ContractDummy.MethodContract) {
        //FormatContract("Getter Contracts:", " ");
        FormatContracts(getterContract, "get ");
      }
      if (setterContract != null && setterContract != ContractDummy.MethodContract) {
        //FormatContract("Setter Contracts:", " ");
        FormatContracts(setterContract, "set ");
      }
    }
    /// <summary>
    /// Formats this's contracts grid based on the given contract information.
    /// </summary>
    protected virtual void FormatContracts(IMethodContract contracts, string headerPrefix = "") {
      Contract.Requires(contracts != null);
      foreach (var pre in contracts.Preconditions)
        FormatContract(headerPrefix + "requires", pre.OriginalSource);
      foreach (var post in contracts.Postconditions)
        FormatContract(headerPrefix + "ensures", post.OriginalSource);
      foreach (var thrown in contracts.ThrownExceptions)
        FormatContract(headerPrefix + "ensures on throw", "of " + TypeHelper.GetTypeName(thrown.ExceptionType, NameFormattingOptions.OmitContainingType | NameFormattingOptions.OmitContainingNamespace) + " that " + thrown.Postcondition.OriginalSource);
    }
    /// <summary>
    /// Adds a row in this's contracts grid based on the given contract information.
    /// </summary>
    protected virtual void FormatContract(string header, string content) {
      //New Row
      contractsGrid.RowDefinitions.Add(new RowDefinition());

      //Stack Panel
      var stackPanel = new StackPanel() { Orientation = System.Windows.Controls.Orientation.Horizontal };
      contractsGrid.Children.Add(stackPanel);
      Grid.SetRow(stackPanel, contractsGrid.RowDefinitions.Count - 1);

      //Header
      var headerBlock = new TextBlock(new Run(header + " "));
      headerBlock.Foreground = Brushes.Black;
      //headerBlock.FontWeight = headerWeight;
      //contractsGrid.Children.Add(headerBlock);
      //Grid.SetColumn(headerBlock, 0);
      //Grid.SetRow(headerBlock, contractsGrid.RowDefinitions.Count - 1);
      stackPanel.Children.Add(headerBlock);

      //Content
      var contentBlock = new TextBlock(new Run(content));
      contentBlock.Foreground = Brushes.Black;
      //contentBlock.FontWeight = runWeight;
      //contractsGrid.Children.Add(contentBlock);
      //Grid.SetColumn(contentBlock, 1);
      //Grid.SetRow(contentBlock, contractsGrid.RowDefinitions.Count - 1);
      stackPanel.Children.Add(contentBlock);
    }

    protected string SmartFormat(string contracts) {
      var result = contracts;
      if ((_options & AdornmentOptions.SmartFormatting) != 0) {
        //var startTime = DateTime.Now;

        var trySmartReplace = result;

        try {
          //Simplify how contracts look
          trySmartReplace = trySmartReplace.SmartReplace("Contract.OldValue<{0}>({1})", "old({1})");
          trySmartReplace = trySmartReplace.SmartReplace("Contract.OldValue({0})", "old({0})");
          trySmartReplace = trySmartReplace.SmartReplace("Contract.Result<{0}>()", "result");
          trySmartReplace = trySmartReplace.SmartReplace("Contract.ValueAtReturn<{0}>({1})", "out({1})");
          trySmartReplace = trySmartReplace.SmartReplace("Contract.ValueAtReturn({0})", "out({0})");
          trySmartReplace = trySmartReplace.SmartReplace("Contract.ForAll<{0}>({1})", "forall({1})");
          trySmartReplace = trySmartReplace.SmartReplace("Contract.ForAll({0})", "forall({0})");
        } catch (Exception) {
          trySmartReplace = null;
          _logger.WriteToLog("Error: Smart formatting failed!");
          _logger.WriteToLog(result);
        }

        if (trySmartReplace != null)
          result = trySmartReplace;

        //var elapsedTime = DateTime.Now - startTime;
        //_logger.WriteToLog("\t(Smart formatting took " + elapsedTime.Milliseconds + "ms)");
      }
      return result;
    }

    void CollapseAdornment_Click(object sender, RoutedEventArgs e) {
      _logger.PublicEntry(() => {
        ToggleCollapseAdornment();
      }, "CollapseAdornment_Click");
    }

    /// <summary>
    /// Shrinks this adornment into a collapsed state.
    /// </summary>
    /// <remarks>
    /// This is different than setting this adornment's Visiblity to Collapsed.
    /// </remarks>
    protected virtual void ToggleCollapseAdornment() {
      IsCollapsedByUser = !IsCollapsedByUser;

      if (IsCollapsedByUser) {
        summaryBlock.Visibility = Visibility.Visible;
        OnCollapsed();
        UpdateToolTip();
      } else {
        summaryBlock.Visibility = Visibility.Collapsed;
        contractsGrid.Visibility = Visibility.Visible;
        OnExpanded();
        UpdateToolTip();
      }

      ShouldRefreshLineTransformer = true;
    }

    protected virtual void OnExpanded() {
      //if (IsPure)
      //  _purityElement.Visibility = Visibility.Visible;//TODO: Remove, dead code
    }
    protected virtual void OnCollapsed() {
      //if (IsPure)
      //  _purityElement.Visibility = Visibility.Collapsed;//TODO: Remove, dead code
    }

    /// <summary>
    /// Makes sure the this's tool tip is updated. The tooltip changes if this adornment is collapsed by the user or not.
    /// </summary>
    protected virtual void UpdateToolTip() {
      if (IsCollapsedByUser) {
        contractsGridHolder.Child = null;
        rootGrid.ToolTip = contractsGrid;
      } else {
        rootGrid.ToolTip = ToolTipContent;
        contractsGridHolder.Child = contractsGrid;
      }
      summaryBlock.Text = ToolTipContent;
    }
  }

  public enum AdornmentOptions {
    None=0,
    SmartFormatting = 1,
    SyntaxColoring = SmartFormatting << 1,
    CollapseWithRegion = SyntaxColoring << 1,
  }
}
