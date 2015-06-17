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

// File System.Windows.Controls.TextBlock.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0067
// Disable the "this event is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.Windows.Controls
{
  public partial class TextBlock : System.Windows.FrameworkElement, System.Windows.IContentHost, System.Windows.Markup.IAddChild, IServiceProvider
  {
    #region Methods and constructors
    protected sealed override System.Windows.Size ArrangeOverride(System.Windows.Size arrangeSize)
    {
      return default(System.Windows.Size);
    }

    public static double GetBaselineOffset(System.Windows.DependencyObject element)
    {
      return default(double);
    }

    public static System.Windows.Media.FontFamily GetFontFamily(System.Windows.DependencyObject element)
    {
      return default(System.Windows.Media.FontFamily);
    }

    public static double GetFontSize(System.Windows.DependencyObject element)
    {
      return default(double);
    }

    public static System.Windows.FontStretch GetFontStretch(System.Windows.DependencyObject element)
    {
      return default(System.Windows.FontStretch);
    }

    public static System.Windows.FontStyle GetFontStyle(System.Windows.DependencyObject element)
    {
      return default(System.Windows.FontStyle);
    }

    public static System.Windows.FontWeight GetFontWeight(System.Windows.DependencyObject element)
    {
      return default(System.Windows.FontWeight);
    }

    public static System.Windows.Media.Brush GetForeground(System.Windows.DependencyObject element)
    {
      return default(System.Windows.Media.Brush);
    }

    public static double GetLineHeight(System.Windows.DependencyObject element)
    {
      return default(double);
    }

    public static System.Windows.LineStackingStrategy GetLineStackingStrategy(System.Windows.DependencyObject element)
    {
      return default(System.Windows.LineStackingStrategy);
    }

    public System.Windows.Documents.TextPointer GetPositionFromPoint(System.Windows.Point point, bool snapToText)
    {
      return default(System.Windows.Documents.TextPointer);
    }

    protected virtual new System.Collections.ObjectModel.ReadOnlyCollection<System.Windows.Rect> GetRectanglesCore(System.Windows.ContentElement child)
    {
      return default(System.Collections.ObjectModel.ReadOnlyCollection<System.Windows.Rect>);
    }

    public static System.Windows.TextAlignment GetTextAlignment(System.Windows.DependencyObject element)
    {
      return default(System.Windows.TextAlignment);
    }

    protected override System.Windows.Media.Visual GetVisualChild(int index)
    {
      return default(System.Windows.Media.Visual);
    }

    protected sealed override System.Windows.Media.HitTestResult HitTestCore(System.Windows.Media.PointHitTestParameters hitTestParameters)
    {
      return default(System.Windows.Media.HitTestResult);
    }

    protected virtual new System.Windows.IInputElement InputHitTestCore(System.Windows.Point point)
    {
      return default(System.Windows.IInputElement);
    }

    protected sealed override System.Windows.Size MeasureOverride(System.Windows.Size constraint)
    {
      return default(System.Windows.Size);
    }

    protected virtual new void OnChildDesiredSizeChangedCore(System.Windows.UIElement child)
    {
    }

    protected override System.Windows.Automation.Peers.AutomationPeer OnCreateAutomationPeer()
    {
      return default(System.Windows.Automation.Peers.AutomationPeer);
    }

    protected sealed override void OnPropertyChanged(System.Windows.DependencyPropertyChangedEventArgs e)
    {
    }

    protected sealed override void OnRender(System.Windows.Media.DrawingContext ctx)
    {
    }

    public static void SetBaselineOffset(System.Windows.DependencyObject element, double value)
    {
    }

    public static void SetFontFamily(System.Windows.DependencyObject element, System.Windows.Media.FontFamily value)
    {
    }

    public static void SetFontSize(System.Windows.DependencyObject element, double value)
    {
    }

    public static void SetFontStretch(System.Windows.DependencyObject element, System.Windows.FontStretch value)
    {
    }

    public static void SetFontStyle(System.Windows.DependencyObject element, System.Windows.FontStyle value)
    {
    }

    public static void SetFontWeight(System.Windows.DependencyObject element, System.Windows.FontWeight value)
    {
    }

    public static void SetForeground(System.Windows.DependencyObject element, System.Windows.Media.Brush value)
    {
    }

    public static void SetLineHeight(System.Windows.DependencyObject element, double value)
    {
    }

    public static void SetLineStackingStrategy(System.Windows.DependencyObject element, System.Windows.LineStackingStrategy value)
    {
    }

    public static void SetTextAlignment(System.Windows.DependencyObject element, System.Windows.TextAlignment value)
    {
    }

    public bool ShouldSerializeBaselineOffset()
    {
      return default(bool);
    }

    public bool ShouldSerializeInlines(System.Windows.Markup.XamlDesignerSerializationManager manager)
    {
      return default(bool);
    }

    public bool ShouldSerializeText()
    {
      return default(bool);
    }

    Object System.IServiceProvider.GetService(Type serviceType)
    {
      return default(Object);
    }

    System.Collections.ObjectModel.ReadOnlyCollection<System.Windows.Rect> System.Windows.IContentHost.GetRectangles(System.Windows.ContentElement child)
    {
      return default(System.Collections.ObjectModel.ReadOnlyCollection<System.Windows.Rect>);
    }

    System.Windows.IInputElement System.Windows.IContentHost.InputHitTest(System.Windows.Point point)
    {
      return default(System.Windows.IInputElement);
    }

    void System.Windows.IContentHost.OnChildDesiredSizeChanged(System.Windows.UIElement child)
    {
    }

    void System.Windows.Markup.IAddChild.AddChild(Object value)
    {
    }

    void System.Windows.Markup.IAddChild.AddText(string text)
    {
    }

    public TextBlock(System.Windows.Documents.Inline inline)
    {
      Contract.Ensures(0 <= this.Inlines.Count);
    }

    public TextBlock()
    {
    }
    #endregion

    #region Properties and indexers
    public System.Windows.Media.Brush Background
    {
      get
      {
        return default(System.Windows.Media.Brush);
      }
      set
      {
      }
    }

    public double BaselineOffset
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public System.Windows.LineBreakCondition BreakAfter
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.LineBreakCondition>() == ((System.Windows.LineBreakCondition)(0)));

        return default(System.Windows.LineBreakCondition);
      }
    }

    public System.Windows.LineBreakCondition BreakBefore
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.LineBreakCondition>() == ((System.Windows.LineBreakCondition)(0)));

        return default(System.Windows.LineBreakCondition);
      }
    }

    public System.Windows.Documents.TextPointer ContentEnd
    {
      get
      {
        return default(System.Windows.Documents.TextPointer);
      }
    }

    public System.Windows.Documents.TextPointer ContentStart
    {
      get
      {
        return default(System.Windows.Documents.TextPointer);
      }
    }

    public System.Windows.Media.FontFamily FontFamily
    {
      get
      {
        return default(System.Windows.Media.FontFamily);
      }
      set
      {
      }
    }

    public double FontSize
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public System.Windows.FontStretch FontStretch
    {
      get
      {
        return default(System.Windows.FontStretch);
      }
      set
      {
      }
    }

    public System.Windows.FontStyle FontStyle
    {
      get
      {
        return default(System.Windows.FontStyle);
      }
      set
      {
      }
    }

    public System.Windows.FontWeight FontWeight
    {
      get
      {
        return default(System.Windows.FontWeight);
      }
      set
      {
      }
    }

    public System.Windows.Media.Brush Foreground
    {
      get
      {
        return default(System.Windows.Media.Brush);
      }
      set
      {
      }
    }

    protected virtual new IEnumerator<System.Windows.IInputElement> HostedElementsCore
    {
      get
      {
        return default(IEnumerator<System.Windows.IInputElement>);
      }
    }

    public System.Windows.Documents.InlineCollection Inlines
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Documents.InlineCollection>() != null);

        return default(System.Windows.Documents.InlineCollection);
      }
    }

    public bool IsHyphenationEnabled
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public double LineHeight
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public System.Windows.LineStackingStrategy LineStackingStrategy
    {
      get
      {
        return default(System.Windows.LineStackingStrategy);
      }
      set
      {
      }
    }

    internal protected override System.Collections.IEnumerator LogicalChildren
    {
      get
      {
        return default(System.Collections.IEnumerator);
      }
    }

    public System.Windows.Thickness Padding
    {
      get
      {
        return default(System.Windows.Thickness);
      }
      set
      {
      }
    }

    IEnumerator<System.Windows.IInputElement> System.Windows.IContentHost.HostedElements
    {
      get
      {
        return default(IEnumerator<System.Windows.IInputElement>);
      }
    }

    public string Text
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public System.Windows.TextAlignment TextAlignment
    {
      get
      {
        return default(System.Windows.TextAlignment);
      }
      set
      {
      }
    }

    public System.Windows.TextDecorationCollection TextDecorations
    {
      get
      {
        return default(System.Windows.TextDecorationCollection);
      }
      set
      {
      }
    }

    public System.Windows.Media.TextEffectCollection TextEffects
    {
      get
      {
        return default(System.Windows.Media.TextEffectCollection);
      }
      set
      {
      }
    }

    public System.Windows.TextTrimming TextTrimming
    {
      get
      {
        return default(System.Windows.TextTrimming);
      }
      set
      {
      }
    }

    public System.Windows.TextWrapping TextWrapping
    {
      get
      {
        return default(System.Windows.TextWrapping);
      }
      set
      {
      }
    }

    public System.Windows.Documents.Typography Typography
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Documents.Typography>() != null);

        return default(System.Windows.Documents.Typography);
      }
    }

    protected override int VisualChildrenCount
    {
      get
      {
        return default(int);
      }
    }
    #endregion

    #region Fields
    public readonly static System.Windows.DependencyProperty BackgroundProperty;
    public readonly static System.Windows.DependencyProperty BaselineOffsetProperty;
    public readonly static System.Windows.DependencyProperty FontFamilyProperty;
    public readonly static System.Windows.DependencyProperty FontSizeProperty;
    public readonly static System.Windows.DependencyProperty FontStretchProperty;
    public readonly static System.Windows.DependencyProperty FontStyleProperty;
    public readonly static System.Windows.DependencyProperty FontWeightProperty;
    public readonly static System.Windows.DependencyProperty ForegroundProperty;
    public readonly static System.Windows.DependencyProperty IsHyphenationEnabledProperty;
    public readonly static System.Windows.DependencyProperty LineHeightProperty;
    public readonly static System.Windows.DependencyProperty LineStackingStrategyProperty;
    public readonly static System.Windows.DependencyProperty PaddingProperty;
    public readonly static System.Windows.DependencyProperty TextAlignmentProperty;
    public readonly static System.Windows.DependencyProperty TextDecorationsProperty;
    public readonly static System.Windows.DependencyProperty TextEffectsProperty;
    public readonly static System.Windows.DependencyProperty TextProperty;
    public readonly static System.Windows.DependencyProperty TextTrimmingProperty;
    public readonly static System.Windows.DependencyProperty TextWrappingProperty;
    #endregion
  }
}
