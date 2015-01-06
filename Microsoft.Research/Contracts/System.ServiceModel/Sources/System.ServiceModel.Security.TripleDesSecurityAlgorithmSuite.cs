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

// File System.ServiceModel.Security.TripleDesSecurityAlgorithmSuite.cs
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
  public partial class TripleDesSecurityAlgorithmSuite : SecurityAlgorithmSuite
  {
    #region Methods and constructors
    public override bool IsAsymmetricKeyLengthSupported(int length)
    {
      return default(bool);
    }

    public override bool IsSymmetricKeyLengthSupported(int length)
    {
      return default(bool);
    }

    public TripleDesSecurityAlgorithmSuite()
    {
    }
    #endregion

    #region Properties and indexers
    public override string DefaultAsymmetricKeyWrapAlgorithm
    {
      get
      {
        return default(string);
      }
    }

    public override string DefaultAsymmetricSignatureAlgorithm
    {
      get
      {
        return default(string);
      }
    }

    public override string DefaultCanonicalizationAlgorithm
    {
      get
      {
        return default(string);
      }
    }

    public override string DefaultDigestAlgorithm
    {
      get
      {
        return default(string);
      }
    }

    public override string DefaultEncryptionAlgorithm
    {
      get
      {
        return default(string);
      }
    }

    public override int DefaultEncryptionKeyDerivationLength
    {
      get
      {
        return default(int);
      }
    }

    public override int DefaultSignatureKeyDerivationLength
    {
      get
      {
        return default(int);
      }
    }

    public override int DefaultSymmetricKeyLength
    {
      get
      {
        return default(int);
      }
    }

    public override string DefaultSymmetricKeyWrapAlgorithm
    {
      get
      {
        return default(string);
      }
    }

    public override string DefaultSymmetricSignatureAlgorithm
    {
      get
      {
        return default(string);
      }
    }
    #endregion
  }
}
