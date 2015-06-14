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

// File System.Runtime.Remoting.RemotingServices.cs
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


namespace System.Runtime.Remoting
{
  static public partial class RemotingServices
  {
    #region Methods and constructors
    public static Object Connect(Type classToProxy, string url)
    {
      return default(Object);
    }

    public static Object Connect(Type classToProxy, string url, Object data)
    {
      return default(Object);
    }

    public static bool Disconnect(MarshalByRefObject obj)
    {
      return default(bool);
    }

    public static System.Runtime.Remoting.Messaging.IMethodReturnMessage ExecuteMessage(MarshalByRefObject target, System.Runtime.Remoting.Messaging.IMethodCallMessage reqMsg)
    {
      return default(System.Runtime.Remoting.Messaging.IMethodReturnMessage);
    }

    public static System.Runtime.Remoting.Messaging.IMessageSink GetEnvoyChainForProxy(MarshalByRefObject obj)
    {
      return default(System.Runtime.Remoting.Messaging.IMessageSink);
    }

    public static Object GetLifetimeService(MarshalByRefObject obj)
    {
      return default(Object);
    }

    public static System.Reflection.MethodBase GetMethodBaseFromMethodMessage(System.Runtime.Remoting.Messaging.IMethodMessage msg)
    {
      return default(System.Reflection.MethodBase);
    }

    public static void GetObjectData(Object obj, System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    public static string GetObjectUri(MarshalByRefObject obj)
    {
      return default(string);
    }

    public static ObjRef GetObjRefForProxy(MarshalByRefObject obj)
    {
      return default(ObjRef);
    }

    public static System.Runtime.Remoting.Proxies.RealProxy GetRealProxy(Object proxy)
    {
      return default(System.Runtime.Remoting.Proxies.RealProxy);
    }

    public static Type GetServerTypeForUri(string URI)
    {
      return default(Type);
    }

    public static string GetSessionIdForMethodMessage(System.Runtime.Remoting.Messaging.IMethodMessage msg)
    {
      Contract.Requires(msg != null);
      Contract.Ensures(Contract.Result<string>() == msg.Uri);

      return default(string);
    }

    public static bool IsMethodOverloaded(System.Runtime.Remoting.Messaging.IMethodMessage msg)
    {
      Contract.Requires(msg != null);

      return default(bool);
    }

    public static bool IsObjectOutOfAppDomain(Object tp)
    {
      return default(bool);
    }

    public static bool IsObjectOutOfContext(Object tp)
    {
      return default(bool);
    }

    public static bool IsOneWay(System.Reflection.MethodBase method)
    {
      return default(bool);
    }

    public static bool IsTransparentProxy(Object proxy)
    {
      return default(bool);
    }

    public static void LogRemotingStage(int stage)
    {
    }

    public static ObjRef Marshal(MarshalByRefObject Obj)
    {
      return default(ObjRef);
    }

    public static ObjRef Marshal(MarshalByRefObject Obj, string ObjURI, Type RequestedType)
    {
      return default(ObjRef);
    }

    public static ObjRef Marshal(MarshalByRefObject Obj, string URI)
    {
      return default(ObjRef);
    }

    public static void SetObjectUriForMarshal(MarshalByRefObject obj, string uri)
    {
    }

    public static Object Unmarshal(ObjRef objectRef)
    {
      Contract.Requires(objectRef != null);

      return default(Object);
    }

    public static Object Unmarshal(ObjRef objectRef, bool fRefine)
    {
      Contract.Requires(objectRef != null);

      return default(Object);
    }
    #endregion
  }
}
