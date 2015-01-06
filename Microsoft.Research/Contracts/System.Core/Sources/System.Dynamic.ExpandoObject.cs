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

// File System.Dynamic.ExpandoObject.cs
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


namespace System.Dynamic
{
  sealed public partial class ExpandoObject : IDynamicMetaObjectProvider, IDictionary<string, Object>, ICollection<KeyValuePair<string, Object>>, IEnumerable<KeyValuePair<string, Object>>, System.Collections.IEnumerable, System.ComponentModel.INotifyPropertyChanged
  {
    #region Methods and constructors
    public ExpandoObject()
    {
    }

    void System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.Add(KeyValuePair<string, Object> item)
    {
    }

    void System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.Clear()
    {
    }

    bool System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.Contains(KeyValuePair<string, Object> item)
    {
      return default(bool);
    }

    void System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.CopyTo(KeyValuePair<string, Object>[] array, int arrayIndex)
    {
    }

    bool System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.Remove(KeyValuePair<string, Object> item)
    {
      return default(bool);
    }

    void System.Collections.Generic.IDictionary<System.String,System.Object>.Add(string key, Object value)
    {
    }

    bool System.Collections.Generic.IDictionary<System.String,System.Object>.ContainsKey(string key)
    {
      return default(bool);
    }

    bool System.Collections.Generic.IDictionary<System.String,System.Object>.Remove(string key)
    {
      return default(bool);
    }

    bool System.Collections.Generic.IDictionary<System.String,System.Object>.TryGetValue(string key, out Object value)
    {
      value = default(Object);

      return default(bool);
    }

    IEnumerator<KeyValuePair<string, Object>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.GetEnumerator()
    {
      return default(IEnumerator<KeyValuePair<string, Object>>);
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }

    DynamicMetaObject System.Dynamic.IDynamicMetaObjectProvider.GetMetaObject(System.Linq.Expressions.Expression parameter)
    {
      return default(DynamicMetaObject);
    }
    #endregion

    #region Properties and indexers
    int System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.Count
    {
      get
      {
        return default(int);
      }
    }

    bool System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.IsReadOnly
    {
      get
      {
        return default(bool);
      }
    }

    Object System.Collections.Generic.IDictionary<System.String,System.Object>.this [string key]
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    ICollection<string> System.Collections.Generic.IDictionary<System.String,System.Object>.Keys
    {
      get
      {
        return default(ICollection<string>);
      }
    }

    ICollection<Object> System.Collections.Generic.IDictionary<System.String,System.Object>.Values
    {
      get
      {
        return default(ICollection<Object>);
      }
    }
    #endregion

    #region Events
    event System.ComponentModel.PropertyChangedEventHandler System.ComponentModel.INotifyPropertyChanged.PropertyChanged
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
