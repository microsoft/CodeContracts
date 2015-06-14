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

// File System.Windows.Documents.Block.cs
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


namespace System.Windows.Documents
{
  abstract public partial class Block : TextElement
  {
    #region Methods and constructors
    protected Block()
    {
    }

    public static bool GetIsHyphenationEnabled(System.Windows.DependencyObject element)
    {
      return default(bool);
    }

    public static double GetLineHeight(System.Windows.DependencyObject element)
    {
      return default(double);
    }

    public static System.Windows.LineStackingStrategy GetLineStackingStrategy(System.Windows.DependencyObject element)
    {
      return default(System.Windows.LineStackingStrategy);
    }

    public static System.Windows.TextAlignment GetTextAlignment(System.Windows.DependencyObject element)
    {
      return default(System.Windows.TextAlignment);
    }

    public static void SetIsHyphenationEnabled(System.Windows.DependencyObject element, bool value)
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
    #endregion

    #region Properties and indexers
    public System.Windows.Media.Brush BorderBrush
    {
      get
      {
        return default(System.Windows.Media.Brush);
      }
      set
      {
      }
    }

    public System.Windows.Thickness BorderThickness
    {
      get
      {
        return default(System.Windows.Thickness);
      }
      set
      {
      }
    }

    public bool BreakColumnBefore
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool BreakPageBefore
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public System.Windows.WrapDirection ClearFloaters
    {
      get
      {
        return default(System.Windows.WrapDirection);
      }
      set
      {
      }
    }

    public System.Windows.FlowDirection FlowDirection
    {
      get
      {
        return default(System.Windows.FlowDirection);
      }
      set
      {
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

    public System.Windows.Thickness Margin
    {
      get
      {
        return default(System.Windows.Thickness);
      }
      set
      {
      }
    }

    public System.Windows.Documents.Block NextBlock
    {
      get
      {
        return default(System.Windows.Documents.Block);
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

    public System.Windows.Documents.Block PreviousBlock
    {
      get
      {
        return default(System.Windows.Documents.Block);
      }
    }

    public BlockCollection SiblingBlocks
    {
      get
      {
        return default(BlockCollection);
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
    #endregion

    #region Fields
    public readonly static System.Windows.DependencyProperty BorderBrushProperty;
    public readonly static System.Windows.DependencyProperty BorderThicknessProperty;
    public readonly static System.Windows.DependencyProperty BreakColumnBeforeProperty;
    public readonly static System.Windows.DependencyProperty BreakPageBeforeProperty;
    public readonly static System.Windows.DependencyProperty ClearFloatersProperty;
    public readonly static System.Windows.DependencyProperty FlowDirectionProperty;
    public readonly static System.Windows.DependencyProperty IsHyphenationEnabledProperty;
    public readonly static System.Windows.DependencyProperty LineHeightProperty;
    public readonly static System.Windows.DependencyProperty LineStackingStrategyProperty;
    public readonly static System.Windows.DependencyProperty MarginProperty;
    public readonly static System.Windows.DependencyProperty PaddingProperty;
    public readonly static System.Windows.DependencyProperty TextAlignmentProperty;
    #endregion
  }
}
