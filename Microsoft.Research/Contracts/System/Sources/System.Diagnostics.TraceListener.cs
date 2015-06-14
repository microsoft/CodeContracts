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

// File System.Diagnostics.TraceListener.cs
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


namespace System.Diagnostics
{
  abstract public partial class TraceListener : MarshalByRefObject, IDisposable
  {
    #region Methods and constructors
    public virtual new void Close()
    {
    }

    protected virtual new void Dispose(bool disposing)
    {
    }

    public void Dispose()
    {
    }

    public virtual new void Fail(string message)
    {
    }

    public virtual new void Fail(string message, string detailMessage)
    {
    }

    public virtual new void Flush()
    {
    }

    protected internal virtual new string[] GetSupportedAttributes()
    {
      return default(string[]);
    }

    public virtual new void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, Object[] data)
    {
    }

    public virtual new void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, Object data)
    {
    }

    public virtual new void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
    {
    }

    public virtual new void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id)
    {
    }

    public virtual new void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, Object[] args)
    {
    }

    protected TraceListener()
    {
    }

    protected TraceListener(string name)
    {
    }

    public virtual new void TraceTransfer(TraceEventCache eventCache, string source, int id, string message, Guid relatedActivityId)
    {
    }

    public virtual new void Write(Object o)
    {
    }

    public virtual new void Write(Object o, string category)
    {
    }

    public abstract void Write(string message);

    public virtual new void Write(string message, string category)
    {
    }

    protected virtual new void WriteIndent()
    {
    }

    public virtual new void WriteLine(Object o)
    {
    }

    public virtual new void WriteLine(string message, string category)
    {
    }

    public abstract void WriteLine(string message);

    public virtual new void WriteLine(Object o, string category)
    {
    }
    #endregion

    #region Properties and indexers
    public System.Collections.Specialized.StringDictionary Attributes
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Collections.Specialized.StringDictionary>() != null);

        return default(System.Collections.Specialized.StringDictionary);
      }
    }

    public TraceFilter Filter
    {
      get
      {
        return default(TraceFilter);
      }
      set
      {
      }
    }

    public int IndentLevel
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public int IndentSize
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public virtual new bool IsThreadSafe
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new string Name
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    protected bool NeedIndent
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public TraceOptions TraceOutputOptions
    {
      get
      {
        return default(TraceOptions);
      }
      set
      {
      }
    }
    #endregion
  }
}
