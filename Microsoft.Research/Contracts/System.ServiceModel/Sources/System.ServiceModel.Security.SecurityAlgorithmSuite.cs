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

// File System.ServiceModel.Security.SecurityAlgorithmSuite.cs
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


namespace System.ServiceModel.Security
{
  abstract public partial class SecurityAlgorithmSuite
  {
    #region Methods and constructors
    public abstract bool IsAsymmetricKeyLengthSupported(int length);

    public virtual new bool IsAsymmetricKeyWrapAlgorithmSupported(string algorithm)
    {
      return default(bool);
    }

    public virtual new bool IsAsymmetricSignatureAlgorithmSupported(string algorithm)
    {
      return default(bool);
    }

    public virtual new bool IsCanonicalizationAlgorithmSupported(string algorithm)
    {
      return default(bool);
    }

    public virtual new bool IsDigestAlgorithmSupported(string algorithm)
    {
      return default(bool);
    }

    public virtual new bool IsEncryptionAlgorithmSupported(string algorithm)
    {
      return default(bool);
    }

    public virtual new bool IsEncryptionKeyDerivationAlgorithmSupported(string algorithm)
    {
      return default(bool);
    }

    public virtual new bool IsSignatureKeyDerivationAlgorithmSupported(string algorithm)
    {
      return default(bool);
    }

    public abstract bool IsSymmetricKeyLengthSupported(int length);

    public virtual new bool IsSymmetricKeyWrapAlgorithmSupported(string algorithm)
    {
      return default(bool);
    }

    public virtual new bool IsSymmetricSignatureAlgorithmSupported(string algorithm)
    {
      return default(bool);
    }

    protected SecurityAlgorithmSuite()
    {
    }
    #endregion

    #region Properties and indexers
    public static System.ServiceModel.Security.SecurityAlgorithmSuite Basic128
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Security.SecurityAlgorithmSuite>() != null);

        return default(System.ServiceModel.Security.SecurityAlgorithmSuite);
      }
    }

    public static System.ServiceModel.Security.SecurityAlgorithmSuite Basic128Rsa15
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Security.SecurityAlgorithmSuite>() != null);

        return default(System.ServiceModel.Security.SecurityAlgorithmSuite);
      }
    }

    public static System.ServiceModel.Security.SecurityAlgorithmSuite Basic128Sha256
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Security.SecurityAlgorithmSuite>() != null);

        return default(System.ServiceModel.Security.SecurityAlgorithmSuite);
      }
    }

    public static System.ServiceModel.Security.SecurityAlgorithmSuite Basic128Sha256Rsa15
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Security.SecurityAlgorithmSuite>() != null);

        return default(System.ServiceModel.Security.SecurityAlgorithmSuite);
      }
    }

    public static System.ServiceModel.Security.SecurityAlgorithmSuite Basic192
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Security.SecurityAlgorithmSuite>() != null);

        return default(System.ServiceModel.Security.SecurityAlgorithmSuite);
      }
    }

    public static System.ServiceModel.Security.SecurityAlgorithmSuite Basic192Rsa15
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Security.SecurityAlgorithmSuite>() != null);

        return default(System.ServiceModel.Security.SecurityAlgorithmSuite);
      }
    }

    public static System.ServiceModel.Security.SecurityAlgorithmSuite Basic192Sha256
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Security.SecurityAlgorithmSuite>() != null);

        return default(System.ServiceModel.Security.SecurityAlgorithmSuite);
      }
    }

    public static System.ServiceModel.Security.SecurityAlgorithmSuite Basic192Sha256Rsa15
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Security.SecurityAlgorithmSuite>() != null);

        return default(System.ServiceModel.Security.SecurityAlgorithmSuite);
      }
    }

    public static System.ServiceModel.Security.SecurityAlgorithmSuite Basic256
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Security.SecurityAlgorithmSuite>() != null);

        return default(System.ServiceModel.Security.SecurityAlgorithmSuite);
      }
    }

    public static System.ServiceModel.Security.SecurityAlgorithmSuite Basic256Rsa15
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Security.SecurityAlgorithmSuite>() != null);

        return default(System.ServiceModel.Security.SecurityAlgorithmSuite);
      }
    }

    public static System.ServiceModel.Security.SecurityAlgorithmSuite Basic256Sha256
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Security.SecurityAlgorithmSuite>() != null);

        return default(System.ServiceModel.Security.SecurityAlgorithmSuite);
      }
    }

    public static System.ServiceModel.Security.SecurityAlgorithmSuite Basic256Sha256Rsa15
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Security.SecurityAlgorithmSuite>() != null);

        return default(System.ServiceModel.Security.SecurityAlgorithmSuite);
      }
    }

    public static System.ServiceModel.Security.SecurityAlgorithmSuite Default
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Security.SecurityAlgorithmSuite>() != null);

        return default(System.ServiceModel.Security.SecurityAlgorithmSuite);
      }
    }

    public abstract string DefaultAsymmetricKeyWrapAlgorithm
    {
      get;
    }

    public abstract string DefaultAsymmetricSignatureAlgorithm
    {
      get;
    }

    public abstract string DefaultCanonicalizationAlgorithm
    {
      get;
    }

    public abstract string DefaultDigestAlgorithm
    {
      get;
    }

    public abstract string DefaultEncryptionAlgorithm
    {
      get;
    }

    public abstract int DefaultEncryptionKeyDerivationLength
    {
      get;
    }

    public abstract int DefaultSignatureKeyDerivationLength
    {
      get;
    }

    public abstract int DefaultSymmetricKeyLength
    {
      get;
    }

    public abstract string DefaultSymmetricKeyWrapAlgorithm
    {
      get;
    }

    public abstract string DefaultSymmetricSignatureAlgorithm
    {
      get;
    }

    public static System.ServiceModel.Security.SecurityAlgorithmSuite TripleDes
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Security.SecurityAlgorithmSuite>() != null);

        return default(System.ServiceModel.Security.SecurityAlgorithmSuite);
      }
    }

    public static System.ServiceModel.Security.SecurityAlgorithmSuite TripleDesRsa15
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Security.SecurityAlgorithmSuite>() != null);

        return default(System.ServiceModel.Security.SecurityAlgorithmSuite);
      }
    }

    public static System.ServiceModel.Security.SecurityAlgorithmSuite TripleDesSha256
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Security.SecurityAlgorithmSuite>() != null);

        return default(System.ServiceModel.Security.SecurityAlgorithmSuite);
      }
    }

    public static System.ServiceModel.Security.SecurityAlgorithmSuite TripleDesSha256Rsa15
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Security.SecurityAlgorithmSuite>() != null);

        return default(System.ServiceModel.Security.SecurityAlgorithmSuite);
      }
    }
    #endregion
  }
}
