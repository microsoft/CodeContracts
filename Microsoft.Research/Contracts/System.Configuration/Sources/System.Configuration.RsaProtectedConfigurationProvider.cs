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

// File System.Configuration.RsaProtectedConfigurationProvider.cs
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


namespace System.Configuration
{
  sealed public partial class RsaProtectedConfigurationProvider : ProtectedConfigurationProvider
  {
    #region Methods and constructors
    public void AddKey(int keySize, bool exportable)
    {
      Contract.Ensures(0 <= this.KeyContainerName.Length);
    }

    public override System.Xml.XmlNode Decrypt(System.Xml.XmlNode encryptedNode)
    {
      return default(System.Xml.XmlNode);
    }

    public void DeleteKey()
    {
      Contract.Ensures(0 <= this.KeyContainerName.Length);
    }

    public override System.Xml.XmlNode Encrypt(System.Xml.XmlNode node)
    {
      return default(System.Xml.XmlNode);
    }

    public void ExportKey(string xmlFileName, bool includePrivateParameters)
    {
      Contract.Ensures(0 <= this.KeyContainerName.Length);
    }

    public void ImportKey(string xmlFileName, bool exportable)
    {
      Contract.Requires(xmlFileName != null);
      Contract.Requires(xmlFileName.Length > 0);
      Contract.Ensures(0 <= this.KeyContainerName.Length);
      Contract.Ensures(1 <= xmlFileName.Length);
    }

    public override void Initialize(string name, System.Collections.Specialized.NameValueCollection configurationValues)
    {
    }

    public RsaProtectedConfigurationProvider()
    {
    }
    #endregion

    #region Properties and indexers
    public string CspProviderName
    {
      get
      {
        return default(string);
      }
    }

    public string KeyContainerName
    {
      get
      {
        return default(string);
      }
    }

    public System.Security.Cryptography.RSAParameters RsaPublicKey
    {
      get
      {
        Contract.Ensures(0 <= this.KeyContainerName.Length);

        return default(System.Security.Cryptography.RSAParameters);
      }
    }

    public bool UseMachineContainer
    {
      get
      {
        return default(bool);
      }
    }

    public bool UseOAEP
    {
      get
      {
        return default(bool);
      }
    }
    #endregion
  }
}
