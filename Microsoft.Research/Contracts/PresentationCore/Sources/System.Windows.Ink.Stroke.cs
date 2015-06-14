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

// File System.Windows.Ink.Stroke.cs
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


namespace System.Windows.Ink
{
  public partial class Stroke : System.ComponentModel.INotifyPropertyChanged
  {
    #region Methods and constructors
    public void AddPropertyData(Guid propertyDataId, Object propertyData)
    {
    }

    public virtual new System.Windows.Ink.Stroke Clone()
    {
      return default(System.Windows.Ink.Stroke);
    }

    public bool ContainsPropertyData(Guid propertyDataId)
    {
      return default(bool);
    }

    public void Draw(System.Windows.Media.DrawingContext drawingContext, DrawingAttributes drawingAttributes)
    {
    }

    public void Draw(System.Windows.Media.DrawingContext context)
    {
    }

    protected virtual new void DrawCore(System.Windows.Media.DrawingContext drawingContext, DrawingAttributes drawingAttributes)
    {
    }

    public System.Windows.Input.StylusPointCollection GetBezierStylusPoints()
    {
      return default(System.Windows.Input.StylusPointCollection);
    }

    public virtual new System.Windows.Rect GetBounds()
    {
      return default(System.Windows.Rect);
    }

    public StrokeCollection GetClipResult(IEnumerable<System.Windows.Point> lassoPoints)
    {
      return default(StrokeCollection);
    }

    public StrokeCollection GetClipResult(System.Windows.Rect bounds)
    {
      return default(StrokeCollection);
    }

    public StrokeCollection GetEraseResult(IEnumerable<System.Windows.Point> eraserPath, StylusShape eraserShape)
    {
      return default(StrokeCollection);
    }

    public StrokeCollection GetEraseResult(System.Windows.Rect bounds)
    {
      return default(StrokeCollection);
    }

    public StrokeCollection GetEraseResult(IEnumerable<System.Windows.Point> lassoPoints)
    {
      return default(StrokeCollection);
    }

    public System.Windows.Media.Geometry GetGeometry()
    {
      return default(System.Windows.Media.Geometry);
    }

    public System.Windows.Media.Geometry GetGeometry(DrawingAttributes drawingAttributes)
    {
      return default(System.Windows.Media.Geometry);
    }

    public Object GetPropertyData(Guid propertyDataId)
    {
      return default(Object);
    }

    public Guid[] GetPropertyDataIds()
    {
      return default(Guid[]);
    }

    public bool HitTest(System.Windows.Point point)
    {
      return default(bool);
    }

    public bool HitTest(IEnumerable<System.Windows.Point> lassoPoints, int percentageWithinLasso)
    {
      return default(bool);
    }

    public bool HitTest(System.Windows.Rect bounds, int percentageWithinBounds)
    {
      return default(bool);
    }

    public bool HitTest(IEnumerable<System.Windows.Point> path, StylusShape stylusShape)
    {
      return default(bool);
    }

    public bool HitTest(System.Windows.Point point, double diameter)
    {
      return default(bool);
    }

    protected virtual new void OnDrawingAttributesChanged(PropertyDataChangedEventArgs e)
    {
    }

    protected virtual new void OnDrawingAttributesReplaced(DrawingAttributesReplacedEventArgs e)
    {
    }

    protected virtual new void OnInvalidated(EventArgs e)
    {
    }

    protected virtual new void OnPropertyChanged(System.ComponentModel.PropertyChangedEventArgs e)
    {
    }

    protected virtual new void OnPropertyDataChanged(PropertyDataChangedEventArgs e)
    {
    }

    protected virtual new void OnStylusPointsChanged(EventArgs e)
    {
    }

    protected virtual new void OnStylusPointsReplaced(StylusPointsReplacedEventArgs e)
    {
    }

    public void RemovePropertyData(Guid propertyDataId)
    {
    }

    public Stroke(System.Windows.Input.StylusPointCollection stylusPoints, DrawingAttributes drawingAttributes)
    {
    }

    public Stroke(System.Windows.Input.StylusPointCollection stylusPoints)
    {
    }

    public virtual new void Transform(System.Windows.Media.Matrix transformMatrix, bool applyToStylusTip)
    {
    }
    #endregion

    #region Properties and indexers
    public DrawingAttributes DrawingAttributes
    {
      get
      {
        return default(DrawingAttributes);
      }
      set
      {
      }
    }

    public System.Windows.Input.StylusPointCollection StylusPoints
    {
      get
      {
        return default(System.Windows.Input.StylusPointCollection);
      }
      set
      {
      }
    }
    #endregion

    #region Events
    public event PropertyDataChangedEventHandler DrawingAttributesChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public event DrawingAttributesReplacedEventHandler DrawingAttributesReplaced
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler Invalidated
    {
      add
      {
      }
      remove
      {
      }
    }

    event System.ComponentModel.PropertyChangedEventHandler System.ComponentModel.INotifyPropertyChanged.PropertyChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public event PropertyDataChangedEventHandler PropertyDataChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler StylusPointsChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public event StylusPointsReplacedEventHandler StylusPointsReplaced
    {
      add
      {
      }
      remove
      {
      }
    }
    #endregion
  }
}
