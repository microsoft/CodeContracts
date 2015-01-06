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

// File System.Security.Cryptography.ECDiffieHellmanCng.cs
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
  sealed public partial class ECDiffieHellmanCng : ECDiffieHellman
  {
    #region Methods and constructors
    public override byte[] DeriveKeyMaterial(ECDiffieHellmanPublicKey otherPartyPublicKey)
    {
      return default(byte[]);
    }

    public byte[] DeriveKeyMaterial(CngKey otherPartyPublicKey)
    {
      Contract.Ensures(Contract.Result<byte[]>() != null);
      Contract.Ensures(this.Key.Handle != null);

      return default(byte[]);
    }

    public Microsoft.Win32.SafeHandles.SafeNCryptSecretHandle DeriveSecretAgreementHandle(ECDiffieHellmanPublicKey otherPartyPublicKey)
    {
      Contract.Ensures(this.Key.Handle != null);

      return default(Microsoft.Win32.SafeHandles.SafeNCryptSecretHandle);
    }

    public Microsoft.Win32.SafeHandles.SafeNCryptSecretHandle DeriveSecretAgreementHandle(CngKey otherPartyPublicKey)
    {
      Contract.Ensures(this.Key.Handle != null);

      return default(Microsoft.Win32.SafeHandles.SafeNCryptSecretHandle);
    }

    protected override void Dispose(bool disposing)
    {
    }

    public ECDiffieHellmanCng(int keySize)
    {
      Contract.Ensures(this.LegalKeySizesValue != null);
    }

    public ECDiffieHellmanCng(CngKey key)
    {
      Contract.Ensures(this.Key.AlgorithmGroup != null);
      Contract.Ensures(this.Key.AlgorithmGroup == System.Security.Cryptography.CngAlgorithmGroup.ECDiffieHellman);
      Contract.Ensures(this.LegalKeySizesValue != null);
    }

    public ECDiffieHellmanCng()
    {
      Contract.Ensures(this.LegalKeySizesValue != null);
    }

    public void FromXmlString(string xml, ECKeyXmlFormat format)
    {
      Contract.Ensures(System.Security.Cryptography.CngAlgorithmGroup.ECDiffieHellman == this.Key.AlgorithmGroup);
      Contract.Ensures(this.Key.AlgorithmGroup != null);
    }

    public override void FromXmlString(string xmlString)
    {
    }

    public override string ToXmlString(bool includePrivateParameters)
    {
      return default(string);
    }

    public string ToXmlString(ECKeyXmlFormat format)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
    #endregion

    #region Properties and indexers
    public CngAlgorithm HashAlgorithm
    {
      get
      {
        return default(CngAlgorithm);
      }
      set
      {
      }
    }

    public byte[] HmacKey
    {
      get
      {
        return default(byte[]);
      }
      set
      {
      }
    }

    public CngKey Key
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Security.Cryptography.CngKey>() != null);

        return default(CngKey);
      }
      private set
      {
      }
    }

    public ECDiffieHellmanKeyDerivationFunction KeyDerivationFunction
    {
      get
      {
        return default(ECDiffieHellmanKeyDerivationFunction);
      }
      set
      {
      }
    }

    public byte[] Label
    {
      get
      {
        return default(byte[]);
      }
      set
      {
      }
    }

    public override ECDiffieHellmanPublicKey PublicKey
    {
      get
      {
        return default(ECDiffieHellmanPublicKey);
      }
    }

    public byte[] SecretAppend
    {
      get
      {
        return default(byte[]);
      }
      set
      {
      }
    }

    public byte[] SecretPrepend
    {
      get
      {
        return default(byte[]);
      }
      set
      {
      }
    }

    public byte[] Seed
    {
      get
      {
        return default(byte[]);
      }
      set
      {
      }
    }

    public bool UseSecretAgreementAsHmacKey
    {
      get
      {
        Contract.Ensures(Contract.Result<bool>() == (this.HmacKey == null));

        return default(bool);
      }
    }
    #endregion
  }
}
