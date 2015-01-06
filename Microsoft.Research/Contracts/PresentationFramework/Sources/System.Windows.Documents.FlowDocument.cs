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

// File System.Windows.Documents.FlowDocument.cs
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
  public partial class FlowDocument : System.Windows.FrameworkContentElement, IDocumentPaginatorSource, IServiceProvider, System.Windows.Markup.IAddChild
  {
    #region Methods and constructors
    public FlowDocument()
    {
    }

    public FlowDocument(Block block)
    {
      Contract.Ensures(0 <= this.Blocks.Count);
    }

    protected override System.Windows.Automation.Peers.AutomationPeer OnCreateAutomationPeer()
    {
      return default(System.Windows.Automation.Peers.AutomationPeer);
    }

    protected sealed override void OnPropertyChanged(System.Windows.DependencyPropertyChangedEventArgs e)
    {
    }

    Object System.IServiceProvider.GetService(Type serviceType)
    {
      return default(Object);
    }

    void System.Windows.Markup.IAddChild.AddChild(Object value)
    {
    }

    void System.Windows.Markup.IAddChild.AddText(string text)
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

    public BlockCollection Blocks
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Documents.BlockCollection>() != null);

        return default(BlockCollection);
      }
    }

    public double ColumnGap
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public System.Windows.Media.Brush ColumnRuleBrush
    {
      get
      {
        return default(System.Windows.Media.Brush);
      }
      set
      {
      }
    }

    public double ColumnRuleWidth
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double ColumnWidth
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public TextPointer ContentEnd
    {
      get
      {
        return default(TextPointer);
      }
    }

    public TextPointer ContentStart
    {
      get
      {
        return default(TextPointer);
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

    public bool IsColumnWidthFlexible
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    protected override bool IsEnabledCore
    {
      get
      {
        return default(bool);
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

    public bool IsOptimalParagraphEnabled
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

    public double MaxPageHeight
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double MaxPageWidth
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double MinPageHeight
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double MinPageWidth
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double PageHeight
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public System.Windows.Thickness PagePadding
    {
      get
      {
        return default(System.Windows.Thickness);
      }
      set
      {
      }
    }

    public double PageWidth
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    DocumentPaginator System.Windows.Documents.IDocumentPaginatorSource.DocumentPaginator
    {
      get
      {
        return default(DocumentPaginator);
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

    public Typography Typography
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Documents.Typography>() != null);

        return default(Typography);
      }
    }
    #endregion

    #region Fields
    public readonly static System.Windows.DependencyProperty BackgroundProperty;
    public readonly static System.Windows.DependencyProperty ColumnGapProperty;
    public readonly static System.Windows.DependencyProperty ColumnRuleBrushProperty;
    public readonly static System.Windows.DependencyProperty ColumnRuleWidthProperty;
    public readonly static System.Windows.DependencyProperty ColumnWidthProperty;
    public readonly static System.Windows.DependencyProperty FlowDirectionProperty;
    public readonly static System.Windows.DependencyProperty FontFamilyProperty;
    public readonly static System.Windows.DependencyProperty FontSizeProperty;
    public readonly static System.Windows.DependencyProperty FontStretchProperty;
    public readonly static System.Windows.DependencyProperty FontStyleProperty;
    public readonly static System.Windows.DependencyProperty FontWeightProperty;
    public readonly static System.Windows.DependencyProperty ForegroundProperty;
    public readonly static System.Windows.DependencyProperty IsColumnWidthFlexibleProperty;
    public readonly static System.Windows.DependencyProperty IsHyphenationEnabledProperty;
    public readonly static System.Windows.DependencyProperty IsOptimalParagraphEnabledProperty;
    public readonly static System.Windows.DependencyProperty LineHeightProperty;
    public readonly static System.Windows.DependencyProperty LineStackingStrategyProperty;
    public readonly static System.Windows.DependencyProperty MaxPageHeightProperty;
    public readonly static System.Windows.DependencyProperty MaxPageWidthProperty;
    public readonly static System.Windows.DependencyProperty MinPageHeightProperty;
    public readonly static System.Windows.DependencyProperty MinPageWidthProperty;
    public readonly static System.Windows.DependencyProperty PageHeightProperty;
    public readonly static System.Windows.DependencyProperty PagePaddingProperty;
    public readonly static System.Windows.DependencyProperty PageWidthProperty;
    public readonly static System.Windows.DependencyProperty TextAlignmentProperty;
    public readonly static System.Windows.DependencyProperty TextEffectsProperty;
    #endregion
  }
}
