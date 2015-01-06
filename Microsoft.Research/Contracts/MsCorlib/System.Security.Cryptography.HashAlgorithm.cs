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

  public abstract class HashAlgorithm // : ICryptoTransform
  {

    protected HashAlgorithm() { }

    public virtual int HashSize
    {
      get { return default(int); }
    }

    public virtual int InputBlockSize
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() == 1);
        return default(int);
      }
    }

    public virtual bool CanTransformMultipleBlocks
    {
      get { return default(bool); }
    }

    public virtual Byte[] Hash
    {
      get 
      {
        Contract.Ensures(Contract.Result<byte[]>() != null);
      
        return default(Byte[]); 
      }
    }

    public virtual int OutputBlockSize
    {
      get { return default(int); }
    }

    public virtual bool CanReuseTransform
    {
      get { return default(bool); }
    }

    public virtual void Initialize()
    {
    }

    public void Clear()
    {
    }

    abstract public Byte[] TransformFinalBlock(Byte[] inputBuffer, int inputOffset, int inputCount);

    abstract public int TransformBlock(Byte[] inputBuffer, int inputOffset, int inputCount, Byte[] outputBuffer, int outputOffset);

    public Byte[] ComputeHash(Byte[] buffer, int offset, int count)
    {
      Contract.Requires(buffer != null);
      
      Contract.Requires(offset >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires(count <= buffer.Length);
      Contract.Requires((buffer.Length - count) >= offset);

      return default(Byte[]);
    }
    
    public Byte[] ComputeHash(Byte[] buffer)
    {
      Contract.Requires(buffer != null);

      Contract.Ensures(Contract.Result<Byte[]>() != null);
      
      return default(Byte[]);
    }

    public Byte[] ComputeHash(System.IO.Stream inputStream)
    {
      Contract.Requires(inputStream != null);
      
      Contract.Ensures(Contract.Result<Byte[]>() != null);
      
      return default(Byte[]);
    }
#if !SILVERLIGHT
    public static HashAlgorithm Create(string hashName)
    {
      Contract.Requires(hashName != null);

      Contract.Ensures(Contract.Result<HashAlgorithm>() != null);
      
      return default(HashAlgorithm);
    }

    public static HashAlgorithm Create()
    {
      Contract.Ensures(Contract.Result<HashAlgorithm>() != null);

      return default(HashAlgorithm);
    }
#endif
  }
}
