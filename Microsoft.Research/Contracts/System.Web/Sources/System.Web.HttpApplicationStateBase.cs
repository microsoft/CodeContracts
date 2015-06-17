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

// File System.Web.HttpApplicationStateBase.cs
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


namespace System.Web
{
  abstract public partial class HttpApplicationStateBase : System.Collections.Specialized.NameObjectCollectionBase, System.Collections.ICollection, System.Collections.IEnumerable
  {
    #region Methods and constructors
    public virtual new void Add(string name, Object value)
    {
    }

    public virtual new void Clear()
    {
    }

    public virtual new void CopyTo(Array array, int index)
    {
    }

    public virtual new Object Get(string name)
    {
      return default(Object);
    }

    public virtual new Object Get(int index)
    {
      return default(Object);
    }

    public override System.Collections.IEnumerator GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }

    public virtual new string GetKey(int index)
    {
      return default(string);
    }

    protected HttpApplicationStateBase()
    {
    }

    public virtual new void Lock()
    {
    }

    public virtual new void Remove(string name)
    {
    }

    public virtual new void RemoveAll()
    {
    }

    public virtual new void RemoveAt(int index)
    {
    }

    public virtual new void Set(string name, Object value)
    {
    }

    public virtual new void UnLock()
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new string[] AllKeys
    {
      get
      {
        return default(string[]);
      }
    }

    public virtual new System.Web.HttpApplicationStateBase Contents
    {
      get
      {
        return default(System.Web.HttpApplicationStateBase);
      }
    }

    public override int Count
    {
      get
      {
        return default(int);
      }
    }

    public virtual new bool IsSynchronized
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new Object this [string name]
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public virtual new Object this [int index]
    {
      get
      {
        return default(Object);
      }
    }

    public virtual new HttpStaticObjectsCollectionBase StaticObjects
    {
      get
      {
        return default(HttpStaticObjectsCollectionBase);
      }
    }

    public virtual new Object SyncRoot
    {
      get
      {
        return default(Object);
      }
    }
    #endregion
  }
}
