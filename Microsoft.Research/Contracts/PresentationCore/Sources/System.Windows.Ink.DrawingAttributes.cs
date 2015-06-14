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

// File System.Windows.Ink.DrawingAttributes.cs
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
  public partial class DrawingAttributes : System.ComponentModel.INotifyPropertyChanged
  {
    #region Methods and constructors
    public static bool operator != (System.Windows.Ink.DrawingAttributes first, System.Windows.Ink.DrawingAttributes second)
    {
      return default(bool);
    }

    public static bool operator == (System.Windows.Ink.DrawingAttributes first, System.Windows.Ink.DrawingAttributes second)
    {
      return default(bool);
    }

    public void AddPropertyData(Guid propertyDataId, Object propertyData)
    {
    }

    public virtual new System.Windows.Ink.DrawingAttributes Clone()
    {
      return default(System.Windows.Ink.DrawingAttributes);
    }

    public bool ContainsPropertyData(Guid propertyDataId)
    {
      return default(bool);
    }

    public DrawingAttributes()
    {
    }

    public override bool Equals(Object o)
    {
      return default(bool);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public Object GetPropertyData(Guid propertyDataId)
    {
      return default(Object);
    }

    public Guid[] GetPropertyDataIds()
    {
      return default(Guid[]);
    }

    protected virtual new void OnAttributeChanged(PropertyDataChangedEventArgs e)
    {
    }

    protected virtual new void OnPropertyChanged(System.ComponentModel.PropertyChangedEventArgs e)
    {
    }

    protected virtual new void OnPropertyDataChanged(PropertyDataChangedEventArgs e)
    {
    }

    public void RemovePropertyData(Guid propertyDataId)
    {
    }
    #endregion

    #region Properties and indexers
    public System.Windows.Media.Color Color
    {
      get
      {
        return default(System.Windows.Media.Color);
      }
      set
      {
      }
    }

    public bool FitToCurve
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public double Height
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public bool IgnorePressure
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool IsHighlighter
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public StylusTip StylusTip
    {
      get
      {
        return default(StylusTip);
      }
      set
      {
      }
    }

    public System.Windows.Media.Matrix StylusTipTransform
    {
      get
      {
        return default(System.Windows.Media.Matrix);
      }
      set
      {
      }
    }

    public double Width
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }
    #endregion

    #region Events
    public event PropertyDataChangedEventHandler AttributeChanged
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
    #endregion

    #region Fields
    public readonly static double MaxHeight;
    public readonly static double MaxWidth;
    public readonly static double MinHeight;
    public readonly static double MinWidth;
    #endregion
  }
}
