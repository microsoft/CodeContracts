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
using UtilitiesNamespace;
using System.Diagnostics.Contracts;

namespace Adornments {
  public class MetadataContractAdornment : ContractAdornment {
    public override LineTransformBehavior LineTransformBehavior {
      get {
        return _lineTransformBehavior;//TODO: REMOVE
      }
    }
    LineTransformBehavior _lineTransformBehavior = LineTransformBehavior.Below;
    public override bool IsPure {
      get {
        return base.IsPure;
      }
      set {
        base.IsPure = value;
        if (value && !IsCollapsedByUser)
          _lineTransformBehavior = Adornments.LineTransformBehavior.BelowWithOneLineAbove;
      }
    }
    public override bool IsClosedByUser {
      get {
        return base.IsClosedByUser;
      }
      set {
        base.IsClosedByUser = value;
        if (value)
          _lineTransformBehavior = Adornments.LineTransformBehavior.None;
        else if (IsPure)
          _lineTransformBehavior = Adornments.LineTransformBehavior.BelowWithOneLineAbove;
        else
          _lineTransformBehavior = Adornments.LineTransformBehavior.Below;
      }
    }

    public override int CollapsedRegionDepth {
      get {
        if ((this._options & AdornmentOptions.CollapseWithRegion) != 0)
        {
          return base.CollapsedRegionDepth;
        }
        else
        {
          return 0;
        }
      }
      set {
        if ((this._options & AdornmentOptions.CollapseWithRegion) != 0)
        {
          base.CollapsedRegionDepth = value;
        }
        else
        {
          base.CollapsedRegionDepth = 0;
        }
      }
    }
    public MetadataContractAdornment(ITrackingSpan span, VSTextProperties vsTextProperties, Logger logger, Action queueRefreshLineTransfomer, AdornmentOptions options = AdornmentOptions.None)
      : base(span, vsTextProperties, logger, queueRefreshLineTransfomer, options) 
    {
      Contract.Requires(span != null);
      Contract.Requires(logger != null);

      SizeChanged += OnSizeChanged;
    }

    void OnSizeChanged(object sender, SizeChangedEventArgs e) {
      _logger.PublicEntry(() => {
        RenderTransform = new TranslateTransform(0d, _vsTextProperties.LineHeight + e.NewSize.Height);
      }, "OnSizeChanged");
    }

    //Plain Visual
    protected override void SetVisual() {
      base.SetVisual();
      collapseButton.Visibility = Visibility.Collapsed;
      Opacity = 0.5d;
      //root.Margin = new Thickness(0, 0, 0, 8);
    }
    
    //Box Visual:
    //protected override void SetVisual(Border root) {
    //  base.SetVisual(root);
    //  var backgroundBrush = new LinearGradientBrush();
    //  backgroundBrush.StartPoint = new Point(0.5, 0);
    //  backgroundBrush.EndPoint = new Point(0.5, 1);
    //  backgroundBrush.GradientStops.Add(new GradientStop(Color.FromArgb(125, 251, 251, 253), 0.134));
    //  backgroundBrush.GradientStops.Add(new GradientStop(Color.FromArgb(125, 228, 229, 240), 0.99));
    //  root.Background = backgroundBrush;
    //  root.Padding = new Thickness(2);
    //  root.Margin = new Thickness(0, 1, 0, 1);
    //  root.BorderBrush = Brushes.Gray;
    //  root.BorderThickness = new Thickness(1);
    //  Opacity = 0.5d;
    //  collapseButton.Visibility = System.Windows.Visibility.Collapsed;
    //}

    protected override void StartFormatContracts() {
      if (IsPure) {
        _purityElement.Visibility = System.Windows.Visibility.Visible;
      }
      if (true /*|| _hasNonPurityContracts*/) {//TODO: SOmething very fishy is happening here..
        //Set first open curly brace
        contractsGrid.RowDefinitions.Add(new RowDefinition());
        var openCurly = new TextBlock(new Run("{"));
        if (!_hasNonPurityContracts) openCurly.Visibility = System.Windows.Visibility.Collapsed;
        openCurly.Foreground = _vsTextProperties.TextColor;
        contractsGrid.Children.Add(openCurly);
        Grid.SetColumn(openCurly, 0);
        Grid.SetRow(openCurly, contractsGrid.RowDefinitions.Count - 1);
      }
      //if (IsPure) {
      //  contractsGrid.RowDefinitions.Add(new RowDefinition());
      //  var purityBlock = new TextBlock(new Run(_tabsAsSpaces + "Pure"));
      //  contractsGrid.Children.Add(purityBlock);
      //  Grid.SetRow(purityBlock, contractsGrid.RowDefinitions.Count - 1);
      //}

      base.StartFormatContracts();
    }

    protected override void EndFormatContracts() {
      base.EndFormatContracts();
      if (true /*|| _hasNonPurityContracts*/) {
        //Set second open curly braces
        contractsGrid.RowDefinitions.Add(new RowDefinition());
        var closeCurly = new TextBlock(new Run("}"));
        if (!_hasNonPurityContracts) closeCurly.Visibility = System.Windows.Visibility.Collapsed;
        closeCurly.Foreground = _vsTextProperties.TextColor;
        contractsGrid.Children.Add(closeCurly);
        Grid.SetColumn(closeCurly, 0);
        Grid.SetRow(closeCurly, contractsGrid.RowDefinitions.Count - 1);
      }
    }

    protected override void FormatContract(string header, string content) {
      content = SmartFormat(content);      

      //New Row
      contractsGrid.RowDefinitions.Add(new RowDefinition());

      //Stack Panel
      var stackPanel = new StackPanel() { Orientation = System.Windows.Controls.Orientation.Horizontal };
      contractsGrid.Children.Add(stackPanel);
      Grid.SetRow(stackPanel, contractsGrid.RowDefinitions.Count - 1);

      //Header
      var headerBlock = new TextBlock(new Run(_tabsAsSpaces + header + " "));
      headerBlock.Foreground = _vsTextProperties.KeywordColor;
      //headerBlock.FontWeight = headerWeight;
      //contractsgrid.children.add(headerblock);
      //grid.setcolumn(headerblock, 0);
      //grid.setRow(headerBlock, contractsGrid.RowDefinitions.Count - 1);
      stackPanel.Children.Add(headerBlock);

      //Content
      var contentBlock = new TextBlock(new Run(content));
      contentBlock.Foreground = _vsTextProperties.TextColor;
      //contentBlock.FontWeight = runWeight;
      //contractsGrid.Children.Add(contentBlock);
      //Grid.SetColumn(contentBlock, 1);
      //Grid.SetRow(contentBlock, contractsGrid.RowDefinitions.Count - 1);
      stackPanel.Children.Add(contentBlock);
    }

    protected override void OnExpanded() {
      base.OnExpanded();
      if (IsPure) {
        _purityElement.Visibility = Visibility.Visible;
        _lineTransformBehavior = Adornments.LineTransformBehavior.BelowWithOneLineAbove;
      }
      Opacity = 1.0;//0.8;
      //root.Effect = dropShadowEffect;
    }
    protected override void OnCollapsed() {
      base.OnCollapsed();
      if (IsPure) {
        _purityElement.Visibility = Visibility.Collapsed;
        _lineTransformBehavior = Adornments.LineTransformBehavior.Below;
      }
      if (!IsFocusedOnAdornment)
        Opacity = 0.5d;
      //root.Effect = null;
    }
    protected override void OnFocused() {
      base.OnFocused();
      Opacity = 1.0d;
      //if(IsCollapsedByUser)
      //  Collapse_Click(null, null);
    }
    protected override void OnUnfocused() {
      base.OnUnfocused();
      //if(IsCollapsedByUser)
      Opacity = 0.5d;
    }
  }
}

