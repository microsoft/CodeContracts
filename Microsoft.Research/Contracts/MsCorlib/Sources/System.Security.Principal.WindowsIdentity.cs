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

// File System.Security.Principal.WindowsIdentity.cs
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


namespace System.Security.Principal
{
  public partial class WindowsIdentity : IIdentity, System.Runtime.Serialization.ISerializable, System.Runtime.Serialization.IDeserializationCallback, IDisposable
  {
    #region Methods and constructors
    public void Dispose()
    {
    }

    protected virtual new void Dispose(bool disposing)
    {
    }

    public static System.Security.Principal.WindowsIdentity GetAnonymous()
    {
      Contract.Ensures(Contract.Result<System.Security.Principal.WindowsIdentity>() != null);

      return default(System.Security.Principal.WindowsIdentity);
    }

    public static System.Security.Principal.WindowsIdentity GetCurrent(bool ifImpersonating)
    {
      return default(System.Security.Principal.WindowsIdentity);
    }

    public static System.Security.Principal.WindowsIdentity GetCurrent(TokenAccessLevels desiredAccess)
    {
      return default(System.Security.Principal.WindowsIdentity);
    }

    public static System.Security.Principal.WindowsIdentity GetCurrent()
    {
      return default(System.Security.Principal.WindowsIdentity);
    }

    public static WindowsImpersonationContext Impersonate(IntPtr userToken)
    {
      Contract.Ensures(Contract.Result<System.Security.Principal.WindowsImpersonationContext>() != null);

      return default(WindowsImpersonationContext);
    }

    public virtual new WindowsImpersonationContext Impersonate()
    {
      return default(WindowsImpersonationContext);
    }

    void System.Runtime.Serialization.IDeserializationCallback.OnDeserialization(Object sender)
    {
    }

    void System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    public WindowsIdentity(IntPtr userToken, string type, WindowsAccountType acctType)
    {
    }

    public WindowsIdentity(IntPtr userToken)
    {
    }

    public WindowsIdentity(IntPtr userToken, string type)
    {
    }

    public WindowsIdentity(string sUserPrincipalName, string type)
    {
    }

    public WindowsIdentity(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
      Contract.Requires(info != null);
    }

    public WindowsIdentity(IntPtr userToken, string type, WindowsAccountType acctType, bool isAuthenticated)
    {
    }

    public WindowsIdentity(string sUserPrincipalName)
    {
    }
    #endregion

    #region Properties and indexers
    public string AuthenticationType
    {
      get
      {
        return default(string);
      }
    }

    public IdentityReferenceCollection Groups
    {
      get
      {
        return default(IdentityReferenceCollection);
      }
    }

    public TokenImpersonationLevel ImpersonationLevel
    {
      get
      {
        return default(TokenImpersonationLevel);
      }
    }

    public virtual new bool IsAnonymous
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool IsAuthenticated
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool IsGuest
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool IsSystem
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
    }

    public SecurityIdentifier Owner
    {
      get
      {
        return default(SecurityIdentifier);
      }
    }

    public virtual new IntPtr Token
    {
      get
      {
        return default(IntPtr);
      }
    }

    public SecurityIdentifier User
    {
      get
      {
        return default(SecurityIdentifier);
      }
    }
    #endregion
  }
}
