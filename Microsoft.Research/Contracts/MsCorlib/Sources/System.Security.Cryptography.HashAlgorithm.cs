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

// File System.Security.Cryptography.HashAlgorithm.cs
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
  abstract public partial class HashAlgorithm : ICryptoTransform, IDisposable
  {
    #region Methods and constructors
    public void Clear()
    {
    }

    public byte[] ComputeHash(byte[] buffer, int offset, int count)
    {
      return default(byte[]);
    }

    public byte[] ComputeHash(byte[] buffer)
    {
      return default(byte[]);
    }

    public byte[] ComputeHash(Stream inputStream)
    {
      Contract.Requires(inputStream != null);

      return default(byte[]);
    }

    public static System.Security.Cryptography.HashAlgorithm Create(string hashName)
    {
      return default(System.Security.Cryptography.HashAlgorithm);
    }

    public static System.Security.Cryptography.HashAlgorithm Create()
    {
      return default(System.Security.Cryptography.HashAlgorithm);
    }

    protected virtual new void Dispose(bool disposing)
    {
    }

    public void Dispose()
    {
    }

    protected HashAlgorithm()
    {
    }

    protected abstract void HashCore(byte[] array, int ibStart, int cbSize);

    protected abstract byte[] HashFinal();

    public abstract void Initialize();

    public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
    {
      return default(int);
    }

    public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
    {
      return default(byte[]);
    }
    #endregion

    #region Properties and indexers
    public virtual new bool CanReuseTransform
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool CanTransformMultipleBlocks
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new byte[] Hash
    {
      get
      {
        return default(byte[]);
      }
    }

    public virtual new int HashSize
    {
      get
      {
        return default(int);
      }
    }

    public virtual new int InputBlockSize
    {
      get
      {
        return default(int);
      }
    }

    public virtual new int OutputBlockSize
    {
      get
      {
        return default(int);
      }
    }
    #endregion

    #region Fields
    protected int HashSizeValue;
    internal protected byte[] HashValue;
    protected int State;
    #endregion
  }
}
