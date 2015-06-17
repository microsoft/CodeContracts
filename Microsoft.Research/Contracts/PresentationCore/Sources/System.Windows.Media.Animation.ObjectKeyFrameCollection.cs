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

// File System.Windows.Media.Animation.ObjectKeyFrameCollection.cs
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


namespace System.Windows.Media.Animation
{
  public partial class ObjectKeyFrameCollection : System.Windows.Freezable, System.Collections.IList, System.Collections.ICollection, System.Collections.IEnumerable
  {
    #region Methods and constructors
    public int Add(ObjectKeyFrame keyFrame)
    {
      return default(int);
    }

    public void Clear()
    {
    }

    public System.Windows.Media.Animation.ObjectKeyFrameCollection Clone()
    {
      return default(System.Windows.Media.Animation.ObjectKeyFrameCollection);
    }

    protected override void CloneCore(System.Windows.Freezable sourceFreezable)
    {
    }

    protected override void CloneCurrentValueCore(System.Windows.Freezable sourceFreezable)
    {
    }

    public bool Contains(ObjectKeyFrame keyFrame)
    {
      return default(bool);
    }

    public void CopyTo(ObjectKeyFrame[] array, int index)
    {
    }

    protected override System.Windows.Freezable CreateInstanceCore()
    {
      return default(System.Windows.Freezable);
    }

    protected override bool FreezeCore(bool isChecking)
    {
      return default(bool);
    }

    protected override void GetAsFrozenCore(System.Windows.Freezable sourceFreezable)
    {
    }

    protected override void GetCurrentValueAsFrozenCore(System.Windows.Freezable sourceFreezable)
    {
    }

    public System.Collections.IEnumerator GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }

    public int IndexOf(ObjectKeyFrame keyFrame)
    {
      return default(int);
    }

    public void Insert(int index, ObjectKeyFrame keyFrame)
    {
    }

    public ObjectKeyFrameCollection()
    {
    }

    public void Remove(ObjectKeyFrame keyFrame)
    {
    }

    public void RemoveAt(int index)
    {
    }

    void System.Collections.ICollection.CopyTo(Array array, int index)
    {
    }

    int System.Collections.IList.Add(Object keyFrame)
    {
      return default(int);
    }

    bool System.Collections.IList.Contains(Object keyFrame)
    {
      return default(bool);
    }

    int System.Collections.IList.IndexOf(Object keyFrame)
    {
      return default(int);
    }

    void System.Collections.IList.Insert(int index, Object keyFrame)
    {
    }

    void System.Collections.IList.Remove(Object keyFrame)
    {
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

    public static System.Windows.Media.Animation.ObjectKeyFrameCollection Empty
    {
      get
      {
        return default(System.Windows.Media.Animation.ObjectKeyFrameCollection);
      }
    }

    public bool IsFixedSize
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsSynchronized
    {
      get
      {
        return default(bool);
      }
    }

    public ObjectKeyFrame this [int index]
    {
      get
      {
        return default(ObjectKeyFrame);
      }
      set
      {
      }
    }

    public Object SyncRoot
    {
      get
      {
        return default(Object);
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
