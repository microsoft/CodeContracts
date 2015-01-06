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
using System.Text;

namespace Microsoft.Research.DataStructures
{
  public struct TypedKey<T>
  {
    string key;
    public TypedKey(string key)
    {
      this.key = key;
    }
  }

  public interface ITypedProperties
  {
    bool Contains<T>(TypedKey<T> key);

    void Add<T>(TypedKey<T> key, T value);

    bool TryGetValue<T>(TypedKey<T> key, out T value);
  }

  [Serializable]
  public class TypedProperties : ITypedProperties
  {
    System.Collections.Hashtable table = new System.Collections.Hashtable();

    public bool Contains<T>(TypedKey<T> key)
    {
      return table.Contains(key);
    }

    public void Add<T>(TypedKey<T> key, T value)
    {
      this.table.Add(key, value);
    }

    public bool TryGetValue<T>(TypedKey<T> key, out T value)
    {
      object result = table[key];
      if (result == null)
      {
        value = default(T); return false;
      }
      value = (T)result;
      return true;
    }
  }
}
