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

// File System.Security.Cryptography.DSACryptoServiceProvider.cs
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
  sealed public partial class DSACryptoServiceProvider : DSA, ICspAsymmetricAlgorithm
  {
    #region Methods and constructors
    public override byte[] CreateSignature(byte[] rgbHash)
    {
      return default(byte[]);
    }

    protected override void Dispose(bool disposing)
    {
    }

    public DSACryptoServiceProvider()
    {
    }

    public DSACryptoServiceProvider(CspParameters parameters)
    {
    }

    public DSACryptoServiceProvider(int dwKeySize)
    {
    }

    public DSACryptoServiceProvider(int dwKeySize, CspParameters parameters)
    {
    }

    public byte[] ExportCspBlob(bool includePrivateParameters)
    {
      return default(byte[]);
    }

    public override DSAParameters ExportParameters(bool includePrivateParameters)
    {
      return default(DSAParameters);
    }

    public void ImportCspBlob(byte[] keyBlob)
    {
    }

    public override void ImportParameters(DSAParameters parameters)
    {
    }

    public byte[] SignData(Stream inputStream)
    {
      Contract.Requires(inputStream != null);

      return default(byte[]);
    }

    public byte[] SignData(byte[] buffer)
    {
      return default(byte[]);
    }

    public byte[] SignData(byte[] buffer, int offset, int count)
    {
      return default(byte[]);
    }

    public byte[] SignHash(byte[] rgbHash, string str)
    {
      return default(byte[]);
    }

    public bool VerifyData(byte[] rgbData, byte[] rgbSignature)
    {
      return default(bool);
    }

    public bool VerifyHash(byte[] rgbHash, string str, byte[] rgbSignature)
    {
      return default(bool);
    }

    public override bool VerifySignature(byte[] rgbHash, byte[] rgbSignature)
    {
      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public CspKeyContainerInfo CspKeyContainerInfo
    {
      get
      {
        return default(CspKeyContainerInfo);
      }
    }

    public override string KeyExchangeAlgorithm
    {
      get
      {
        return default(string);
      }
    }

    public override int KeySize
    {
      get
      {
        return default(int);
      }
    }

    public bool PersistKeyInCsp
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool PublicOnly
    {
      get
      {
        return default(bool);
      }
    }

    public override string SignatureAlgorithm
    {
      get
      {
        return default(string);
      }
    }

    public static bool UseMachineKeyStore
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }
    #endregion
  }
}
