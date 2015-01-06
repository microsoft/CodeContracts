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

// File System.Windows.Input.StylusPlugIns.DynamicRenderer.cs
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


namespace System.Windows.Input.StylusPlugIns
{
  public partial class DynamicRenderer : StylusPlugIn
  {
    #region Methods and constructors
    public DynamicRenderer()
    {
    }

    protected System.Windows.Threading.Dispatcher GetDispatcher()
    {
      return default(System.Windows.Threading.Dispatcher);
    }

    protected override void OnAdded()
    {
    }

    protected virtual new void OnDraw(System.Windows.Media.DrawingContext drawingContext, System.Windows.Input.StylusPointCollection stylusPoints, System.Windows.Media.Geometry geometry, System.Windows.Media.Brush fillBrush)
    {
    }

    protected virtual new void OnDrawingAttributesReplaced()
    {
    }

    protected override void OnEnabledChanged()
    {
    }

    protected override void OnIsActiveForInputChanged()
    {
    }

    protected override void OnRemoved()
    {
    }

    protected override void OnStylusDown(RawStylusInput rawStylusInput)
    {
    }

    protected override void OnStylusDownProcessed(Object callbackData, bool targetVerified)
    {
    }

    protected override void OnStylusEnter(RawStylusInput rawStylusInput, bool confirmed)
    {
    }

    protected override void OnStylusLeave(RawStylusInput rawStylusInput, bool confirmed)
    {
    }

    protected override void OnStylusMove(RawStylusInput rawStylusInput)
    {
    }

    protected override void OnStylusUp(RawStylusInput rawStylusInput)
    {
    }

    protected override void OnStylusUpProcessed(Object callbackData, bool targetVerified)
    {
    }

    public virtual new void Reset(System.Windows.Input.StylusDevice stylusDevice, System.Windows.Input.StylusPointCollection stylusPoints)
    {
    }
    #endregion

    #region Properties and indexers
    public System.Windows.Ink.DrawingAttributes DrawingAttributes
    {
      get
      {
        return default(System.Windows.Ink.DrawingAttributes);
      }
      set
      {
      }
    }

    public System.Windows.Media.Visual RootVisual
    {
      get
      {
        return default(System.Windows.Media.Visual);
      }
    }
    #endregion
  }
}
