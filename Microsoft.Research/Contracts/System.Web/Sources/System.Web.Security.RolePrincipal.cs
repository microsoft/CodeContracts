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

// File System.Web.Security.RolePrincipal.cs
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


namespace System.Web.Security
{
  public partial class RolePrincipal : System.Security.Principal.IPrincipal, System.Runtime.Serialization.ISerializable
  {
    #region Methods and constructors
    protected virtual new void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    public string[] GetRoles()
    {
      return default(string[]);
    }

    public bool IsInRole(string role)
    {
      return default(bool);
    }

    public RolePrincipal(string providerName, System.Security.Principal.IIdentity identity, string encryptedTicket)
    {
    }

    public RolePrincipal(System.Security.Principal.IIdentity identity)
    {
    }

    protected RolePrincipal(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    public RolePrincipal(string providerName, System.Security.Principal.IIdentity identity)
    {
    }

    public RolePrincipal(System.Security.Principal.IIdentity identity, string encryptedTicket)
    {
    }

    public void SetDirty()
    {
    }

    void System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    public string ToEncryptedTicket()
    {
      return default(string);
    }
    #endregion

    #region Properties and indexers
    public bool CachedListChanged
    {
      get
      {
        return default(bool);
      }
    }

    public string CookiePath
    {
      get
      {
        return default(string);
      }
    }

    public bool Expired
    {
      get
      {
        return default(bool);
      }
    }

    public DateTime ExpireDate
    {
      get
      {
        return default(DateTime);
      }
    }

    public System.Security.Principal.IIdentity Identity
    {
      get
      {
        return default(System.Security.Principal.IIdentity);
      }
    }

    public bool IsRoleListCached
    {
      get
      {
        return default(bool);
      }
    }

    public DateTime IssueDate
    {
      get
      {
        return default(DateTime);
      }
    }

    public string ProviderName
    {
      get
      {
        return default(string);
      }
    }

    public int Version
    {
      get
      {
        return default(int);
      }
    }
    #endregion
  }
}
