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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Microsoft.VisualStudio.Text.Editor;

namespace Adornments {
  /*
   * This class has no public entry points so it doesn't need a "Logger". If any public entry points are added, we must add a logger!
   */
#if false
  public class PurityAdornment : IAdornment {
    public ITrackingSpan Span { get; set; }
    public FrameworkElement Visual {
      get { return _visual; }
    }
    FrameworkElement _visual;
    public bool IsInCollapsedRegion { get; set; }
    public int CollapsedRegionDepth { get; set; }
    public bool IsClosedByUser { get; set; }
    public bool IsCaretFocus { get; set; }
    public LineTransformBehavior LineTransformBehavior {
      get {
        return LineTransformBehavior.Above;
      }
    }
    VSTextProperties _vsTextProperties;

    public PurityAdornment(VSTextProperties vsTextProperties, ContractAdornment contractAdornment) {
      _vsTextProperties = vsTextProperties;

      var purityBlock = new TextBlock();
      purityBlock.Inlines.Add(new Run("["));
      var pureRun = new Run("Pure");
      pureRun.Foreground = _vsTextProperties.TypeColor;
      purityBlock.Inlines.Add(pureRun);
      purityBlock.Inlines.Add(new Run("]"));
      _visual = purityBlock;

      //contractAdornment.Unfocused += OnUnfocused;
      //contractAdornment.Focused += OnFocused;
      //contractAdornment.Collapsed += OnCollapsed;
      //contractAdornment.Expanded += OnExpanded;
    }

    void OnExpanded() {
      Visual.Visibility = Visibility.Visible;
    }
    void OnCollapsed() {
      Visual.Visibility = Visibility.Collapsed;
    }

    void OnFocused() {
      Visual.Opacity = 1d;
    }
    void OnUnfocused() {
      Visual.Opacity = 0.5d;
    }
  }
#endif
}
