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

// File System.Diagnostics.Trace.cs
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
  sealed public partial class Trace
  {
    #region Methods and constructors
    public static void Assert(bool condition)
    {
    }

    public static void Assert(bool condition, string message, string detailMessage)
    {
    }

    public static void Assert(bool condition, string message)
    {
    }

    public static void Close()
    {
    }

    public static void Fail(string message)
    {
    }

    public static void Fail(string message, string detailMessage)
    {
    }

    public static void Flush()
    {
    }

    public static void Indent()
    {
    }

    public static void Refresh()
    {
    }

    internal Trace()
    {
    }

    public static void TraceError(string message)
    {
    }

    public static void TraceError(string format, Object[] args)
    {
    }

    public static void TraceInformation(string message)
    {
    }

    public static void TraceInformation(string format, Object[] args)
    {
    }

    public static void TraceWarning(string message)
    {
    }

    public static void TraceWarning(string format, Object[] args)
    {
    }

    public static void Unindent()
    {
    }

    public static void Write(string message)
    {
    }

    public static void Write(Object value)
    {
    }

    public static void Write(Object value, string category)
    {
    }

    public static void Write(string message, string category)
    {
    }

    public static void WriteIf(bool condition, Object value)
    {
    }

    public static void WriteIf(bool condition, Object value, string category)
    {
    }

    public static void WriteIf(bool condition, string message)
    {
    }

    public static void WriteIf(bool condition, string message, string category)
    {
    }

    public static void WriteLine(Object value, string category)
    {
    }

    public static void WriteLine(string message, string category)
    {
    }

    public static void WriteLine(string message)
    {
    }

    public static void WriteLine(Object value)
    {
    }

    public static void WriteLineIf(bool condition, string message, string category)
    {
    }

    public static void WriteLineIf(bool condition, Object value)
    {
    }

    public static void WriteLineIf(bool condition, string message)
    {
    }

    public static void WriteLineIf(bool condition, Object value, string category)
    {
    }
    #endregion

    #region Properties and indexers
    public static bool AutoFlush
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public static CorrelationManager CorrelationManager
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Diagnostics.CorrelationManager>() != null);

        return default(CorrelationManager);
      }
    }

    public static int IndentLevel
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public static int IndentSize
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public static TraceListenerCollection Listeners
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Diagnostics.TraceListenerCollection>() != null);

        return default(TraceListenerCollection);
      }
    }

    public static bool UseGlobalLock
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }
    #endregion
  }
}
