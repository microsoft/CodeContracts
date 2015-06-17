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

// File System.Reflection.Emit.DynamicILInfo.cs
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


namespace System.Reflection.Emit
{
  public partial class DynamicILInfo
  {
    #region Methods and constructors
    internal DynamicILInfo()
    {
    }

    public int GetTokenFor(RuntimeFieldHandle field, RuntimeTypeHandle contextType)
    {
      return default(int);
    }

    public int GetTokenFor(RuntimeFieldHandle field)
    {
      return default(int);
    }

    public int GetTokenFor(RuntimeMethodHandle method, RuntimeTypeHandle contextType)
    {
      return default(int);
    }

    public int GetTokenFor(byte[] signature)
    {
      return default(int);
    }

    public int GetTokenFor(string literal)
    {
      return default(int);
    }

    public int GetTokenFor(RuntimeTypeHandle type)
    {
      return default(int);
    }

    public int GetTokenFor(RuntimeMethodHandle method)
    {
      return default(int);
    }

    public int GetTokenFor(DynamicMethod method)
    {
      return default(int);
    }

    unsafe public void SetCode(byte* code, int codeSize, int maxStackSize)
    {
    }

    public void SetCode(byte[] code, int maxStackSize)
    {
      Contract.Ensures((code.Length - Contract.OldValue(code.Length)) <= 0);
    }

    public void SetExceptions(byte[] exceptions)
    {
      Contract.Ensures((exceptions.Length - Contract.OldValue(exceptions.Length)) <= 0);
    }

    unsafe public void SetExceptions(byte* exceptions, int exceptionsSize)
    {
    }

    public void SetLocalSignature(byte[] localSignature)
    {
      Contract.Ensures((localSignature.Length - Contract.OldValue(localSignature.Length)) <= 0);
    }

    unsafe public void SetLocalSignature(byte* localSignature, int signatureSize)
    {
    }
    #endregion

    #region Properties and indexers
    public DynamicMethod DynamicMethod
    {
      get
      {
        return default(DynamicMethod);
      }
    }
    #endregion
  }
}
