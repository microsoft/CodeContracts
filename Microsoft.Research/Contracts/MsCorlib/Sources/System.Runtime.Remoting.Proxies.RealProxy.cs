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

// File System.Runtime.Remoting.Proxies.RealProxy.cs
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


namespace System.Runtime.Remoting.Proxies
{
  abstract public partial class RealProxy
  {
    #region Methods and constructors
    protected void AttachServer(MarshalByRefObject s)
    {
    }

    public virtual new System.Runtime.Remoting.ObjRef CreateObjRef(Type requestedType)
    {
      return default(System.Runtime.Remoting.ObjRef);
    }

    protected MarshalByRefObject DetachServer()
    {
      Contract.Ensures(Contract.Result<System.MarshalByRefObject>() != null);

      return default(MarshalByRefObject);
    }

    public virtual new IntPtr GetCOMIUnknown(bool fIsMarshalled)
    {
      return default(IntPtr);
    }

    public virtual new void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    public Type GetProxiedType()
    {
      return default(Type);
    }

    public static Object GetStubData(System.Runtime.Remoting.Proxies.RealProxy rp)
    {
      return default(Object);
    }

    public virtual new Object GetTransparentProxy()
    {
      return default(Object);
    }

    protected MarshalByRefObject GetUnwrappedServer()
    {
      return default(MarshalByRefObject);
    }

    public System.Runtime.Remoting.Activation.IConstructionReturnMessage InitializeServerObject(System.Runtime.Remoting.Activation.IConstructionCallMessage ctorMsg)
    {
      return default(System.Runtime.Remoting.Activation.IConstructionReturnMessage);
    }

    public abstract System.Runtime.Remoting.Messaging.IMessage Invoke(System.Runtime.Remoting.Messaging.IMessage msg);

    protected RealProxy()
    {
    }

    protected RealProxy(Type classToProxy, IntPtr stub, Object stubData)
    {
      Contract.Requires(classToProxy != null);
    }

    protected RealProxy(Type classToProxy)
    {
      Contract.Requires(classToProxy != null);
    }

    public virtual new void SetCOMIUnknown(IntPtr i)
    {
    }

    public static void SetStubData(System.Runtime.Remoting.Proxies.RealProxy rp, Object stubData)
    {
    }

    public virtual new IntPtr SupportsInterface(ref Guid iid)
    {
      return default(IntPtr);
    }
    #endregion
  }
}
