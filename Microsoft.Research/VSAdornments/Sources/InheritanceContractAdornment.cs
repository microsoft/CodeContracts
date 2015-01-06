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
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Text;
using Microsoft.Cci.Contracts;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Effects;
using System.Windows.Input;
using System.Diagnostics.Contracts;
using System.Windows.Shapes;
using UtilitiesNamespace;

namespace Adornments {
  public class InheritanceContractAdornment : ContractAdornment {
    public override LineTransformBehavior LineTransformBehavior {
      get {
        return LineTransformBehavior.Above;//return _lineTransformBehavior; //TODO: Fix properley, dead code now exists that attempts to change the LineTransformBehavior
      }
    }
#if LINE_BEHAVIOR
    LineTransformBehavior _lineTransformBehavior = LineTransformBehavior.Below;
#endif
    public override bool IsPure {
      get {
        return base.IsPure;
      }
      set {
        base.IsPure = value;
#if LINE_BEHAVIOR
        if (value && !IsCollapsedByUser)
          _lineTransformBehavior = Adornments.LineTransformBehavior.BelowWithOneLineAbove;//TODO: Dead?
#endif
      }
    }
    Rectangle _grayBar;

    public InheritanceContractAdornment(ITrackingSpan span, VSTextProperties vsTextProperties, Logger logger, Action queueRefreshLineTransformer, AdornmentOptions options = AdornmentOptions.None)
      : base(span, vsTextProperties, logger, queueRefreshLineTransformer, options) {
        Contract.Requires(span != null);
        Contract.Requires(logger != null);
      //SizeChanged += OnSizeChanged;
    }

    void OnSizeChanged(object sender, SizeChangedEventArgs e) {
      _logger.PublicEntry(() => {
        RenderTransform = new TranslateTransform(0d, _vsTextProperties.LineHeight + e.NewSize.Height);//TODO: Dead?
      }, "OnSizeChanged");
    }

    protected override void SetVisual() {
      //base.SetVisual();//TODO: Fix proper. Right now we don't call base's SetVisual to avoid drawing the "pure" box, but eventually we want to give the base the privilage of being able to SetVisual stuff.
      //rootBorder.Margin = new Thickness(0, 0, 0, 0);
      collapseButton.Visibility = System.Windows.Visibility.Collapsed;
      ToggleCollapseAdornment();//Starts in the collapsed state
      summaryBlock.FontStyle = FontStyles.Italic;
      summaryBlock.Foreground = Brushes.DimGray;
      Opacity = opaqueLo;
      contractsGrid.Cursor = Cursors.Hand;
      contractsGrid.MouseLeftButtonUp += OnClicked;
      summaryBlock.Cursor = Cursors.Hand;
      summaryBlock.MouseLeftButtonUp += OnClicked;
      //_purityElement.Cursor = Cursors.Hand;
      //_purityElement.MouseLeftButtonUp += OnClicked;
      //rootBorder.BorderBrush = Brushes.Gray;
      _grayBar = new Rectangle() { Width = 2, Fill = Brushes.Gray, Margin = new Thickness(3, 0, 3, 0), Visibility = System.Windows.Visibility.Collapsed };
      rootStack.Children.Insert(0, _grayBar);
    }

    void OnClicked(object sender, MouseButtonEventArgs e) {
      _logger.PublicEntry(() => {
        ToggleCollapseAdornment();
      }, "OnClicked");
    }

    protected override void StartFormatContracts() {
      base.StartFormatContracts();
    }
    protected override void EndFormatContracts() {
      if (IsPure) {
        contractsGrid.RowDefinitions.Add(new RowDefinition());
        var purityBlock = new TextBlock(new Run(/*_tabsAsSpaces + */"[Pure]") { Foreground = Brushes.DimGray, FontStyle = FontStyles.Italic });
        contractsGrid.Children.Add(purityBlock);
        Grid.SetRow(purityBlock, contractsGrid.RowDefinitions.Count - 1);
      }
      base.EndFormatContracts();
      if (contractsGrid.RowDefinitions.Count < 4)
        ToggleCollapseAdornment();
    }

    protected override void FormatContract(string header, string content) {
      content = SmartFormat(content);

      //New Row
      contractsGrid.RowDefinitions.Add(new RowDefinition());

      //Stack Panel
      var stackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
      contractsGrid.Children.Add(stackPanel);
      Grid.SetRow(stackPanel, contractsGrid.RowDefinitions.Count - 1);

      //Header
      var headerBlock = new TextBlock(new Run(/*_tabsAsSpaces + */header + " "));
      if((_options & AdornmentOptions.SyntaxColoring) != 0)
        headerBlock.Foreground = _vsTextProperties.KeywordColor;
      else
        headerBlock.Foreground = Brushes.DimGray;
      headerBlock.FontStyle = FontStyles.Italic;
      //contractsGrid.Children.Add(headerBlock);
      //Grid.SetColumn(headerBlock, 0);
      //Grid.SetRow(headerBlock, contractsGrid.RowDefinitions.Count - 1);
      stackPanel.Children.Add(headerBlock);

      //Content
      var contentBlock = new TextBlock(new Run(content));
      if ((_options & AdornmentOptions.SyntaxColoring) != 0)
        contentBlock.Foreground = _vsTextProperties.TextColor;
      else
        contentBlock.Foreground = Brushes.DimGray;
      //contentBlock.FontStyle = FontStyles.Italic;
      //contractsGrid.Children.Add(contentBlock);
      //Grid.SetColumn(contentBlock, 1);
      //Grid.SetRow(contentBlock, contractsGrid.RowDefinitions.Count - 1);
      stackPanel.Children.Add(contentBlock);
    }

    const double opaqueHi = 0.9;
    const double opaqueLo = 0.7;

    protected override void OnExpanded() {
      base.OnExpanded();
#if LINE_BEHAVIOR
      if (IsPure)
        _lineTransformBehavior = Adornments.LineTransformBehavior.BelowWithOneLineAbove;
#endif
      Opacity = opaqueHi;
      if (_grayBar != null)
        _grayBar.Visibility = Visibility.Visible;
      //rootBorder.BorderThickness = new Thickness(0.8d);
      //rootBorder.Padding = new Thickness(2);
    }
    protected override void OnCollapsed() {
      base.OnCollapsed();
#if LINE_BEHAVIOR
      _lineTransformBehavior = Adornments.LineTransformBehavior.Below;
#endif
      if (!IsFocusedOnAdornment)
        Opacity = opaqueLo;
      if (_grayBar != null)
        _grayBar.Visibility = Visibility.Collapsed;
      //rootBorder.BorderThickness = new Thickness();
      //rootBorder.Padding = new Thickness();
    }
    protected override void OnFocused() {
      base.OnFocused();
      Opacity = opaqueHi;
    }
    protected override void OnUnfocused() {
      base.OnUnfocused();
      Opacity = opaqueLo;
    }
  }
}