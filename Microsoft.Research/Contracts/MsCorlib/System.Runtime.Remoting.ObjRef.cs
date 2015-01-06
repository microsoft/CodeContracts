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

// File System.Runtime.Remoting.ObjRef.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
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
  public partial class ObjRef : System.Runtime.Serialization.IObjectReference, System.Runtime.Serialization.ISerializable
  {
    #region Methods and constructors
    public virtual new void GetObjectData (System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    public virtual new Object GetRealObject (System.Runtime.Serialization.StreamingContext context)
    {
      return default(Object);
    }

    public bool IsFromThisAppDomain ()
    {
#if false
      Contract.Requires (this.ChannelInfo != null);
      Contract.Requires (this.ChannelInfo.ChannelData != null);
      Contract.Ensures (this.ChannelInfo.ChannelData != null);
      Contract.Ensures (this.ChannelInfo.ChannelData.Length >= 0);
#endif
      return default(bool);
    }

    public bool IsFromThisProcess ()
    {
      return default(bool);
    }


    public ObjRef (MarshalByRefObject o, Type requestedType)
    {
    }

    protected ObjRef (System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
      Contract.Requires (info != null);
    }


    public ObjRef ()
    {
    }

    #endregion

    #region Properties and indexers
    public virtual new IChannelInfo ChannelInfo
    {
      get
      {
        return default(IChannelInfo);
      }
      set
      {
      }
    }

    public virtual new IEnvoyInfo EnvoyInfo
    {
      get
      {
        return default(IEnvoyInfo);
      }
      set
      {
      }
    }

    public virtual new IRemotingTypeInfo TypeInfo
    {
      get
      {
        return default(IRemotingTypeInfo);
      }
      set
      {
      }
    }

    public virtual new string URI
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

#endif