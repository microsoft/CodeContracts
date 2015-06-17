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

// File System.Security.Cryptography.CngKey.cs
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


namespace System.Security.Cryptography
{
  sealed public partial class CngKey : IDisposable
  {
    #region Methods and constructors
    internal CngKey()
    {
    }

    public static CngKey Create(CngAlgorithm algorithm)
    {
      Contract.Ensures(Contract.Result<System.Security.Cryptography.CngKey>() != null);

      return default(CngKey);
    }

    public static CngKey Create(CngAlgorithm algorithm, string keyName, CngKeyCreationParameters creationParameters)
    {
      Contract.Ensures(Contract.Result<System.Security.Cryptography.CngKey>() != null);

      return default(CngKey);
    }

    public static CngKey Create(CngAlgorithm algorithm, string keyName)
    {
      Contract.Ensures(Contract.Result<System.Security.Cryptography.CngKey>() != null);

      return default(CngKey);
    }

    public void Delete()
    {
    }

    public void Dispose()
    {
    }

    public static bool Exists(string keyName, CngProvider provider)
    {
      return default(bool);
    }

    public static bool Exists(string keyName)
    {
      return default(bool);
    }

    public static bool Exists(string keyName, CngProvider provider, CngKeyOpenOptions options)
    {
      return default(bool);
    }

    public byte[] Export(CngKeyBlobFormat format)
    {
      Contract.Ensures(Contract.Result<byte[]>() != null);

      return default(byte[]);
    }

    public CngProperty GetProperty(string name, CngPropertyOptions options)
    {
      return default(CngProperty);
    }

    public bool HasProperty(string name, CngPropertyOptions options)
    {
      return default(bool);
    }

    public static CngKey Import(byte[] keyBlob, CngKeyBlobFormat format, CngProvider provider)
    {
      Contract.Ensures(Contract.Result<System.Security.Cryptography.CngKey>() != null);

      return default(CngKey);
    }

    public static CngKey Import(byte[] keyBlob, CngKeyBlobFormat format)
    {
      Contract.Ensures(Contract.Result<System.Security.Cryptography.CngKey>() != null);

      return default(CngKey);
    }

    public static CngKey Open(string keyName, CngProvider provider, CngKeyOpenOptions openOptions)
    {
      Contract.Ensures(Contract.Result<System.Security.Cryptography.CngKey>() != null);

      return default(CngKey);
    }

    public static CngKey Open(string keyName, CngProvider provider)
    {
      Contract.Ensures(Contract.Result<System.Security.Cryptography.CngKey>() != null);

      return default(CngKey);
    }

    public static CngKey Open(string keyName)
    {
      Contract.Ensures(Contract.Result<System.Security.Cryptography.CngKey>() != null);

      return default(CngKey);
    }

    public static CngKey Open(Microsoft.Win32.SafeHandles.SafeNCryptKeyHandle keyHandle, CngKeyHandleOpenOptions keyHandleOpenOptions)
    {
      Contract.Ensures(Contract.Result<System.Security.Cryptography.CngKey>() != null);

      return default(CngKey);
    }

    public void SetProperty(CngProperty property)
    {
    }
    #endregion

    #region Properties and indexers
    public CngAlgorithm Algorithm
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Security.Cryptography.CngAlgorithm>() != null);

        return default(CngAlgorithm);
      }
    }

    public CngAlgorithmGroup AlgorithmGroup
    {
      get
      {
        return default(CngAlgorithmGroup);
      }
    }

    public CngExportPolicies ExportPolicy
    {
      get
      {
        return default(CngExportPolicies);
      }
    }

    public Microsoft.Win32.SafeHandles.SafeNCryptKeyHandle Handle
    {
      get
      {
        Contract.Ensures(Contract.Result<Microsoft.Win32.SafeHandles.SafeNCryptKeyHandle>() != null);

        return default(Microsoft.Win32.SafeHandles.SafeNCryptKeyHandle);
      }
    }

    public bool IsEphemeral
    {
      get
      {
        return default(bool);
      }
      private set
      {
      }
    }

    public bool IsMachineKey
    {
      get
      {
        return default(bool);
      }
    }

    public string KeyName
    {
      get
      {
        return default(string);
      }
    }

    public int KeySize
    {
      get
      {
        return default(int);
      }
    }

    public CngKeyUsages KeyUsage
    {
      get
      {
        return default(CngKeyUsages);
      }
    }

    public IntPtr ParentWindowHandle
    {
      get
      {
        return default(IntPtr);
      }
      set
      {
      }
    }

    public CngProvider Provider
    {
      get
      {
        return default(CngProvider);
      }
    }

    public Microsoft.Win32.SafeHandles.SafeNCryptProviderHandle ProviderHandle
    {
      get
      {
        Contract.Ensures(Contract.Result<Microsoft.Win32.SafeHandles.SafeNCryptProviderHandle>() != null);

        return default(Microsoft.Win32.SafeHandles.SafeNCryptProviderHandle);
      }
    }

    public CngUIPolicy UIPolicy
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Security.Cryptography.CngUIPolicy>() != null);

        return default(CngUIPolicy);
      }
    }

    public string UniqueName
    {
      get
      {
        return default(string);
      }
    }
    #endregion
  }
}
