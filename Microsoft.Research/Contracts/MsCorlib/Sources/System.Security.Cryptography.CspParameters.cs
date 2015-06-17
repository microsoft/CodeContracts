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

// File System.Security.Cryptography.CspParameters.cs
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
  sealed public partial class CspParameters
  {
    #region Methods and constructors
    public CspParameters(int providerType, string providerName, string keyContainerName, System.Security.AccessControl.CryptoKeySecurity cryptoKeySecurity, System.Security.SecureString keyPassword)
    {
      Contract.Ensures(0 <= keyContainerName.Length);
      Contract.Ensures(0 <= providerName.Length);
      Contract.Ensures(keyContainerName == this.KeyContainerName);
      Contract.Ensures(keyContainerName.Length == this.KeyContainerName.Length);
      Contract.Ensures(providerName == this.ProviderName);
      Contract.Ensures(providerName.Length == this.ProviderName.Length);
      Contract.Ensures(providerType == this.ProviderType);
      Contract.Ensures(this.KeyContainerName != null);
      Contract.Ensures(this.KeyNumber == -(1));
      Contract.Ensures(this.ProviderName != null);
    }

    public CspParameters(int providerType, string providerName, string keyContainerName, System.Security.AccessControl.CryptoKeySecurity cryptoKeySecurity, IntPtr parentWindowHandle)
    {
      Contract.Ensures(0 <= keyContainerName.Length);
      Contract.Ensures(0 <= providerName.Length);
      Contract.Ensures(keyContainerName == this.KeyContainerName);
      Contract.Ensures(keyContainerName.Length == this.KeyContainerName.Length);
      Contract.Ensures(providerName == this.ProviderName);
      Contract.Ensures(providerName.Length == this.ProviderName.Length);
      Contract.Ensures(providerType == this.ProviderType);
      Contract.Ensures(this.KeyContainerName != null);
      Contract.Ensures(this.KeyNumber == -(1));
      Contract.Ensures(this.ProviderName != null);
    }

    public CspParameters()
    {
      Contract.Ensures(0 <= this.KeyContainerName.Length);
      Contract.Ensures(this.KeyContainerName != null);
      Contract.Ensures(this.KeyContainerName == null);
      Contract.Ensures(this.KeyContainerName.Length == this.ProviderName.Length);
      Contract.Ensures(this.KeyNumber == -(1));
      Contract.Ensures(this.ProviderName != null);
      Contract.Ensures(this.ProviderName == null);
    }

    public CspParameters(int dwTypeIn)
    {
      Contract.Ensures(0 <= this.KeyContainerName.Length);
      Contract.Ensures(dwTypeIn == this.ProviderType);
      Contract.Ensures(this.KeyContainerName != null);
      Contract.Ensures(this.KeyContainerName == null);
      Contract.Ensures(this.KeyContainerName.Length == this.ProviderName.Length);
      Contract.Ensures(this.KeyNumber == -(1));
      Contract.Ensures(this.ProviderName != null);
      Contract.Ensures(this.ProviderName == null);
    }

    public CspParameters(int dwTypeIn, string strProviderNameIn)
    {
      Contract.Ensures(0 <= strProviderNameIn.Length);
      Contract.Ensures(0 <= this.KeyContainerName.Length);
      Contract.Ensures(dwTypeIn == this.ProviderType);
      Contract.Ensures(strProviderNameIn == this.ProviderName);
      Contract.Ensures(strProviderNameIn.Length == this.ProviderName.Length);
      Contract.Ensures(this.KeyContainerName != null);
      Contract.Ensures(this.KeyContainerName == null);
      Contract.Ensures(this.KeyNumber == -(1));
      Contract.Ensures(this.ProviderName != null);
    }

    public CspParameters(int dwTypeIn, string strProviderNameIn, string strContainerNameIn)
    {
      Contract.Ensures(dwTypeIn == this.ProviderType);
      Contract.Ensures(strContainerNameIn == this.KeyContainerName);
      Contract.Ensures(strContainerNameIn.Length == this.KeyContainerName.Length);
      Contract.Ensures(strProviderNameIn == this.ProviderName);
      Contract.Ensures(strProviderNameIn.Length == this.ProviderName.Length);
      Contract.Ensures(this.KeyNumber == -(1));
    }
    #endregion

    #region Properties and indexers
    public System.Security.AccessControl.CryptoKeySecurity CryptoKeySecurity
    {
      get
      {
        return default(System.Security.AccessControl.CryptoKeySecurity);
      }
      set
      {
      }
    }

    public CspProviderFlags Flags
    {
      get
      {
        Contract.Ensures(((System.Security.Cryptography.CspProviderFlags)(Int32.MinValue)) <= Contract.Result<System.Security.Cryptography.CspProviderFlags>());
        Contract.Ensures(Contract.Result<System.Security.Cryptography.CspProviderFlags>() <= ((System.Security.Cryptography.CspProviderFlags)(Int32.MaxValue)));

        return default(CspProviderFlags);
      }
      set
      {
      }
    }

    public System.Security.SecureString KeyPassword
    {
      get
      {
        return default(System.Security.SecureString);
      }
      set
      {
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
    #endregion

    #region Fields
    public string KeyContainerName;
    public int KeyNumber;
    public string ProviderName;
    public int ProviderType;
    #endregion
  }
}
