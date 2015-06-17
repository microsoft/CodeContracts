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

// File System.Windows.Controls.AccessText.cs
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
  public partial class AccessText : System.Windows.FrameworkElement, System.Windows.Markup.IAddChild
  {
    #region Methods and constructors
    public AccessText()
    {
    }

    protected sealed override System.Windows.Size ArrangeOverride(System.Windows.Size arrangeSize)
    {
      return default(System.Windows.Size);
    }

    protected override System.Windows.Media.Visual GetVisualChild(int index)
    {
      return default(System.Windows.Media.Visual);
    }

    protected sealed override System.Windows.Size MeasureOverride(System.Windows.Size constraint)
    {
      return default(System.Windows.Size);
    }

    void System.Windows.Markup.IAddChild.AddChild(Object value)
    {
    }

    void System.Windows.Markup.IAddChild.AddText(string text)
    {
    }
    #endregion

    #region Properties and indexers
    public char AccessKey
    {
      get
      {
        return default(char);
      }
    }

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
    public readonly static System.Windows.DependencyProperty LineHeightProperty;
    public readonly static System.Windows.DependencyProperty LineStackingStrategyProperty;
    public readonly static System.Windows.DependencyProperty TextAlignmentProperty;
    public readonly static System.Windows.DependencyProperty TextDecorationsProperty;
    public readonly static System.Windows.DependencyProperty TextEffectsProperty;
    public readonly static System.Windows.DependencyProperty TextProperty;
    public readonly static System.Windows.DependencyProperty TextTrimmingProperty;
    public readonly static System.Windows.DependencyProperty TextWrappingProperty;
    #endregion
  }
}
