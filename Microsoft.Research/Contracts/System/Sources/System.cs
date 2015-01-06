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

// File System.cs
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
  public enum GenericUriParserOptions
  {
    Default = 0, 
    GenericAuthority = 1, 
    AllowEmptyAuthority = 2, 
    NoUserInfo = 4, 
    NoPort = 8, 
    NoQuery = 16, 
    NoFragment = 32, 
    DontConvertPathBackslashes = 64, 
    DontCompressPath = 128, 
    DontUnescapePathDotsAndSlashes = 256, 
    Idn = 512, 
    IriParsing = 1024, 
  }

  public enum UriComponents
  {
    Scheme = 1, 
    UserInfo = 2, 
    Host = 4, 
    Port = 8, 
    Path = 16, 
    Query = 32, 
    Fragment = 64, 
    StrongPort = 128, 
    KeepDelimiter = 1073741824, 
    SerializationInfoString = -2147483648, 
    AbsoluteUri = 127, 
    HostAndPort = 132, 
    StrongAuthority = 134, 
    SchemeAndServer = 13, 
    HttpRequestUrl = 61, 
    PathAndQuery = 48, 
  }

  public enum UriFormat
  {
    UriEscaped = 1, 
    Unescaped = 2, 
    SafeUnescaped = 3, 
  }

  public enum UriHostNameType
  {
    Unknown = 0, 
    Basic = 1, 
    Dns = 2, 
    IPv4 = 3, 
    IPv6 = 4, 
  }

  public enum UriIdnScope
  {
    None = 0, 
    AllExceptIntranet = 1, 
    All = 2, 
  }

  public enum UriKind
  {
    RelativeOrAbsolute = 0, 
    Absolute = 1, 
    Relative = 2, 
  }

  public enum UriPartial
  {
    Scheme = 0, 
    Authority = 1, 
    Path = 2, 
    Query = 3, 
  }
}
