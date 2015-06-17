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

// File System.Windows.Ink.StrokeCollection.cs
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
  public partial class StrokeCollection : System.Collections.ObjectModel.Collection<Stroke>, System.ComponentModel.INotifyPropertyChanged, System.Collections.Specialized.INotifyCollectionChanged
  {
    #region Methods and constructors
    public void Add(System.Windows.Ink.StrokeCollection strokes)
    {
    }

    public void AddPropertyData(Guid propertyDataId, Object propertyData)
    {
    }

    protected sealed override void ClearItems()
    {
    }

    public void Clip(System.Windows.Rect bounds)
    {
    }

    public void Clip(IEnumerable<System.Windows.Point> lassoPoints)
    {
    }

    public virtual new System.Windows.Ink.StrokeCollection Clone()
    {
      return default(System.Windows.Ink.StrokeCollection);
    }

    public bool ContainsPropertyData(Guid propertyDataId)
    {
      return default(bool);
    }

    public void Draw(System.Windows.Media.DrawingContext context)
    {
    }

    public void Erase(System.Windows.Rect bounds)
    {
    }

    public void Erase(IEnumerable<System.Windows.Point> eraserPath, StylusShape eraserShape)
    {
    }

    public void Erase(IEnumerable<System.Windows.Point> lassoPoints)
    {
    }

    public System.Windows.Rect GetBounds()
    {
      return default(System.Windows.Rect);
    }

    public IncrementalLassoHitTester GetIncrementalLassoHitTester(int percentageWithinLasso)
    {
      return default(IncrementalLassoHitTester);
    }

    public IncrementalStrokeHitTester GetIncrementalStrokeHitTester(StylusShape eraserShape)
    {
      return default(IncrementalStrokeHitTester);
    }

    public Object GetPropertyData(Guid propertyDataId)
    {
      return default(Object);
    }

    public Guid[] GetPropertyDataIds()
    {
      return default(Guid[]);
    }

    public System.Windows.Ink.StrokeCollection HitTest(System.Windows.Rect bounds, int percentageWithinBounds)
    {
      return default(System.Windows.Ink.StrokeCollection);
    }

    public System.Windows.Ink.StrokeCollection HitTest(System.Windows.Point point)
    {
      return default(System.Windows.Ink.StrokeCollection);
    }

    public System.Windows.Ink.StrokeCollection HitTest(IEnumerable<System.Windows.Point> lassoPoints, int percentageWithinLasso)
    {
      return default(System.Windows.Ink.StrokeCollection);
    }

    public System.Windows.Ink.StrokeCollection HitTest(System.Windows.Point point, double diameter)
    {
      return default(System.Windows.Ink.StrokeCollection);
    }

    public System.Windows.Ink.StrokeCollection HitTest(IEnumerable<System.Windows.Point> path, StylusShape stylusShape)
    {
      return default(System.Windows.Ink.StrokeCollection);
    }

    public int IndexOf(Stroke stroke)
    {
      return default(int);
    }

    protected sealed override void InsertItem(int index, Stroke stroke)
    {
    }

    protected virtual new void OnPropertyChanged(System.ComponentModel.PropertyChangedEventArgs e)
    {
    }

    protected virtual new void OnPropertyDataChanged(PropertyDataChangedEventArgs e)
    {
    }

    protected virtual new void OnStrokesChanged(StrokeCollectionChangedEventArgs e)
    {
    }

    public void Remove(System.Windows.Ink.StrokeCollection strokes)
    {
    }

    protected sealed override void RemoveItem(int index)
    {
    }

    public void RemovePropertyData(Guid propertyDataId)
    {
    }

    public void Replace(System.Windows.Ink.StrokeCollection strokesToReplace, System.Windows.Ink.StrokeCollection strokesToReplaceWith)
    {
    }

    public void Replace(Stroke strokeToReplace, System.Windows.Ink.StrokeCollection strokesToReplaceWith)
    {
    }

    public virtual new void Save(Stream stream, bool compress)
    {
    }

    public void Save(Stream stream)
    {
    }

    protected sealed override void SetItem(int index, Stroke stroke)
    {
    }

    public StrokeCollection(IEnumerable<Stroke> strokes)
    {
    }

    public StrokeCollection(Stream stream)
    {
    }

    public StrokeCollection()
    {
    }

    public void Transform(System.Windows.Media.Matrix transformMatrix, bool applyToStylusTip)
    {
    }
    #endregion

    #region Events
    event System.Collections.Specialized.NotifyCollectionChangedEventHandler System.Collections.Specialized.INotifyCollectionChanged.CollectionChanged
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

    public event StrokeCollectionChangedEventHandler StrokesChanged
    {
      add
      {
      }
      remove
      {
      }
    }
    #endregion

    #region Fields
    public readonly static string InkSerializedFormat;
    #endregion
  }
}
