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

namespace Microsoft.Research.DataStructures
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics;
  using System.Text;
  using System.IO;


  public class Relation<S, T> {
    private DoubleTable<S, T, bool> representation = new DoubleTable<S,T,bool>();

    public void Add(S s, T t) {
      representation.Add(s, t, true);
    }

    private static readonly ICollection<T> EmptyRange = new T[0];

    public ICollection<T> this[S key] {
      get {
        Dictionary<T, bool>/*?*/ range;
        if (representation.TryGetValue(key, out range)) {
          //^ assume range != null;
          return range.Keys;
        }
        return EmptyRange;
      }
    }
  }


}