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

// File Microsoft.Win32.RegistryKey.cs
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


namespace Microsoft.Win32
{
  sealed public partial class RegistryKey : MarshalByRefObject, IDisposable
  {
    #region Methods and constructors
    public void Close()
    {
    }

    public RegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck, RegistryOptions options)
    {
      return default(RegistryKey);
    }

    public RegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck, System.Security.AccessControl.RegistrySecurity registrySecurity)
    {
      return default(RegistryKey);
    }

    public RegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck, RegistryOptions registryOptions, System.Security.AccessControl.RegistrySecurity registrySecurity)
    {
      return default(RegistryKey);
    }

    public RegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck)
    {
      return default(RegistryKey);
    }

    public RegistryKey CreateSubKey(string subkey)
    {
      return default(RegistryKey);
    }

    public void DeleteSubKey(string subkey)
    {
      Contract.Ensures(0 <= subkey.Length);
    }

    public void DeleteSubKey(string subkey, bool throwOnMissingSubKey)
    {
      Contract.Ensures(0 <= subkey.Length);
    }

    public void DeleteSubKeyTree(string subkey)
    {
      Contract.Ensures(0 <= subkey.Length);
    }

    public void DeleteSubKeyTree(string subkey, bool throwOnMissingSubKey)
    {
      Contract.Ensures(0 <= subkey.Length);
    }

    public void DeleteValue(string name, bool throwOnMissingValue)
    {
    }

    public void DeleteValue(string name)
    {
    }

    public void Dispose()
    {
    }

    public void Flush()
    {
    }

    public static RegistryKey FromHandle(Microsoft.Win32.SafeHandles.SafeRegistryHandle handle, RegistryView view)
    {
      Contract.Ensures(Contract.Result<Microsoft.Win32.RegistryKey>() != null);

      return default(RegistryKey);
    }

    public static RegistryKey FromHandle(Microsoft.Win32.SafeHandles.SafeRegistryHandle handle)
    {
      Contract.Ensures(Contract.Result<Microsoft.Win32.RegistryKey>() != null);

      return default(RegistryKey);
    }

    public System.Security.AccessControl.RegistrySecurity GetAccessControl()
    {
      Contract.Ensures(Contract.Result<System.Security.AccessControl.RegistrySecurity>() != null);

      return default(System.Security.AccessControl.RegistrySecurity);
    }

    public System.Security.AccessControl.RegistrySecurity GetAccessControl(System.Security.AccessControl.AccessControlSections includeSections)
    {
      Contract.Ensures(Contract.Result<System.Security.AccessControl.RegistrySecurity>() != null);

      return default(System.Security.AccessControl.RegistrySecurity);
    }

    public string[] GetSubKeyNames()
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return default(string[]);
    }

    public Object GetValue(string name)
    {
      return default(Object);
    }

    public Object GetValue(string name, Object defaultValue, RegistryValueOptions options)
    {
      return default(Object);
    }

    public Object GetValue(string name, Object defaultValue)
    {
      return default(Object);
    }

    public RegistryValueKind GetValueKind(string name)
    {
      return default(RegistryValueKind);
    }

    public string[] GetValueNames()
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return default(string[]);
    }

    public static RegistryKey OpenBaseKey(RegistryHive hKey, RegistryView view)
    {
      Contract.Ensures(Contract.Result<Microsoft.Win32.RegistryKey>() != null);

      return default(RegistryKey);
    }

    public static RegistryKey OpenRemoteBaseKey(RegistryHive hKey, string machineName, RegistryView view)
    {
      Contract.Ensures(Contract.Result<Microsoft.Win32.RegistryKey>() != null);
      Contract.Ensures(false);

      return default(RegistryKey);
    }

    public static RegistryKey OpenRemoteBaseKey(RegistryHive hKey, string machineName)
    {
      Contract.Ensures(false);

      return default(RegistryKey);
    }

    public RegistryKey OpenSubKey(string name, RegistryKeyPermissionCheck permissionCheck)
    {
      return default(RegistryKey);
    }

    public RegistryKey OpenSubKey(string name, bool writable)
    {
      return default(RegistryKey);
    }

    public RegistryKey OpenSubKey(string name)
    {
      return default(RegistryKey);
    }

    public RegistryKey OpenSubKey(string name, RegistryKeyPermissionCheck permissionCheck, System.Security.AccessControl.RegistryRights rights)
    {
      return default(RegistryKey);
    }

    internal RegistryKey()
    {
    }

    public void SetAccessControl(System.Security.AccessControl.RegistrySecurity registrySecurity)
    {
    }

    public void SetValue(string name, Object value, RegistryValueKind valueKind)
    {
    }

    public void SetValue(string name, Object value)
    {
    }

    public override string ToString()
    {
      return default(string);
    }
    #endregion

    #region Properties and indexers
    public Microsoft.Win32.SafeHandles.SafeRegistryHandle Handle
    {
      get
      {
        return default(Microsoft.Win32.SafeHandles.SafeRegistryHandle);
      }
    }

    public string Name
    {
      get
      {
        return default(string);
      }
    }

    public int SubKeyCount
    {
      get
      {
        return default(int);
      }
    }

    public int ValueCount
    {
      get
      {
        return default(int);
      }
    }

    public RegistryView View
    {
      get
      {
        return default(RegistryView);
      }
    }
    #endregion
  }
}
