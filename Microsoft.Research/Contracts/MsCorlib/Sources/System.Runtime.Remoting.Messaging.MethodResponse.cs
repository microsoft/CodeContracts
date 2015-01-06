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

// File System.Runtime.Remoting.Messaging.MethodResponse.cs
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
  public partial class MethodResponse : IMethodReturnMessage, IMethodMessage, IMessage, System.Runtime.Serialization.ISerializable, ISerializationRootObject, IInternalMessage
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

    public virtual new void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    public Object GetOutArg(int argNum)
    {
      return default(Object);
    }

    public string GetOutArgName(int index)
    {
      return default(string);
    }

    public virtual new Object HeaderHandler(Header[] h)
    {
      return default(Object);
    }

    public MethodResponse(Header[] h1, IMethodCallMessage mcm)
    {
    }

    public void RootSetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext ctx)
    {
    }

    bool System.Runtime.Remoting.Messaging.IInternalMessage.HasProperties()
    {
      return default(bool);
    }

    void System.Runtime.Remoting.Messaging.IInternalMessage.SetCallContext(LogicalCallContext newCallContext)
    {
    }

    void System.Runtime.Remoting.Messaging.IInternalMessage.SetURI(string val)
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

    public Object ReturnValue
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

    #region Fields
    protected System.Collections.IDictionary ExternalProperties;
    protected System.Collections.IDictionary InternalProperties;
    #endregion
  }
}
