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

// File System.Uri.cs
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


namespace System
{
  public partial class Uri : System.Runtime.Serialization.ISerializable
  {
    #region Methods and constructors
    public static bool operator != (System.Uri uri1, System.Uri uri2)
    {
      return default(bool);
    }

    public static bool operator == (System.Uri uri1, System.Uri uri2)
    {
      return default(bool);
    }

    protected virtual new void Canonicalize()
    {
    }

    public static UriHostNameType CheckHostName(string name)
    {
      Contract.Ensures(((System.UriHostNameType)(0)) <= Contract.Result<System.UriHostNameType>());
      Contract.Ensures(Contract.Result<System.UriHostNameType>() <= ((System.UriHostNameType)(4)));

      return default(UriHostNameType);
    }

    public static bool CheckSchemeName(string schemeName)
    {
      return default(bool);
    }

    protected virtual new void CheckSecurity()
    {
    }

    public static int Compare(System.Uri uri1, System.Uri uri2, UriComponents partsToCompare, UriFormat compareFormat, StringComparison comparisonType)
    {
      return default(int);
    }

    public override bool Equals(Object comparand)
    {
      return default(bool);
    }

    protected virtual new void Escape()
    {
    }

    public static string EscapeDataString(string stringToEscape)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    protected static string EscapeString(string str)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    public static string EscapeUriString(string stringToEscape)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    public static int FromHex(char digit)
    {
      Contract.Ensures(0 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() < digit);
      Contract.Ensures(Contract.Result<int>() <= 15);

      return default(int);
    }

    public string GetComponents(UriComponents components, UriFormat format)
    {
      return default(string);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public string GetLeftPart(UriPartial part)
    {
      return default(string);
    }

    protected void GetObjectData(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
    {
      Contract.Requires(serializationInfo != null);
    }

    public static string HexEscape(char character)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    public static char HexUnescape(string pattern, ref int index)
    {
      Contract.Requires(pattern != null);
      Contract.Ensures(1 <= index);
      Contract.Ensures(1 <= pattern.Length);

      return default(char);
    }

    protected virtual new bool IsBadFileSystemCharacter(char character)
    {
      return default(bool);
    }

    public bool IsBaseOf(System.Uri uri)
    {
      return default(bool);
    }

    protected static bool IsExcludedCharacter(char character)
    {
      return default(bool);
    }

    public static bool IsHexDigit(char character)
    {
      return default(bool);
    }

    public static bool IsHexEncoding(string pattern, int index)
    {
      Contract.Requires(pattern != null);

      return default(bool);
    }

    protected virtual new bool IsReservedCharacter(char character)
    {
      return default(bool);
    }

    public bool IsWellFormedOriginalString()
    {
      return default(bool);
    }

    public static bool IsWellFormedUriString(string uriString, UriKind uriKind)
    {
      return default(bool);
    }

    public string MakeRelative(System.Uri toUri)
    {
      return default(string);
    }

    public System.Uri MakeRelativeUri(System.Uri uri)
    {
      Contract.Ensures(Contract.Result<System.Uri>() != null);

      return default(System.Uri);
    }

    protected virtual new void Parse()
    {
    }

    void System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
    {
    }

    public override string ToString()
    {
      return default(string);
    }

    public static bool TryCreate(string uriString, UriKind uriKind, out System.Uri result)
    {
      result = default(System.Uri);

      return default(bool);
    }

    public static bool TryCreate(System.Uri baseUri, System.Uri relativeUri, out System.Uri result)
    {
      result = default(System.Uri);

      return default(bool);
    }

    public static bool TryCreate(System.Uri baseUri, string relativeUri, out System.Uri result)
    {
      result = default(System.Uri);

      return default(bool);
    }

    protected virtual new string Unescape(string path)
    {
      Contract.Requires(0 <= path.Length);
      Contract.Requires(path != null);

      return default(string);
    }

    public static string UnescapeDataString(string stringToUnescape)
    {
      Contract.Ensures(0 <= stringToUnescape.Length);
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    public Uri(string uriString, UriKind uriKind)
    {
    }

    public Uri(string uriString, bool dontEscape)
    {
    }

    public Uri(string uriString)
    {
    }

    public Uri(System.Uri baseUri, System.Uri relativeUri)
    {
      Contract.Requires(relativeUri != null);
    }

    protected Uri(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
    {
      Contract.Requires(serializationInfo != null);
    }

    public Uri(System.Uri baseUri, string relativeUri)
    {
    }

    public Uri(System.Uri baseUri, string relativeUri, bool dontEscape)
    {
    }
    #endregion

    #region Properties and indexers
    public string AbsolutePath
    {
      get
      {
        return default(string);
      }
    }

    public string AbsoluteUri
    {
      get
      {
        return default(string);
      }
    }

    public string Authority
    {
      get
      {
        return default(string);
      }
    }

    public string DnsSafeHost
    {
      get
      {
        return default(string);
      }
    }

    public string Fragment
    {
      get
      {
        return default(string);
      }
    }

    public string Host
    {
      get
      {
        return default(string);
      }
    }

    public UriHostNameType HostNameType
    {
      get
      {
        Contract.Ensures(((System.UriHostNameType)(0)) <= Contract.Result<System.UriHostNameType>());
        Contract.Ensures(Contract.Result<System.UriHostNameType>() <= ((System.UriHostNameType)(4)));

        return default(UriHostNameType);
      }
    }

    public bool IsAbsoluteUri
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsDefaultPort
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsFile
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsLoopback
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsUnc
    {
      get
      {
        return default(bool);
      }
    }

    public string LocalPath
    {
      get
      {
        return default(string);
      }
    }

    public string OriginalString
    {
      get
      {
        return default(string);
      }
    }

    public string PathAndQuery
    {
      get
      {
        return default(string);
      }
    }

    public int Port
    {
      get
      {
        return default(int);
      }
    }

    public string Query
    {
      get
      {
        return default(string);
      }
    }

    public string Scheme
    {
      get
      {
        return default(string);
      }
    }

    public string[] Segments
    {
      get
      {
        return default(string[]);
      }
    }

    public bool UserEscaped
    {
      get
      {
        return default(bool);
      }
    }

    public string UserInfo
    {
      get
      {
        return default(string);
      }
    }
    #endregion

    #region Fields
    public readonly static string SchemeDelimiter;
    public readonly static string UriSchemeFile;
    public readonly static string UriSchemeFtp;
    public readonly static string UriSchemeGopher;
    public readonly static string UriSchemeHttp;
    public readonly static string UriSchemeHttps;
    public readonly static string UriSchemeMailto;
    public readonly static string UriSchemeNetPipe;
    public readonly static string UriSchemeNetTcp;
    public readonly static string UriSchemeNews;
    public readonly static string UriSchemeNntp;
    #endregion
  }
}
