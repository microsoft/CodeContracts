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

using System;
using System.Diagnostics.Contracts;

namespace System
{
  public enum UriKind
  {
    RelativeOrAbsolute,
    Absolute,
    Relative
  }

  public enum UriFormat
  {
    SafeUnescaped = 3,
    Unescaped = 2,
    UriEscaped = 1
  }

  public enum UriComponents
  {
    AbsoluteUri = 0x7f,
    Fragment = 0x40,
    Host = 4,
    HostAndPort = 0x84,
    HttpRequestUrl = 0x3d,
    KeepDelimiter = 0x40000000,
    Path = 0x10,
    PathAndQuery = 0x30,
    Port = 8,
    Query = 0x20,
    Scheme = 1,
    SchemeAndServer = 13,
    SerializationInfoString = -2147483648,
    StrongAuthority = 0x86,
    StrongPort = 0x80,
    UserInfo = 2
  }

#if !SILVERLIGHT
  public enum UriPartial
  {
    Scheme,
    Authority,
    Path,
    Query
  }

#endif

 



  public class Uri
  {
    #region Constructors

#if !SILVERLIGHT
    public Uri(Uri baseUri, string relativeUri, bool dontEscape)
    {
      Contract.Requires(baseUri != null);
      Contract.Requires(relativeUri != null);
    }
#endif

    public Uri(Uri baseUri, string relativeUri)
    {
      Contract.Requires(baseUri != null);
      Contract.Requires(relativeUri != null);
    }

#if !SILVERLIGHT
    public Uri(string uriString, bool dontEscape)
    {
      Contract.Requires(uriString != null);
    }
#endif

    public Uri(string uriString)
    {
      Contract.Requires(uriString != null);
    }

    #endregion

    #region Properties

    public string AbsolutePath
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

    public string AbsoluteUri
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

#if !SILVERLIGHT
    public string Authority
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
#endif

    public string DnsSafeHost
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

    public string Fragment
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

    public string Host
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

#if false
        public UriHostNameType HostNameType
        {
          get;
        }
#endif

#if !SILVERLIGHT
    extern public bool IsDefaultPort
    {
      get;
    }

    extern public bool IsFile
    {
      get;
    }

    extern public bool IsLoopback
    {
      get;
    }
#endif

    extern public bool IsUnc
    {
      get;
    }

    public string LocalPath
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }


    public string OriginalString
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

#if !SILVERLIGHT
    public string PathAndQuery
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
#endif

    extern public int Port
    {
      get;
    }


    public string Query
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);

      }
    }

    public string Scheme
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

#if !SILVERLIGHT
    public String[] Segments
    {
      get
      {
        Contract.Ensures(Contract.Result<string[]>() != null);
        Contract.Ensures(Contract.ForAll(Contract.Result<string[]>(), x => x != null));
        
        return default(string[]);
      }
    }
#endif

    extern public bool UserEscaped
    {
      get;
    }

    public string UserInfo
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }


    #endregion

    #region Methods

    // extern public static UriHostNameType CheckHostName(string name);

    extern public static bool CheckSchemeName(string schemeName);

    [Pure]
    public static int Compare(
        Uri uri1,
        Uri uri2,
        UriComponents partsToCompare,
        UriFormat compareFormat,
        StringComparison comparisonType
    )
    {
      return default(int);
    }

    [Pure]
    public static string EscapeDataString(
        string stringToEscape
    )
    {
      Contract.Requires(stringToEscape != null);
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    [Pure]
    public static string EscapeUriString(
        string stringToEscape
    )
    {
      Contract.Requires(stringToEscape != null);
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    [Pure]
    public static int FromHex(Char digit)
    {
      Contract.Ensures(Contract.Result<int>() >= 0);
      Contract.Ensures(Contract.Result<int>() < 16);

      return default(int);
    }

    [Pure]
    public string GetComponents(
        UriComponents components,
        UriFormat format
    )
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }

#if !SILVERLIGHT
    [Pure]
    public string GetLeftPart(UriPartial part)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
#endif

#if !SILVERLIGHT
    [Pure]
    public static string HexEscape(char character)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }

    [Pure]
    public static Char HexUnescape(string pattern, ref int index)
    {
      Contract.Requires(pattern != null);
      Contract.Requires(index >= 0);
      Contract.Requires(index < pattern.Length);
      Contract.Ensures(index > Contract.OldValue(index));

      return default(Char);
    }
#endif

    [Pure]
    public bool IsBaseOf(Uri uri)
    {
      return default(bool);
    }

    [Pure]
    public static bool IsHexDigit(Char character)
    {
      return default(bool);
    }

#if !SILVERLIGHT

    [Pure]
    public static bool IsHexEncoding(string pattern, int index)
    {
      Contract.Requires(pattern != null);
      Contract.Requires(index >= 0);
      Contract.Requires(index < pattern.Length);

      return default(bool);
    }

    [Pure]
    public bool IsWellFormedOriginalString()
    {
      return default(bool);
    }
#endif

    [Pure]
    public static bool IsWellFormedUriString(string uriString, UriKind uriKind)
    {
      return default(bool);
    }

    [Pure]
    public Uri MakeRelativeUri(Uri toUri)
    {
      Contract.Ensures(Contract.Result<Uri>() != null);
      return default(Uri);
    }

    [Pure]
    public static bool TryCreate(
        string uriString,
        UriKind uriKind,
        out Uri result
    )
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out result) != null);
      result = null;
      return default(bool);
    }

    [Pure]
    public static bool TryCreate(
        Uri baseUri,
        string relativeUri,
        out Uri result
    )
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out result) != null);
      result = null;
      return default(bool);
    }

    public static bool TryCreate(
        Uri baseUri,
        Uri relativeUri,
        out Uri result
    )
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out result) != null);
      result = null;
      return default(bool);
    }

    [Pure]
    public static string UnescapeDataString(string stringToUnescape)
    {
      Contract.Requires(stringToUnescape != null);
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    #endregion
  }
}