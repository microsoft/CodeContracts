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
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Diagnostics.Contracts;

namespace System.Runtime.Remoting.Proxies {
  // Summary:
  //     Provides base functionality for proxies.
  [ComVisible(true)]
  public abstract class RealProxy {
    // Summary:
    //     Initializes a new instance of the System.Runtime.Remoting.Proxies.RealProxy
    //     class with default values.
    protected RealProxy();
    //
    // Summary:
    //     Initializes a new instance of the System.Runtime.Remoting.Proxies.RealProxy
    //     class that represents a remote object of the specified System.Type.
    //
    // Parameters:
    //   classToProxy:
    //     The System.Type of the remote object for which to create a proxy.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     classToProxy is not an interface, and is not derived from System.MarshalByRefObject.
    protected RealProxy(Type classToProxy);
    //
    // Summary:
    //     Initializes a new instance of the System.Runtime.Remoting.Proxies.RealProxy
    //     class.
    //
    // Parameters:
    //   classToProxy:
    //     The System.Type of the remote object for which to create a proxy.
    //
    //   stub:
    //     A stub to associate with the new proxy instance.
    //
    //   stubData:
    //     The stub data to set for the specified stub and the new proxy instance.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     classToProxy is not an interface, and is not derived from System.MarshalByRefObject.
    protected RealProxy(Type classToProxy, IntPtr stub, object stubData);

    // Summary:
    //     Attaches the current proxy instance to the specified remote System.MarshalByRefObject.
    //
    // Parameters:
    //   s:
    //     The System.MarshalByRefObject that the current proxy instance represents.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The immediate caller does not have UnmanagedCode permission.
    protected void AttachServer(MarshalByRefObject s);
    //
    // Summary:
    //     Creates an System.Runtime.Remoting.ObjRef for the specified object type,
    //     and registers it with the remoting infrastructure as a client-activated object.
    //
    // Parameters:
    //   requestedType:
    //     The object type that an System.Runtime.Remoting.ObjRef is created for.
    //
    // Returns:
    //     A new instance of System.Runtime.Remoting.ObjRef that is created for the
    //     specified type.
    public virtual ObjRef CreateObjRef(Type requestedType);
    //
    // Summary:
    //     Detaches the current proxy instance from the remote server object that it
    //     represents.
    //
    // Returns:
    //     The detached server object.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The immediate caller does not have UnmanagedCode permission.
    protected MarshalByRefObject DetachServer();
    //
    // Summary:
    //     Requests an unmanaged reference to the object represented by the current
    //     proxy instance.
    //
    // Parameters:
    //   fIsMarshalled:
    //     true if the object reference is requested for marshaling to a remote location;
    //     false if the object reference is requested for communication with unmanaged
    //     objects in the current process through COM.
    //
    // Returns:
    //     A pointer to a [<topic://cpconcomcallablewrapper>] if the object reference
    //     is requested for communication with unmanaged objects in the current process
    //     through COM, or a pointer to a cached or newly generated IUnknown COM interface
    //     if the object reference is requested for marshaling to a remote location.
    public virtual IntPtr GetCOMIUnknown(bool fIsMarshalled);
    //
    // Summary:
    //     Adds the transparent proxy of the object represented by the current instance
    //     of System.Runtime.Remoting.Proxies.RealProxy to the specified System.Runtime.Serialization.SerializationInfo.
    //
    // Parameters:
    //   info:
    //     The System.Runtime.Serialization.SerializationInfo into which the transparent
    //     proxy is serialized.
    //
    //   context:
    //     The source and destination of the serialization.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The info or context parameter is null.
    //
    //   System.Security.SecurityException:
    //     The immediate caller does not have SerializationFormatter permission.
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context);
    //
    // Summary:
    //     Returns the System.Type of the object that the current instance of System.Runtime.Remoting.Proxies.RealProxy
    //     represents.
    //
    // Returns:
    //     The System.Type of the object that the current instance of System.Runtime.Remoting.Proxies.RealProxy
    //     represents.
    public Type GetProxiedType();
    //
    // Summary:
    //     Retrieves stub data that is stored for the specified proxy.
    //
    // Parameters:
    //   rp:
    //     The proxy for which stub data is requested.
    //
    // Returns:
    //     Stub data for the specified proxy.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The immediate caller does not have UnmanagedCode permission.
    public static object GetStubData(RealProxy rp);
    //
    // Summary:
    //     Returns the transparent proxy for the current instance of System.Runtime.Remoting.Proxies.RealProxy.
    //
    // Returns:
    //     The transparent proxy for the current proxy instance.
    public virtual object GetTransparentProxy();
    //
    // Summary:
    //     Returns the server object that is represented by the current proxy instance.
    //
    // Returns:
    //     The server object that is represented by the current proxy instance.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The immediate caller does not have UnmanagedCode permission.
    protected MarshalByRefObject GetUnwrappedServer();
    //
    // Summary:
    //     Initializes a new instance of the object System.Type of the remote object
    //     that the current instance of System.Runtime.Remoting.Proxies.RealProxy represents
    //     with the specified System.Runtime.Remoting.Activation.IConstructionCallMessage.
    //
    // Parameters:
    //   ctorMsg:
    //     A construction call message that contains the constructor parameters for
    //     the new instance of the remote object that is represented by the current
    //     System.Runtime.Remoting.Proxies.RealProxy. Can be null.
    //
    // Returns:
    //     The result of the construction request.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The immediate caller does not have UnmanagedCode permission.
    [ComVisible(true)]
    public IConstructionReturnMessage InitializeServerObject(IConstructionCallMessage ctorMsg);
    //
    // Summary:
    //     When overridden in a derived class, invokes the method that is specified
    //     in the provided System.Runtime.Remoting.Messaging.IMessage on the remote
    //     object that is represented by the current instance.
    //
    // Parameters:
    //   msg:
    //     A System.Runtime.Remoting.Messaging.IMessage that contains a System.Collections.IDictionary
    //     of information about the method call.
    //
    // Returns:
    //     The message returned by the invoked method, containing the return value and
    //     any out or ref parameters.
    public abstract IMessage Invoke(IMessage msg);
    //
    // Summary:
    //     Stores an unmanaged proxy of the object that is represented by the current
    //     instance.
    //
    // Parameters:
    //   i:
    //     A pointer to the IUnknown interface for the object that is represented by
    //     the current proxy instance.
    public virtual void SetCOMIUnknown(IntPtr i);
    //
    // Summary:
    //     Sets the stub data for the specified proxy.
    //
    // Parameters:
    //   rp:
    //     The proxy for which to set stub data.
    //
    //   stubData:
    //     The new stub data.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The immediate caller does not have UnmanagedCode permission.
    public static void SetStubData(RealProxy rp, object stubData);
    //
    // Summary:
    //     Requests a COM interface with the specified ID.
    //
    // Parameters:
    //   iid:
    //     A reference to the requested interface.
    //
    // Returns:
    //     A pointer to the requested interface.
    public virtual IntPtr SupportsInterface(ref Guid iid);
  }
}
