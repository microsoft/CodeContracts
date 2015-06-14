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

using System.Diagnostics.Contracts;
using System;

namespace System.Runtime.Remoting.Messaging
{

  public class MethodResponse
  {

    extern public Object[] OutArgs
    {
      get;
    }

    extern public int ArgCount
    {
      get;
    }

    extern public string Uri
    {
      get;
      set;
    }

    extern public bool HasVarArgs
    {
      get;
    }

    extern public int OutArgCount
    {
      get;
    }

    extern public System.Collections.IDictionary Properties
    {
      get;
    }

    extern public Object[] Args
    {
      get;
    }

    extern public object ReturnValue
    {
      get;
    }

    extern public Exception Exception
    {
      get;
    }

    extern public object MethodSignature
    {
      get;
    }

    extern public LogicalCallContext LogicalCallContext
    {
      get;
    }

    extern public string TypeName
    {
      get;
    }

    extern public System.Reflection.MethodBase MethodBase
    {
      get;
    }

    extern public string MethodName
    {
      get;
    }

    public string GetOutArgName(int index)
    {

      return default(string);
    }
    public object GetOutArg(int argNum)
    {

      return default(object);
    }
    public string GetArgName(int index)
    {
      Contract.Requires(index >= 0);

      return default(string);
    }
    public object GetArg(int argNum)
    {

      return default(object);
    }
    public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {

    }
    public void RootSetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext ctx)
    {

    }
    public object HeaderHandler(Header[] h)
    {

      return default(object);
    }
    public MethodResponse(Header[] h1, IMethodCallMessage mcm)
    {
      Contract.Requires(mcm != null);
      return default(MethodResponse);
    }
  }
}
