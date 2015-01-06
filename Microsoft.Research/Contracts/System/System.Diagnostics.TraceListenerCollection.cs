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

#if !SILVERLIGHT

using System.Diagnostics.Contracts;
using System;

namespace System.Diagnostics
{

  public class TraceListenerCollection : System.Collections.IList, System.Collections.ICollection
  {
    internal TraceListenerCollection() { }
    public TraceListener this[int i]
    {
      get
      {
        Contract.Requires(i >= 0);
        Contract.Requires(i < Count);
        Contract.Ensures(Contract.Result<TraceListener>() != null);

        return default(TraceListener);
      }
      set
      {
        Contract.Requires(i >= 0);
        Contract.Requires(i < Count);
        Contract.Requires(value != null);
      }
    }

    extern public virtual int Count
    {
      get;
    }

    public void AddRange(TraceListenerCollection value)
    {
      Contract.Requires(value != null);

    }
    public void AddRange(TraceListener[] value)
    {
      Contract.Requires(value != null);

    }
    public int Add(TraceListener listener)
    {
      return default(int);
    }

    #region IList Members

    int Collections.IList.Add(object value)
    {
      throw new NotImplementedException();
    }


    bool Collections.IList.Contains(object value)
    {
      throw new NotImplementedException();
    }

    int Collections.IList.IndexOf(object value)
    {
      throw new NotImplementedException();
    }

    void Collections.IList.Insert(int index, object value)
    {
      throw new NotImplementedException();
    }

    bool Collections.IList.IsFixedSize
    {
      get { throw new NotImplementedException(); }
    }

    bool Collections.IList.IsReadOnly
    {
      get { throw new NotImplementedException(); }
    }

    void Collections.IList.Remove(object value)
    {
      throw new NotImplementedException();
    }


    object Collections.IList.this[int index]
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    #endregion

    #region ICollection Members

    void Collections.ICollection.CopyTo(Array array, int index)
    {
      throw new NotImplementedException();
    }


    bool Collections.ICollection.IsSynchronized
    {
      get { throw new NotImplementedException(); }
    }

    object Collections.ICollection.SyncRoot
    {
      get { throw new NotImplementedException(); }
    }

    #endregion


    #region IList Members


    public void Clear()
    {
      throw new NotImplementedException();
    }

    public void RemoveAt(int index)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region IEnumerable Members

    public Collections.IEnumerator GetEnumerator()
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
#endif