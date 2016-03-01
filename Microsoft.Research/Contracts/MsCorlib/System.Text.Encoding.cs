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

namespace System.Text
{

  public class Encoding
  {
    protected Encoding() { }

#if !SILVERLIGHT
    extern public virtual int WindowsCodePage
    {
      get;
    }

    extern public virtual bool IsMailNewsDisplay
    {
      get;
    }
#endif

    public static Encoding UTF8
    {
      get
      {
        Contract.Ensures(Contract.Result<Encoding>() != null);
        return default(Encoding);
      }
    }

#if !SILVERLIGHT
    public static Encoding Default
    {
      get
      {
        Contract.Ensures(Contract.Result<Encoding>() != null);
        return default(Encoding);
      }
    }

    extern public virtual bool IsMailNewsSave
    {
      get;
    }
#endif


#if !SILVERLIGHT
    public virtual string EncodingName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

    public virtual string BodyName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

    public static Encoding UTF7
    {
      get
      {
        Contract.Ensures(Contract.Result<Encoding>() != null);
        return default(Encoding);
      }
    }

    extern public virtual bool IsBrowserSave
    {
      get;
    }


    public virtual string HeaderName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

    extern public virtual int CodePage
    {
      get;
    }
#endif

    public static Encoding Unicode
    {
      get
      {
        Contract.Ensures(Contract.Result<Encoding>() != null);
        return default(Encoding);
      }
    }

    public virtual string WebName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

    public static Encoding BigEndianUnicode
    {
      get
      {
        Contract.Ensures(Contract.Result<Encoding>() != null);
        return default(Encoding);
      }
    }

#if !SILVERLIGHT
    public static Encoding ASCII
    {
      get
      {
        Contract.Ensures(Contract.Result<Encoding>() != null);
        return default(Encoding);
      }
    }
#endif

#if !SILVERLIGHT
    extern virtual public bool IsBrowserDisplay
    {
      get;
    }
#endif

    [Pure]
    public virtual string GetString(byte[] bytes, int index, int count)
    {
      Contract.Requires(bytes != null);
      Contract.Requires(index >= 0 && index <= bytes.Length);
      Contract.Requires(count >= 0 && index + count <= bytes.Length);
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
#if !SILVERLIGHT
    [Pure]
    public virtual string GetString(byte[] bytes)
    {
      Contract.Requires(bytes != null);
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
#endif
    [Pure]
    public virtual int GetMaxCharCount(int byteCount)
    {
      Contract.Requires(byteCount >= 0);
      Contract.Ensures(Contract.Result<int>() >= 0);
      return default(int);
    }
    [Pure]
    public virtual int GetMaxByteCount(int charCount)
    {
      Contract.Requires(charCount >= 0);
      Contract.Ensures(Contract.Result<int>() >= 0);
      return default(int);
    }
    [Pure]
    public virtual Encoder GetEncoder()
    {
      Contract.Ensures(Contract.Result<Encoder>() != null);
      return default(Encoder);
    }
    [Pure]
    public virtual Decoder GetDecoder()
    {
      Contract.Ensures(Contract.Result<Decoder>() != null);
      return default(Decoder);
    }
    public virtual int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
    {
      Contract.Requires(bytes != null);
      Contract.Requires(byteIndex >= 0 && byteIndex <= bytes.Length);
      Contract.Requires(byteCount >= 0 && byteIndex + byteCount <= bytes.Length);
      Contract.Requires(chars != null);
      Contract.Requires(charIndex >= 0 && charIndex <= chars.Length);

      Contract.Ensures(Contract.Result<int>() >= 0);

      return default(int);
    }
    [Pure]
    public virtual char[] GetChars(byte[] bytes, int index, int count)
    {
      Contract.Requires(bytes != null);
      Contract.Requires(index >= 0 && index <= bytes.Length);
      Contract.Requires(count >= 0 && index + count <= bytes.Length);
      Contract.Ensures(Contract.Result<char[]>() != null);

      return default(char[]);
    }
    [Pure]
    public virtual char[] GetChars(byte[] bytes)
    {
      Contract.Requires(bytes != null);
      Contract.Ensures(Contract.Result<char[]>() != null);

      return default(char[]);
    }
    [Pure]
    public virtual int GetCharCount(byte[] bytes, int index, int count)
    {
      Contract.Requires(bytes != null);
      Contract.Requires(index >= 0 && index <= bytes.Length);
      Contract.Requires(count >= 0 && index + count <= bytes.Length);
      Contract.Ensures(Contract.Result<int>() >= 0);

      return default(int);
    }
    [Pure]
    public virtual int GetCharCount(byte[] bytes)
    {
      Contract.Requires(bytes != null);
      Contract.Ensures(Contract.Result<int>() >= 0);

      return default(int);
    }
    public virtual int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
    {
      Contract.Requires(s != null);
      Contract.Requires(charIndex >= 0 && charIndex <= s.Length);
      Contract.Requires(charCount >= 0 && charIndex + charCount <= s.Length);
      Contract.Requires(bytes != null);
      Contract.Requires(byteIndex >= 0 && byteIndex <= bytes.Length);

      Contract.Ensures(Contract.Result<int>() >= 0);

      return default(int);
    }
    [Pure]
    public virtual byte[] GetBytes(string s)
    {
      Contract.Requires(s != null);
      Contract.Ensures(Contract.Result<byte[]>() != null);

      return default(byte[]);
    }
    public virtual int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
    {
      Contract.Requires(chars != null);
      Contract.Requires(charIndex >= 0 && charIndex <= chars.Length);
      Contract.Requires(charCount >= 0 && charIndex + charCount <= chars.Length);
      Contract.Requires(bytes != null);
      Contract.Requires(byteIndex >= 0 && byteIndex <= bytes.Length);

      Contract.Ensures(Contract.Result<int>() >= 0);

      return default(int);
    }
    [Pure]
    public virtual byte[] GetBytes(char[] chars, int index, int count)
    {
      Contract.Requires(chars != null);
      Contract.Requires(index >= 0 && index <= chars.Length);
      Contract.Requires(count >= 0 && index + count <= chars.Length);
      Contract.Ensures(Contract.Result<byte[]>() != null);

      return default(byte[]);
    }
    [Pure]
    public virtual byte[] GetBytes(char[] chars)
    {
      Contract.Requires(chars != null);
      Contract.Ensures(Contract.Result<byte[]>() != null);

      return default(byte[]);
    }
    [Pure]
    public virtual int GetByteCount(char[] chars, int index, int count)
    {
      Contract.Requires(chars != null);
      Contract.Requires(index >= 0 && index <= chars.Length);
      Contract.Requires(count >= 0 && index + count <= chars.Length);
      Contract.Ensures(Contract.Result<int>() >= 0);

      return default(int);
    }
    [Pure]
    public virtual int GetByteCount(string s)
    {
      Contract.Requires(s != null);
      Contract.Ensures(Contract.Result<int>() >= 0);

      return default(int);
    }
    [Pure]
    public virtual int GetByteCount(char[] chars)
    {
      Contract.Requires(chars != null);
      Contract.Ensures(Contract.Result<int>() >= 0);

      return default(int);
    }
    [Pure]
    public virtual byte[] GetPreamble()
    {
      Contract.Ensures(Contract.Result<byte[]>() != null);

      return default(byte[]);
    }
    [Pure]
    public static Encoding GetEncoding(string name)
    {
      Contract.Ensures(Contract.Result<Encoding>() != null);
      return default(Encoding);
    }
#if !SILVERLIGHT
    [Pure]
    public static Encoding GetEncoding(int codepage)
    {
      Contract.Requires(codepage >= 0);
      Contract.Requires(codepage <= 65535);
      Contract.Ensures(Contract.Result<Encoding>() != null);
      return default(Encoding);
    }
#endif
    [Pure]
    public static byte[] Convert(Encoding srcEncoding, Encoding dstEncoding, byte[] bytes, int index, int count)
    {
      Contract.Requires(srcEncoding != null);
      Contract.Requires(dstEncoding != null);
      Contract.Requires(bytes != null);
      Contract.Requires(index >= 0 && index <= bytes.Length);
      Contract.Requires(count >= 0 && index + count <= bytes.Length);
      Contract.Ensures(Contract.Result<byte[]>() != null);
      return default(byte[]);
    }
    [Pure]
    public static byte[] Convert(Encoding srcEncoding, Encoding dstEncoding, byte[] bytes)
    {
      Contract.Requires(bytes != null);
      Contract.Ensures(Contract.Result<byte[]>() != null);
      return default(byte[]);
    }
  }
}
