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

// File System.Text.Encoding.cs
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
  abstract public partial class Encoding : ICloneable
  {
    #region Methods and constructors
    public virtual new Object Clone()
    {
      return default(Object);
    }

    public static byte[] Convert(System.Text.Encoding srcEncoding, System.Text.Encoding dstEncoding, byte[] bytes)
    {
      return default(byte[]);
    }

    public static byte[] Convert(System.Text.Encoding srcEncoding, System.Text.Encoding dstEncoding, byte[] bytes, int index, int count)
    {
      return default(byte[]);
    }

    protected Encoding()
    {
    }

    protected Encoding(int codePage)
    {
    }

    public override bool Equals(Object value)
    {
      return default(bool);
    }

    unsafe public virtual new int GetByteCount(char* chars, int count)
    {
      return default(int);
    }

    public virtual new int GetByteCount(char[] chars)
    {
      return default(int);
    }

    public virtual new int GetByteCount(string s)
    {
      return default(int);
    }

    public abstract int GetByteCount(char[] chars, int index, int count);

    unsafe public virtual new int GetBytes(char* chars, int charCount, byte* bytes, int byteCount)
    {
      return default(int);
    }

    public virtual new int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
    {
      return default(int);
    }

    public virtual new byte[] GetBytes(char[] chars, int index, int count)
    {
      return default(byte[]);
    }

    public virtual new byte[] GetBytes(char[] chars)
    {
      return default(byte[]);
    }

    public virtual new byte[] GetBytes(string s)
    {
      return default(byte[]);
    }

    public abstract int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex);

    public abstract int GetCharCount(byte[] bytes, int index, int count);

    unsafe public virtual new int GetCharCount(byte* bytes, int count)
    {
      return default(int);
    }

    public virtual new int GetCharCount(byte[] bytes)
    {
      return default(int);
    }

    unsafe public virtual new int GetChars(byte* bytes, int byteCount, char* chars, int charCount)
    {
      return default(int);
    }

    public virtual new char[] GetChars(byte[] bytes, int index, int count)
    {
      return default(char[]);
    }

    public virtual new char[] GetChars(byte[] bytes)
    {
      return default(char[]);
    }

    public abstract int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex);

    public virtual new Decoder GetDecoder()
    {
      return default(Decoder);
    }

    public virtual new Encoder GetEncoder()
    {
      return default(Encoder);
    }

    public static System.Text.Encoding GetEncoding(string name)
    {
      return default(System.Text.Encoding);
    }

    public static System.Text.Encoding GetEncoding(int codepage, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
    {
      Contract.Ensures(Contract.Result<System.Text.Encoding>() != null);

      return default(System.Text.Encoding);
    }

    public static System.Text.Encoding GetEncoding(string name, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
    {
      Contract.Ensures(Contract.Result<System.Text.Encoding>() != null);

      return default(System.Text.Encoding);
    }

    public static System.Text.Encoding GetEncoding(int codepage)
    {
      return default(System.Text.Encoding);
    }

    public static EncodingInfo[] GetEncodings()
    {
      return default(EncodingInfo[]);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public abstract int GetMaxByteCount(int charCount);

    public abstract int GetMaxCharCount(int byteCount);

    public virtual new byte[] GetPreamble()
    {
      return default(byte[]);
    }

    public virtual new string GetString(byte[] bytes)
    {
      return default(string);
    }

    public virtual new string GetString(byte[] bytes, int index, int count)
    {
      return default(string);
    }

    public bool IsAlwaysNormalized()
    {
      return default(bool);
    }

    public virtual new bool IsAlwaysNormalized(NormalizationForm form)
    {
      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public static System.Text.Encoding ASCII
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Text.Encoding>() != null);

        return default(System.Text.Encoding);
      }
    }

    public static System.Text.Encoding BigEndianUnicode
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Text.Encoding>() != null);

        return default(System.Text.Encoding);
      }
    }

    public virtual new string BodyName
    {
      get
      {
        return default(string);
      }
    }

    public virtual new int CodePage
    {
      get
      {
        return default(int);
      }
    }

    public DecoderFallback DecoderFallback
    {
      get
      {
        return default(DecoderFallback);
      }
      set
      {
      }
    }

    public static System.Text.Encoding Default
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Text.Encoding>() != null);

        return default(System.Text.Encoding);
      }
    }

    public EncoderFallback EncoderFallback
    {
      get
      {
        return default(EncoderFallback);
      }
      set
      {
      }
    }

    public virtual new string EncodingName
    {
      get
      {
        return default(string);
      }
    }

    public virtual new string HeaderName
    {
      get
      {
        return default(string);
      }
    }

    public virtual new bool IsBrowserDisplay
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool IsBrowserSave
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool IsMailNewsDisplay
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool IsMailNewsSave
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool IsSingleByte
    {
      get
      {
        return default(bool);
      }
    }

    public static System.Text.Encoding Unicode
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Text.Encoding>() != null);

        return default(System.Text.Encoding);
      }
    }

    public static System.Text.Encoding UTF32
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Text.Encoding>() != null);

        return default(System.Text.Encoding);
      }
    }

    public static System.Text.Encoding UTF7
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Text.Encoding>() != null);

        return default(System.Text.Encoding);
      }
    }

    public static System.Text.Encoding UTF8
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Text.Encoding>() != null);

        return default(System.Text.Encoding);
      }
    }

    public virtual new string WebName
    {
      get
      {
        return default(string);
      }
    }

    public virtual new int WindowsCodePage
    {
      get
      {
        return default(int);
      }
    }
    #endregion
  }
}
