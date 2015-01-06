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

// File System.Web.Routing.RouteValueDictionary.cs
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


namespace System.Web.Routing
{
  public partial class RouteValueDictionary : IDictionary<string, Object>, ICollection<KeyValuePair<string, Object>>, IEnumerable<KeyValuePair<string, Object>>, System.Collections.IEnumerable
  {
    #region Methods and constructors
    public void Add(string key, Object value)
    {
    }

    public void Clear()
    {
    }

    public bool ContainsKey(string key)
    {
      return default(bool);
    }

    public bool ContainsValue(Object value)
    {
      return default(bool);
    }

    public Dictionary<string, Object>.Enumerator GetEnumerator()
    {
      return default(Dictionary<string, Object>.Enumerator);
    }

    public bool Remove(string key)
    {
      return default(bool);
    }

    public RouteValueDictionary()
    {
    }

    public RouteValueDictionary(Object values)
    {
    }

    public RouteValueDictionary(IDictionary<string, Object> dictionary)
    {
    }

    void System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.Add(KeyValuePair<string, Object> item)
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

    IEnumerator<KeyValuePair<string, Object>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.GetEnumerator()
    {
      return default(IEnumerator<KeyValuePair<string, Object>>);
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }

    public bool TryGetValue(string key, out Object value)
    {
      value = default(Object);

      return default(bool);
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

    public Object this [string key]
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public Dictionary<string, Object>.KeyCollection Keys
    {
      get
      {
        return default(Dictionary<string, Object>.KeyCollection);
      }
    }

    bool System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.IsReadOnly
    {
      get
      {
        return default(bool);
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

    public Dictionary<string, Object>.ValueCollection Values
    {
      get
      {
        return default(Dictionary<string, Object>.ValueCollection);
      }
    }
    #endregion
  }
}
