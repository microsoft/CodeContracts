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

using System.Diagnostics.Contracts;
using System;

namespace System.Security.Cryptography
{

    [ContractClass(typeof(SymmetricAlgorithmContracts))]
  public abstract class SymmetricAlgorithm {
#if false
    extern public int FeedbackSize
    {
      get;
      set;
    }

    extern public KeySizes[] LegalBlockSizes
    {
      get;
    }

    extern public int BlockSize
    {
      get;
      set;
    }


    public CipherMode Mode
    {
      get { return default(CipherMode); }
      set
      {
        Contract.Requires((int)value >= 1);
        Contract.Requires(4 >= (int)value);
      }
    }

      extern public int KeySize
    {
      get;
      set;
    }

    public Byte[] IV
    {
      get { return default(Byte[]); }
      set
      {
        Contract.Requires(value != null);
      }
    }

    public Byte[] Key
    {
      get { return default(Byte[]); }
      set
      {
        Contract.Requires(value != null);
      }
    }


    extern public KeySizes[] LegalKeySizes
    {
      get;
    }

        public PaddingMode Padding
        {
          get;
            set
            {
                Contract.Requires((int)value >= 1);
                Contract.Requires(3 >= (int)value);
            }
        }

    public void GenerateIV()
    {

    }
    public void GenerateKey()
    {

    }
#endif
    public abstract ICryptoTransform CreateDecryptor(Byte[] arg0, Byte[] arg1);

    public virtual ICryptoTransform CreateDecryptor()
    {
      Contract.Ensures(Contract.Result<ICryptoTransform>() != null);
      return default(ICryptoTransform);
    }
    public abstract ICryptoTransform CreateEncryptor(Byte[] arg0, Byte[] arg1);

    public virtual ICryptoTransform CreateEncryptor()
    {
      Contract.Ensures(Contract.Result<ICryptoTransform>() != null);
      return default(ICryptoTransform);
    }
    
#if false
      public static SymmetricAlgorithm Create(string algName)
    {

      return default(SymmetricAlgorithm);
    }
    public static SymmetricAlgorithm Create()
    {

      return default(SymmetricAlgorithm);
    }
    public bool ValidKeySize(int bitLength)
    {

      return default(bool);
    }
    public void Clear()
    {

    }
    public SymmetricAlgorithm()
    {

    }
#endif
  }

    [ContractClassFor(typeof(SymmetricAlgorithm))]
  public class SymmetricAlgorithmContracts : SymmetricAlgorithm {
      public override ICryptoTransform CreateDecryptor(byte[] arg0, byte[] arg1) {
          Contract.Ensures(Contract.Result<ICryptoTransform>() != null);
          return default(ICryptoTransform);
      }

      public override ICryptoTransform CreateEncryptor(byte[] arg0, byte[] arg1) {
          Contract.Ensures(Contract.Result<ICryptoTransform>() != null);
          return default(ICryptoTransform);
      }
  }
}
