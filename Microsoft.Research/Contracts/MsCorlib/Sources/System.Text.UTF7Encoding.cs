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

// File System.Text.UTF7Encoding.cs
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


namespace System.Text
{
  public partial class UTF7Encoding : Encoding
  {
    #region Methods and constructors
    public override bool Equals(Object value)
    {
      return default(bool);
    }

    unsafe public override int GetByteCount(char* chars, int count)
    {
      return default(int);
    }

    public override int GetByteCount(char[] chars, int index, int count)
    {
      return default(int);
    }

    public override int GetByteCount(string s)
    {
      return default(int);
    }

    public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
    {
      return default(int);
    }

    public override int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
    {
      return default(int);
    }

    unsafe public override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount)
    {
      return default(int);
    }

    unsafe public override int GetCharCount(byte* bytes, int count)
    {
      return default(int);
    }

    public override int GetCharCount(byte[] bytes, int index, int count)
    {
      return default(int);
    }

    unsafe public override int GetChars(byte* bytes, int byteCount, char* chars, int charCount)
    {
      return default(int);
    }

    public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
    {
      return default(int);
    }

    public override Decoder GetDecoder()
    {
      return default(Decoder);
    }

    public override Encoder GetEncoder()
    {
      return default(Encoder);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public override int GetMaxByteCount(int charCount)
    {
      return default(int);
    }

    public override int GetMaxCharCount(int byteCount)
    {
      return default(int);
    }

    public override string GetString(byte[] bytes, int index, int count)
    {
      return default(string);
    }

    public UTF7Encoding()
    {
    }

    public UTF7Encoding(bool allowOptionals)
    {
    }
    #endregion
  }
}
