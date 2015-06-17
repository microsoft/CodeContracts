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

// File System.Windows.Documents.AnchoredBlock.cs
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
  abstract public partial class AnchoredBlock : Inline
  {
    #region Methods and constructors
    protected AnchoredBlock(Block block, TextPointer insertionPosition)
    {
    }

    public bool ShouldSerializeBlocks(System.Windows.Markup.XamlDesignerSerializationManager manager)
    {
      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public BlockCollection Blocks
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Documents.BlockCollection>() != null);

        return default(BlockCollection);
      }
    }

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
    public readonly static System.Windows.DependencyProperty LineHeightProperty;
    public readonly static System.Windows.DependencyProperty LineStackingStrategyProperty;
    public readonly static System.Windows.DependencyProperty MarginProperty;
    public readonly static System.Windows.DependencyProperty PaddingProperty;
    public readonly static System.Windows.DependencyProperty TextAlignmentProperty;
    #endregion
  }
}
