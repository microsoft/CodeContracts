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

// File System.Windows.Media.DoubleCollection.cs
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


namespace System.Windows.Media
{
  sealed public partial class DoubleCollection : System.Windows.Freezable, IFormattable, System.Collections.IList, System.Collections.ICollection, IList<double>, ICollection<double>, IEnumerable<double>, System.Collections.IEnumerable
  {
    #region Methods and constructors
    public void Add(double value)
    {
    }

    public void Clear()
    {
    }

    public DoubleCollection Clone()
    {
      return default(DoubleCollection);
    }

    protected override void CloneCore(System.Windows.Freezable source)
    {
    }

    public DoubleCollection CloneCurrentValue()
    {
      return default(DoubleCollection);
    }

    protected override void CloneCurrentValueCore(System.Windows.Freezable source)
    {
    }

    public bool Contains(double value)
    {
      return default(bool);
    }

    public void CopyTo(double[] array, int index)
    {
    }

    protected override System.Windows.Freezable CreateInstanceCore()
    {
      return default(System.Windows.Freezable);
    }

    public DoubleCollection(int capacity)
    {
    }

    public DoubleCollection(IEnumerable<double> collection)
    {
    }

    public DoubleCollection()
    {
    }

    protected override void GetAsFrozenCore(System.Windows.Freezable source)
    {
    }

    protected override void GetCurrentValueAsFrozenCore(System.Windows.Freezable source)
    {
    }

    public DoubleCollection.Enumerator GetEnumerator()
    {
      return default(DoubleCollection.Enumerator);
    }

    public int IndexOf(double value)
    {
      return default(int);
    }

    public void Insert(int index, double value)
    {
    }

    public static DoubleCollection Parse(string source)
    {
      return default(DoubleCollection);
    }

    public bool Remove(double value)
    {
      return default(bool);
    }

    public void RemoveAt(int index)
    {
    }

    IEnumerator<double> System.Collections.Generic.IEnumerable<System.Double>.GetEnumerator()
    {
      return default(IEnumerator<double>);
    }

    void System.Collections.ICollection.CopyTo(Array array, int index)
    {
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }

    int System.Collections.IList.Add(Object value)
    {
      return default(int);
    }

    bool System.Collections.IList.Contains(Object value)
    {
      return default(bool);
    }

    int System.Collections.IList.IndexOf(Object value)
    {
      return default(int);
    }

    void System.Collections.IList.Insert(int index, Object value)
    {
    }

    void System.Collections.IList.Remove(Object value)
    {
    }

    string System.IFormattable.ToString(string format, IFormatProvider provider)
    {
      return default(string);
    }

    public string ToString(IFormatProvider provider)
    {
      return default(string);
    }
    #endregion

    #region Properties and indexers
    public int Count
    {
      get
      {
        return default(int);
      }
    }

    public double this [int index]
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    bool System.Collections.Generic.ICollection<System.Double>.IsReadOnly
    {
      get
      {
        return default(bool);
      }
    }

    bool System.Collections.ICollection.IsSynchronized
    {
      get
      {
        return default(bool);
      }
    }

    Object System.Collections.ICollection.SyncRoot
    {
      get
      {
        return default(Object);
      }
    }

    bool System.Collections.IList.IsFixedSize
    {
      get
      {
        return default(bool);
      }
    }

    bool System.Collections.IList.IsReadOnly
    {
      get
      {
        return default(bool);
      }
    }

    Object System.Collections.IList.this [int index]
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }
    #endregion
  }
}
