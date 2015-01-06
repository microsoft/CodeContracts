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
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace System.Web.UI
{
  public class ControlCollection
  {
    public ControlCollection(Control owner)
    {
      Contract.Requires(owner != null);
    }

    public virtual void Add(Control child)
    {
      Contract.Requires(child != null);
    }
    public virtual void AddAt(int index, Control child)
    {
      Contract.Requires(child != null);
      Contract.Requires(index >= -1);
      Contract.Requires(index <= Count);
    }
    [Pure]
    public virtual bool Contains(Control c)
    {
      Contract.Requires(c != null);
      return default(bool);
    }
    public virtual int IndexOf(Control value)
    {
      Contract.Requires(value != null);
      Contract.Ensures(Contract.Result<int>() >= -1);
      Contract.Ensures(Contract.Result<int>() < Count);
      return default(int);
    }
    public virtual void Remove(Control value)
    {
      Contract.Requires(value != null);
    }

    extern public virtual int Count { get; }

    public virtual Control this[int index]
    {
      get
      {
        Contract.Ensures(Contract.Result<Control>() != null);
        return default(Control);
      }
    }
    protected Control Owner
    {
      get
      {
        Contract.Ensures(Contract.Result<Control>() != null);
        return default(Control);
      }
    }

  }
}
