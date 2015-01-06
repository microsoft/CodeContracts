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



  public class DoubleTable<KEY1, KEY2, ELEM> : Dictionary<KEY1, Dictionary<KEY2, ELEM>>
  {
    public DoubleTable() : base() { }

    public void Add(KEY1 key1, KEY2 key2, ELEM element) {
      Dictionary<KEY2, ELEM>/*?*/ table;
      if (!(this.TryGetValue(key1, out table))) {
        table = new Dictionary<KEY2, ELEM>();
        this.Add(key1, table);
      }
      //^ assert table != null;
      table.Add(key2, element);
    }

    public ELEM this[KEY1 key1, KEY2 key2]
    {
      get
      {
        return this[key1][key2];
      }
      set
      {
        Dictionary<KEY2, ELEM>/*?*/ table;
        if (!(this.TryGetValue(key1, out table))) {
          table = new Dictionary<KEY2, ELEM>();
          this.Add(key1, table);
        }
        //^ assert table != null;
        table[key2] = value;
      }
    }


    public IEnumerable<KEY1> Keys1 { get { return this.Keys; } }

    public IEnumerable<KEY2> Keys2(KEY1 key1)
    {
      Dictionary<KEY2, ELEM>/*?*/ range;
      if (this.TryGetValue(key1, out range)) {
        //^ assume range != null;
        return range.Keys;
      }
      return new KEY2[0];
    }

    public bool ContainsKey(KEY1 key1, KEY2 key2)
    {
      Dictionary<KEY2, ELEM>/*?*/ range;
      if (this.TryGetValue(key1, out range)) {
        //^ assume range != null;
        return range.ContainsKey(key2);
      }
      return false;
    }

    public bool TryGetValue(KEY1 key1, KEY2 key2, out ELEM/*?*/ result)
    {
      Dictionary<KEY2, ELEM>/*?*/ range;
      if (this.TryGetValue(key1, out range)) {
        //^ assume range != null;
        return range.TryGetValue(key2, out result);
      }
      result = default(ELEM);
      return false;
    }
  }

}