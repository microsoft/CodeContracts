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

// File System.Security.PermissionSet.cs
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


namespace System.Security
{
  public partial class PermissionSet : ISecurityEncodable, System.Collections.ICollection, System.Collections.IEnumerable, IStackWalk, System.Runtime.Serialization.IDeserializationCallback
  {
    #region Methods and constructors
    public IPermission AddPermission(IPermission perm)
    {
      return default(IPermission);
    }

    protected virtual new IPermission AddPermissionImpl(IPermission perm)
    {
      return default(IPermission);
    }

    public void Assert()
    {
    }

    public bool ContainsNonCodeAccessPermissions()
    {
      return default(bool);
    }

    public static byte[] ConvertPermissionSet(string inFormat, byte[] inData, string outFormat)
    {
      Contract.Ensures(false);

      return default(byte[]);
    }

    public virtual new System.Security.PermissionSet Copy()
    {
      return default(System.Security.PermissionSet);
    }

    public virtual new void CopyTo(Array array, int index)
    {
    }

    public void Demand()
    {
    }

    public void Deny()
    {
    }

    public override bool Equals(Object obj)
    {
      return default(bool);
    }

    public virtual new void FromXml(SecurityElement et)
    {
    }

    public System.Collections.IEnumerator GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }

    protected virtual new System.Collections.IEnumerator GetEnumeratorImpl()
    {
      return default(System.Collections.IEnumerator);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public IPermission GetPermission(Type permClass)
    {
      return default(IPermission);
    }

    protected virtual new IPermission GetPermissionImpl(Type permClass)
    {
      return default(IPermission);
    }

    public System.Security.PermissionSet Intersect(System.Security.PermissionSet other)
    {
      return default(System.Security.PermissionSet);
    }

    public bool IsEmpty()
    {
      return default(bool);
    }

    public bool IsSubsetOf(System.Security.PermissionSet target)
    {
      return default(bool);
    }

    public bool IsUnrestricted()
    {
      return default(bool);
    }

    public PermissionSet(System.Security.PermissionSet permSet)
    {
    }

    public PermissionSet(System.Security.Permissions.PermissionState state)
    {
    }

    public void PermitOnly()
    {
    }

    public IPermission RemovePermission(Type permClass)
    {
      return default(IPermission);
    }

    protected virtual new IPermission RemovePermissionImpl(Type permClass)
    {
      return default(IPermission);
    }

    public static void RevertAssert()
    {
    }

    public IPermission SetPermission(IPermission perm)
    {
      return default(IPermission);
    }

    protected virtual new IPermission SetPermissionImpl(IPermission perm)
    {
      return default(IPermission);
    }

    void System.Runtime.Serialization.IDeserializationCallback.OnDeserialization(Object sender)
    {
    }

    public override string ToString()
    {
      return default(string);
    }

    public virtual new SecurityElement ToXml()
    {
      return default(SecurityElement);
    }

    public System.Security.PermissionSet Union(System.Security.PermissionSet other)
    {
      return default(System.Security.PermissionSet);
    }
    #endregion

    #region Properties and indexers
    public virtual new int Count
    {
      get
      {
        return default(int);
      }
    }

    public virtual new bool IsReadOnly
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool IsSynchronized
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new Object SyncRoot
    {
      get
      {
        return default(Object);
      }
    }
    #endregion
  }
}
