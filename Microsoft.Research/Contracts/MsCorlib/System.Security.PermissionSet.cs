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
using System.Diagnostics.Contracts;

namespace System.Security
{

  public class PermissionSet
  {

    extern public bool IsReadOnly
    {
      get;
    }

    extern public object SyncRoot
    {
      get;
    }

    extern public bool IsSynchronized
    {
      get;
    }

    extern public int Count
    {
      get;
    }

    public bool ContainsNonCodeAccessPermissions()
    {

      return default(bool);
    }
    public static Byte[] ConvertPermissionSet(string inFormat, Byte[] inData, string outFormat)
    {
      Contract.Requires(inData == null || inFormat != null);
      Contract.Requires(outFormat != null);

      return default(Byte[]);
    }
    public SecurityElement ToXml()
    {

      return default(SecurityElement);
    }
    public void FromXml(SecurityElement et)
    {

    }

    [Pure]
    [GlobalAccess(false)]
    [Escapes(true, false)]
    public System.Collections.IEnumerator GetEnumerator()
    {
      Contract.Ensures(result.IsNew);

      Contract.Ensures(Contract.Result<System.Collections.IEnumerator>() != null);
      return default(System.Collections.IEnumerator);
    }
    public PermissionSet Copy()
    {

      return default(PermissionSet);
    }
    public void PermitOnly()
    {

    }
    public void Deny()
    {

    }
    public void Assert()
    {

    }
    public void Demand()
    {

    }
    public PermissionSet Union(PermissionSet other)
    {

      return default(PermissionSet);
    }
    public PermissionSet Intersect(PermissionSet other)
    {

      return default(PermissionSet);
    }
    public bool IsSubsetOf(PermissionSet target)
    {

      return default(bool);
    }
    public bool IsUnrestricted()
    {

      return default(bool);
    }
    public IPermission RemovePermission(Type permClass)
    {

      return default(IPermission);
    }
    public IPermission AddPermission(IPermission perm)
    {

      return default(IPermission);
    }
    public IPermission SetPermission(IPermission perm)
    {

      return default(IPermission);
    }
    public IPermission GetPermission(Type permClass)
    {

      return default(IPermission);
    }
    public bool IsEmpty()
    {

      return default(bool);
    }
    public void CopyTo(Array array, int index)
    {
      Contract.Requires(array != null);

    }
    public PermissionSet(PermissionSet permSet)
    {

      return default(PermissionSet);
    }
    public PermissionSet(System.Security.Permissions.PermissionState state)
    {
      Contract.Requires((int)state == 1 || (int)state == 0);
      return default(PermissionSet);
    }
  }
}
