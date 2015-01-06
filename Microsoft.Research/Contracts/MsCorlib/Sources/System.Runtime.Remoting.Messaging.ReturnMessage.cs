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

// File System.Runtime.Remoting.Messaging.ReturnMessage.cs
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


namespace System.Runtime.Remoting.Messaging
{
  public partial class ReturnMessage : IMethodReturnMessage, IMethodMessage, IMessage
  {
    #region Methods and constructors
    public Object GetArg(int argNum)
    {
      return default(Object);
    }

    public string GetArgName(int index)
    {
      return default(string);
    }

    public Object GetOutArg(int argNum)
    {
      return default(Object);
    }

    public string GetOutArgName(int index)
    {
      return default(string);
    }

    public ReturnMessage(Object ret, Object[] outArgs, int outArgsCount, LogicalCallContext callCtx, IMethodCallMessage mcm)
    {
    }

    public ReturnMessage(Exception e, IMethodCallMessage mcm)
    {
    }
    #endregion

    #region Properties and indexers
    public int ArgCount
    {
      get
      {
        return default(int);
      }
    }

    public Object[] Args
    {
      get
      {
        return default(Object[]);
      }
    }

    public Exception Exception
    {
      get
      {
        return default(Exception);
      }
    }

    public bool HasVarArgs
    {
      get
      {
        return default(bool);
      }
    }

    public LogicalCallContext LogicalCallContext
    {
      get
      {
        return default(LogicalCallContext);
      }
    }

    public System.Reflection.MethodBase MethodBase
    {
      get
      {
        return default(System.Reflection.MethodBase);
      }
    }

    public string MethodName
    {
      get
      {
        return default(string);
      }
    }

    public Object MethodSignature
    {
      get
      {
        return default(Object);
      }
    }

    public int OutArgCount
    {
      get
      {
        return default(int);
      }
    }

    public Object[] OutArgs
    {
      get
      {
        return default(Object[]);
      }
    }

    public virtual new System.Collections.IDictionary Properties
    {
      get
      {
        return default(System.Collections.IDictionary);
      }
    }

    public virtual new Object ReturnValue
    {
      get
      {
        return default(Object);
      }
    }

    public string TypeName
    {
      get
      {
        return default(string);
      }
    }

    public string Uri
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }
    #endregion
  }
}
